using Rgvc.Api;
using Rgvc.Api.Rest.Shared.Middleware;
using Rgvc.Application;
using Rgvc.Domain;
using Rgvc.Infra;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApi().AddDomain().AddApplication().AddInfrastructure();

var app = builder.Build();

app.UseRouting();
app.UseRestMiddleware();
app.MapControllers();

app.Run();

public partial class Program;
