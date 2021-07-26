$demo = $PSScriptRoot
$root = Get-Location
$xamarin = Split-Path $demo -Parent
$examples = Split-Path $xamarin -Parent
$dlibdotnet = Split-Path $examples -Parent
$nuget = Join-Path $dlibdotnet nuget

Set-Location $demo
Set-Location Demo
dotnet restore -s $nuget

Set-Location $demo
Set-Location Demo.iOS
dotnet restore -s $nuget
msbuild "/p:Configuration=Release;Platform=iPhoneSimulator" "/fileLoggerParameters:LogFile=../BuildIOS.log;Verbosity=quiet;Encoding=UTF-8"
Set-Location $root