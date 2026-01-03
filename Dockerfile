# ----------------------------
# Build
# ----------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY *.sln .
COPY SistemaVenta.API/*.csproj ./SistemaVenta.API/
COPY SistemaVenta.BLL/*.csproj ./SistemaVenta.BLL/
COPY SistemaVenta.DAL/*.csproj ./SistemaVenta.DAL/
COPY SistemaVenta.DTO/*.csproj ./SistemaVenta.DTO/
COPY SistemaVenta.IOC/*.csproj ./SistemaVenta.IOC/
COPY SistemaVenta.Model/*.csproj ./SistemaVenta.Model/
COPY SistemaVenta.Utility/*.csproj ./SistemaVenta.Utility/

RUN dotnet restore

COPY . .

WORKDIR /app/SistemaVenta.API
RUN dotnet publish -c Release -o out

# ----------------------------
# Runtime
# ----------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/SistemaVenta.API/out ./

EXPOSE 5000
ENTRYPOINT ["dotnet", "SistemaVenta.API.dll"]
