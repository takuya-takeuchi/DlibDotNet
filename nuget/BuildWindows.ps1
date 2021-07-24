Param()

# import class and function
$ScriptPath = $PSScriptRoot
$DlibDotNetRoot = Split-Path $ScriptPath -Parent
$ScriptPath = Join-Path $DlibDotNetRoot "nuget" | `
              Join-Path -ChildPath "BuildUtils.ps1"
import-module $ScriptPath -function *

$OperatingSystem="win"

# Store current directory
$Current = Get-Location

$BuildSourceHash = [Config]::GetBinaryLibraryWindowsHash()

$IntelMKLDir = $env:MKL_WIN
if ([string]::IsNullOrEmpty($IntelMKLDir))
{
   Write-Host "Environmental Value 'MKL_WIN' is not defined." -ForegroundColor Yellow
}

if ($IntelMKLDir -And !(Test-Path $IntelMKLDir))
{
   Write-Host "Environmental Value 'MKL_WIN' does not exist." -ForegroundColor Yellow
}

$BuildTargets = @()
$BuildTargets += [BuildTarget]::new("desktop", "cpu",  64, "$OperatingSystem-x64", "", 0                 )
$BuildTargets += [BuildTarget]::new("desktop", "cpu",  32, "$OperatingSystem-x86", "", 0                 )
$BuildTargets += [BuildTarget]::new("desktop", "mkl",  64, "$OperatingSystem-x64", "", "${$IntelMKLDir}" )
$BuildTargets += [BuildTarget]::new("desktop", "mkl",  32, "$OperatingSystem-x86", "", "${$IntelMKLDir}" )
# $BuildTargets += [BuildTarget]::new("desktop", "cuda", 64, "$OperatingSystem-x64", "", 90                )
# $BuildTargets += [BuildTarget]::new("desktop", "cuda", 64, "$OperatingSystem-x64", "", 91                )
$BuildTargets += [BuildTarget]::new("desktop", "cuda", 64, "$OperatingSystem-x64", "", 92                )
$BuildTargets += [BuildTarget]::new("desktop", "cuda", 64, "$OperatingSystem-x64", "", 100               )
$BuildTargets += [BuildTarget]::new("desktop", "cuda", 64, "$OperatingSystem-x64", "", 101               )
$BuildTargets += [BuildTarget]::new("desktop", "cuda", 64, "$OperatingSystem-x64", "", 102               )
$BuildTargets += [BuildTarget]::new("desktop", "cuda", 64, "$OperatingSystem-x64", "", 110               )
$BuildTargets += [BuildTarget]::new("desktop", "cuda", 64, "$OperatingSystem-x64", "", 111               )
$BuildTargets += [BuildTarget]::new("desktop", "cuda", 64, "$OperatingSystem-x64", "", 112               )

foreach ($BuildTarget in $BuildTargets)
{
   $BuildTarget.OperatingSystem = ${OperatingSystem}
   
   $ret = [Config]::Build($DlibDotNetRoot, $False, $BuildSourceHash, $BuildTarget)
   if ($ret -eq $False)
   {
      Set-Location -Path $Current
      exit -1
   }
}

# Move to Root directory 
Set-Location -Path $Current