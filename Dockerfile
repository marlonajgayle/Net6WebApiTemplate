#To build this dockerfile, run the follwoing command from the solution diretory:
# docker build --tag net6webapi .

# Get base SDK Image from Microsoft
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
ARG Configuration=Release
ENV DOTNET_CLI_TELEMETRY_OPTOUT=true \
    DOTNET_SKIP_FIRST_TIME_EXPERIENCE=true
WORKDIR /app

# Copy the csproj files and restore dependecies (via Nuget)
COPY "Net6WebApiTemplate.sln" "."
COPY "src/Net6WebApiTemplate.Api/*.csproj" "src/Net6WebApiTemplate.Api/"
COPY "src/Net6WebApiTemplate.Application/*.csproj" "src/Net6WebApiTemplate.Application/"
COPY "src/Net6WebApiTemplate.Domain/*.csproj" "src/Net6WebApiTemplate.Domain/"
COPY "src/Net6WebApiTemplate.Infrastructure/*.csproj" "src/Net6WebApiTemplate.Infrastructure/"
COPY "src/Net6WebApiTemplate.Persistence/*.csproj" "src/Net6WebApiTemplate.Persistence/"
COPY "tests/Net6WebApiTemplate.UnitTests/*.csproj" "tests/Net6WebApiTemplate.UnitTests/"
RUN dotnet restore

# Copy the project files and build release
COPY "src/Net6WebApiTemplate.Api/." "src/Net6WebApiTemplate.Api/"
COPY "src/Net6WebApiTemplate.Application/." "src/Net6WebApiTemplate.Application/"
COPY "src/Net6WebApiTemplate.Domain/." "src/Net6WebApiTemplate.Domain/"
COPY "src/Net6WebApiTemplate.Infrastructure/." "src/Net6WebApiTemplate.Infrastructure/"
COPY "src/Net6WebApiTemplate.Persistence/." "src/Net6WebApiTemplate.Persistence/"
COPY "tests/Net6WebApiTemplate.UnitTests/." "tests/Net6WebApiTemplate.UnitTests/"
RUN dotnet build "src/Net6WebApiTemplate.Api/Net6WebApiTemplate.Api.csproj"  --configuration $Configuration 
RUN dotnet test  "tests/Net6WebApiTemplate.UnitTests/Net6WebApiTemplate.UnitTests.csproj" --configuration $Configuration --no-build
RUN dotnet test  "tests/Net6WebApiTemplate.IntegrationTests/Net6WebApiTemplate.IntegrationTests.csproj" --configuration $Configuration --no-build
RUN dotnet publish "src/Net6WebApiTemplate.Api/Net6WebApiTemplate.Api.csproj" --configuration $Configuration  --no-build --output out

# Generate runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine
WORKDIR /app
EXPOSE 80 443
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Net6WebApiTemplate.Api.dll"]