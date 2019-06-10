FROM microsoft/dotnet:sdk AS build-env
WORKDIR /app

# Copy everything and build the project
COPY . ./
RUN dotnet restore SpacePlanetsMvc/*.csproj

# Copy everything else and build
COPY . ./
RUN dotnet publish SpacePlanetsMvc/*.csproj -c Release -o out

# Build runtime image
FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /app/SpacePlanetsMvc/out ./

ENTRYPOINT ["dotnet", "SpacePlanetsMvc.dll"]