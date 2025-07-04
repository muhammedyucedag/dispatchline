FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DispatchLine.Web/DispatchLine.Web.csproj", "DispatchLine.Web/"]
COPY ["DispatchLine.Application/DispatchLine.Application.csproj", "DispatchLine.Application/"]
COPY ["DispatchLine.Domain/DispatchLine.Domain.csproj", "DispatchLine.Domain/"]
COPY ["DispatchLine.Infrastructure/DispatchLine.Infrastructure.csproj", "DispatchLine.Infrastructure/"]
RUN dotnet restore "DispatchLine.Web/DispatchLine.Web.csproj"
COPY . .
WORKDIR "/src/DispatchLine.Web"
RUN dotnet build "./DispatchLine.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DispatchLine.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DispatchLine.Web.dll"]
