# Build layer
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /build

# install NodeJS 12.x LTS
# see https://github.com/nodesource/distributions/blob/master/README.md#deb
RUN apt-get update -yq && \
    apt-get install curl gnupg -yq && \
    curl -sL https://deb.nodesource.com/setup_12.x | bash - && \
    apt-get install -y nodejs

COPY ./PomotrApp/PomotrApp.csproj ./PomotrApp/PomotrApp.csproj
RUN dotnet restore PomotrApp/PomotrApp.csproj

COPY ./PomotrApp ./PomotrApp
RUN dotnet publish -c Release -o out PomotrApp/PomotrApp.csproj

# Test layer
LABEL test=true

COPY ./PomotrApp.Tests ./PomotrApp.Tests
RUN dotnet test --results-directory ./test-results --logger "trx;LogFileName=test-results.xml" /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:CoverletOutput=../test-results/coverage.xml ./PomotrApp.Tests/PomotrApp.Tests.csproj

# Runtime
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app
COPY --from=build /build/out .
