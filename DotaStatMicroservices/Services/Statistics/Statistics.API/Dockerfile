FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5036

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Services/Statistics/Statistics.API/Statistics.API.csproj", "Services/Statistics/Statistics.API/"]
COPY ["Common/CommonExtensions/CommonExtensions.csproj", "Common/CommonExtensions/CommonExtensions.csproj"]
RUN dotnet restore "Services/Statistics/Statistics.API/Statistics.API.csproj"
COPY . .
WORKDIR /src/Services/Statistics/Statistics.API
RUN dotnet build "Statistics.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Statistics.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Statistics.API.dll"]
