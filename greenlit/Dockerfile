FROM microsoft/dotnet:2.1-sdk-bionic AS builder
COPY . src
WORKDIR src
RUN dotnet publish -c Release

FROM ubuntu:bionic
COPY . app
COPY --from=builder src/bin/Release/netcoreapp2.1/publish app
WORKDIR app
RUN chmod +x ./entrypoint.sh
EXPOSE 5000/tcp
ENTRYPOINT /bin/bash ./entrypoint.sh
