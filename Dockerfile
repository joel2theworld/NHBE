# Use the official .NET 6 SDK image as a build environment
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copy the CSPROJ file and restore any dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the application code and build it
COPY . ./
RUN dotnet publish -c Release -o out

# Use a runtime image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .

# Expose the application on port 80
EXPOSE 80

# Set the entry point to your application DLL
ENTRYPOINT ["dotnet", "NeighbourhoodHelp.Api.dll"]
