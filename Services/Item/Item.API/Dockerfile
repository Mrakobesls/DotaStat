FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5022

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Item/Item.API/Item.API.csproj", "Services/Item/Item.API/"]
COPY ["Common/CommonExtensions/CommonExtensions.csproj", "Common/CommonExtensions/CommonExtensions.csproj"]
RUN dotnet restore "Services/Item/Item.API/Item.API.csproj"
COPY . .
WORKDIR "/src/Services/Item/Item.API"
RUN dotnet build "Item.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Item.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Item.API.dll"]
