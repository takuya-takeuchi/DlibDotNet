#!/bin/bash

# ***************************************
# Arguments
# $1: Build Configuration (Release/Debug)
# $2: Target (cpu/cuda)
# ***************************************
if [ $# -ne 2 ]; then
  echo "Error: Specify build configuration [Release/Debug] and Target [cpu/cuda/arm]"
  exit 1
fi

CUDDIR=`pwd`

if [ "$(uname)" == 'Darwin' ]; then
  OS='osx'
elif [ "$(expr substr $(uname -s) 1 5)" == 'Linux' ]; then
  OS='linux'
else
  echo "Your platform ($(uname -a)) is not supported."
  exit 1
fi

OUTPUT=build_${OS}_$2

if [ $2 = "cpu" ]; then
   mkdir -p ${OUTPUT}
   cd ${OUTPUT}
   cmake -D DLIB_USE_CUDA=OFF ..
elif [ $2 = "cuda" ]; then
   mkdir -p ${OUTPUT}
   cd ${OUTPUT}
   cmake -D DLIB_USE_CUDA=ON ..
elif [ $2 = "arm" ]; then
   mkdir -p ${OUTPUT}
   cd ${OUTPUT}
   cmake -D DLIB_USE_CUDA=OFF \
         -D ENABLE_NEON=ON \
         -D CMAKE_C_COMPILER=/usr/bin/arm-linux-gnueabihf-gcc \
         -D CMAKE_CXX_COMPILER=/usr/bin/arm-linux-gnueabihf-g++ \
         ..
elif [ $2 = "arm64" ]; then
   mkdir -p ${OUTPUT}
   cd ${OUTPUT}
   cmake -D DLIB_USE_CUDA=OFF \
         -D ENABLE_NEON=ON \
         -D CMAKE_C_COMPILER=/usr/bin/aarch64-linux-gnu-gcc \
         -D CMAKE_CXX_COMPILER=/usr/bin/aarch64-linux-gnu-g++ \
         ..
else
   echo "Error: Target should be [cpu/cuda/arm]"
   exit 1
fi

cmake --build . --config $1
cd ${CURDIR}
