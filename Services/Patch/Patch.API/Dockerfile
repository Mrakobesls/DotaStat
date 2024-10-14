FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5021

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Patch/Patch.API/Patch.API.csproj", "Services/Patch/Patch.API/"]
RUN dotnet restore "Services/Patch/Patch.API/Patch.API.csproj"
COPY . .
WORKDIR "/src/Services/Patch/Patch.API"
RUN dotnet build "Patch.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Patch.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Patch.API.dll"]
