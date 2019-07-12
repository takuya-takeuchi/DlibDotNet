FROM arm64v8/ubuntu:16.04_amd64
LABEL maintainer "Takuya Takeuchi <takuya.takeuchi.dev@gmail.com>"

# install package to build
RUN apt-get update && apt-get install -y \
    build-essential \
    libopenblas-dev \
    liblapack-dev \
    libx11-dev \
    cmake \ 
 && apt-get clean && rm -rf /var/lib/apt/lists/*

# install arm compiler
RUN apt-get update && apt-get install -y \
    libc6-armel-cross \
    libc6-dev-armel-cross \
    binutils-arm-linux-gnueabi \
    g++-arm-linux-gnueabihf \
    gcc-arm-linux-gnueabihf \
 && apt-get clean && rm -rf /var/lib/apt/lists/*
 
# set compiler 
#ENV CMAKE_C_COMPILER /usr/bin/gcc 
#ENV CMAKE_CXX_COMPILER /usr/bin/g++

# Install prerequisites
RUN apt-get update && apt-get install -y \
    libunwind8 \
    wget \
    tar \
    apt-transport-https \
 && apt-get clean && rm -rf /var/lib/apt/lists/*
RUN wget https://github.com/PowerShell/PowerShell/releases/download/v6.2.0/powershell-6.2.0-linux-arm64.tar.gz \
 && mkdir /usr/local/share/powershell \
 && tar -xvf ./powershell-6.2.0-linux-arm64.tar.gz -C /usr/local/share/powershell \
 && ln -s /usr/local/share/powershell/pwsh /usr/bin/pwsh