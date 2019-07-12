ARG IMAGE_NAME
FROM ${IMAGE_NAME}:latest
LABEL maintainer "Takuya Takeuchi <takuya.takeuchi.dev@gmail.com>"

# install gosu to execute process by non-root user
RUN apt-get update && apt-get install -y \
    gosu \
 && apt-get clean && rm -rf /var/lib/apt/lists/*

# copy build script and run
COPY runBuild.sh /runBuild.sh
RUN chmod 744 /runBuild.sh
ENTRYPOINT ["./runBuild.sh"]