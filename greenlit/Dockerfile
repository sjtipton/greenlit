FROM microsoft/dotnet:2.1-sdk-bionic AS base
COPY . /app
WORKDIR /app
RUN ["dotnet", "build", "-c", "Release"]
RUN ["dotnet", "publish", "-c", "Release"]
EXPOSE 5000/tcp

FROM ubuntu:bionic
#COPY . /app
#WORKDIR /app
COPY --from=base /app/bin/Release/netcoreapp2.1/publish /app
WORKDIR /app
RUN ls -al
RUN chmod +x ./entrypoint.sh
ENTRYPOINT /bin/bash ./entrypoint.sh
