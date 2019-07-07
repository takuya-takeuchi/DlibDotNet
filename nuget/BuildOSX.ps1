Param()

$OperatingSystem="osx"

# Store current directory
$Current = Get-Location
$DlibDotNetRoot = (Split-Path (Get-Location) -Parent)
$DlibDotNetSourceRoot = Join-Path $DlibDotNetRoot src

$ArchitectureHash = @{32 = "x86"; 64 = "x64"}
$BuildSourceArray = @("DlibDotNet.Native", "DlibDotNet.Native.Dnn")
$BuildSourceHash = @{"DlibDotNet.Native" = "libDlibDotNetNative.dylib"; "DlibDotNet.Native.Dnn" = "libDlibDotNetNativeDnn.dylib"}

$BuildTargets = @()
$BuildTargets += New-Object PSObject -Property @{Target = "cpu";  Architecture = 64; RID = "$OperatingSystem-x64";   CUDA = 0   }
$BuildTargets += New-Object PSObject -Property @{Target = "mkl";  Architecture = 64; RID = "$OperatingSystem-x64";   CUDA = 0   }

foreach($BuildTarget in $BuildTargets)
{
    $target = $BuildTarget.Target
    $architecture = $BuildTarget.Architecture
    $libraryDir = Join-Path "artifacts" $target
    $build = "build_" + $OperatingSystem + "_" + $target + "_" + $ArchitectureHash[$architecture]
  
    foreach($Source in $BuildSourceArray)
    {
      $srcDir = Join-Path $DlibDotNetSourceRoot $Source
  
      # Move to build target directory
      Set-Location -Path $srcDir
  
      $arc = $ArchitectureHash[$architecture]
      Write-Host "Build $Source [$arc] for $target" -ForegroundColor Green
      $command = "./BuildUnix.sh Release $target $architecture"
      Invoke-Expression $command

      if ($lastexitcode -ne 0)
      {
         Set-Location -Path $Current
         exit -1
      }
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
                Join-Path -ChildPath ($OperatingSystem + "-" + $ArchitectureHash[$architecture]) | `
                Join-Path -ChildPath native | `
                Join-Path -ChildPath $dll
  
      Write-Host "Copy $dll to $output" -ForegroundColor Green
      Copy-Item $binary $output
    }
}

# Move to Root directory 
Set-Location -Path $Current