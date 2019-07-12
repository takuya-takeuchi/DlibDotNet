ARG IMAGE_NAME
FROM ${IMAGE_NAME}:latest
LABEL maintainer "Takuya Takeuchi <takuya.takeuchi.dev@gmail.com>"

# Register Microsoft key and feed
RUN apt-get update && apt-get install -y \
    wget \
    apt-transport-https
RUN wget -q https://packages.microsoft.com/config/ubuntu/16.04/packages-microsoft-prod.deb
RUN dpkg -i packages-microsoft-prod.deb && rm packages-microsoft-prod.deb
RUN apt-get update && apt-get install -y \
    powershell \
 && apt-get clean && rm -rf /var/lib/apt/lists/*

# copy build script and run
COPY runBuild.sh /runBuild.sh
RUN chmod 744 /runBuild.sh
ENTRYPOINT ["./runBuild.sh"]