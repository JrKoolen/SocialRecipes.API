FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SocialRecipes.API/SocialRecipes.API.csproj", "SocialRecipes.API/"]
RUN dotnet restore "./SocialRecipes.API/SocialRecipes.API.csproj"
COPY . .
WORKDIR "/src/SocialRecipes.API"
RUN dotnet build "./SocialRecipes.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SocialRecipes.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV ASPNETCORE_ENVIRONMENT=Development

ENTRYPOINT ["dotnet", "SocialRecipes.API.dll"]
