FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5139

ENV ASPNETCORE_URLS=http://+:5139
RUN apt-get update && apt-get install curl -y 
# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["estoque.service/estoque.service.csproj", "estoque.service/"]
COPY ["estoque.domain/estoque.domain.csproj", "estoque.domain/"]
COPY ["estoque.infra/estoque.infra.csproj", "estoque.infra/"]
RUN dotnet restore "estoque.service/estoque.service.csproj"
COPY . .
WORKDIR "/src/estoque.service"
RUN dotnet build "estoque.service.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "estoque.service.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "estoque.service.dll"]
