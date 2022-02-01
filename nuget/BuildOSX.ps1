Param()

# import class and function
$ScriptPath = $PSScriptRoot
$DlibDotNetRoot = Split-Path $ScriptPath -Parent
$ScriptPath = Join-Path $DlibDotNetRoot "nuget" | `
              Join-Path -ChildPath "BuildUtils.ps1"
import-module $ScriptPath -function *

$OperatingSystem="osx"

# Store current directory
$Current = Get-Location

$BuildSourceHash = [Config]::GetBinaryLibraryOSXHash()

# https://docs.microsoft.com/ja-jp/dotnet/core/rid-catalog#macos-rids
# osx-x86 does not support
$BuildTargets = @()
$BuildTargets += [BuildTarget]::new("desktop", "cpu", 64, "$OperatingSystem-x64", "" )
# $BuildTargets += [BuildTarget]::new("desktop", "cpu", 32, "$OperatingSystem-x86", "" )
$BuildTargets += [BuildTarget]::new("desktop", "mkl", 64, "$OperatingSystem-x64", "" )
# $BuildTargets += [BuildTarget]::new("desktop", "mkl", 32, "$OperatingSystem-x86", "" )

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