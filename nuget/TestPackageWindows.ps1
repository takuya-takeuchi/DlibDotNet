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

$OperatingSystem="win"

# Store current directory
$Current = Get-Location

$BuildTargets = @()
$BuildTargets += New-Object PSObject -Property @{Package = "DlibDotNet";         PlatformTarget="x64"; RID = "$OperatingSystem-x64"; }
$BuildTargets += New-Object PSObject -Property @{Package = "DlibDotNet";         PlatformTarget="x86"; RID = "$OperatingSystem-x86"; }
$BuildTargets += New-Object PSObject -Property @{Package = "DlibDotNet.MKL";     PlatformTarget="x64"; RID = "$OperatingSystem-x64"; }
$BuildTargets += New-Object PSObject -Property @{Package = "DlibDotNet.MKL";     PlatformTarget="x86"; RID = "$OperatingSystem-x86"; }
$BuildTargets += New-Object PSObject -Property @{Package = "DlibDotNet.CUDA92";  PlatformTarget="x64"; RID = "$OperatingSystem-x64"; }
$BuildTargets += New-Object PSObject -Property @{Package = "DlibDotNet.CUDA100"; PlatformTarget="x64"; RID = "$OperatingSystem-x64"; }
$BuildTargets += New-Object PSObject -Property @{Package = "DlibDotNet.CUDA101"; PlatformTarget="x64"; RID = "$OperatingSystem-x64"; }
$BuildTargets += New-Object PSObject -Property @{Package = "DlibDotNet.CUDA102"; PlatformTarget="x64"; RID = "$OperatingSystem-x64"; }
$BuildTargets += New-Object PSObject -Property @{Package = "DlibDotNet.CUDA110"; PlatformTarget="x64"; RID = "$OperatingSystem-x64"; }
$BuildTargets += New-Object PSObject -Property @{Package = "DlibDotNet.CUDA111"; PlatformTarget="x64"; RID = "$OperatingSystem-x64"; }

foreach($BuildTarget in $BuildTargets)
{
   $package = $BuildTarget.Package
   $platformTarget = $BuildTarget.PlatformTarget
   $runtimeIdentifier = $BuildTarget.RID
   $versionStr = $Version

   if ([string]::IsNullOrEmpty($versionStr))
   {
      $packages = Get-ChildItem "${Current}/*" -include *.nupkg | `
                  Where-Object -FilterScript {$_.Name -match "${package}\.([0-9\.]+).nupkg"} | `
                  Sort-Object -Property Name -Descending
      foreach ($file in $packages)
      {
         Write-Host $file -ForegroundColor Blue
      }

      foreach ($file in $packages)
      {
         $file = Split-Path $file -leaf
         $file = $file -replace "${package}\.",""
         $file = $file -replace "\.nupkg",""
         $versionStr = $file
         break
      }

      if ([string]::IsNullOrEmpty($versionStr))
      {
         Write-Host "Version is not specified" -ForegroundColor Red
         exit -1
      }
   }

   $command = ".\\TestPackage.ps1 -Package ${package} -Version $versionStr -PlatformTarget ${platformTarget} -RuntimeIdentifier ${runtimeIdentifier}"
   Invoke-Expression $command

   if ($lastexitcode -ne 0)
   {
      Set-Location -Path $Current
      exit -1
   }
}