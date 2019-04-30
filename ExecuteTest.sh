#!/bin/bash

# ***************************************
# Arguments
# $1: Copy TestData (1:true, 0:false)
# $2: Target (cpu/cuda/arm)
# ***************************************
if [ $# -ne 2 ]; then
  echo "Error: Specify Copy TestData [1/0] and Target [cpu/cuda/arm]"
  exit 1
fi

# Check Operating System
if [ "$(uname)" == 'Darwin' ]; then
  OS='osx'
elif [ "$(expr substr $(uname -s) 1 5)" == 'Linux' ]; then
  OS='linux'
else
  echo "Your platform ($(uname -a)) is not supported."
  exit 1
fi

# only x64
OUTPUT=build_${OS}_$2_x64

TARGET=test/DlibDotNet.Tests/bin/Release/netcoreapp2.1

if [ "$1" == "1" ]; then
  DST_DATA=${TARGET}/data
  mkdir -p ${DST_DATA}
  cp -Rf test/DlibDotNet.Tests/data/* ${DST_DATA}
fi

NATIVEDIR=src/DlibDotNet.Native/${OUTPUT}
NATIVEDNNDIR=src/DlibDotNet.Native.Dnn/${OUTPUT}

if [ "$(uname)" == 'Darwin' ]; then
 cp ${NATIVEDIR}/libDlibDotNetNative.dylib ${TARGET}
 cp ${NATIVEDNNDIR}/libDlibDotNetNativeDnn.dylib ${TARGET}
elif [ "$(expr substr $(uname -s) 1 5)" == 'Linux' ]; then
 cp ${NATIVEDIR}/libDlibDotNetNative.so ${TARGET}
 cp ${NATIVEDNNDIR}/libDlibDotNetNativeDnn.so ${TARGET}
fi

dotnet test test/DlibDotNet.Tests/DlibDotNet.Tests.csproj -c Release