FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR .
COPY . .
RUN dotnet restore
RUN dotnet test GoodStuff.ProductApi.Application.Tests/GoodStuff.ProductApi.Application.Tests.csproj

RUN dotnet publish GoodStuff.ProductApi.Presentation/GoodStuff.ProductApi.Presentation.csproj --no-restore -o /publish/

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS run
WORKDIR /publish
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "GoodStuff.ProductApi.Presentation.dll"]