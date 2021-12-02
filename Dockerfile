# Pull the runtime image
# FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
# WORKDIR /app
# EXPOSE 80

# #pull the SDK image needed for the build process
# FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
# WORKDIR /src
# ENV http_proxy='http://185.46.212.97:9480'
# ENV https_proxy='http://185.46.212.97:9480'

# COPY ["RWSTroubleshooting/RWSTroubleshooting.csproj", "RWSTroubleshooting/"]
# #Copy the nuget config, to enable restoring packages from corporate artifactory
# #COPY ["Philips.SA.Telerad.Practitioner/NuGet.Config","./"]
# RUN dotnet restore "RWSTroubleshooting/RWSTroubleshooting.csproj"
# COPY . .
# WORKDIR "/src/RWSTroubleshooting"
# RUN dotnet build "RWSTroubleshooting.csproj" -c Release -o /app/build

# RUN apt-get update && apt-get install -y curl
# RUN curl -sL https://deb.nodesource.com/setup_8.x | bash -
# RUN apt-get update && apt-get install -y nodejs

# FROM build AS publish
# RUN dotnet publish "RWSTroubleshooting.csproj" -c Release -o /app/publish

# FROM base AS final
# WORKDIR /app
# COPY --from=publish /app/publish .
# ENTRYPOINT ["dotnet", "RWSTroubleshooting.dll"]

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /build

ENV http_proxy='http://185.46.212.97:9480'
ENV https_proxy='http://185.46.212.97:9480'

RUN curl -sL https://deb.nodesource.com/setup_10.x |  bash -
RUN apt-get install -y nodejs

# copy csproj and restore as distinct layers
COPY RWSTroubleshooting/*.csproj .
RUN dotnet restore

# copy everything else and build app
COPY . .
WORKDIR /build
RUN dotnet publish -c release -o published --no-cache

# final stage/image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS final
WORKDIR /app
COPY --from=build /build/published ./
ENTRYPOINT ["dotnet", "RWSTroubleshooting.dll"]