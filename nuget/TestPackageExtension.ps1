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
      $Extension,

      [Parameter(
      Mandatory=$True,
      Position = 4
      )][string]
      $ExtensionVersion,

      [Parameter(
      Mandatory=$True,
      Position = 5
      )][string]
      $PlatformTarget,

      [Parameter(
      Mandatory=$True,
      Position = 6
      )][string]
      $RuntimeIdentifier
)

Set-StrictMode -Version Latest

function Clear-PackakgeCache([string]$PackageName, [string]$PackageVersion)
{
   # Linux is executed on container
   if ($global:IsWindows -or $global:IsMacOS)
   {
      $path = (dotnet nuget locals global-packages --list).Replace('info : global-packages: ', '').Trim()
      if ($path)
      {
         $path = (dotnet nuget locals global-packages --list).Replace('global-packages: ', '').Trim()
      }
      $path =  Join-Path $path $PackageName | `
               Join-Path -ChildPath $PackageVersion
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
                  Join-Path -ChildPath $Extension | `
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
                        Join-Path -ChildPath "${Extension}.Tests"

      $TargetDir = Join-Path $WorkDir "${Extension}.Tests"
      if (Test-Path "$TargetDir") {
         Remove-Item -Path "$TargetDir" -Recurse -Force > $null
      }

      Copy-Item "$NativeTestDir" "$WorkDir" -Recurse

      $DlibDotNetTestDir = Join-Path $DlibDotNetRoot test | `
                           Join-Path -ChildPath "DlibDotNet.Tests"

      $targetBaseDir = Join-Path $WorkDir "DlibDotNet.Tests"
      if (Test-Path "$targetBaseDir") {
         Remove-Item -Path "$targetBaseDir" -Recurse -Force > $null
      }

      New-Item -Type Directory "${targetBaseDir}" -Force  > $null
      $testBaseFiles = @(
         "TestBase.cs",
         "TestDataBase.cs"
      )
      foreach($testBaseFile in $testBaseFiles)
      {
         $src = Join-Path "${DlibDotNetTestDir}" ${testBaseFile}
         $dst = Join-Path "${targetBaseDir}" ${testBaseFile}
         Copy-Item "${src}" "${dst}" -Force
      }

      Set-Location -Path "$TargetDir"

      Clear-PackakgeCache -PackageName $Package -PackageVersion $Version
      Clear-PackakgeCache -PackageName $Extension -PackageVersion $ExtensionVersion

      # restore package from local nuget pacakge
      # And drop stdout message
      Write-Host "dotnet add package ${package} -v ${VERSION} --source ${NugetDir}" -Foreground Blue
      dotnet add package $package -v $VERSION --source "$NugetDir" > $null
      Write-Host "dotnet add package ${Extension} -v ${ExtensionVersion} --source ${NugetDir}" -Foreground Blue
      dotnet add package $Extension -v $ExtensionVersion --source "$NugetDir" > $null

      # Copy Dependencies
      $OutDir = Join-Path $TargetDir bin | `
                  Join-Path -ChildPath Release | `
                  Join-Path -ChildPath netcoreapp2.0
      if (!(Test-Path "$OutDir")) {
         New-Item "$OutDir" -ItemType Directory > $null
      }

      $ErrorActionPreference = "silentlycontinue"
      $env:PlatformTarget = $PlatformTarget
      $dotnetPath = ""
      $runsetting = ""
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

      Write-Host "${dotnetPath} test -c Release -r "$TestDir" --logger trx" -Foreground Yellow
      # & ${dotnetPath} test -c Release -r "$TestDir" --logger trx
      & ${dotnetPath} test -c Release --logger trx
      if ($lastexitcode -eq 0) {
         Write-Host "Test Successful" -ForegroundColor Green
      } else {
         Write-Host "Test Fail for $package" -ForegroundColor Red
         Set-Location -Path $Current
         exit -1
      }

      $ErrorActionPreference = "continue"

      # move to current
      Set-Location -Path "${Current}"

      # to make sure, delete
      if (Test-Path "${WorkDir}") {
         Remove-Item -Path "$WorkDir" -Recurse -Force
      }
   }
}

$BuildTargets = @()
$BuildTargets += New-Object PSObject -Property @{PlatformTarget = "x64"; Architecture = 64; Package = "DlibDotNet";  }

# Store current directory
$Current = Get-Location
$DlibDotNetRoot = (Split-Path (Get-Location) -Parent)

$targets = $BuildTargets.Where({$PSItem.Package -eq $Package}).Where({$PSItem.PlatformTarget -eq $PlatformTarget})
RunTest $targets

# Move to Root directory
Set-Location -Path $Current