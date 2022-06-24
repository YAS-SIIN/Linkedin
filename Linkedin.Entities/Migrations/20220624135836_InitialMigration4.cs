using Microsoft.EntityFrameworkCore.Migrations;

namespace Linkedin.Entities.Migrations
{
    public partial class InitialMigration4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ActivityId",
                table: "Activities",
                newName: "ActivityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ActivityId",
                table: "Activities",
                newName: "ActivityId");
        }
    }
}
