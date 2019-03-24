@setlocal enabledelayedexpansion
@echo off
@rem ***************************************
@rem Arguments
@rem %1: Build Configuration (Release/Debug)
@rem %2: Target (cpu/cuda)
@rem %3: Architecture (32/64)
@rem ***************************************

if "%1"=="" ( 
  @echo Error: Speficy build configuration [Release/Debug]
  @exit /B
)

if "%2"=="" ( 
  @echo Error: Speficy Target [cpu/cuda]
  @exit /B
)

if "%3"=="" ( 
  @echo Error: Speficy Architecture [32/64]
  @exit /B
)

set CURDIR=%cd%

if "%3"=="32" (
   set OUTPUT=build_win_%2_x86
) else if "%3"=="64" (
   set OUTPUT=build_win_%2_x64
)
  
if "%2"=="cpu" (

  if "%3"=="32" (
    if not exist %OUTPUT% mkdir %OUTPUT%
    cd %OUTPUT%
    cmake -G "Visual Studio 15 2017" -T host=x64 ^
          -D DLIB_USE_CUDA=OFF ^
          ..
  ) else if "%3"=="64" (
    if not exist %OUTPUT% mkdir %OUTPUT%
    cd %OUTPUT%
    cmake -G "Visual Studio 15 2017 Win64" -T host=x64 ^
          -D DLIB_USE_CUDA=OFF ^
          ..
  ) else (
    @echo Error: Architecture should be [32/64]
    @exit /B
  )

) else if "%2"=="cuda" (

  if "%3"=="32" (
    if not exist %OUTPUT% mkdir %OUTPUT%
    cd %OUTPUT%
    cmake -G "Visual Studio 15 2017" -T host=x64 ^
          -D DLIB_USE_CUDA=ON ^
          ..
  ) else if "%3"=="64" (
    if not exist %OUTPUT% mkdir %OUTPUT%
    cd %OUTPUT%
    cmake -G "Visual Studio 15 2017 Win64" -T host=x64 ^
          -D DLIB_USE_CUDA=ON ^
          ..
  ) else (
    @echo Error: Architecture should be [32/64]
    @exit /B
  )

) else (
  @echo Error: Target should be [cpu/cuda]
  @exit /B
)

cmake --build . --config %1
cd %CURDIR%