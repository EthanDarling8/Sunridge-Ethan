using Microsoft.EntityFrameworkCore.Migrations;

namespace Sunridge.DataAccess.Migrations
{
    public partial class FormFields2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Equipment_Sugestions",
                table: "Forms");

            migrationBuilder.AddColumn<string>(
                name: "Activity",
                table: "Forms",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "Forms",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Hours",
                table: "Forms",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Suggestion",
                table: "Forms",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Activity",
                table: "Forms");

            migrationBuilder.DropColumn(
                name: "Comments",
                table: "Forms");

            migrationBuilder.DropColumn(
                name: "Hours",
                table: "Forms");

            migrationBuilder.DropColumn(
                name: "Suggestion",
                table: "Forms");

            migrationBuilder.AddColumn<string>(
                name: "Equipment_Sugestions",
                table: "Forms",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
