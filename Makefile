COMPOSE_DEV := docker compose -f docker-compose.yml
COMPOSE_PROD := docker compose -f docker-compose.prod.yml

.PHONY: dev down build test install-ef ef-add-migration prod-up prod-down

dev:
	$(COMPOSE_DEV) up --build

down:
	$(COMPOSE_DEV) down

build:
	dotnet build Rgvc.sln

test:
	dotnet test Rgvc.sln

install-ef:
	dotnet tool install --global dotnet-ef --version 8.0.8

ef-add-migration:
	$(if $(MIGRATION),,$(error MIGRATION variable is required. Usage: make ef-add-migration MIGRATION=YourMigrationName))
	dotnet ef migrations add $(MIGRATION) --project src/Rgvc.Infra/Rgvc.Infra.csproj --startup-project src/Rgvc.Api/Rgvc.Api.csproj --output-dir Data/Migrations --framework net8.0

prod-up:
	$(COMPOSE_PROD) up --build -d

prod-down:
	$(COMPOSE_PROD) down -v
