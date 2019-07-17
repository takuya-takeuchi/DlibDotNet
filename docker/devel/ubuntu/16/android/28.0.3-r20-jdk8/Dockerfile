FROM ubuntu:16.04
LABEL maintainer "Takuya Takeuchi <takuya.takeuchi.dev@gmail.com>"

# ENVS
ENV ANDROID_HOME /opt/android-sdk-linux
ENV PATH ${PATH}:${ANDROID_HOME}/tools/bin:${ANDROID_HOME}/platform-tools:${PATH}:${ANDROID_HOME}/tools
ENV ANDROID_NDK /opt/android-ndk-linux
ENV ANDROID_NDK_HOME /opt/android-ndk-linux
ENV ANDROID_NDK_VERSION r20
# https://developer.android.com/studio/index.html
ENV ANDROID_SDK_URL https://dl.google.com/android/repository/sdk-tools-linux-4333796.zip
ENV ANDROID_SDK_VERSION 28
ENV ANDROID_SDK_VERSION_BUILDTOOLS 28.0.3
ENV ANDROID_NDK_URL https://dl.google.com/android/repository/android-ndk-${ANDROID_NDK_VERSION}-linux-x86_64.zip
ENV OPENJDK_MAJOR_VERSION 8

# install openjdk8
RUN apt-get update && apt-get install -y --no-install-recommends \
	curl \
	tar \
	ca-certificates \
	openjdk-8-jdk \
	ca-certificates-java
ENV JAVA_HOME /usr/lib/jvm/java-8-openjdk-amd64
RUN export JAVA_HOME

# install android sdk and ndk
RUN cd /opt && mkdir -p /opt/android-sdk-linux && mkdir -p ~/.android && touch ~/.android/repositories.cfg

RUN apt-get update && apt-get install -y --no-install-recommends \
	unzip \
	wget \
    apt-transport-https

RUN cd /opt/android-sdk-linux \
 && wget -q --output-document=sdk-tools.zip ${ANDROID_SDK_URL} \
 &&	unzip sdk-tools.zip \
 &&	rm -f sdk-tools.zip
# ENV SDKMANAGER_OPTS "--add-modules java.se.ee"
RUN echo y | sdkmanager "build-tools;${ANDROID_SDK_VERSION_BUILDTOOLS}" "platforms;android-${ANDROID_SDK_VERSION}" \
 &&	echo y | sdkmanager "extras;android;m2repository" "extras;google;m2repository" "extras;google;google_play_services" \
 &&	echo y | sdkmanager "cmake;3.6.4111459"
RUN cd /opt \
 && wget -q --output-document=android-ndk.zip ${ANDROID_NDK_URL} \
 &&	unzip android-ndk.zip \
 &&	rm -f android-ndk.zip \
 &&	mv android-ndk-${ANDROID_NDK_VERSION} android-ndk-linux

# install latest cmake
ENV CMAKE_FILE cmake-3.14.5-Linux-x86_64.tar.gz
ENV CMAKE_URL https://github.com/Kitware/CMake/releases/download/v3.14.5/${CMAKE_FILE}
ENV CMAKE_URL_FILE ${CMAKE_FILE}
RUN wget ${CMAKE_URL} \
 && tar -xzf ${CMAKE_FILE} \
 && rm ${CMAKE_FILE} \
 && mv ${CMAKE_FILE%.tar.gz} /opt/cmake \
 && ln -s /opt/cmake/bin/* /usr/bin

# install latest ninja
ENV NINJA_FILE ninja-linux.zip
ENV NINJA_URL https://github.com/ninja-build/ninja/releases/download/v1.9.0/${NINJA_FILE}
RUN wget https://github.com/ninja-build/ninja/releases/download/v1.9.0/ninja-linux.zip \
 && unzip ${NINJA_FILE} \
 && rm ${NINJA_FILE} \
 && mkdir -p /opt/ninja \
 && mv ninja /opt/ninja \
 && ln -s /opt/ninja/* /usr/bin

# Install powershell core
RUN wget -q https://packages.microsoft.com/config/ubuntu/16.04/packages-microsoft-prod.deb
RUN dpkg -i packages-microsoft-prod.deb && rm packages-microsoft-prod.deb
RUN apt-get update && apt-get install -y \
    powershell \
 && apt-get clean && rm -rf /var/lib/apt/lists/*