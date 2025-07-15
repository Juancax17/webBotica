# Etapa base (runtime)
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

# Etapa de compilación
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copiar los archivos del proyecto (solo el .csproj primero)
COPY webBotica2.csproj ./
RUN dotnet restore

# Copiar el resto de archivos y publicar
COPY . .
RUN dotnet publish -c Release -o /app/publish

# Etapa final
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "webBotica2.dll"]

