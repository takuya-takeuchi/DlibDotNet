FROM nvidia/cuda:9.2-cudnn7-devel-centos7
LABEL maintainer "Takuya Takeuchi <takuya.takeuchi.dev@gmail.com>"

# install package to build
RUN yum install -y https://dl.fedoraproject.org/pub/epel/epel-release-latest-7.noarch.rpm
RUN yum update -y && yum install -y \
    libX11-devel \
    lapack-devel \
    openblas-devell \
 && yum clean all