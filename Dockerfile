   FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
   WORKDIR /app
   EXPOSE 80

   FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
   WORKDIR /src
   COPY ["src/LogisticaApi.Api/LogisticaApi.Api.csproj", "src/LogisticaApi.Api/"]
   COPY ["src/LogisticaApi.Application/LogisticaApi.Application.csproj", "src/LogisticaApi.Application/"]
   COPY ["src/LogisticaApi.Domain/LogisticaApi.Domain.csproj", "src/LogisticaApi.Domain/"]
   COPY ["src/LogisticaApi.Infrastructure/LogisticaApi.Infrastructure.csproj", "src/LogisticaApi.Infrastructure/"]
   RUN dotnet restore "src/LogisticaApi.Api/LogisticaApi.Api.csproj"
   COPY . .
   WORKDIR "/src/src/LogisticaApi.Api"
   RUN dotnet build "LogisticaApi.Api.csproj" -c Release -o /app/build

   FROM build AS publish
   RUN dotnet publish "LogisticaApi.Api.csproj" -c Release -o /app/publish

   FROM base AS final
   WORKDIR /app
   COPY --from=publish /app/publish .
   ENTRYPOINT ["dotnet", "LogisticaApi.Api.dll"]
   