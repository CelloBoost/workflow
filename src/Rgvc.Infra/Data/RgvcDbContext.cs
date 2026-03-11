using Microsoft.EntityFrameworkCore;
using Rgvc.Domain.Models;
using Rgvc.Infra.Data.Config;

namespace Rgvc.Infra.Data;

public class RgvcDbContext : DbContext
{
    public RgvcDbContext(DbContextOptions<RgvcDbContext> options)
        : base(options) { }

    public DbSet<SystemData> SystemData => Set<SystemData>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new SystemDataConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}
