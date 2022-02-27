FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY *.csproj .
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
RUN mkdir -p /app/wwwroot/uploads
ENV ASPNETCORE_URLS=http://+:5000
COPY --from=build /app .
CMD ["dotnet", "devantler-cms.dll"]
