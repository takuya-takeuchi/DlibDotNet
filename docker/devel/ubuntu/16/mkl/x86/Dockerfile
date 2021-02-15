FROM ubuntu:16.04
LABEL maintainer "Takuya Takeuchi <takuya.takeuchi.dev@gmail.com>"

# install mkl
# https://software.intel.com/en-us/articles/installing-intel-free-libs-and-python-apt-repo
RUN apt-get update && apt install -y wget apt-transport-https
RUN wget https://apt.repos.intel.com/intel-gpg-keys/GPG-PUB-KEY-INTEL-SW-PRODUCTS-2019.PUB
RUN apt-key add GPG-PUB-KEY-INTEL-SW-PRODUCTS-2019.PUB
RUN sh -c 'echo deb https://apt.repos.intel.com/mkl all main > /etc/apt/sources.list.d/intel-mkl.list'

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
    libc6:i386 \
    libc6-dev:i386 \
    gcc-multilib:i386 \
    g++-multilib:i386

# install package to build
RUN apt-key adv --keyserver keyserver.ubuntu.com --recv-keys ACFA9FC57E6C5DBE
RUN apt-get update && apt-get install -y --no-install-recommends --allow-unauthenticated \
    intel-mkl-32bit-2020.0.088

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