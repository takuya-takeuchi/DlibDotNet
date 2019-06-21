#***************************************
#Arguments
#%1: Version of Release (19.17.0.yyyyMMdd)
#***************************************
Param([Parameter(
      Mandatory=$True,
      Position = 1
      )][string]
      $Version
)

$OperatingSystem="linux"
$Distribution="ubuntu"
$DistributionVersion="16"

# Store current directory
$Current = Get-Location
$DlibDotNetRoot = (Split-Path (Get-Location) -Parent)
$DlibDotNetSourceRoot = Join-Path $DlibDotNetRoot src
$DockerDir = Join-Path $Current docker

Set-Location -Path $DockerDir

$DockerFileDir = Join-Path $DockerDir test  | `
                 Join-Path -ChildPath $Distribution | `
                 Join-Path -ChildPath $DistributionVersion

$ArchitectureHash = @{32 = "x86"; 64 = "x64"}

$BuildTargets = @()
$BuildTargets += New-Object PSObject -Property @{Target = "cpu";  Architecture = 64; CUDA = 0;   Package = "DlibDotNet"         }
$BuildTargets += New-Object PSObject -Property @{Target = "mkl";  Architecture = 64; CUDA = 0;   Package = "DlibDotNet.MKL"     }
$BuildTargets += New-Object PSObject -Property @{Target = "cuda"; Architecture = 64; CUDA = 90;  Package = "DlibDotNet.CUDA90"  }
$BuildTargets += New-Object PSObject -Property @{Target = "cuda"; Architecture = 64; CUDA = 91;  Package = "DlibDotNet.CUDA91"  }
$BuildTargets += New-Object PSObject -Property @{Target = "cuda"; Architecture = 64; CUDA = 92;  Package = "DlibDotNet.CUDA92"  }
$BuildTargets += New-Object PSObject -Property @{Target = "cuda"; Architecture = 64; CUDA = 100; Package = "DlibDotNet.CUDA100" }
$BuildTargets += New-Object PSObject -Property @{Target = "cuda"; Architecture = 64; CUDA = 101; Package = "DlibDotNet.CUDA101" }

foreach($BuildTarget in $BuildTargets)
{
  $target = $BuildTarget.Target
  $architecture = $BuildTarget.Architecture
  $cudaVersion = $BuildTarget.CUDA
  $package = $BuildTarget.Package
  $options = New-Object 'System.Collections.Generic.List[string]'
  if ($target -ne "cuda")
  {
     $dockername = "dlibdotnet/test/$Distribution/$DistributionVersion/$Target"
     $imagename  = "dlibdotnet/runtime/$Distribution/$DistributionVersion/$Target"
  }
  else
  {
     $dockername = "dlibdotnet/test/$Distribution/$DistributionVersion/$Target/$cudaVersion"
     $cudaVersion = ($cudaVersion / 10).ToString("0.0")
     $imagename  = "dlibdotnet/runtime/$Distribution/$DistributionVersion/$Target/$cudaVersion"
  }

  docker build -q -t $dockername $DockerFileDir --build-arg IMAGE_NAME="$imagename"

  Write-Host "Start '$dockername'" -ForegroundColor Green
  docker run --rm `
             -v "$($DlibDotNetRoot):/opt/data/DlibDotNet" `
             -t "$dockername" $Version $package $Distribution $DistributionVersion
}

# Move to Root directory 
Set-Location -Path $Current