Param()

# import class and function
$ScriptPath = $PSScriptRoot
$DlibDotNetRoot = Split-Path $ScriptPath -Parent
$ScriptPath = Join-Path $DlibDotNetRoot "nuget" | `
              Join-Path -ChildPath "BuildUtils.ps1"
import-module $ScriptPath -function *

$OperatingSystem="linux"
$Distribution="ubuntu"
$DistributionVersion="16"

# Store current directory
$Current = Get-Location

$BuildSourceHash = [Config]::GetBinaryLibraryLinuxHash()

# https://github.com/dotnet/coreclr/issues/9265
# linux-x86 does not support
$BuildTargets = @()
$BuildTargets += [BuildTarget]::new("desktop", "cpu",  64,  "$OperatingSystem-x64",   "/x64", 0   )
# $BuildTargets += [BuildTarget]::new("desktop", "cpu",  32,  "$OperatingSystem-x86",   "/x86", 0   )
$BuildTargets += [BuildTarget]::new("desktop", "mkl",  64,  "$OperatingSystem-x64",   "/x64", 0   )
# $BuildTargets += [BuildTarget]::new("desktop", "mkl",  32,  "$OperatingSystem-x86",   "/x86", 0   )
$BuildTargets += [BuildTarget]::new("desktop", "cuda", 64,  "$OperatingSystem-x64",       "", 92  )
$BuildTargets += [BuildTarget]::new("desktop", "cuda", 64,  "$OperatingSystem-x64",       "", 100 )
$BuildTargets += [BuildTarget]::new("desktop", "cuda", 64,  "$OperatingSystem-x64",       "", 101 )
$BuildTargets += [BuildTarget]::new("desktop", "cuda", 64,  "$OperatingSystem-x64",       "", 102 )
$BuildTargets += [BuildTarget]::new("desktop", "cuda", 64,  "$OperatingSystem-x64",       "", 110 )
$BuildTargets += [BuildTarget]::new("desktop", "cuda", 64,  "$OperatingSystem-x64",       "", 111 )
$BuildTargets += [BuildTarget]::new("desktop", "cuda", 64,  "$OperatingSystem-x64",       "", 112 )
# $BuildTargets += [BuildTarget]::new("desktop", "arm",  64,  "$OperatingSystem-arm64", "/arm64", 0   )
# $BuildTargets += [BuildTarget]::new("desktop", "arm",  32,  "$OperatingSystem-arm",       "", 0   )


foreach($BuildTarget in $BuildTargets)
{
   $BuildTarget.OperatingSystem     = ${OperatingSystem}
   $BuildTarget.Distribution        = ${Distribution}
   $BuildTarget.DistributionVersion = ${DistributionVersion}
   
   $ret = [Config]::Build($DlibDotNetRoot, $True, $BuildSourceHash, $BuildTarget)
   if ($ret -eq $False)
   {
      Set-Location -Path $Current
      exit -1
   }
}

# Move to Root directory 
Set-Location -Path $Current