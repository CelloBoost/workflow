# RGVC - RGV Controller

## Layers
- `src/Rgvc.Api`: API layer (ASP.NET Core Rest Controllers)
- `src/Rgvc.Domain`: Domain layer
- `src/Rgvc.Application`: Application layer
- `src/Rgvc.Infra`: Infrastructure layer (implementation, EF Core + PostgreSQL)

Each layer has its own DI entry file:
- `src/Rgvc.Api/DependencyInjection.cs` -> `AddApi()`
- `src/Rgvc.Domain/DependencyInjection.cs` -> `AddDomain()`
- `src/Rgvc.Application/DependencyInjection.cs` -> `AddApplication()`
- `src/Rgvc.Infra/DependencyInjection.cs` -> `AddInfrastructure()`

## Run (Docker dev mode)
```bash
make dev
```

Docker PostgreSQL image tag is configured via `.env`.

Then open:
- `GET http://localhost:8080/api/rgvc/v1/health`
Or use the provided `.http` files for more API calls.

## Run (Docker production mode)
```bash
make prod-up
make prod-down
```

This mode uses:
- `docker-compose.prod.yml`
- `Dockerfile` target `runtime` (no source bind mount, no `dotnet watch`)
- `ASPNETCORE_ENVIRONMENT=Production`

## Production Image
Build only the production runtime image:
```bash
docker build --target runtime -t rgvc-api:prod .
```
