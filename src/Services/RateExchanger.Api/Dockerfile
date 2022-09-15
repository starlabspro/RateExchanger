FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RateExchanger.Api/RateExchanger.Api.csproj", "RateExchanger.Api/"]
RUN dotnet restore "RateExchanger.Api/RateExchanger.Api.csproj"
COPY . .
WORKDIR "/src/RateExchanger.Api"
RUN dotnet build "RateExchanger.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RateExchanger.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RateExchanger.Api.dll"]
