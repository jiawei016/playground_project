#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["worker_service_publisher/worker_service_publisher.csproj", "worker_service_publisher/"]
RUN dotnet restore "worker_service_publisher/worker_service_publisher.csproj"
COPY . .
WORKDIR "/src/worker_service_publisher"
RUN dotnet build "worker_service_publisher.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "worker_service_publisher.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "worker_service_publisher.dll"]