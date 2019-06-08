Param()

$Distribution="ubuntu"
$DistributionVersion="16"

# Store current directory
$Current = Get-Location
$DlibDotNetRoot = (Split-Path (Get-Location) -Parent)
$DlibDotNetSourceRoot = Join-Path $DlibDotNetRoot src
$DockerDir = Join-Path $Current docker

Set-Location -Path $DockerDir

$ArchitectureHash = @{32 = "x86"; 64 = "x64"}
$BuildSourceArray = @("DlibDotNet.Native", "DlibDotNet.Native.Dnn")
$BuildSourceHash = @{"DlibDotNet.Native" = "libDlibDotNetNative.dll"; "DlibDotNet.Native.Dnn" = "DlibDotNetNativeDnn.dll"}
$IntelMKLDir = ""

$BuildTargets = @()
$BuildTargets += New-Object PSObject -Property @{Target = "cpu";  Architecture = 64; CUDA = 0   }
$BuildTargets += New-Object PSObject -Property @{Target = "mkl";  Architecture = 64; CUDA = 0   }
$BuildTargets += New-Object PSObject -Property @{Target = "cuda"; Architecture = 64; CUDA = 92  }
$BuildTargets += New-Object PSObject -Property @{Target = "cuda"; Architecture = 64; CUDA = 100 }

foreach($BuildTarget in $BuildTargets)
{
  $target = $BuildTarget.Target
  $architecture = $BuildTarget.Architecture
  $cudaVersion = $BuildTarget.CUDA
  $options = New-Object 'System.Collections.Generic.List[string]'
  if ($target -eq "cuda")
  {
     $build = "build_linux_" + $target + "_" + $ArchitectureHash[$architecture]
     $dockername = "dlibdotnet/build/$Distribution/$DistributionVersion/$Target"
     $imagename  = "dlibdotnet/devel/$Distribution/$DistributionVersion/$Target"
     $libraryDir = $target
  }
  elseif ($target -eq "mkl")
  {
     $build = "build_linux_" + $target + "_" + $ArchitectureHash[$architecture]
     $options.Add($IntelMKLDir)
     $dockername = "dlibdotnet/build/$Distribution/$DistributionVersion/$Target"
     $imagename  = "dlibdotnet/devel/$Distribution/$DistributionVersion/$Target"
     $libraryDir = $target
  }
  else
  {
     $build = "build_linux_" + $target + "-" + $cudaVersion + "_" + $ArchitectureHash[$architecture]
     $options.Add($cudaVersion.ToString())
     $cudaVersion = ($cudaVersion / 10).ToString("0.0")
     $dockername = "dlibdotnet/build/$Distribution/$DistributionVersion/$Target/$cudaVersion"
     $imagename  = "dlibdotnet/devel/$Distribution/$DistributionVersion/$Target/$cudaVersion"
     $libraryDir = $target + "-" + $cudaVersion
  }

  foreach($Source in $BuildSourceArray)
  {
    $srcDir = Join-Path $DlibDotNetSourceRoot $Source

    # Move to build target directory
    Set-Location -Path $srcDir

    $arc = $ArchitectureHash[$architecture]
    Write-Host "Build $Source [$arc] for $target" -ForegroundColor Green
    powershell .\BuildWindowsVS2017.ps1 Release $target $architecture ($options -join " ")
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
              Join-Path -ChildPath ("linux-" + $ArchitectureHash[$architecture]) | `
              Join-Path -ChildPath native | `
              Join-Path -ChildPath $dll

    Write-Host "Copy $dll to $output" -ForegroundColor Green
    Copy-Item $binary $output
  }
}

# Move to Root directory 
Set-Location -Path $Current