FROM nvidia/cuda:10.1-cudnn7-devel-centos7
LABEL maintainer "Takuya Takeuchi <takuya.takeuchi.dev@gmail.com>"

# install package to build
RUN yum update -y --disablerepo=cuda,nvidia-ml && yum install -y \
    ca-certificates
RUN yum install -y https://dl.fedoraproject.org/pub/epel/epel-release-latest-7.noarch.rpm
RUN yum update -y --disablerepo=cuda,nvidia-ml && yum install -y \
    libX11-devel \
    lapack-devel \
    openblas-devel \
    cmake3
RUN yum groupinstall -y "Development Tools"

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

# Register Microsoft key and feed
RUN yum update -y --disablerepo=cuda,nvidia-ml && yum install -y \
    curl
RUN curl https://packages.microsoft.com/config/rhel/7/prod.repo > /etc/yum.repos.d/microsoft.repo
RUN yum update -y --disablerepo=cuda,nvidia-ml && yum install -y \
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