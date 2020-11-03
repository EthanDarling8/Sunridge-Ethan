using Microsoft.EntityFrameworkCore.Migrations;

namespace Sunridge.DataAccess.Migrations
{
    public partial class PhotoGalleryUpdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "PhotoAlbum");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "PhotoAlbum",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "PhotoAlbum",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Photo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Photo",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "PhotoAlbum");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "PhotoAlbum");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Photo");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Photo");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "PhotoAlbum",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
