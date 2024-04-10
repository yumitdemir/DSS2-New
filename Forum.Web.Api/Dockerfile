FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Debug

WORKDIR /src

COPY ["Forum.Web.Api/Forum.Web.Api.csproj", "Forum.Web.Api/"]
COPY ["Forum.Infrastructure/Forum.Infrastructure.csproj", "Forum.Infrastructure/"]
COPY ["Forum.Application/Forum.Application.csproj", "Forum.Application/"]
COPY ["Forum.Domain/Forum.Domain.csproj", "Forum.Domain/"]

RUN dotnet restore "./Forum.Web.Api/Forum.Web.Api.csproj"

COPY . .

WORKDIR "/src/Forum.Web.Api"
RUN dotnet build "./Forum.Web.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Debug
RUN dotnet publish "./Forum.Web.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 80

ENV ASPNETCORE_ENVIRONMENT=Development

ENTRYPOINT ["dotnet", "Forum.Web.Api.dll"]