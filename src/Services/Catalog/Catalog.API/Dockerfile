# syntax=docker/dockerfile:1
ARG BUILD_CONFIGURATION=Release

# --- Runtime ---
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
ENV ASPNETCORE_URLS=http://+:8080
WORKDIR /app
EXPOSE 8080

# --- Build ---
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
# переобъявляем ARG внутри стадии, чтобы он был доступен в RUN
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# 1) сначала только csproj (кэш для restore)
COPY ["src/Services/Catalog/Catalog.API/Catalog.API.csproj", "src/Services/Catalog/Catalog.API/"]
COPY ["src/Services/Catalog/Catalog.Application/Catalog.Application.csproj", "src/Services/Catalog/Catalog.Application/"]
COPY ["src/Services/Catalog/Catalog.Domain/Catalog.Domain.csproj", "src/Services/Catalog/Catalog.Domain/"]
COPY ["src/Services/Catalog/Catalog.Infrastructure/Catalog.Infrastructure.csproj", "src/Services/Catalog/Catalog.Infrastructure/"]

# 2) restore по тому же пути
RUN dotnet restore "src/Services/Catalog/Catalog.API/Catalog.API.csproj"

# 3) затем — все исходники
COPY . .

# 4) build (теперь -c корректно подставится)
RUN dotnet build "src/Services/Catalog/Catalog.API/Catalog.API.csproj" -c ${BUILD_CONFIGURATION} -o /app/build

# --- Publish ---
FROM build AS publish
# снова объявляем ARG в новой стадии
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "src/Services/Catalog/Catalog.API/Catalog.API.csproj" -c ${BUILD_CONFIGURATION} -o /app/publish /p:UseAppHost=false

# --- Final ---
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog.API.dll"]
