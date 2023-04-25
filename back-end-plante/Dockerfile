FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["back-end-plante/back-end-plante.csproj", "back-end-plante/"]
RUN dotnet restore "back-end-plante/back-end-plante.csproj"
COPY . .
WORKDIR "/src/back-end-plante"
RUN dotnet build "back-end-plante.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "back-end-plante.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "back-end-plante.dll"]
