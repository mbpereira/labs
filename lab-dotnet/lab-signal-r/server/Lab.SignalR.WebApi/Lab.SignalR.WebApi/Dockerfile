FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Lab.SignalR.WebApi/Lab.SignalR.WebApi.csproj", "Lab.SignalR.WebApi/"]
RUN dotnet restore "Lab.SignalR.WebApi/Lab.SignalR.WebApi.csproj"
COPY . .
WORKDIR "/src/Lab.SignalR.WebApi"
RUN dotnet build "Lab.SignalR.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Lab.SignalR.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Lab.SignalR.WebApi.dll"]
