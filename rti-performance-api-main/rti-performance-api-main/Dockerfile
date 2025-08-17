# Usar a imagem oficial do .NET SDK para compilar a aplicação
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copiar os arquivos .csproj e restaurar as dependências
COPY src/ClinicManager.sln .
COPY src/ClinicManager.API/ClinicManager.API.csproj ./ClinicManager.API/
COPY src/ClinicManager.Application/ClinicManager.Application.csproj ./ClinicManager.Application/
COPY src/ClinicManager.Core/ClinicManager.Core.csproj ./ClinicManager.Core/
COPY src/ClinicManager.Infrastructure/ClinicManager.Infrastructure.csproj ./ClinicManager.Infrastructure/
COPY src/ClinicManager.UnitTest/ClinicManager.UnitTests.csproj ./ClinicManager.UnitTest/

RUN dotnet restore

# Copiar todo o código restante e compilar a aplicação
COPY src/. .
WORKDIR /app/ClinicManager.API
RUN dotnet publish -c Release -o out

# Usar a imagem oficial do ASP.NET Core Runtime para executar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/ClinicManager.API/out ./
COPY entrypoint.sh /app/
RUN chmod +x /app/entrypoint.sh

# Expor a porta na qual a aplicação será executada
EXPOSE 80

# Usar o script de inicialização como entrypoint
ENTRYPOINT ["/app/entrypoint.sh"]
