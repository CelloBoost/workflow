using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rgvc.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialSystemData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "system",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    version = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_system", x => x.id);
                    table.CheckConstraint("ck_system_single_row", "\"id\" = 1");
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "system");
        }
    }
}
