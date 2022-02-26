FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY *.csproj .
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app/wwwroot/uploads
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:5000
ENV DOTNET_EnableDiagnostics=0
COPY --from=build /app .
ENTRYPOINT ["dotnet", "devantler-cms.dll"]
