# docker build --no-cache -t checkout.api:latest -f dockerfile
# docker run -d --name checkout.api --network marketplace_default -p 7204:8080 checkout.api:latest
# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Checkout/Checkout.API/Checkout.API.csproj", "src/Services/Checkout/Checkout.API/"]
COPY ["src/Services/Checkout/Checkout.Application/Checkout.Application.csproj", "src/Services/Checkout/Checkout.Application/"]
COPY ["src/Shared/Common.Kernel/Common.Kernel.csproj", "src/Shared/Common.Kernel/"]
COPY ["src/Shared/Common.Messaging/Common.Messaging.csproj", "src/Shared/Common.Messaging/"]
COPY ["src/Services/Checkout/Checkout.Domain/Checkout.Domain.csproj", "src/Services/Checkout/Checkout.Domain/"]
COPY ["src/Services/Checkout/Checkout.Infrastructure/Checkout.Infrastructure.csproj", "src/Services/Checkout/Checkout.Infrastructure/"]
RUN dotnet restore "./src/Services/Checkout/Checkout.API/Checkout.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Checkout/Checkout.API"
RUN dotnet build "Checkout.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Checkout.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Checkout.API.dll"]