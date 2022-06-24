using Microsoft.EntityFrameworkCore.Migrations;

namespace Linkedin.Entities.Migrations
{
    public partial class InitialMigration7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LinkedinActivities_Users_UserId",
                table: "LinkedinActivities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LinkedinActivities",
                table: "LinkedinActivities");

            migrationBuilder.RenameTable(
                name: "LinkedinActivities",
                newName: "Activities");

            migrationBuilder.RenameIndex(
                name: "IX_LinkedinActivities_UserId",
                table: "Activities",
                newName: "IX_Activities_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Activities",
                table: "Activities",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Users_UserId",
                table: "Activities",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Users_UserId",
                table: "Activities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Activities",
                table: "Activities");

            migrationBuilder.RenameTable(
                name: "Activities",
                newName: "LinkedinActivities");

            migrationBuilder.RenameIndex(
                name: "IX_Activities_UserId",
                table: "LinkedinActivities",
                newName: "IX_LinkedinActivities_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LinkedinActivities",
                table: "LinkedinActivities",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LinkedinActivities_Users_UserId",
                table: "LinkedinActivities",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
