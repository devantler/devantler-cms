FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY *.csproj ./
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:6.0
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000
WORKDIR /app/wwwroot/uploads
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "devantler-cms.dll"]
