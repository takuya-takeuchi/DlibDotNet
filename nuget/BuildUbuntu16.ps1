Param()

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

$ArchitectureHash = @{32 = "x86"; 64 = "x64"}
$BuildSourceArray = @("DlibDotNet.Native", "DlibDotNet.Native.Dnn")
$BuildSourceHash = @{"DlibDotNet.Native" = "libDlibDotNetNative.so"; "DlibDotNet.Native.Dnn" = "libDlibDotNetNativeDnn.so"}

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
      $libraryDir = Join-Path "artifacts" $target
      $build = "build_" + $OperatingSystem + "_" + $target + "_" + $ArchitectureHash[$architecture]

      if (($target -eq "arm") -And ($architecture -eq 64)) {
         $dockername = "dlibdotnet/build/$Distribution/$DistributionVersion/$Target" + "64"
         $imagename  = "dlibdotnet/devel/$Distribution/$DistributionVersion/$Target" + "64"
      } else {
         $dockername = "dlibdotnet/build/$Distribution/$DistributionVersion/$Target"
         $imagename  = "dlibdotnet/devel/$Distribution/$DistributionVersion/$Target"
      }
   }
   else
   {
      $libraryDir = Join-Path "artifacts" ($target + "-" + $cudaVersion)
      $build = "build_" + $OperatingSystem + "_" + $target + "-" + $cudaVersion + "_" + $ArchitectureHash[$architecture]
      $options.Add($cudaVersion.ToString())
      $cudaVersion = ($cudaVersion / 10).ToString("0.0")
      $dockername = "dlibdotnet/build/$Distribution/$DistributionVersion/$Target/$cudaVersion"
      $imagename  = "dlibdotnet/devel/$Distribution/$DistributionVersion/$Target/$cudaVersion"
   }

   Write-Host "Start 'docker build -q -t $dockername $DockerFileDir --build-arg IMAGE_NAME=""$imagename""'" -ForegroundColor Green
   docker build -q -t $dockername $DockerFileDir --build-arg IMAGE_NAME="$imagename"

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
   foreach($Source in $BuildSourceArray)
   {
      $dll = $BuildSourceHash[$Source]
      $srcDir = Join-Path $DlibDotNetSourceRoot $Source

      $binary = Join-Path $srcDir $build  | `
               Join-Path -ChildPath $dll
      $output = Join-Path $Current $libraryDir  | `
               Join-Path -ChildPath runtimes | `
               Join-Path -ChildPath ($rid) | `
               Join-Path -ChildPath native | `
               Join-Path -ChildPath $dll

      Write-Host "Copy $dll to $output" -ForegroundColor Green
      Copy-Item $binary $output
   }
}

# Move to Root directory 
Set-Location -Path $Current