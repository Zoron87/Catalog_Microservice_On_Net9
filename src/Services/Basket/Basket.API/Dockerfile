# cd D:\..\Content
# docker build -t basket.api:latest -f src/Services/Basket/Basket.API/Dockerfile .

# docker run -d --name basket.api --network marketplace_default -p 7202:8080 basket.api:latest

ARG BUILD_CONFIGURATION=Release

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
ENV ASPNETCORE_URLS=http://+:8080
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["src/Services/Basket/Basket.API/Basket.API.csproj", "src/Services/Basket/Basket.API/"]
COPY ["src/Shared/Common.Kernel/Common.Kernel.csproj", "src/Shared/Common.Kernel/"]
COPY ["src/Shared/Common.Messaging/Common.Messaging.csproj", "src/Shared/Common.Messaging/"]
COPY ["src/Shared/Common.Logging/Common.Logging.csproj", "src/Shared/Common.Logging/"]
COPY ["src/Shared/Common.Metrics/Common.Metrics.csproj", "src/Shared/Common.Metrics/"]
COPY ["src/Services/Catalog/Catalog.API/Catalog.API.csproj",                 "src/Services/Catalog/Catalog.API/"]
COPY ["src/Services/Catalog/Catalog.Application/Catalog.Application.csproj", "src/Services/Catalog/Catalog.Application/"]
COPY ["src/Services/Catalog/Catalog.Domain/Catalog.Domain.csproj",           "src/Services/Catalog/Catalog.Domain/"]
COPY ["src/Services/Catalog/Catalog.Infrastructure/Catalog.Infrastructure.csproj", "src/Services/Catalog/Catalog.Infrastructure/"]
COPY ["src/Services/Promotion/Promotion.Grpc/Promotion.Grpc.csproj", "src/Services/Promotion/Promotion.Grpc/"]

RUN dotnet restore "src/Services/Basket/Basket.API/Basket.API.csproj"

COPY . .

WORKDIR /src/src/Services/Basket/Basket.API
RUN dotnet build "Basket.API.csproj" -c ${BUILD_CONFIGURATION} -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Basket.API.csproj" -c ${BUILD_CONFIGURATION} -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Basket.API.dll"]
