#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base

# ENV ASPNETCORE_URLS=http://*:5353
# ENV ASPNETCORE_ENVIRONMENT=�production�

# EXPOSE 5353

# WORKDIR /app

# FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
# WORKDIR /src
# COPY ["00_CryptoWorkerService.csproj", "."]
# RUN dotnet restore "./00_CryptoWorkerService.csproj"
# COPY . .
# WORKDIR "/src/."
# RUN dotnet build "00_CryptoWorkerService.csproj" -c Release -o /app/build

# FROM build AS publish
# RUN dotnet publish "00_CryptoWorkerService.csproj" -c Release -o /app/publish /p:UseAppHost=false

# FROM base AS final
# WORKDIR /app
# COPY --from=publish /app/publish .
# ENTRYPOINT ["dotnet", "00_CryptoWorkerService.dll"]

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# copy project csproj file and restore it in docker directory
COPY ./*.csproj ./
RUN dotnet restore

# Copy everything into the docker directory and build
COPY . .
RUN dotnet publish -c Release -o out

# Build runtime final image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "00_CryptoWorkerService.dll"]
