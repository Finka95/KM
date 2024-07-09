FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5004

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["GraphQlService/GraphQlService/GraphQlService.csproj", "GraphQlService/"]
COPY ["GraphQlService/GraphQlService.BLL/GraphQlService.BLL.csproj", "GraphQlService.BLL/"]
RUN dotnet restore "./GraphQlService/GraphQlService.csproj"
COPY . .
WORKDIR "/src/GraphQlService"
RUN dotnet build "GraphQlService/GraphQlService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "GraphQlService/GraphQlService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GraphQlService.dll"]