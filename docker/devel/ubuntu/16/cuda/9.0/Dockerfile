FROM nvidia/cuda:9.0-cudnn7-devel-ubuntu16.04
LABEL maintainer "Takuya Takeuchi <takuya.takeuchi.dev@gmail.com>"

# install package to build
RUN apt-get update && apt-get install -y \
    build-essential \
    libopenblas-dev \
    liblapack-dev \
    libx11-dev \
    cmake \
    && apt-get clean && rm -rf /var/lib/apt/lists/*

# set compiler
ENV CMAKE_C_COMPILER=/usr/bin/gcc
ENV CMAKE_CXX_COMPILER=/usr/bin/g++

# set env to build by using CUDA
ENV CUDA_PATH /usr/local/cuda
ENV PATH $CUDA_PATH/bin:$PATH
ENV CPATH $CUDA_PATH/include:$CPATH
ENV LD_LIBRARY_PATH $CUDA_PATH/lib64:$LD_LIBRARY_PATH
ENV NCCL_ROOT /usr/local/nccl
ENV CPATH $NCCL_ROOT/include:$CPATH
ENV LD_LIBRARY_PATH $NCCL_ROOT/lib/:$LD_LIBRARY_PATH
ENV LIBRARY_PATH $NCCL_ROOT/lib/:$LIBRARY_PATH