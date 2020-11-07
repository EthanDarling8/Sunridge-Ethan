using Microsoft.EntityFrameworkCore.Migrations;

namespace Sunridge.DataAccess.Migrations
{
    public partial class NewsAttachment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Attachment",
                table: "News",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attachment",
                table: "News");
        }
    }
}
