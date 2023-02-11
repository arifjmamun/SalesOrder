FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SalesOrder.App/SalesOrder.App.csproj", "SalesOrder.App/"]
RUN dotnet restore "SalesOrder.App/SalesOrder.App.csproj"
COPY . .
WORKDIR "/src/SalesOrder.App"
RUN dotnet build "SalesOrder.App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SalesOrder.App.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SalesOrder.App.dll"]
