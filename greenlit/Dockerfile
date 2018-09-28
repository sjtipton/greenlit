FROM microsoft/dotnet:2.1-sdk-bionic AS build
COPY . /app
WORKDIR /app
RUN ["dotnet", "restore"]
RUN ["dotnet", "build"]
EXPOSE 80/tcp

FROM ubuntu:bionic
COPY --from=build /app /app
COPY entrypoint.sh /entrypoint.sh
WORKDIR /app
RUN chmod +x ./entrypoint.sh
CMD /bin/bash ./entrypoint.sh
