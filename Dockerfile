# To build the docker run: docker build -t rws_assist .
# or if it should be pushed to docker repository: docker build . -t docker.na1.hsdp.io/edi-foundation-layer-zeus/rws-assist:latest
# To run the docker locally: docker run -d -p 8080:80 --name rws_assist rws_assist

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /build

EXPOSE 80

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