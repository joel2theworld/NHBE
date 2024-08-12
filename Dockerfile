# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copy the solution file and restore dependencies
COPY *.sln ./
COPY NeighbourhoodHelp.Api/NeighbourhoodHelp.Api.csproj NeighbourhoodHelp.Api/
COPY NeighbourhoodHelp.Model/NeighbourhoodHelp.Model.csproj NeighbourhoodHelp.Model/
COPY NeighbourhoodHelp.Data/NeighbourhoodHelp.Data.csproj NeighbourhoodHelp.Data/
COPY NeighbourhoodHelp.Infrastructure/NeighbourhoodHelp.Infrastructure.csproj NeighbourhoodHelp.Infrastructure/
COPY NeighbourhoodHelp.Core/NeighbourhoodHelp.Core.csproj NeighbourhoodHelp.Core/
RUN dotnet restore

# Copy the entire project and build
COPY . ./
WORKDIR /app/NeighbourhoodHelp.Api
RUN dotnet publish -c Release -o out

# Stage 2: Set up the runtime environment
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/NeighbourhoodHelp.Api/out .

# Expose the HTTP port
EXPOSE 80

# Set the entry point to the API project DLL
ENTRYPOINT ["dotnet", "NeighbourhoodHelp.Api.dll"]
