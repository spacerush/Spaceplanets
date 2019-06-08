FROM microsoft/dotnet:sdk AS build-env
WORKDIR /app
EXPOSE 5000
 
WORKDIR /src
COPY *.sln ./
COPY SpacePlanetsMvc/SpacePlanetsMvc.csproj SpacePlanetsMvc/
COPY SpacePlanets.SharedModels/SpacePlanets.SharedModels.csproj SpacePlanets.SharedModels/
RUN dotnet restore
COPY . .
WORKDIR /src/SpacePlanetsMvc
RUN dotnet build -c $Configuration -o /app

FROM builder AS publish
ARG Configuration=Release
RUN dotnet publish -c $Configuration -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SpacePlanetsMvc.dll"]