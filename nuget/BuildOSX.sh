#!/bin/bash

ROOT=`pwd`

# Get root of DlibDotNet directory
DDNDIR=`dirname $(pwd)`

CONFIG=Release
NATIVE=${DDNDIR}/src/DlibDotNet.Native
NATIVEDNN=${DDNDIR}/src/DlibDotNet.Native.Dnn

arch=(
    "cpu"
    #"cuda"
)

for a in "${arch[@]}" ; do
    cd ${NATIVE}
    ./BuildUnix.sh ${CONFIG} ${a} 64
    cp ${NATIVE}/build_osx_${a}/libDlibDotNetNative.dylib ${ROOT}/${a}/runtimes/osx-x64/native
done

for a in "${arch[@]}" ; do
    cd ${NATIVEDNN}
    ./BuildUnix.sh ${CONFIG} ${a} 64
    cp ${NATIVEDNN}/build_osx_${a}/libDlibDotNetNativeDnn.dylib ${ROOT}/${a}/runtimes/osx-x64/native
done

cd ${ROOT}
