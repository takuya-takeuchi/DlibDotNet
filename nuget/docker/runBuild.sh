#!/bin/bash

TARGET=$1
ARCH=$2
DDNROOT=/opt/data/DlibDotNet

if [ "${TARGET}" == 'cpu' ] || [ "${TARGET}" == 'cuda' ]; then

   if [ "$2" == "32" ]; then
      OUTDIR=${DDNROOT}/nuget/${TARGET}/runtimes/linux-x86/native
   elif [ "$2" == "64" ]; then
      OUTDIR=${DDNROOT}/nuget/${TARGET}/runtimes/linux-x64/native
   fi

elif [ "${TARGET}" == 'arm' ]; then

   if [ "$2" == "32" ]; then
      OUTDIR=${DDNROOT}/nuget/${TARGET}/runtimes/linux-arm/native
   elif [ "$2" == "64" ]; then
      OUTDIR=${DDNROOT}/nuget/${TARGET}/runtimes/linux-arm64/native
   fi

else
  echo "Specified architecture '${TARGET}' is not supported."
  exit 1
fi

CONFIG=Release

if [ "$2" == "32" ]; then
   BUILDDIR=build_linux_${TARGET}_x86
elif [ "$2" == "64" ]; then
   BUILDDIR=build_linux_${TARGET}_x64
fi

cd ${DDNROOT}/src/DlibDotNet.Native
./BuildUnix.sh ${CONFIG} ${TARGET} ${ARCH}
cp ${BUILDDIR}/*.so ${OUTDIR}

cd ${DDNROOT}/src/DlibDotNet.Native.Dnn
./BuildUnix.sh ${CONFIG} ${TARGET} ${ARCH}
cp ${BUILDDIR}/*.so ${OUTDIR}
