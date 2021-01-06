#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/Users.API/Users.API.csproj", "src/Users.API/"]
COPY ["src/Users.Infrastructure.MongoDB/Users.Infrastructure.MongoDB.csproj", "src/Users.Infrastructure.MongoDB/"]
COPY ["src/Users.Core/Users.Core.csproj", "src/Users.Core/"]
RUN dotnet restore "src/Users.API/Users.API.csproj"
COPY . .
WORKDIR "/src/src/Users.API"
RUN dotnet build "Users.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Users.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Users.API.dll"]