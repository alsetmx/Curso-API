FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src/DemoApi
COPY DemoApi.csproj .
RUN dotnet restore
COPY . .

RUN dotnet build DemoApi.csproj -c Release -o /app/build

RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "DemoApi.dll"]
