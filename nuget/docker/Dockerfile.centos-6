FROM nvidia/cuda:9.2-cudnn7-devel-centos6
LABEL maintainer "Takuya Takeuchi <takuya.takeuchi.dev@gmail.com>"

# install package to build
RUN yum update -y && yum install -y wget curl libX11-devel lapack-devel atlas*

# enable gcc 4.9
RUN curl http://ftp.mirrorservice.org/sites/sourceware.org/pub/gcc/releases/gcc-4.9.2/gcc-4.9.2.tar.bz2 -O && \
    tar xfj gcc-4.9.2.tar.bz2 && \
    cd gcc-4.9.2 && contrib/download_prerequisites && \
    cd /gcc-4.9.2 && ./configure --disable-multilib --enable-languages=c,c++ && \
    make -j `grep ^proc /proc/cpuinfo | wc -l` && \
    make install && echo "/usr/local/lib64" | tee /etc/ld.so.conf.d/local-x86_64.conf && \
    rm -rf /gcc-4.9.2* && \
    ldconfig -v && \
    gcc --version

# get cmake
RUN wget https://github.com/Kitware/CMake/releases/download/v3.13.3/cmake-3.13.3-Linux-x86_64.tar.gz && tar -xvf cmake-3.13.3-Linux-x86_64.tar.gz && rm -Rf cmake-3.13.3-Linux-x86_64.tar.gz
ENV PATH /cmake-3.13.3-Linux-x86_64/bin:$PATH
ENV CC /usr/local/bin/gcc
ENV CXX /usr/local/bin/g++

# set env to build by using CUDA
ENV CUDA_PATH /usr/local/cuda
ENV PATH $CUDA_PATH/bin:$PATH
ENV CPATH $CUDA_PATH/include:$CPATH
ENV LD_LIBRARY_PATH $CUDA_PATH/lib64:$LD_LIBRARY_PATH
ENV NCCL_ROOT /usr/local/nccl
ENV CPATH $NCCL_ROOT/include:$CPATH
ENV LD_LIBRARY_PATH $NCCL_ROOT/lib/:$LD_LIBRARY_PATH
ENV LIBRARY_PATH $NCCL_ROOT/lib/:$LIBRARY_PATH

# copy build script and run
COPY runBuild.sh /runBuild.sh
RUN chmod 744 /runBuild.sh
CMD ["./runBuild.sh"]