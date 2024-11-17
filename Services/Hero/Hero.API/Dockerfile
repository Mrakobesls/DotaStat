FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5023

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
#ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Hero/Hero.API/Hero.API.csproj", "Services/Hero/Hero.API/"]
COPY ["Common/CommonExtensions/CommonExtensions.csproj", "Common/CommonExtensions/CommonExtensions.csproj"]
RUN dotnet restore "Services/Hero/Hero.API/Hero.API.csproj"
COPY . .
WORKDIR "/src/Services/Hero/Hero.API"
RUN dotnet build "Hero.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Hero.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Hero.API.dll"]
