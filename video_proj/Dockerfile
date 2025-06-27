# Base image for runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Set environment to Development
ENV ASPNETCORE_ENVIRONMENT=Development

# SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project and restore
COPY ["video_proj.csproj", "."]
RUN dotnet restore "video_proj.csproj"

# Copy all files and build
COPY . .
RUN dotnet build "video_proj.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the build output
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "video_proj.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "video_proj.dll"]
