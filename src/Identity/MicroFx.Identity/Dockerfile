FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["MicroFx.Identity/MicroFx.Identity.csproj", "MicroFx.Identity/"]
RUN dotnet restore "MicroFx.Identity/MicroFx.Identity.csproj"
COPY . .
WORKDIR "/src/MicroFx.Identity"
RUN dotnet build "MicroFx.Identity.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "MicroFx.Identity.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MicroFx.Identity.dll"]