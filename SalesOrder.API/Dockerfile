FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SalesOrder.API/SalesOrder.API.csproj", "SalesOrder.API/"]
RUN dotnet restore "SalesOrder.API/SalesOrder.API.csproj"
COPY . .
WORKDIR "/src/SalesOrder.API"
RUN dotnet build "SalesOrder.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SalesOrder.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SalesOrder.API.dll"]
