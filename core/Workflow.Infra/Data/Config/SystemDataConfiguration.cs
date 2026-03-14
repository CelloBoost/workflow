using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workflow.Domain.Models;

namespace Workflow.Infra.Data.Config;

public class SystemDataConfiguration : IEntityTypeConfiguration<SystemData>
{
    public void Configure(EntityTypeBuilder<SystemData> builder)
    {
        builder.ToTable(
            "system",
            tableBuilder =>
            {
                tableBuilder.HasCheckConstraint("ck_system_single_row", "\"id\" = 1");
            }
        );

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id").ValueGeneratedNever();

        builder.Property(x => x.Version).HasColumnName("version").HasMaxLength(32).IsRequired();
    }
}
