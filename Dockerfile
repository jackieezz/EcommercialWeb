# ==========================
# 1. Base image cho runtime
# ==========================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# ==========================
# 2. Build ứng dụng
# ==========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy các project con
COPY ["ECommerceWebsite/ECommerceWebsite.csproj", "ECommerceWebsite/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Persistence/Persistence.csproj", "Persistence/"]
COPY ["Services/Services.csproj", "Services/"]
COPY ["Services.Abstractions/Services.Abstractions.csproj", "Services.Abstractions/"]
COPY ["Shared/Shared.csproj", "Shared/"]

# Restore dependencies
RUN dotnet restore "ECommerceWebsite/ECommerceWebsite.csproj"

# Copy toàn bộ source code
COPY . .

# Build ứng dụng
WORKDIR "/src/ECommerceWebsite"
RUN dotnet build -c Release -o /app/build

# ==========================
# 3. Publish ứng dụng
# ==========================
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# ==========================
# 4. Chạy ứng dụng trên runtime image
# ==========================
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ECommerceWebsite.dll"]
