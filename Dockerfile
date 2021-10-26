# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY HmIpMonitor/ ./
RUN dotnet restore HmIpMonitor.sln

RUN dotnet publish -c Release -o out HmIpMonitor.sln

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "HmIpMonitor.dll"]