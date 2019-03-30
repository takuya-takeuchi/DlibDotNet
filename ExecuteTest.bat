@setlocal enabledelayedexpansion
@echo off
@rem ***************************************
@rem Arguments
@rem %1: Copy TestData (1:true, 0:false)
@rem ***************************************

if "%1"=="" ( 
  @echo Error: Copy TestData [1/0]
  @exit /B
)

if "%1"=="1" (
  set DST_DATA=test\DlibDotNet.Tests\bin\Release\netcoreapp2.1\data
  if NOT EXIST "!DST_DATA!" (
     mkdir "!DST_DATA!"
  )
  xcopy test\DlibDotNet.Tests\data "!DST_DATA!" /y /s
)

dotnet test test\DlibDotNet.Tests\DlibDotNet.Tests.csproj -c Release