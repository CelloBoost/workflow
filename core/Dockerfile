ARG DOTNET_VERSION=10.0

FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS dev

WORKDIR /src

ARG SRC_PREFIX=
COPY ${SRC_PREFIX}Workflow.sln ./
COPY ${SRC_PREFIX}Directory.Packages.props ./
COPY ${SRC_PREFIX}Workflow.Api/Workflow.Api.csproj Workflow.Api/
COPY ${SRC_PREFIX}Workflow.Domain/Workflow.Domain.csproj Workflow.Domain/
COPY ${SRC_PREFIX}Workflow.Application/Workflow.Application.csproj Workflow.Application/
COPY ${SRC_PREFIX}Workflow.Infra/Workflow.Infra.csproj Workflow.Infra/

RUN dotnet restore Workflow.Api/Workflow.Api.csproj

COPY ${SRC_PREFIX}. .

WORKDIR /src/Workflow.Api
EXPOSE 8080

CMD ["dotnet", "watch", "run", "--no-launch-profile", "--urls", "http://0.0.0.0:8080"]

FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS build

WORKDIR /src

ARG SRC_PREFIX=
COPY ${SRC_PREFIX}Workflow.sln ./
COPY ${SRC_PREFIX}Directory.Packages.props ./
COPY ${SRC_PREFIX}Workflow.Api/Workflow.Api.csproj Workflow.Api/
COPY ${SRC_PREFIX}Workflow.Domain/Workflow.Domain.csproj Workflow.Domain/
COPY ${SRC_PREFIX}Workflow.Application/Workflow.Application.csproj Workflow.Application/
COPY ${SRC_PREFIX}Workflow.Infra/Workflow.Infra.csproj Workflow.Infra/

RUN dotnet restore Workflow.Api/Workflow.Api.csproj

COPY ${SRC_PREFIX}. .

WORKDIR /src/Workflow.Api
RUN dotnet build Workflow.Api.csproj -c Release --no-restore -o /app/build

FROM build AS publish
RUN dotnet publish Workflow.Api.csproj -c Release --no-restore -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION} AS runtime

WORKDIR /app

ENV ASPNETCORE_URLS=http://0.0.0.0:8080 \
    DOTNET_RUNNING_IN_CONTAINER=true

EXPOSE 8080

USER $APP_UID

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Workflow.Api.dll"]
