#!/bin/bash

TARGET=$1
ARCH=$2
DDNROOT=/opt/data/DlibDotNet

if [ $# -eq 3 ]; then
   CUDA=$3
fi

CONFIG=Release

cd ${DDNROOT}/src/DlibDotNet.Native
pwsh Build.ps1 ${CONFIG} ${TARGET} ${ARCH} ${CUDA}

cd ${DDNROOT}/src/DlibDotNet.Native.Dnn
pwsh Build.ps1 ${CONFIG} ${TARGET} ${ARCH} ${CUDA}