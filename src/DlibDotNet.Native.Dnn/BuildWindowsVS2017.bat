@setlocal enabledelayedexpansion
@echo off
@rem ***************************************
@rem Arguments
@rem %1: Build Configuration (Release/Debug)
@rem %: Target (cpu/cuda)
@rem ***************************************

if "%1"=="" ( 
  @echo Error: Speficy build configuration [Release/Debug]
  @exit /B
)

if "%2"=="" ( 
  @echo Error: Speficy Target [cpu/cuda]
  @exit /B
)

set CURDIR=%cd%
set OUTPUT=build_win_%2

if "%2"=="cpu" (
  set OUTPUT=build_%2
  if not exist %OUTPUT% mkdir %OUTPUT%
  cd %OUTPUT%
  cmake -G "Visual Studio 15 2017 Win64" -T host=x64 ^
        -D DLIB_USE_CUDA=OFF ^
        ..
) else if "%2"=="cuda" (
  set OUTPUT=build_%2
  if not exist %OUTPUT% mkdir %OUTPUT%
  cd %OUTPUT%
  cmake -G "Visual Studio 15 2017 Win64" -T host=x64 ^
        -D DLIB_USE_CUDA=ON ^
        ..
) else ( 
  @echo Error: Target should be [cpu/cuda]
  @exit /B
)

cmake --build . --config %1
cd %CURDIR%