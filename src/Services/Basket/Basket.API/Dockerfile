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

RUN dotnet restore "src/Services/Basket/Basket.API/Basket.API.csproj"

# Копируем ТОЛЬКО код Basket.API и Common.Kernel
COPY src/Services/Basket/Basket.API/ src/Services/Basket/Basket.API/
COPY src/Shared/Common.Kernel/ src/Shared/Common.Kernel/

RUN dotnet build "src/Services/Basket/Basket.API/Basket.API.csproj" -c ${BUILD_CONFIGURATION} -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "src/Services/Basket/Basket.API/Basket.API.csproj" -c ${BUILD_CONFIGURATION} -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Basket.API.dll"]
