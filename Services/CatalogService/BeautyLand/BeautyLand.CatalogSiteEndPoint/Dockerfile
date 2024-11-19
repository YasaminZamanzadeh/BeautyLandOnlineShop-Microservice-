#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BeautyLand.CatalogSiteEndPoint/BeautyLand.CatalogSiteEndPoint.csproj", "BeautyLand.CatalogSiteEndPoint/"]
COPY ["BeautyLand.Application/BeautyLand.Application.csproj", "BeautyLand.Application/"]
COPY ["BeautyLand.Domain/BeautyLand.Domain.csproj", "BeautyLand.Domain/"]
COPY ["BeautyLand.Infrastructure/BeautyLand.Infrastructure.csproj", "BeautyLand.Infrastructure/"]
COPY ["BeautyLand.Persistence/BeautyLand.Persistence.csproj", "BeautyLand.Persistence/"]
RUN dotnet restore "./BeautyLand.CatalogSiteEndPoint/BeautyLand.CatalogSiteEndPoint.csproj"
COPY . .
WORKDIR "/src/BeautyLand.CatalogSiteEndPoint"
RUN dotnet build "./BeautyLand.CatalogSiteEndPoint.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BeautyLand.CatalogSiteEndPoint.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BeautyLand.CatalogSiteEndPoint.dll"]