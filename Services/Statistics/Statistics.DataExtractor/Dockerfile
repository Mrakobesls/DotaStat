FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Statistics/Statistics.DataExtractor/Statistics.DataExtractor.csproj", "Services/Statistics/Statistics.DataExtractor/"]
RUN dotnet restore "Services/Statistics/Statistics.DataExtractor/Statistics.DataExtractor.csproj"
COPY . .
WORKDIR "/src/Services/Statistics/Statistics.DataExtractor"
RUN dotnet build "Statistics.DataExtractor.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Statistics.DataExtractor.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Statistics.DataExtractor.dll"]
