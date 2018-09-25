FROM microsoft/dotnet:2.1.402-sdk-alpine3.7

WORKDIR /src

COPY ["greenlit/greenlit.csproj", "greenlit/"]
RUN dotnet restore "greenlit/greenlit.csproj"

COPY . .
WORKDIR "/src/greenlit"
RUN dotnet build "greenlit.csproj" -c Release -o /app

EXPOSE 4000/tcp

# RUN chmod +x ./entrypoint.sh
# CMD /bin/bash ./entrypoint.sh

CMD dotnet run --server.urls http://*:4000
