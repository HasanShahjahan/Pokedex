FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["src/domain/core/Pokedex.Api/Pokedex.Api.csproj", "src/domain/core/Pokedex.Api/"]
RUN dotnet restore "src/domain/core/Pokedex.Api/Pokedex.Api.csproj"
COPY . .
WORKDIR "/src/src/domain/core/Pokedex.Api"
RUN dotnet build "Pokedex.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Pokedex.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pokedex.Api.dll"]

CMD ASPNETCORE_URLS=http://*:$PORT dotnet Pokedex.Api.dll