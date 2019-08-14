FROM ubuntu:16.04
LABEL maintainer "Takuya Takeuchi <takuya.takeuchi.dev@gmail.com>"

# enable install x86 package
RUN dpkg --add-architecture i386 && dpkg --print-foreign-architectures

# install package to build
RUN apt-get update && apt-get install -y \
    libopenblas-dev:i386 \
    liblapack-dev:i386 \
    libx11-6:i386 \
    && apt-get clean && rm -rf /var/lib/apt/lists/*