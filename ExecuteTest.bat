@setlocal enabledelayedexpansion
@echo off
@rem ***************************************
@rem Arguments
@rem %1: Copy TestData (1:true, 0:false)
@rem %2: Target (cpu/cuda/arm)
@rem ***************************************

if "%1"=="" ( 
  @echo Error: Copy TestData [1/0]
  @exit /B
)

if "%2"=="" ( 
  @echo Error: Target [cpu/cuda/arm]
  @exit /B
)

@set OUTPUT=build_win_%2_x64

@set TARGET=test\DlibDotNet.Tests\bin\Release\netcoreapp2.1

if "%1"=="1" (
  @set DST_DATA=!TARGET!\data
  if NOT EXIST "!DST_DATA!" (
     mkdir "!DST_DATA!"
  )
  @xcopy test\DlibDotNet.Tests\data "!DST_DATA!" /y /s
)

@set NATIVEDIR=src\DlibDotNet.Native\!OUTPUT!
@set NATIVEDNNDIR=src\DlibDotNet.Native.Dnn\!OUTPUT!

@copy !NATIVEDIR!\Release\DlibDotNetNative.dll !TARGET! /y
@copy !NATIVEDNNDIR!\Release\DlibDotNetNativeDnn.dll !TARGET! /y

dotnet test test\DlibDotNet.Tests\DlibDotNet.Tests.csproj -c Release