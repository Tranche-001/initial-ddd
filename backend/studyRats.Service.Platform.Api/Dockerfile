# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Packages.props", "."]
COPY ["studyRats.Service.Platform.Api/studyRats.Service.Platform.Api.csproj", "studyRats.Service.Platform.Api/"]
COPY ["studyRats.Service.Platform.Application/studyRats.Service.Platform.Application.csproj", "studyRats.Service.Platform.Application/"]
COPY ["studyRats.Service.Platform.Domain/studyRats.Service.Platform.Domain.csproj", "studyRats.Service.Platform.Domain/"]
COPY ["studyRats.Service.Platform.Facade/studyRats.Service.Platform.Facade.csproj", "studyRats.Service.Platform.Facade/"]
COPY ["studyRats.Service.Platform.Data/studyRats.Service.Platform.Data.csproj", "studyRats.Service.Platform.Data/"]
RUN dotnet restore "./studyRats.Service.Platform.Api/studyRats.Service.Platform.Api.csproj"
COPY . .
WORKDIR "/src/studyRats.Service.Platform.Api"
RUN dotnet build "./studyRats.Service.Platform.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./studyRats.Service.Platform.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "studyRats.Service.Platform.Api.dll"]