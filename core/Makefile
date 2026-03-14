COMPOSE_DEV := docker compose --env-file .env -f docker-compose.yml
COMPOSE_PROD := docker compose --env-file .env -f docker-compose.prod.yml
SOLUTION := Workflow.sln
TEST_PROJECT := Workflow.Tests/Workflow.Tests.csproj
DEVICE_IMAGE ?= workflow-api
COVERAGE_RESULTS_DIR := .coverage
COVERAGE_REPORT_DIR := $(COVERAGE_RESULTS_DIR)/CoverageReport
REPORTGENERATOR := $(HOME)/.dotnet/tools/reportgenerator

.PHONY: dev down build test coverage build-image build-image-dev install-ef ef-add-migration prod-up prod-down

dev:
	$(COMPOSE_DEV) up --build

down:
	$(COMPOSE_DEV) down

build:
	dotnet build $(SOLUTION)

test:
	dotnet test $(TEST_PROJECT)

coverage:
	rm -rf $(COVERAGE_RESULTS_DIR)
	dotnet test $(TEST_PROJECT) --collect:"XPlat Code Coverage" --results-directory $(COVERAGE_RESULTS_DIR)
	@if [ ! -x "$(REPORTGENERATOR)" ]; then \
		dotnet tool install --global dotnet-reportgenerator-globaltool; \
	fi
	$(REPORTGENERATOR) -reports:"$(COVERAGE_RESULTS_DIR)/**/coverage.cobertura.xml" -targetdir:"$(COVERAGE_REPORT_DIR)" -reporttypes:"Html;TextSummary"
	@echo "Coverage summary: $(COVERAGE_REPORT_DIR)/Summary.txt"
	@echo "Coverage HTML: $(COVERAGE_REPORT_DIR)/index.html"

build-image:
	docker build --target runtime --build-arg SRC_PREFIX= -f Dockerfile -t $(DEVICE_IMAGE):prod .

build-image-dev:
	docker build --target dev --build-arg SRC_PREFIX= -f Dockerfile -t $(DEVICE_IMAGE):dev .

install-ef:
	dotnet tool install --global dotnet-ef --version 10.0.4

ef-add-migration:
	$(if $(MIGRATION),,$(error MIGRATION variable is required. Usage: make ef-add-migration MIGRATION=YourMigrationName))
	dotnet ef migrations add $(MIGRATION) --project Workflow.Infra/Workflow.Infra.csproj --startup-project Workflow.Api/Workflow.Api.csproj --output-dir Data/Migrations --framework net10.0

prod-up:
	$(COMPOSE_PROD) up --build -d

prod-down:
	$(COMPOSE_PROD) down -v
