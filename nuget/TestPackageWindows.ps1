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

Set-StrictMode -Version Latest

$OperatingSystem="win"

# Store current directory
$Current = Get-Location

$BuildTargets = @()
$BuildTargets += New-Object PSObject -Property @{Package = "DlibDotNet";         RID = "$OperatingSystem-x64"; }
$BuildTargets += New-Object PSObject -Property @{Package = "DlibDotNet";         RID = "$OperatingSystem-x86"; }
$BuildTargets += New-Object PSObject -Property @{Package = "DlibDotNet.MKL";     RID = "$OperatingSystem-x64"; }
$BuildTargets += New-Object PSObject -Property @{Package = "DlibDotNet.MKL";     RID = "$OperatingSystem-x86"; }
$BuildTargets += New-Object PSObject -Property @{Package = "DlibDotNet.CUDA92";  RID = "$OperatingSystem-x64"; }
$BuildTargets += New-Object PSObject -Property @{Package = "DlibDotNet.CUDA100"; RID = "$OperatingSystem-x64"; }
$BuildTargets += New-Object PSObject -Property @{Package = "DlibDotNet.CUDA101"; RID = "$OperatingSystem-x64"; }

foreach($BuildTarget in $BuildTargets)
{
   $command = ".\\TestPackage.ps1 -Package ${BuildTarget.Package} -Version $Version -RuntimeIdentifier ${BuildTarget.RID}"
   Invoke-Expression $command

   if ($lastexitcode -ne 0)
   {
      Set-Location -Path $Current
      exit -1
   }
}