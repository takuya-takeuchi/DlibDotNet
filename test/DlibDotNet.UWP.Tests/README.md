# DlibDotNet.UWP.Tests

## Build

````
cd DlibDotNEt.UWP.Tests
set msbuild="C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\amd64\MSBuild.exe"
%msbuild% DlibDotNet.UWP.Tests.csproj /p:Configuration=Release;OutDir=Package;AppxBundle=Always;AppxBundlePlatforms="x64"
````

## Create Self-signed certificate (*.cer)

Specify empty password

````
> set makecert="C:\Program Files (x86)\Windows Kits\10\bin\x64\makecert.exe"
> %makecert% -r -n "CN=DlibDotNet" -sv DlibDotNet.pvk DlibDotNet.cer
Succeeded
````

## Create *.pfx

````
> set pvk2pfx="C:\Program Files (x86)\Windows Kits\10\bin\x64\pvk2pfx.exe"
> %pvk2pfx% -pvk DlibDotNet.pvk -spc DlibDotNet.cer -pfx DlibDotNet.pfx
````

## Add *.cer file to system

1. Double click DlibDotNet.cer
1. Click **Install Certificate**
1. Select **Local Machine**
1. Select **Place all certificates in the following store** and select **Browse**
1. Select **Trusted Root Certification Authorities** and click **OK**

## Sign appx

````
> set signtool="C:\Program Files (x86)\Windows Kits\10\bin\x64\signtool.exe"
> %signtool% sign /a /fd SHA256 /f DlibDotNet.pfx DlibDotNet.UWP.Tests_1.0.0.0_x64.appx
Done Adding Additional Store
Successfully signed: DlibDotNet.UWP.Tests_1.0.0.0_x64.appx
````

## Start Windows App Certification Kit

````
> set appcert="C:\Program Files (x86)\Windows Kits\10\App Certification Kit\appcert.exe"
> set appx="<DlibDotNet.UWP.Tests root>\Package\DlibDotNet.UWP.Tests\DlibDotNet.UWP.Tests_1.0.0.0_x64.appx"
> set report="<DlibDotNet.UWP.Tests root>\Package\DlibDotNet.UWP.Tests\AppCertReport.xml"
> if -e %report% del %report%
> %appcert% test -appxpackagepath %appx%  -reportoutputpath %report%
````