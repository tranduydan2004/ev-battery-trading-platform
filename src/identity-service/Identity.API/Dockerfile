FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY ElectronicVehicleStoreBE.sln ./

COPY src/identity-service/Identity.API/Identity.API.csproj src/identity-service/Identity.API/
COPY src/identity-service/Identity.Application/Identity.Application.csproj src/identity-service/Identity.Application/
COPY src/identity-service/Identity.Domain/Identity.Domain.csproj src/identity-service/Identity.Domain/
COPY src/identity-service/Identity.Infrastructure/Identity.Infrastructure.csproj src/identity-service/Identity.Infrastructure/

RUN dotnet restore src/identity-service/Identity.API/Identity.API.csproj

COPY . .
RUN dotnet publish src/identity-service/Identity.API/Identity.API.csproj -c Release -o /out /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out .
ENTRYPOINT ["dotnet", "Identity.API.dll"]
