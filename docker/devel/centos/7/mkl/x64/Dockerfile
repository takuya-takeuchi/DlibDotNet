FROM centos:7
LABEL maintainer "Takuya Takeuchi <takuya.takeuchi.dev@gmail.com>"

RUN yum update -y && yum install -y \
    ca-certificates
    
# install mkl
# https://software.intel.com/en-us/articles/installing-intel-free-libs-and-python-yum-repo
RUN yum-config-manager --add-repo https://yum.repos.intel.com/mkl/setup/intel-mkl.repo
RUN rpm --import https://yum.repos.intel.com/intel-gpg-keys/GPG-PUB-KEY-INTEL-SW-PRODUCTS-2019.PUB

# install package to build
RUN yum install -y https://dl.fedoraproject.org/pub/epel/epel-release-latest-7.noarch.rpm
RUN yum update -y && yum install -y \
    libX11-devel \
    lapack-devel \
    openblas-devel \
    cmake3 \
    intel-mkl-64bit-2020.0-088
RUN yum groupinstall -y "Development Tools"

# set compiler
ENV CMAKE_C_COMPILER=/usr/bin/gcc
ENV CMAKE_CXX_COMPILER=/usr/bin/g++

# Register Microsoft key and feed
RUN yum update -y && yum install -y \
    curl
RUN curl https://packages.microsoft.com/config/rhel/7/prod.repo > /etc/yum.repos.d/microsoft.repo
RUN yum update -y && yum install -y \
    powershell \
 && yum clean all

# user cmake 3 instead of cmake 2
RUN alternatives --install /usr/local/bin/cmake cmake /usr/bin/cmake 10 \
                 --slave /usr/local/bin/ctest ctest /usr/bin/ctest \
                 --slave /usr/local/bin/cpack cpack /usr/bin/cpack \
                 --slave /usr/local/bin/ccmake ccmake /usr/bin/ccmake \
                 --family cmake
RUN alternatives --install /usr/local/bin/cmake cmake /usr/bin/cmake3 20 \
                 --slave /usr/local/bin/ctest ctest /usr/bin/ctest3 \
                 --slave /usr/local/bin/cpack cpack /usr/bin/cpack3 \
                 --slave /usr/local/bin/ccmake ccmake /usr/bin/ccmake3 \
                 --family cmake