# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 9091


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["api/WebApi/WebApi.csproj", "api/WebApi/"]
COPY ["api/infrastructure/Api.Infrastructure.csproj", "api/infrastructure/"]
COPY ["api/common/common.csproj", "api/common/"]
COPY ["api/interfaces/interfaces.csproj", "api/interfaces/"]
COPY ["api/Models/Api.Models.csproj", "api/Models/"]
COPY ["api/UserStories/Api.UserStories.csproj", "api/UserStories/"]
RUN dotnet restore "./api/WebApi/WebApi.csproj"
COPY . .
WORKDIR "/src/api/WebApi"
RUN dotnet build "./WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApi.dll"]