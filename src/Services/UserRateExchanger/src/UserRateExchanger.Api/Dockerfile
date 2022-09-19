FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/UserRateExchanger/src/UserRateExchanger.Api/UserRateExchanger.Api.csproj", "UserRateExchanger.Api/"]
RUN dotnet restore "src/Services/UserRateExchanger/src/UserRateExchanger.Api/UserRateExchanger.Api.csproj"
COPY . .
WORKDIR "/src/UserRateExchanger.Api"
RUN dotnet build "UserRateExchanger.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserRateExchanger.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserRateExchanger.Api.dll"]
