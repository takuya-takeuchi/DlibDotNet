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
$AndroidVersion="28.0.3-r20-jdk8"
$AndroidNativeApiLevel="24"

# Store current directory
$Current = Get-Location

$BuildSourceHash = [Config]::GetBinaryLibraryXamarinLinuxHash()

# https://docs.microsoft.com/ja-jp/xamarin/cross-platform/cpp/
# arm64-v8a
# armeabi-v7a
# x86
# x86_64
$BuildTargets = @()
$BuildTargets += [BuildTarget]::new("android", "arm", 64, "arm64-v8a",   "" )
# $BuildTargets += [BuildTarget]::new("android", "arm", 32, "armeabi-v7a", "" )
$BuildTargets += [BuildTarget]::new("android", "arm", 64, "x86_64",      "" )
# $BuildTargets += [BuildTarget]::new("android", "arm", 32, "x86",         "" )

foreach($BuildTarget in $BuildTargets)
{
   $BuildTarget.OperatingSystem       = ${OperatingSystem}
   $BuildTarget.Distribution          = ${Distribution}
   $BuildTarget.DistributionVersion   = ${DistributionVersion}
   $BuildTarget.AndroidVersion        = ${AndroidVersion}
   $BuildTarget.AndroidNativeApiLevel = ${AndroidNativeApiLevel}
   
   $ret = [Config]::Build($DlibDotNetRoot, $True, $BuildSourceHash, $BuildTarget)
   if ($ret -eq $False)
   {
      Set-Location -Path $Current
      exit -1
   }
}

# Move to Root directory 
Set-Location -Path $Current