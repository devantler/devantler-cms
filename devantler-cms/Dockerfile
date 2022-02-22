FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base 
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY *.csproj ./
RUN dotnet restore
COPY . .
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app/wwwroot/uploads
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "devantler-cms.dll"]
