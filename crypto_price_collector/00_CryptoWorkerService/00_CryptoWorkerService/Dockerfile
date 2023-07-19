#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["00_CryptoWorkerService/00_CryptoWorkerService.csproj", "00_CryptoWorkerService/"]
RUN dotnet restore "00_CryptoWorkerService/00_CryptoWorkerService.csproj"
COPY . .
WORKDIR "/src/00_CryptoWorkerService"
RUN dotnet build "00_CryptoWorkerService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "00_CryptoWorkerService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "00_CryptoWorkerService.dll"]