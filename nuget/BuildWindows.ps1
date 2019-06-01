Param()

# Store current directory
$Current = Get-Location
$DlibDotNetRoot = (Split-Path (Get-Location) -Parent)
$DlibDotNetSourceRoot = Join-Path $DlibDotNetRoot src

$Configuration = "Release"
$TargetArray = @("cpu","cuda","mkl")
$CUDAVersionArray = @(92,100)
$IntelMKLDir = ""
$ArchitectureArray = @(64)
$ArchitectureHash = @{32 = "x86"; 64 = "x64"}
$BuildSourceArray = @("DlibDotNet.Native", "DlibDotNet.Native.Dnn")
$BuildSourceHash = @{"DlibDotNet.Native" = "DlibDotNetNative.dll"; "DlibDotNet.Native.Dnn" = "DlibDotNetNativeDnn.dll"}

foreach($source in $BuildSourceArray)
{
  foreach($target in $TargetArray)
  {
    foreach($architecture in $ArchitectureArray)
    {
      $arc = $ArchitectureHash[$architecture]
      $srcDir = Join-Path $DlibDotNetSourceRoot $source

      # Move to build target directory
      Set-Location -Path $srcDir

      if ($target -eq "cpu")
      {
        Write-Host "Build $source [$arc] for $target" -ForegroundColor Green
        powershell .\BuildWindowsVS2017.ps1 $Configuration $target $architecture

        # Copy output binary
        $dll = $BuildSourceHash[$source]
        $build = "build_win_" + $target + "_" + $ArchitectureHash[$architecture]
        $binary = Join-Path $srcDir $build  | `
                  Join-Path -ChildPath $Configuration | `
                  Join-Path -ChildPath $dll
        $output = Join-Path $Current $target  | `
                  Join-Path -ChildPath runtimes | `
                  Join-Path -ChildPath ("win-" + $ArchitectureHash[$architecture]) | `
                  Join-Path -ChildPath native | `
                  Join-Path -ChildPath $dll

        Write-Host "`tCopy $dll"
        Copy-Item $binary $output
      }
      elseif ($target -eq "mkl")
      {
        Write-Host "Build $source [$arc] for $target" -ForegroundColor Green
        powershell .\BuildWindowsVS2017.ps1 $Configuration $target $architecture $IntelMKLDir

        # Copy output binary
        $dll = $BuildSourceHash[$source]
        $build = "build_win_" + $target + "_" + $ArchitectureHash[$architecture]
        $binary = Join-Path $srcDir $build  | `
                  Join-Path -ChildPath $Configuration | `
                  Join-Path -ChildPath $dll
        $output = Join-Path $Current $target  | `
                  Join-Path -ChildPath runtimes | `
                  Join-Path -ChildPath ("win-" + $ArchitectureHash[$architecture]) | `
                  Join-Path -ChildPath native | `
                  Join-Path -ChildPath $dll

        Write-Host "`tCopy $dll"
        Copy-Item $binary $output
      }
      else
      {
        foreach($cuda in $CUDAVersionArray)
        {
          Write-Host "Build $source [$arc] for CUDA $cuda" -ForegroundColor Green
          powershell .\BuildWindowsVS2017.ps1 $Configuration $target $architecture $cuda

          # Copy output binary
          $dll = $BuildSourceHash[$source]
          $build = "build_win_" + $target + "-" + $cuda + "_" + $ArchitectureHash[$architecture]
          $binary = Join-Path $srcDir $build  | `
                    Join-Path -ChildPath $Configuration | `
                    Join-Path -ChildPath $dll
          $output = Join-Path $Current ($target + "-" + $cuda)  | `
                    Join-Path -ChildPath runtimes | `
                    Join-Path -ChildPath ("win-" + $ArchitectureHash[$architecture]) | `
                    Join-Path -ChildPath native | `
                    Join-Path -ChildPath $dll

          Write-Host "`tCopy $dll" -ForegroundColor Green
          Copy-Item $binary $output
        }
      }
    }
  }
}

# Move to Root directory 
Set-Location -Path $Current

# Check version
Write-Host ""
Write-Host "********************" -ForegroundColor Green
Write-Host "Check file version  " -ForegroundColor Green
Write-Host "********************" -ForegroundColor Green
foreach($source in $BuildSourceArray)
{
  foreach($target in $TargetArray)
  {
    foreach($architecture in $ArchitectureArray)
    {
      $arc = $ArchitectureHash[$architecture]

      if (($target -eq "cpu") -Or ($target -eq "mkl"))
      {
        # Create output binary
        $dll = $BuildSourceHash[$source]
        $output = Join-Path $Current $target  | `
                  Join-Path -ChildPath runtimes | `
                  Join-Path -ChildPath ("win-" + $ArchitectureHash[$architecture]) | `
                  Join-Path -ChildPath native | `
                  Join-Path -ChildPath $dll

        Write-Host "$dll" -ForegroundColor Green
        filever /v $output | findstr ProductVersion
      }
      else
      {
        foreach($cuda in $CUDAVersionArray)
        {
          # Create output binary
          $dll = $BuildSourceHash[$source]
          $output = Join-Path $Current ($target + "-" + $cuda)  | `
                    Join-Path -ChildPath runtimes | `
                    Join-Path -ChildPath ("win-" + $ArchitectureHash[$architecture]) | `
                    Join-Path -ChildPath native | `
                    Join-Path -ChildPath $dll

          Write-Host "$dll" -ForegroundColor Green
          filever /v $output | findstr ProductVersion
        }
      }
    }
  }
}