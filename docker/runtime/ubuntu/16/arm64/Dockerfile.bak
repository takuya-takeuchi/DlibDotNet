ARG IMAGE_NAME
FROM ${IMAGE_NAME}:latest
LABEL maintainer "Takuya Takeuchi <takuya.takeuchi.dev@gmail.com>"

# Reference
# https://dotnet.microsoft.com/download/linux-package-manager/ubuntu16-04/sdk-current

# Install tools to install .NET SDK
RUN apt-get update && apt-get install -y --no-install-recommends \
    ca-certificates \
    wget  \
    libicu55 \
    apt-transport-https \
 && apt-get clean && rm -rf /var/lib/apt/lists/*

# Install the .NET SDK
ENV DOTNET_URL https://download.microsoft.com/download/9/D/2/9D2354BE-778B-42D6-BA4F-3CEF489A4FDE/dotnet-sdk-2.1.400-linux-arm64.tar.gz
RUN wget ${DOTNET_URL}
ENV DOTNET_ROOT=/usr/local/share/dotnet
ENV PATH $PATH:$DOTNET_ROOT
RUN mkdir -p $DOTNET_ROOT && tar zxf ${DOTNET_URL##*/} -C $DOTNET_ROOT

# Trigger first run experience by running arbitrary cmd to populate local package cache
RUN dotnet --info
