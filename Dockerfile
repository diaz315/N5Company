#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["UserPermission.Api/UserPermission.Api.csproj", "UserPermission.Api/"]
COPY ["UserPermission.Repository/UserPermission.Repository.csproj", "UserPermission.Repository/"]
COPY ["UserPermission.Domain/UserPermission.Domain.csproj", "UserPermission.Domain/"]
RUN dotnet restore "UserPermission.Api/UserPermission.Api.csproj"
COPY . .
WORKDIR "/src/UserPermission.Api"
RUN dotnet build "UserPermission.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserPermission.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserPermission.Api.dll"]