FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5003

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["NotificationService/NotificationService.API/NotificationService.API.csproj", "NotificationService.API/"]
COPY ["NotificationService/NotificationService.BLL/NotificationService.BLL.csproj", "NotificationService.BLL/"]
RUN dotnet restore "./NotificationService.API/NotificationService.API.csproj"
COPY . .
WORKDIR "/src/NotificationService"
RUN dotnet build "NotificationService.API/NotificationService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "NotificationService.API/NotificationService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NotificationService.API.dll"]