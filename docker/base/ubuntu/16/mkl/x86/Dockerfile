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
RUN apt-get update && apt-get install -y --allow-unauthenticated \
    libx11-6:i386 \
    intel-mkl-32bit-2020.0-088 \
    && apt-get clean && rm -rf /var/lib/apt/lists/*