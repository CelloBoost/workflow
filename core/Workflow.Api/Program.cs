using Workflow.Api;
using Workflow.Api.Rest.Shared.Middleware;
using Workflow.Application;
using Workflow.Domain;
using Workflow.Infra;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApi().AddDomain().AddApplication().AddInfrastructure();

var app = builder.Build();

app.UseRouting();
app.UseRestMiddleware();
app.MapControllers();

app.Run();

public partial class Program;
