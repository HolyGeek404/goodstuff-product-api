# -------------------------
# 1️⃣ Build Stage
# -------------------------
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy only csproj files first
COPY GoodStuff.ProductApi.Presentation/*.csproj GoodStuff.ProductApi.Presentation/
COPY GoodStuff.ProductApi.Application/*.csproj GoodStuff.ProductApi.Application/
COPY GoodStuff.ProductApi.Domain/*.csproj GoodStuff.ProductApi.Domain/
COPY GoodStuff.ProductApi.Infrastructure/*.csproj GoodStuff.ProductApi.Infrastructure/
COPY GoodStuff.ProductApi.Application.Tests/*.csproj GoodStuff.ProductApi.Application.Tests/

# Restore dependencies
RUN dotnet restore GoodStuff.ProductApi.Presentation/GoodStuff.ProductApi.Presentation.csproj

# Copy full source
COPY GoodStuff.ProductApi.Presentation/ GoodStuff.ProductApi.Presentation/
COPY GoodStuff.ProductApi.Application/ GoodStuff.ProductApi.Application/
COPY GoodStuff.ProductApi.Domain/ GoodStuff.ProductApi.Domain/
COPY GoodStuff.ProductApi.Infrastructure/ GoodStuff.ProductApi.Infrastructure/
COPY GoodStuff.ProductApi.Application.Tests/ GoodStuff.ProductApi.Application.Tests/

# Optional: test stage
FROM build AS test
RUN dotnet test GoodStuff.ProductApi.Application.Tests/GoodStuff.ProductApi.Application.Tests.csproj --no-restore

# Publish stage
FROM build AS publish
RUN dotnet publish GoodStuff.ProductApi.Presentation/GoodStuff.ProductApi.Presentation.csproj \
    --no-restore -c Release -o /app/publish

# -------------------------
# 2️⃣ Runtime Stage
# -------------------------
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

# Copy only published files
COPY --from=publish /app/publish .

# Set environment variables for HTTPS
ENV ASPNETCORE_URLS=https://+:8443
ENV ASPNETCORE_HTTPS_PORTS=8443

ENTRYPOINT ["dotnet", "GoodStuff.ProductApi.Presentation.dll"]
