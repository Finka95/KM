FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5002

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SubscriptionService/SubscriptionService.API/SubscriptionService.API.csproj", "SubscriptionService.API/"]
COPY ["SubscriptionService/SubscriptionService.BLL/SubscriptionService.BLL.csproj", "SubscriptionService.BLL/"]
COPY ["SubscriptionService/SubscriptionService.DAL/SubscriptionService.DAL.csproj", "SubscriptionService.DAL/"]
COPY ["SubscriptionService/SubscriptionService.Domain/SubscriptionService.Domain.csproj", "SubscriptionService.Domain/"]
RUN dotnet restore "./SubscriptionService.API/SubscriptionService.API.csproj"
COPY . .
WORKDIR "/src/SubscriptionService/SubscriptionService.API"
RUN dotnet build "./SubscriptionService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SubscriptionService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SubscriptionService.API.dll"]