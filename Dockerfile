ARG DOTNET_VERSION=8.0

FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS dev

WORKDIR /src

COPY Rgvc.sln ./
COPY Directory.Packages.props ./
COPY src/Rgvc.Api/Rgvc.Api.csproj src/Rgvc.Api/
COPY src/Rgvc.Domain/Rgvc.Domain.csproj src/Rgvc.Domain/
COPY src/Rgvc.Application/Rgvc.Application.csproj src/Rgvc.Application/
COPY src/Rgvc.Infra/Rgvc.Infra.csproj src/Rgvc.Infra/

RUN dotnet restore src/Rgvc.Api/Rgvc.Api.csproj

COPY . .

WORKDIR /src/src/Rgvc.Api
EXPOSE 8080

CMD ["dotnet", "watch", "run", "--no-restore", "--no-launch-profile", "--urls", "http://0.0.0.0:8080"]

FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS build

WORKDIR /src

COPY Rgvc.sln ./
COPY Directory.Packages.props ./
COPY src/Rgvc.Api/Rgvc.Api.csproj src/Rgvc.Api/
COPY src/Rgvc.Domain/Rgvc.Domain.csproj src/Rgvc.Domain/
COPY src/Rgvc.Application/Rgvc.Application.csproj src/Rgvc.Application/
COPY src/Rgvc.Infra/Rgvc.Infra.csproj src/Rgvc.Infra/

RUN dotnet restore src/Rgvc.Api/Rgvc.Api.csproj

COPY . .
WORKDIR /src/src/Rgvc.Api
RUN dotnet build Rgvc.Api.csproj -c Release --no-restore -o /app/build

FROM build AS publish
RUN dotnet publish Rgvc.Api.csproj -c Release --no-restore -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION} AS runtime

WORKDIR /app

ENV ASPNETCORE_URLS=http://0.0.0.0:8080 \
    DOTNET_RUNNING_IN_CONTAINER=true

EXPOSE 8080

USER $APP_UID

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Rgvc.Api.dll"]
