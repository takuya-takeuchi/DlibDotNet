# DlibDotNet.UWP.Tests

This project aims to test whether DlibDotNet.UWP can pass for **Windows App Certification Kit**.

## 1. Create certifications

This project already contains the following files

* DlibDotNet.cer
* DlibDotNet.pvk
* DlibDotNet.pfx

These files are for demonstration.

### Create Self-signed certificate (*.cer)

Specify **password** as password.  
**-eku** must be spefieid.

````bat
> set makecert="C:\Program Files (x86)\Windows Kits\10\bin\x64\makecert.exe"
> %makecert% -n "CN=DlibDotNet" -a sha256 -b 01/01/2000 -e 01/01/2100 -eku 1.3.6.1.5.5.7.3.3 -cy end -r -sv DlibDotNet.pvk DlibDotNet.cer
Succeeded
````

### Create *.pfx

Specify **password** as password of pfx.

````bat
> set pvk2pfx="C:\Program Files (x86)\Windows Kits\10\bin\x64\pvk2pfx.exe"
> %pvk2pfx% -pvk DlibDotNet.pvk -spc DlibDotNet.cer -pfx DlibDotNet.pfx -pi password
````

### Add *.pfx file to system

1. Double click *DlibDotNet.pfx*

![import](images/01.png "import")

2. Select **Local Computer** and click **Next**

![import](images/02.png "import")

3. Click **Next***

![import](images/03.png "import")

4. Input password that is used on execute pvk2pfx
5. Click **Next**

![import](images/04.png "import")

6. Select **Place all certificates in the following store** and select **Browse**

![import](images/05.png "import")

7. Select **Trusted Root Certification Authorities** and click **OK**
8. Click **Next**

![import](images/06.png "import")

9. Click **Finish**

### Check install result of *pfx

1. Run **certmgr.msc**

![certmgr](images/certmgr.png "certmgr")

2. Select **Trusted Root Certification Authorities** and **Certificates**
3. You can see code signing of **DlibDotNet**

## 2. Build Application 

You must create **DlibDotNet.UWP.nupkg** in nuget directory before build.

### Build

````bat
> cd DlibDotNet
> set rootDlibDotNet=%cd%
> cd test\DlibDotNet.UWP.Tests
> set OutputDir=Package
> set msbuild="C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\amd64\MSBuild.exe"
> if exist Package rmdir Package /s /q
> %msbuild% DlibDotNet.UWP.Tests.csproj ^
            /t:restore ^
            /t:Rebuild ^
            /p:RestoreAdditionalProjectSources=%rootDlibDotNet%\nuget ^
            /p:RestoreNoCache=true ^
            /p:Configuration=Release ^
            /p:Platform="x64" ^
            /p:OutDir=%OutputDir% ^
            /p:AppxBundle=Always ^
            /p:AppxBundlePlatforms="x64"
````

### Sign appx

Specify **password** as pfx password for **/p**.  
Basically, this pfx must not be test certifications but formal certifications.

````bat
> cd DlibDotNet.UWP.Tests
> set OutputDir=Package
> set signtool="C:\Program Files (x86)\Windows Kits\10\bin\x64\signtool.exe"
> %signtool% sign /a /fd SHA256 /f DlibDotNet.pfx /p password %OutputDir%\DlibDotNet.UWP.Tests\DlibDotNet.UWP.Tests_1.0.0.0_x64.appx
Done Adding Additional Store
Successfully signed: Package\DlibDotNet.UWP.Tests\DlibDotNet.UWP.Tests_1.0.0.0_x64.appx
````

## 3. Start Windows App Certification Kit

````bat
> cd DlibDotNet.UWP.Tests
> set rootUWPTest=%cd%
> set appcert="C:\Program Files (x86)\Windows Kits\10\App Certification Kit\appcert.exe"
> set appx="%rootUWPTest%\Package\DlibDotNet.UWP.Tests\DlibDotNet.UWP.Tests_1.0.0.0_x64.appx"
> set report="%rootUWPTest%\Package\DlibDotNet.UWP.Tests\AppCertReport.xml"
> if exist %report% del %report%
> %appcert% test -appxpackagepath %appx%  -reportoutputpath %report%
````