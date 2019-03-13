#!/bin/bash

ARCH=$1

if [ "${ARCH}" == 'cpu' ]; then
  PACKAGE='cpu'
elif [ "${ARCH}" == 'gpu' ]; then
  PACKAGE='gpu'
elif [ "${ARCH}" == 'arm' ] || [ "${ARCH}" == 'arm64' ]; then
  PACKAGE='arm'
else
  echo "Specified architecture '${ARCH}' is not supported."
  exit 1
fi

DDNROOT=/opt/data/DlibDotNet
OUTDIR=${DDNROOT}/nuget/${ARCH}/runtimes/linux-${ARCH}/native
CONFIG=Release

cd ${DDNROOT}/src/DlibDotNet.Native
./BuildUnix.sh ${CONFIG} ${ARCH}
cp build_linux_${ARCH}/*.so ${OUTDIR}

cd ${DDNROOT}/src/DlibDotNet.Native.Dnn
./BuildUnix.sh ${CONFIG} ${ARCH}
cp build_linux_${ARCH}/*.so ${OUTDIR}