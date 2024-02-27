FROM ghcr.io/architecture-it/net-sdk:8.0 as build
WORKDIR /app
COPY . .
RUN dotnet restore
WORKDIR "/app/src/Api"
RUN dotnet build "desafio_backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "desafio_backend.csproj" -c Release -o /app/publish

FROM ghcr.io/architecture-it/net:8.0
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "desafio_backend.dll"]