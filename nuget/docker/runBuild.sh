#!/bin/bash

DDNROOT=/opt/data/DlibDotNet
CPUDIR=${DDNROOT}/nuget/cpu/runtimes/linux-x64/native
CUDADIR=${DDNROOT}/nuget/cuda/runtimes/linux-x64/native
CONFIG=Release

cd ${DDNROOT}/src/DlibDotNet.Native
./BuildUnix.sh ${CONFIG} cpu
./BuildUnix.sh ${CONFIG} cuda
cp build_linux_cpu/*.so ${CPUDIR}
cp build_linux_cuda/*.so ${CUDADIR}

cd ${DDNROOT}/src/DlibDotNet.Native.Dnn
./BuildUnix.sh ${CONFIG} cpu
./BuildUnix.sh ${CONFIG} cuda
cp build_linux_cpu/*.so ${CPUDIR}
cp build_linux_cuda/*.so ${CUDADIR}