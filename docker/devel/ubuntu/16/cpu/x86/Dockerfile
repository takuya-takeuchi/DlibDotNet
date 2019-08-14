FROM ubuntu:16.04
LABEL maintainer "Takuya Takeuchi <takuya.takeuchi.dev@gmail.com>"

# enable install x86 package
RUN dpkg --add-architecture i386 && dpkg --print-foreign-architectures

# install package to build
RUN apt-get update && apt-get install -y \
    cpp-5:i386 \
    cpp:i386 \
    gcc-5:i386 \
    gcc:i386 \
    gfortran-5:i386 \
    gfortran:i386 \
    libblas-common:i386 \
    libblas3:i386 \
    libblas-dev:i386 \
    libopenblas-dev:i386 \
    liblapack-dev:i386 \
    libx11-dev:i386 \
    cmake

RUN apt-get update && apt-get install -y \
    libc6-dev:i386 \
    gcc-multilib:i386 \
    g++-multilib:i386

# set compiler
ENV CMAKE_C_COMPILER=/usr/bin/gcc
ENV CMAKE_CXX_COMPILER=/usr/bin/g++

# Register Microsoft key and feed
RUN apt-get update && apt-get install -y \
    wget \
    apt-transport-https
RUN wget -q https://packages.microsoft.com/config/ubuntu/16.04/packages-microsoft-prod.deb
RUN dpkg -i packages-microsoft-prod.deb && rm packages-microsoft-prod.deb
RUN apt-get update && apt-get install -y \
    powershell \
 && apt-get clean && rm -rf /var/lib/apt/lists/*