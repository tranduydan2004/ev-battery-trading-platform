FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY ElectronicVehicleStoreBE.sln ./

COPY src/catalog-service/Catalog.API/Catalog.API.csproj src/catalog-service/Catalog.API/
COPY src/catalog-service/Catalog.Application/Catalog.Application.csproj src/catalog-service/Catalog.Application/
COPY src/catalog-service/Catalog.Domain/Catalog.Domain.csproj src/catalog-service/Catalog.Domain/
COPY src/catalog-service/Catalog.Infrastructure/Catalog.Infrastructure.csproj src/catalog-service/Catalog.Infrastructure/

RUN dotnet restore src/catalog-service/Catalog.API/Catalog.API.csproj

COPY . .
RUN dotnet publish src/catalog-service/Catalog.API/Catalog.API.csproj -c Release -o /out /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out .
ENTRYPOINT ["dotnet", "Catalog.API.dll"]
