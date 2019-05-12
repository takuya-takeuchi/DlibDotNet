#!/bin/bash

TARGET=$1
ARCH=$2
DDNROOT=/opt/data/DlibDotNet

if [ $# -eq 3 ]; then
   CUDA=$3
fi

if [ "${TARGET}" == 'cpu' ] ; then

   if [ "$2" == "32" ]; then
      OUTDIR=${DDNROOT}/nuget/${TARGET}/runtimes/linux-x86/native
      BUILDDIR=build_linux_${TARGET}_x86
   elif [ "$2" == "64" ]; then
      OUTDIR=${DDNROOT}/nuget/${TARGET}/runtimes/linux-x64/native
      BUILDDIR=build_linux_${TARGET}_x64
   fi

elif [ "${TARGET}" == 'cuda' ]; then

   if [ "$2" == "32" ]; then
      OUTDIR=${DDNROOT}/nuget/${TARGET}-${CUDA}/runtimes/linux-x86/native
      BUILDDIR=build_linux_${TARGET}-${CUDA}_x86
   elif [ "$2" == "64" ]; then
      OUTDIR=${DDNROOT}/nuget/${TARGET}-${CUDA}/runtimes/linux-x64/native
      BUILDDIR=build_linux_${TARGET}-${CUDA}_x64
   fi

elif [ "${TARGET}" == 'arm' ]; then

   if [ "$2" == "32" ]; then
      OUTDIR=${DDNROOT}/nuget/${TARGET}/runtimes/linux-arm/native
      BUILDDIR=build_linux_${TARGET}_x86
   elif [ "$2" == "64" ]; then
      OUTDIR=${DDNROOT}/nuget/${TARGET}/runtimes/linux-arm64/native
      BUILDDIR=build_linux_${TARGET}_x64
   fi

else
  echo "Specified architecture '${TARGET}' is not supported."
  exit 1
fi

CONFIG=Release

cd ${DDNROOT}/src/DlibDotNet.Native
./BuildUnix.sh ${CONFIG} ${TARGET} ${ARCH} ${CUDA}
cp ${BUILDDIR}/*.so ${OUTDIR}

cd ${DDNROOT}/src/DlibDotNet.Native.Dnn
./BuildUnix.sh ${CONFIG} ${TARGET} ${ARCH} ${CUDA}
cp ${BUILDDIR}/*.so ${OUTDIR}