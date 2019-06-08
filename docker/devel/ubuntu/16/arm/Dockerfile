FROM ubuntu:16.04
LABEL maintainer "Takuya Takeuchi <takuya.takeuchi.dev@gmail.com>"

# install package to build
RUN apt-get update && apt-get install -y \
    build-essential \
    libopenblas-dev \
    liblapack-dev \
    libx11-dev \
    cmake

# install arm and arm64 compiler
RUN apt-get update && apt-get install -y \
    libc6-armel-cross \
    libc6-dev-armel-cross \
    binutils-arm-linux-gnueabi \
    gcc-arm-linux-gnueabihf \
    g++-arm-linux-gnueabihf \
    g++-aarch64-linux-gnu \
    gcc-aarch64-linux-gnu \
    && apt-get clean && rm -rf /var/lib/apt/lists/*

# set compiler
ENV CMAKE_C_COMPILER=/usr/bin/gcc
ENV CMAKE_CXX_COMPILER=/usr/bin/g++