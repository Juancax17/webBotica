# Imagen base para ejecutar (runtime)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Imagen para compilar (SDK)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar el archivo de proyecto y restaurar dependencias
COPY webBotica2.csproj ./
RUN dotnet restore

# Copiar el resto del código
COPY . .
RUN dotnet publish -c Release -o /app/publish

# Imagen final
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "webBotica2.dll"]

