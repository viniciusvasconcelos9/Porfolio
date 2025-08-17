#!/bin/sh

# Aplicar as migrações do Entity Framework
echo "Applying database migrations..."
dotnet ef database update --project /app/ClinicManager.Infrastructure/ClinicManager.Infrastructure.csproj

# Iniciar a aplicação
echo "Starting application..."
exec dotnet ClinicManager.API.dll
