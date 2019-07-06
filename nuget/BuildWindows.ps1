Param()

$OperatingSystem="win"

# Store current directory
$Current = Get-Location
$DlibDotNetRoot = (Split-Path (Get-Location) -Parent)
$DlibDotNetSourceRoot = Join-Path $DlibDotNetRoot src

$ArchitectureHash = @{32 = "x86"; 64 = "x64"}
$BuildSourceArray = @("DlibDotNet.Native", "DlibDotNet.Native.Dnn")
$BuildSourceHash = @{"DlibDotNet.Native" = "DlibDotNetNative.dll"; "DlibDotNet.Native.Dnn" = "DlibDotNetNativeDnn.dll"}

$IntelMKLDir = $env:MKL_WIN
if ([string]::IsNullOrEmpty($IntelMKLDir)){
  Write-Host "Environmental Value 'MKL_WIN' is not defined." -ForegroundColor Yellow
}

if (($IntelMKLDir -ne $null) -And !(Test-Path $IntelMKLDir)) {
  Write-Host "Environmental Value 'MKL_WIN' does not exist." -ForegroundColor Yellow
}

$BuildTargets = @()
$BuildTargets += New-Object PSObject -Property @{Target = "cpu";  Architecture = 64; CUDA = 0   }
$BuildTargets += New-Object PSObject -Property @{Target = "mkl";  Architecture = 64; CUDA = 0   }
#$BuildTargets += New-Object PSObject -Property @{Target = "cuda"; Architecture = 64; CUDA = 90  }
#$BuildTargets += New-Object PSObject -Property @{Target = "cuda"; Architecture = 64; CUDA = 91  }
$BuildTargets += New-Object PSObject -Property @{Target = "cuda"; Architecture = 64; CUDA = 92  }
$BuildTargets += New-Object PSObject -Property @{Target = "cuda"; Architecture = 64; CUDA = 100 }
$BuildTargets += New-Object PSObject -Property @{Target = "cuda"; Architecture = 64; CUDA = 101 }

foreach($BuildTarget in $BuildTargets)
{
   $target = $BuildTarget.Target
   $architecture = $BuildTarget.Architecture
   $cudaVersion = $BuildTarget.CUDA
   $options = New-Object 'System.Collections.Generic.List[string]'
   if ($target -eq "cpu")
   {
      $libraryDir = Join-Path "artifacts" $target
      $build = "build_" + $OperatingSystem + "_" + $target + "_" + $ArchitectureHash[$architecture]
   }
   elseif ($target -eq "mkl")
   {
      $libraryDir = Join-Path "artifacts" $target
      $build = "build_" + $OperatingSystem + "_" + $target + "_" + $ArchitectureHash[$architecture]
      $options.Add($IntelMKLDir)
   }
   else
   {
      $libraryDir = Join-Path "artifacts" ($target + "-" + $cudaVersion)
      $build = "build_" + $OperatingSystem + "_" + $target + "-" + $cudaVersion + "_" + $ArchitectureHash[$architecture]
      $options.Add($cudaVersion.ToString())
      $cudaVersion = ($cudaVersion / 10).ToString("0.0")
   }

   foreach($Source in $BuildSourceArray)
   {
      $srcDir = Join-Path $DlibDotNetSourceRoot $Source

      # Move to build target directory
      Set-Location -Path $srcDir

      $arc = $ArchitectureHash[$architecture]
      Write-Host "Build $Source [$arc] for $target" -ForegroundColor Green
      powershell .\BuildWindowsVS2017.ps1 Release $target $architecture ($options -join " ")

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
               Join-Path -ChildPath Release | `
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