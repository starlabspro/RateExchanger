FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RateExchanger/RateExchanger.csproj", "RateExchanger/"]
RUN dotnet restore "RateExchanger/RateExchanger.csproj"
COPY . .
WORKDIR "/src/RateExchanger"
RUN dotnet build "RateExchanger.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RateExchanger.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RateExchanger.dll"]
