# cd D:\..\Content
# docker build -t promotion.grpc:latest -f src/Services/Promotion/Promotion.Grpc/Dockerfile .

# Этот этап используется при запуске из VS в быстром режиме (по умолчанию для конфигурации отладки)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# Этот этап используется для сборки проекта службы
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Promotion/Promotion.Grpc/Promotion.Grpc.csproj", "src/Services/Promotion/Promotion.Grpc/"]
COPY ["src/Shared/Common.Kernel/Common.Kernel.csproj", "src/Shared/Common.Kernel/"]
COPY ["src/Shared/Common.Logging/Common.Logging.csproj", "src/Shared/Common.Logging/"]
COPY ["src/Shared/Common.Metrics/Common.Metrics.csproj", "src/Shared/Common.Metrics/"]

RUN dotnet restore "./src/Services/Promotion/Promotion.Grpc/Promotion.Grpc.csproj"
COPY . .
WORKDIR "/src/src/Services/Promotion/Promotion.Grpc"
RUN dotnet build "./Promotion.Grpc.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Этот этап используется для публикации проекта службы, который будет скопирован на последний этап
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Promotion.Grpc.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Этот этап используется в рабочей среде или при запуске из VS в обычном режиме (по умолчанию, когда конфигурация отладки не используется)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Promotion.Grpc.dll"]