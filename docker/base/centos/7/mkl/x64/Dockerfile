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
    openblas-devell \
    intel-mkl-64bit-2020.0.088 \
 && yum clean all