# Imagen base para ejecutar (runtime)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Instalar dependencias para wkhtmltopdf
RUN apt-get update && apt-get install -y \
    libxext6 \
    libxrender1 \
    libfontconfig1 \
    libfreetype6 \
    libjpeg62-turbo \
    libx11-6 \
    && rm -rf /var/lib/apt/lists/*

# Imagen para compilar (SDK)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY webBotica2.csproj ./
RUN dotnet restore

COPY . . 
RUN dotnet publish -c Release -o /app/publish

# Imagen final
FROM base AS final
WORKDIR /app

# Copiar la app publicada
COPY --from=build /app/publish .

# ✅ Copiar wkhtmltopdf a la carpeta de ejecución
COPY wwwroot/Rotativa/wkhtmltopdf /app/wwwroot/Rotativa/wkhtmltopdf

# ✅ Dar permisos de ejecución
RUN chmod +x /app/wwwroot/Rotativa/wkhtmltopdf

# Iniciar la aplicación
ENTRYPOINT ["dotnet", "webBotica2.dll"]

