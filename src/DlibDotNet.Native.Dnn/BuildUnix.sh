#!/bin/bash

# ***************************************
# Arguments
# $1: Build Configuration (Release/Debug)
# $2: Target (cpu/cuda/arm)
# $3: Architecture  (32/64)
# ***************************************
if [ $# -ne 3 ]; then
  echo "Error: Specify build configuration [Release/Debug], Target [cpu/cuda/arm] and Architecture [32/64]"
  exit 1
fi

CUDDIR=`pwd`

# Check Operating System
if [ "$(uname)" == 'Darwin' ]; then
  OS='osx'
elif [ "$(expr substr $(uname -s) 1 5)" == 'Linux' ]; then
  OS='linux'
else
  echo "Your platform ($(uname -a)) is not supported."
  exit 1
fi

# Check Architecture
if [ "$3" != "32" ] && [ "$3" != "64" ]; then
  echo "Architecture '($3)' is not supported."
  exit 1
fi

if [ "$3" == "32" ]; then
   OUTPUT=build_${OS}_$2_x86
elif [ "$3" == "64" ]; then
   OUTPUT=build_${OS}_$2_x64
fi

if [ "$2" == "cpu" ]; then
   mkdir -p ${OUTPUT}
   cd ${OUTPUT}
   cmake -D DLIB_USE_CUDA=OFF ..
elif [ "$2" == "cuda" ]; then
   mkdir -p ${OUTPUT}
   cd ${OUTPUT}
   cmake -D DLIB_USE_CUDA=ON ..
elif [ "$2" == "arm" ]; then
   mkdir -p ${OUTPUT}
   cd ${OUTPUT}

   if [ "$3" == "32" ]; then
      cmake -D DLIB_USE_CUDA=OFF \
            -D ENABLE_NEON=ON \
            -D CMAKE_C_COMPILER=/usr/bin/arm-linux-gnueabihf-gcc \
            -D CMAKE_CXX_COMPILER=/usr/bin/arm-linux-gnueabihf-g++ \
            ..
   elif [ "$3" == "64" ]; then
      cmake -D DLIB_USE_CUDA=OFF \
            -D ENABLE_NEON=ON \
            -D CMAKE_C_COMPILER=/usr/bin/aarch64-linux-gnu-gcc \
            -D CMAKE_CXX_COMPILER=/usr/bin/aarch64-linux-gnu-g++ \
            ..
   fi
else
   echo "Error: Target should be [cpu/cuda/arm] but '$2' is specified"
   exit 1
fi

cmake --build . --config $1
cd ${CURDIR}
