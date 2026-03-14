# Workflow

目前內容包含：

- `core/Workflow.Api`: ASP.NET Core Web API
- `core/Workflow.Application`: application layer
- `core/Workflow.Domain`: domain layer
- `core/Workflow.Infra`: infrastructure / EF Core / PostgreSQL
- `core/Workflow.Tests`: NUnit 整合與單元測試

## Quick Start

在 repo root：

```bash
make build
make test
make dev
```

API 預設路由前綴：

```text
/api/workflow/v1
```

目前預設資料庫連線也已改為 `workflow`：

```text
Host=localhost;Port=5432;Database=workflow;Username=workflow;Password=workflow
```

## Next

接下來可以直接在這個骨架上開始替換目前的 `System` 範例內容，改成真正的 workflow domain / use cases / endpoints。
