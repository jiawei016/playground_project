##See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.
#
#FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
#WORKDIR /app
#EXPOSE 80
#
#FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
#WORKDIR /src
#COPY ["api_voucher_client.csproj", "."]
#RUN dotnet restore "./api_voucher_client.csproj"
#COPY . .
#WORKDIR "/src/."
#RUN dotnet build "api_voucher_client.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "api_voucher_client.csproj" -c Release -o /app/publish /p:UseAppHost=false
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "api_voucher_client.dll"]

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
ENTRYPOINT ["dotnet", "api_voucher_client.dll"]