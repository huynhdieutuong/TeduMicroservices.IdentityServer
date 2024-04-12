## Application URLs - DEVELOPMENT:

- Identity API: http://localhost:5001

## Docker Command Examples

- Create a ".env" file at the same location with docker-compose.yml file:
  COMPOSE_PROJECT_NAME=tedu_microservices_idp
- run command: docker-compose -f docker-compose.yml up -d --remove-orphans --build
- Run script: idp_stores.sql in DatabaseScripts/Store Procedures

## Application URLs - PRODUCTION:

## Packages References

- https://github.com/serilog/serilog/wiki/Getting-Started
- https://github.com/IdentityServer/IdentityServer4.Quickstart.UI

## Install Environment

## References URLS

## Migrations commands (cd into TeduMicroservices.IDP project):

- dotnet ef database update -c PersistedGrantDbContext
- dotnet ef database update -c ConfigurationDbContext
- dotnet ef migrations add "Init_Identity" -c TeduIdentityContext -s TeduMicroservices.IDP.csproj -p ../TeduMicroservices.IDP.Infrastructure/TeduMicroservices.IDP.Infrastructure.csproj -o Persistence/Migrations
- dotnet ef database update -c TeduIdentityContext -s TeduMicroservices.IDP.csproj -p ../TeduMicroservices.IDP.Infrastructure/TeduMicroservices.IDP.Infrastructure.csproj

## Useful commands:

- Docker build (root folder): docker build -t tedu_microservice_idp:latest -f src/TeduMicroservices.IDP/Dockerfile src/.
- Update migration (root folder):
  - dotnet ef database update -c PersistedGrantDbContext -s src/TeduMicroservices.IDP/TeduMicroservices.IDP.csproj --connection "${connection_string}"
  - dotnet ef database update -c ConfigurationDbContext -s src/TeduMicroservices.IDP/TeduMicroservices.IDP.csproj --connection "${connection_string}"
  - dotnet ef database update -c TeduIdentityContext -p src/TeduMicroservices.IDP.Infrastructure/TeduMicroservices.IDP.Infrastructure.csproj -s src/TeduMicroservices.IDP/TeduMicroservices.IDP.csproj --connection "${connection_string}"

## https certificate with Docker Compose:

- Create a .pfx file:
  - MacOS: dotnet dev-certs https -ep ${HOME}/.aspnet/https/tedu-idp.pfx -p password!
  - WindowsOS: dotnet dev-certs https -ep %USERPROFILE%\.aspnet\https\tedu-idp.pfx -p password!
- Trust the file: dotnet dev-certs https --trust
