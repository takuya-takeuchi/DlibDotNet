#***************************************
#Arguments
#%1: Test Package (DlibDotNet.CUDA92)
#%2: Version of Release (19.17.0.yyyyMMdd)
#***************************************
Param([Parameter(
      Mandatory=$True,
      Position = 1
      )][string]
      $Package,

      [Parameter(
      Mandatory=$True,
      Position = 2
      )][string]
      $Version,

      [Parameter(
      Mandatory=$True,
      Position = 3
      )][string]
      $PlatformTarget,

      [Parameter(
      Mandatory=$True,
      Position = 4
      )][string]
      $RuntimeIdentifier
)

Set-StrictMode -Version Latest

function Clear-PackakgeCache([string]$Package, [string]$Version)
{
   # Linux is executed on container
   if ($global:IsWindows -or $global:IsMacOS)
   {
      $path = (dotnet nuget locals global-packages --list).Replace('info : global-packages: ', '').Trim()
      if ($path)
      {
         $path = (dotnet nuget locals global-packages --list).Replace('global-packages: ', '').Trim()
      }
      $path =  Join-Path $path $Package | `
               Join-Path -ChildPath $Version
      if (Test-Path $path)
      {
         Write-Host "Remove '$path'" -Foreground Green
         Remove-Item -Path "$path" -Recurse -Force
      }
   }
}

function RunTest($BuildTargets)
{
   foreach($BuildTarget in $BuildTargets)
   {
      $package = $BuildTarget.Package

      # Test
      $WorkDir = Join-Path $DlibDotNetRoot work
      $NugetDir = Join-Path $DlibDotNetRoot nuget
      $TestDir = Join-Path $NugetDir artifacts | `
                  Join-Path -ChildPath test | `
                  Join-Path -ChildPath $package | `
                  Join-Path -ChildPath $Version | `
                  Join-Path -ChildPath $RuntimeIdentifier

      if (!(Test-Path "$WorkDir")) {
         New-Item "$WorkDir" -ItemType Directory > $null
      }
      if (!(Test-Path "$TestDir")) {
         New-Item "$TestDir" -ItemType Directory > $null
      }

      $env:DLIBDOTNET_VERSION = $VERSION
      $env:DLIBDOTNET_GUI_SUPPORT = 1

      $NativeTestDir = Join-Path $DlibDotNetRoot test | `
                        Join-Path -ChildPath DlibDotNet.Native.Tests

      $TargetDir = Join-Path $WorkDir DlibDotNet.Native.Tests
      if (Test-Path "$TargetDir") {
         Remove-Item -Path "$TargetDir" -Recurse -Force > $null
      }

      Copy-Item "$NativeTestDir" "$WorkDir" -Recurse

      Set-Location -Path "$TargetDir"

      Clear-PackakgeCache -Package $Package -Version $Version

      # restore package from local nuget pacakge
      # And drop stdout message
      dotnet add package $package -v $VERSION --source "$NugetDir" > $null

      # Copy Dependencies
      $OutDir = Join-Path $TargetDir bin | `
                  Join-Path -ChildPath Release | `
                  Join-Path -ChildPath netcoreapp2.0 | `
                  Join-Path -ChildPath $RuntimeIdentifier
      if (!(Test-Path "$OutDir")) {
         New-Item "$OutDir" -ItemType Directory > $null
      }

      if ($IsWindows)
      {
         if ($null -ne $BuildTarget.Dependencies)
         {
            foreach($Dependency in $BuildTarget.Dependencies)
            {
               Copy-Item "$Dependency" "$OutDir"
            }
         }
      }

      $ErrorActionPreference = "silentlycontinue"
      $env:PlatformTarget = $PlatformTarget
      $dotnetPath = ""
      if ($global:IsWindows)
      {
         switch($PlatformTarget)
         {
            "x64"
            {
               $dotnetPath = Join-Path $env:ProgramFiles "dotnet\dotnet.exe"
            }
            "x86"
            {
               $dotnetPath = Join-Path ${env:ProgramFiles(x86)} "dotnet\dotnet.exe"
            }
         }
      }
      else
      {
         $dotnetPath = "dotnet"
      }

      & ${dotnetPath} test -c Release -r "$TestDir" -p:RuntimeIdentifier=$RuntimeIdentifier --logger trx
      if ($lastexitcode -eq 0) {
         Write-Host "Test Successful" -ForegroundColor Green
      } else {
         Write-Host "Test Fail for $package" -ForegroundColor Red
         Set-Location -Path $Current
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
}

# For windows
# For DlibDotNet.CUDA90
$tmp90 = New-Object 'System.Collections.Generic.List[string]'
$tmp90.Add("$env:CUDA_PATH_V9_0\bin\cublas64_90.dll")
$tmp90.Add("$env:CUDA_PATH_V9_0\bin\cudnn64_7.dll")
$tmp90.Add("$env:CUDA_PATH_V9_0\bin\curand64_90.dll")
$tmp90.Add("$env:CUDA_PATH_V9_0\bin\cusolver64_90.dll")

# For DlibDotNet.CUDA91
$tmp91 = New-Object 'System.Collections.Generic.List[string]'
$tmp91.Add("$env:CUDA_PATH_V9_1\bin\cublas64_91.dll")
$tmp91.Add("$env:CUDA_PATH_V9_1\bin\cudnn64_7.dll")
$tmp91.Add("$env:CUDA_PATH_V9_1\bin\curand64_91.dll")
$tmp91.Add("$env:CUDA_PATH_V9_1\bin\cusolver64_91.dll")

# For DlibDotNet.CUDA92
$tmp92 = New-Object 'System.Collections.Generic.List[string]'
$tmp92.Add("$env:CUDA_PATH_V9_2\bin\cublas64_92.dll")
$tmp92.Add("$env:CUDA_PATH_V9_2\bin\cudnn64_7.dll")
$tmp92.Add("$env:CUDA_PATH_V9_2\bin\curand64_92.dll")
$tmp92.Add("$env:CUDA_PATH_V9_2\bin\cusolver64_92.dll")

# For DlibDotNet.CUDA100
$tmp100 = New-Object 'System.Collections.Generic.List[string]'
$tmp100.Add("$env:CUDA_PATH_V10_0\bin\cublas64_100.dll")
$tmp100.Add("$env:CUDA_PATH_V10_0\bin\cudnn64_7.dll")
$tmp100.Add("$env:CUDA_PATH_V10_0\bin\curand64_100.dll")
$tmp100.Add("$env:CUDA_PATH_V10_0\bin\cusolver64_100.dll")

# For DlibDotNet.CUDA101
$tmp101 = New-Object 'System.Collections.Generic.List[string]'
$tmp101.Add("$env:CUDA_PATH_V10_1\bin\cublas64_10.dll")
$tmp101.Add("$env:CUDA_PATH_V10_1\bin\cudnn64_7.dll")
$tmp101.Add("$env:CUDA_PATH_V10_1\bin\curand64_10.dll")
$tmp101.Add("$env:CUDA_PATH_V10_1\bin\cusolver64_10.dll")

# For mkl
$tmpmkl = New-Object 'System.Collections.Generic.List[string]'
$tmpmkl.Add("$env:MKL_WIN\redist\intel64_win\mkl\mkl_core.dll")
$tmpmkl.Add("$env:MKL_WIN\redist\intel64_win\mkl\mkl_intel_thread.dll")
$tmpmkl.Add("$env:MKL_WIN\redist\intel64_win\mkl\mkl_avx2.dll")
$tmpmkl.Add("$env:MKL_WIN\redist\intel64_win\compiler\libiomp5md.dll")

$tmpmkl86 = New-Object 'System.Collections.Generic.List[string]'
$tmpmkl86.Add("$env:MKL_WIN\redist\ia32_win\mkl\mkl_core.dll")
$tmpmkl86.Add("$env:MKL_WIN\redist\ia32_win\mkl\mkl_intel_thread.dll")
$tmpmkl86.Add("$env:MKL_WIN\redist\ia32_win\mkl\mkl_avx2.dll")
$tmpmkl86.Add("$env:MKL_WIN\redist\ia32_win\compiler\libiomp5md.dll")

$BuildTargets = @()
$BuildTargets += New-Object PSObject -Property @{PlatformTarget = "x64"; Architecture = 64; Package = "DlibDotNet";         Dependencies = $null     }
$BuildTargets += New-Object PSObject -Property @{PlatformTarget = "x86"; Architecture = 32; Package = "DlibDotNet";         Dependencies = $null     }
$BuildTargets += New-Object PSObject -Property @{PlatformTarget = "x64"; Architecture = 64; Package = "DlibDotNet.CUDA90";  Dependencies = $tmp90    }
$BuildTargets += New-Object PSObject -Property @{PlatformTarget = "x64"; Architecture = 64; Package = "DlibDotNet.CUDA91";  Dependencies = $tmp91    }
$BuildTargets += New-Object PSObject -Property @{PlatformTarget = "x64"; Architecture = 64; Package = "DlibDotNet.CUDA92";  Dependencies = $tmp92    }
$BuildTargets += New-Object PSObject -Property @{PlatformTarget = "x64"; Architecture = 64; Package = "DlibDotNet.CUDA100"; Dependencies = $tmp100   }
$BuildTargets += New-Object PSObject -Property @{PlatformTarget = "x64"; Architecture = 64; Package = "DlibDotNet.CUDA101"; Dependencies = $tmp101   }
$BuildTargets += New-Object PSObject -Property @{PlatformTarget = "x64"; Architecture = 64; Package = "DlibDotNet.MKL";     Dependencies = $tmpmkl   }
$BuildTargets += New-Object PSObject -Property @{PlatformTarget = "x86"; Architecture = 32; Package = "DlibDotNet.MKL";     Dependencies = $tmpmkl86 }
$BuildTargets += New-Object PSObject -Property @{PlatformTarget = "arm"; Architecture = 32; Package = "DlibDotNet.ARM";     Dependencies = $null     }

# Store current directory
$Current = Get-Location
$DlibDotNetRoot = (Split-Path (Get-Location) -Parent)

$targets = $BuildTargets.Where({$PSItem.Package -eq $Package}).Where({$PSItem.PlatformTarget -eq $PlatformTarget})
RunTest $targets

# Move to Root directory
Set-Location -Path $Current