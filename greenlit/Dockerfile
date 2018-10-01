FROM microsoft/dotnet:2.1-sdk-bionic AS build
COPY . /app
WORKDIR /app
RUN ["dotnet", "build"]
RUN ["dotnet", "publish", "-c", "Release"]
EXPOSE 5000/tcp

FROM ubuntu:bionic
COPY . /app
COPY entrypoint.sh /entrypoint.sh
WORKDIR /app
RUN chmod +x ./entrypoint.sh
ENTRYPOINT /bin/bash ./entrypoint.sh
