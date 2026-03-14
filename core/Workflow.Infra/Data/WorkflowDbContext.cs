using Microsoft.EntityFrameworkCore;
using Workflow.Domain.Models;
using Workflow.Infra.Data.Config;

namespace Workflow.Infra.Data;

public class WorkflowDbContext : DbContext
{
    public WorkflowDbContext(DbContextOptions<WorkflowDbContext> options)
        : base(options) { }

    public DbSet<SystemData> SystemData => Set<SystemData>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new SystemDataConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}
