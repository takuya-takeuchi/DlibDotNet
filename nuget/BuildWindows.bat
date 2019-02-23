@echo off

@rem Get root of DlibDotNet directory
@set ROOT=%cd%
@cd %~dp0\..
@set DDNDIR=%cd%
@cd %ROOT%

@set CONFIG=Release
@set NATIVE=%DDNDIR%\src\DlibDotNet.Native
@set NATIVEDNN=%DDNDIR%\src\DlibDotNet.Native.Dnn

@set arch=cpu cuda
for %%a in (%arch%) do (
  @cd %NATIVE%
  @call BuildWindowsVS2017.bat %CONFIG% %%a
  @copy %NATIVE%\build_win_%%a\%CONFIG%\DlibDotNetNative.dll %ROOT%\%%a\runtimes\win-x64\native /y
)

@set arch=cpu cuda
for %%a in (%arch%) do (
  @cd %NATIVEDNN%
  @call BuildWindowsVS2017.bat %CONFIG% %%a
  @copy %NATIVEDNN%\build_win_%%a\%CONFIG%\DlibDotNetNativeDnn.dll %ROOT%\%%a\runtimes\win-x64\native /y
)

@cd %ROOT%

@rem check file version
for %%a in (%arch%) do (
  @filever /v %ROOT%\%%a\runtimes\win-x64\native\DlibDotNetNative.dll | findstr ProductVersion
  @filever /v %ROOT%\%%a\runtimes\win-x64\native\DlibDotNetNativeDnn.dll | findstr ProductVersion
)