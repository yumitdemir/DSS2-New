#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Debug

WORKDIR /src
COPY ["Forum.Web.UI/Forum.Web.UI.csproj", "Forum.Web.UI/"]

RUN dotnet restore "./Forum.Web.UI/Forum.Web.UI.csproj"

COPY . .

WORKDIR "/src/Forum.Web.UI"
RUN dotnet build "./Forum.Web.UI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Forum.Web.UI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 80
ENV ASPNETCORE_ENVIRONMENT=Development

ENTRYPOINT ["dotnet", "Forum.Web.UI.dll"]