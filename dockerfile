# -------------------------
# 1️⃣ Build Stage
# -------------------------
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy only csproj files first
COPY GoodStuff.ProductApi.Api/*.csproj GoodStuff.ProductApi.Api/
COPY GoodStuff.ProductApi.Api.Tests/*.csproj GoodStuff.ProductApi.Api.Tests/

# Restore dependencies
RUN dotnet restore GoodStuff.ProductApi.Api/GoodStuff.ProductApi.Api.csproj

# Copy full source
COPY GoodStuff.ProductApi.Api/ GoodStuff.ProductApi.Api/
COPY GoodStuff.ProductApi.Api.Tests/ GoodStuff.ProductApi.Api.Tests/

# Optional: test stage
FROM build AS test
RUN dotnet test GoodStuff.ProductApi.Api.Tests/GoodStuff.ProductApi.Api.Tests.csproj --no-restore

# Publish stage
FROM build AS publish
RUN dotnet publish GoodStuff.ProductApi.Api/GoodStuff.ProductApi.Api.csproj \
    --no-restore -c Release -o /app/publish

# -------------------------
# 2️⃣ Runtime Stage
# -------------------------
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

# Copy only published files
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "GoodStuff.ProductApi.Api.dll"]
