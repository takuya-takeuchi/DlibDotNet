#!/bin/bash

# ***************************************
# Arguments
# $1: Build Configuration (Release/Debug)
# $2: Target (cpu/cuda)
# ***************************************
if [ $# -ne 2 ]; then
  echo "Error: Specify build configuration [Release/Debug] and Target [cpu/cuda]"
  exit 1
fi

CUDDIR=`pwd`
OUTPUT=build_$2

if [ $2 = "cpu" ]; then
   mkdir -p ${OUTPUT}
   cd ${OUTPUT}
   cmake -D DLIB_USE_CUDA=OFF ..
elif [ $2 = "cuda" ]; then
   mkdir -p ${OUTPUT}
   cd ${OUTPUT}
   cmake -D DLIB_USE_CUDA=ON ..
else
   echo "Error: Target should be [cpu/cuda]"
   exit 1
fi

cmake --build . --config $1
cd ${CURDIR}
