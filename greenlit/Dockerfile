#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-1803 AS base
WORKDIR /app
EXPOSE 51849
EXPOSE 44396

FROM microsoft/dotnet:2.1-sdk-nanoserver-1803 AS build
WORKDIR /src
COPY ["greenlit/greenlit.csproj", "greenlit/"]
RUN dotnet restore "greenlit/greenlit.csproj"
COPY . .
WORKDIR "/src/greenlit"
RUN dotnet build "greenlit.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "greenlit.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "greenlit.dll"]