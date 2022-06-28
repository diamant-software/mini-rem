FROM harbor.diamant-software.de/library/dotnet/aspnet:6.0.3-bullseye-slim-diamant-1 AS base

EXPOSE 8000

FROM harbor.diamant-software.de/library/dotnet/sdk:6.0.201-bullseye-slim-diamant-1 AS sdk

WORKDIR /src

FROM sdk as build



COPY FruitsApi.sln .
COPY NuGet.config NuGet.config

COPY FruitsApi FruitsApi/

RUN dotnet build -c Release -o /app/build 


# Dateien für Release vorbereiten
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# Finale Stage ohne SDK für die Ausführung des Dienstes
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV ASPNETCORE_URLS=http://+:8000

ENTRYPOINT ["dotnet", "FruitsApi.dll"]