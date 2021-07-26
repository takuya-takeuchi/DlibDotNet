$root = Get-Location
Set-Location Demo.iOS
msbuild "/p:Configuration=Release;Platform=iPhoneSimulator" "/fileLoggerParameters:LogFile=../BuildIOS.log;Verbosity=quiet;Encoding=UTF-8"
Set-Location $root