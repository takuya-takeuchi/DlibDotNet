Param()

# import class and function
$ScriptPath = $PSScriptRoot
$DlibDotNetRoot = Split-Path $ScriptPath -Parent
$NugetPath = Join-Path $DlibDotNetRoot "nuget" | `
             Join-Path -ChildPath "BuildUtils.ps1"
import-module $NugetPath -function *

$OperatingSystem="linux"
$Distribution="ubuntu"
$DistributionVersion="16"

# Store current directory
$Current = Get-Location
$DlibDotNetRoot = (Split-Path (Get-Location) -Parent)
$DlibDotNetSourceRoot = Join-Path $DlibDotNetRoot src
$DockerDir = Join-Path $Current docker

Set-Location -Path $DockerDir

$DockerFileDir = Join-Path $DockerDir build  | `
                 Join-Path -ChildPath $Distribution | `
                 Join-Path -ChildPath $DistributionVersion

$BuildSourceHash = [Config]::GetBinaryLibraryHash()

$BuildTargets = @()
$BuildTargets += New-Object PSObject -Property @{Target = "cpu";  Architecture = 64; RID = "$OperatingSystem-x64";   CUDA = 0   }
$BuildTargets += New-Object PSObject -Property @{Target = "mkl";  Architecture = 64; RID = "$OperatingSystem-x64";   CUDA = 0   }
$BuildTargets += New-Object PSObject -Property @{Target = "cuda"; Architecture = 64; RID = "$OperatingSystem-x64";   CUDA = 92  }
$BuildTargets += New-Object PSObject -Property @{Target = "cuda"; Architecture = 64; RID = "$OperatingSystem-x64";   CUDA = 100 }
$BuildTargets += New-Object PSObject -Property @{Target = "cuda"; Architecture = 64; RID = "$OperatingSystem-x64";   CUDA = 101 }
$BuildTargets += New-Object PSObject -Property @{Target = "arm";  Architecture = 64; RID = "$OperatingSystem-arm64"; CUDA = 0   }
$BuildTargets += New-Object PSObject -Property @{Target = "arm";  Architecture = 32; RID = "$OperatingSystem-arm";   CUDA = 0   }

foreach($BuildTarget in $BuildTargets)
{
   $target = $BuildTarget.Target
   $architecture = $BuildTarget.Architecture
   $rid = $BuildTarget.RID
   $cudaVersion = $BuildTarget.CUDA

   $options = New-Object 'System.Collections.Generic.List[string]'
   if ($target -ne "cuda")
   {
      $option = ""

      if (($target -eq "arm") -And ($architecture -eq 64))
      {
         $dockername = "dlibdotnet/build/$Distribution/$DistributionVersion/$Target" + "64"
         $imagename  = "dlibdotnet/devel/$Distribution/$DistributionVersion/$Target" + "64"
      }
      else
      {
         $dockername = "dlibdotnet/build/$Distribution/$DistributionVersion/$Target"
         $imagename  = "dlibdotnet/devel/$Distribution/$DistributionVersion/$Target"
      }
   }
   else
   {
      $option = $cudaVersion

      $cudaVersion = ($cudaVersion / 10).ToString("0.0")
      $dockername = "dlibdotnet/build/$Distribution/$DistributionVersion/$Target/$cudaVersion"
      $imagename  = "dlibdotnet/devel/$Distribution/$DistributionVersion/$Target/$cudaVersion"
   }

   $Config = [Config]::new("Release", $target, $architecture, $option)
   $libraryDir = Join-Path "artifacts" $Config.GetArtifactDirectoryName()
   $build = $Config.GetBuildDirectoryName()

   Write-Host "Start 'docker build -t $dockername $DockerFileDir --build-arg IMAGE_NAME=""$imagename""'" -ForegroundColor Green
   docker build --force-rm=true -t $dockername $DockerFileDir --build-arg IMAGE_NAME="$imagename"

   if ($lastexitcode -ne 0)
   {
      Set-Location -Path $Current
      exit -1
   }

   Write-Host "Start 'docker run --rm -v ""$($DlibDotNetRoot):/opt/data/DlibDotNet"" -t $dockername'" -ForegroundColor Green
   docker run --rm `
               -v "$($DlibDotNetRoot):/opt/data/DlibDotNet" `
               -t "$dockername" $target $architecture ($options -join " ")

   if ($lastexitcode -ne 0)
   {
      Set-Location -Path $Current
      exit -1
   }

   # Copy output binary
   foreach ($key in $BuildSourceHash.keys)
   {
      $srcDir = Join-Path $DlibDotNetSourceRoot $key
      $dll = $BuildSourceHash[$key]
      $dstDir = Join-Path $Current $libraryDir

      CopyToArtifact -configuration "Release" -srcDir $srcDir -build $build -libraryName $dll -dstDir $dstDir -rid $rid
   }
}

# Move to Root directory 
Set-Location -Path $Current