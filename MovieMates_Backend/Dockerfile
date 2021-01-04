# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY MovieMates_Backend/*.sln .
COPY MovieMates_Backend/*.csproj ./MovieMates_Backend/
RUN dotnet restore MovieMates_Backend
RUN dotnet restore ./MovieMates_Backend
RUN dotnet restore "MovieMates_Backend"

# copy everything else and build app
COPY . .
WORKDIR /source/MovieMates_Backend
RUN dotnet publish -c release -o /app 

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:3.1
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "MovieMates_Backend.dll"]