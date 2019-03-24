@echo off

@rem Get root of DlibDotNet directory
@set ROOT=%cd%
@cd %~dp0\..
@set DDNDIR=%cd%
@cd %ROOT%

@set CONFIG=Release
@set NATIVE=%DDNDIR%\src\DlibDotNet.Native
@set NATIVEDNN=%DDNDIR%\src\DlibDotNet.Native.Dnn

@set target=cpu cuda
@set arch=64

for %%t in (%target%) do (
  for %%a in (%arch%) do (
    @cd %NATIVE%
    @call BuildWindowsVS2017.bat %CONFIG% %%t %%a

    if "%%a"=="32" (
      @copy %NATIVE%\build_win_%%t_x86\%CONFIG%\DlibDotNetNative.dll %ROOT%\%%t\runtimes\win-x86\native /y
    ) else if "%%a"=="64" (
      @copy %NATIVE%\build_win_%%t_x64\%CONFIG%\DlibDotNetNative.dll %ROOT%\%%t\runtimes\win-x64\native /y
    )
  )
)

for %%t in (%target%) do (
  for %%a in (%arch%) do (
    @cd %NATIVEDNN%
    @call BuildWindowsVS2017.bat %CONFIG% %%t %%a    

    if "%%a"=="32" (
      @copy %NATIVEDNN%\build_win_%%t_x86\%CONFIG%\DlibDotNetNativeDnn.dll %ROOT%\%%t\runtimes\win-x86\native /y
    ) else if "%%a"=="64" (
      @copy %NATIVEDNN%\build_win_%%t_x64\%CONFIG%\DlibDotNetNativeDnn.dll %ROOT%\%%t\runtimes\win-x64\native /y
    )
  )
)

@cd %ROOT%

@echo;
@echo ********************
@echo Check file version
@echo ********************
for %%t in (%target%) do (
  for %%a in (%arch%) do (

    if "%%a"=="32" (
      @echo %%t - x86
      @echo\    DlibDotNetNative.dll
      @filever /v %ROOT%\%%t\runtimes\win-x86\native\DlibDotNetNative.dll | findstr ProductVersion
      @echo\    DlibDotNetNativeDnn.dll
      @filever /v %ROOT%\%%t\runtimes\win-x86\native\DlibDotNetNativeDnn.dll | findstr ProductVersion
    ) else if "%%a"=="64" (
      @echo %%t - x64
      @echo\    DlibDotNetNative.dll
      @filever /v %ROOT%\%%t\runtimes\win-x64\native\DlibDotNetNative.dll | findstr ProductVersion
      @echo\    DlibDotNetNativeDnn.dll
      @filever /v %ROOT%\%%t\runtimes\win-x64\native\DlibDotNetNativeDnn.dll | findstr ProductVersion
    )
  )
)