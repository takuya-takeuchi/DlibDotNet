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

$OperatingSystem="windows"
$Distribution="windows"
$DistributionVersion="16"

# Store current directory
$Current = Get-Location
$DlibDotNetRoot = (Split-Path (Get-Location) -Parent)

$ArchitectureHash = @{32 = "x86"; 64 = "x64"}

$BuildTargets = @()
$BuildTargets += New-Object PSObject -Property @{Target = "cpu";  Architecture = 64; CUDA = 0;   Package = "DlibDotNet"         }
$BuildTargets += New-Object PSObject -Property @{Target = "cuda"; Architecture = 64; CUDA = 92;  Package = "DlibDotNet.CUDA92"  }
$BuildTargets += New-Object PSObject -Property @{Target = "cuda"; Architecture = 64; CUDA = 100; Package = "DlibDotNet.CUDA100" }
$BuildTargets += New-Object PSObject -Property @{Target = "mkl";  Architecture = 64; CUDA = 0;   Package = "DlibDotNet.MKL"     }

foreach($BuildTarget in $BuildTargets)
{
  $target = $BuildTarget.Target
  $architecture = $BuildTarget.Architecture
  $package = $BuildTarget.Package


  # Test
  $WorkDir = Join-Path $DlibDotNetRoot work
  $NugetDir = Join-Path $DlibDotNetRoot nuget
  $TestDir = Join-Path $NugetDir artifacts | `
             Join-Path -ChildPath test | `
             Join-Path -ChildPath $package | `
             Join-Path -ChildPath $Version | `
             Join-Path -ChildPath $OperatingSystem

  if (!(Test-Path "$WorkDir")) {
     New-Item "$WorkDir" -ItemType Directory > $null
  }
  if (!(Test-Path "$TestDir")) {
     New-Item "$TestDir" -ItemType Directory > $null
  }
  
  $env:DLIBDOTNET_VERSION = $VERSION
  
  $NativeTestDir = Join-Path $DlibDotNetRoot test | `
                   Join-Path -ChildPath DlibDotNet.Native.Tests

  $TargetDir = Join-Path $WorkDir DlibDotNet.Native.Tests
  if (Test-Path "$TargetDir") {
     Remove-Item -Path "$TargetDir" -Recurse -Force
  }

  Copy-Item "$NativeTestDir" "$WorkDir" -Recurse

  Set-Location -Path "$TargetDir"
  
  # restore package from local nuget pacakge
  # And drop stdout message
  dotnet add package $package -s "$NugetDir" > $null
  
  $ErrorActionPreference = "silentlycontinue"
  dotnet test -c Release -r "$TestDir" --logger trx

  if ($lastexitcode -eq 0) {
     Write-Host "Test Successful" -ForegroundColor Green
  } else {
     Write-Host "Test Fail for $package" -ForegroundColor Red
     exit -1
  }

  $ErrorActionPreference = "continue"

  # move to current
  Set-Location -Path "$Current"

  # to make sure, delete
  if (Test-Path "$WorkDir") {
     Remove-Item -Path "$WorkDir" -Recurse -Force
  }
}

# Move to Root directory 
Set-Location -Path $Current