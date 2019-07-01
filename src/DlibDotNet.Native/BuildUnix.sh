#!/bin/bash

# ***************************************
# Arguments
# $1: Build Configuration (Release/Debug)
# $2: Target (cpu/cuda/arm/mkl)
# $3: Architecture  (32/64)
# $4: CUDA version if Target is cuda [90/91/92/100/101]
# ***************************************
if [ "$2" == "cuda" ]; then
   if [ $# -ne 4 ]; then
     echo "Error: Specify build configuration [Release/Debug], Target [cpu/cuda/arm], Architecture [32/64] and CUDA version  [90/91/92/100/101]"
     exit 1
   fi
else
   if [ $# -ne 3 ]; then
     echo "Error: Specify build configuration [Release/Debug], Target [cpu/cuda/arm/mkl] and Architecture [32/64]"
     exit 1
   fi
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

if [ "$2" == "cuda" ]; then
   target=$2-$4
else
   target=$2
fi

if [ "$3" == "32" ]; then
   OUTPUT=build_${OS}_${target}_x86
elif [ "$3" == "64" ]; then
   OUTPUT=build_${OS}_${target}_x64
fi

if [ "$2" == "cpu" ]; then
   mkdir -p ${OUTPUT}
   cd ${OUTPUT}
   # If install Intel MKL, cmake uses it
   cmake -D DLIB_USE_CUDA=OFF \
         -D mkl_include_dir="" \
         -D mkl_intel="" \
         -D mkl_rt="" \
         -D mkl_thread="" \
         -D mkl_pthread="" \
         -D LIBPNG_IS_GOOD=OFF -D PNG_FOUND=OFF -D PNG_LIBRARY_RELEASE="" -D PNG_LIBRARY_DEBUG="" -D PNG_PNG_INCLUDE_DIR="" \
         ..
elif [ "$2" == "cuda" ]; then
   mkdir -p ${OUTPUT}
   cd ${OUTPUT}
   cmake -D DLIB_USE_CUDA=ON \
         -D DLIB_USE_BLAS=OFF \
         -D LIBPNG_IS_GOOD=OFF -D PNG_FOUND=OFF -D PNG_LIBRARY_RELEASE="" -D PNG_LIBRARY_DEBUG="" -D PNG_PNG_INCLUDE_DIR="" \
         ..
elif [ "$2" == "arm" ]; then
   mkdir -p ${OUTPUT}
   cd ${OUTPUT}

   if [ "$3" == "32" ]; then
      cmake -D DLIB_USE_CUDA=OFF \
            -D ENABLE_NEON=ON \
            -D DLIB_USE_BLAS=ON \
            -D CMAKE_C_COMPILER=/usr/bin/arm-linux-gnueabihf-gcc \
            -D CMAKE_CXX_COMPILER=/usr/bin/arm-linux-gnueabihf-g++ \
            -D LIBPNG_IS_GOOD=OFF -D PNG_FOUND=OFF -D PNG_LIBRARY_RELEASE="" -D PNG_LIBRARY_DEBUG="" -D PNG_PNG_INCLUDE_DIR="" \
            ..
   elif [ "$3" == "64" ]; then
      cmake -D DLIB_USE_CUDA=OFF \
            -D ENABLE_NEON=ON \
            -D DLIB_USE_BLAS=ON \
            -D CMAKE_C_COMPILER=/usr/bin/aarch64-linux-gnu-gcc \
            -D CMAKE_CXX_COMPILER=/usr/bin/aarch64-linux-gnu-g++ \
            -D LIBPNG_IS_GOOD=OFF -D PNG_FOUND=OFF -D PNG_LIBRARY_RELEASE="" -D PNG_LIBRARY_DEBUG="" -D PNG_PNG_INCLUDE_DIR="" \
            ..
   fi
elif [ "$2" == "mkl" ]; then
   mkdir -p ${OUTPUT}
   cd ${OUTPUT}
   cmake -D DLIB_USE_CUDA=OFF \
         -D DLIB_USE_BLAS=ON \
         -D LIBPNG_IS_GOOD=OFF -D PNG_FOUND=OFF -D PNG_LIBRARY_RELEASE="" -D PNG_LIBRARY_DEBUG="" -D PNG_PNG_INCLUDE_DIR="" \
         ..
else
   echo "Error: Target should be [cpu/cuda/arm] but '$2' is specified"
   exit 1
fi

cmake --build . --config $1
cd ${CURDIR}
