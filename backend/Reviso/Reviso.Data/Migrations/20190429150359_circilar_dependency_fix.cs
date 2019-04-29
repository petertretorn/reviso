using Microsoft.EntityFrameworkCore.Migrations;

namespace Reviso.Data.Migrations
{
    public partial class circilar_dependency_fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Project",
                table: "Invoice",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Project",
                table: "Invoice");
        }
    }
}
