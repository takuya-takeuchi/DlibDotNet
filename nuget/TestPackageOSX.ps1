#***************************************
#Arguments
#%1: Version of Release (19.17.0.yyyyMMdd)
#***************************************
Param([Parameter(
      Mandatory=$False,
      Position = 1
      )][string]
      $Version
)

Set-StrictMode -Version Latest

$OperatingSystem="osx"

# Store current directory
$Current = Get-Location

$BuildTargets = @()
$BuildTargets += New-Object PSObject -Property @{Package = "DlibDotNet";     PlatformTarget="x64"; RID = "$OperatingSystem-x64"; }
# $BuildTargets += New-Object PSObject -Property @{Package = "DlibDotNet";     PlatformTarget="x86"; RID = "$OperatingSystem-x86"; }
$BuildTargets += New-Object PSObject -Property @{Package = "DlibDotNet.MKL"; PlatformTarget="x64"; RID = "$OperatingSystem-x64"; }
# $BuildTargets += New-Object PSObject -Property @{Package = "DlibDotNet.MKL"; PlatformTarget="x86"; RID = "$OperatingSystem-x86"; }

if ([string]::IsNullOrEmpty($Version))
{
   $packages = Get-ChildItem *.* -include *.nupkg | Sort-Object -Property Name -Descending
   foreach ($file in $packages)
   {
      $file = Split-Path $file -leaf
      $file = $file -replace "DlibDotNet(\.[a-zA-Z]+[0-9]*)*\.",""
      $file = $file -replace "\.nupkg",""
      $Version = $file
      break
   }
}

foreach($BuildTarget in $BuildTargets)
{
   $package = $BuildTarget.Package
   $platformTarget = $BuildTarget.PlatformTarget
   $runtimeIdentifier = $BuildTarget.RID
   $command = ".\\TestPackage.ps1 -Package ${package} -Version $Version -PlatformTarget ${platformTarget} -RuntimeIdentifier ${runtimeIdentifier}"
   Invoke-Expression $command

   if ($lastexitcode -ne 0)
   {
      Set-Location -Path $Current
      exit -1
   }
}