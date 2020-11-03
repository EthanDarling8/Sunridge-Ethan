using Microsoft.EntityFrameworkCore.Migrations;

namespace Sunridge.DataAccess.Migrations
{
    public partial class PhotoGalleryUpdate5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhotoAlbum_AspNetUsers_ApplicationUserId",
                table: "PhotoAlbum");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "PhotoAlbum",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Photo",
                maxLength: 60,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoAlbum_AspNetUsers_ApplicationUserId",
                table: "PhotoAlbum",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhotoAlbum_AspNetUsers_ApplicationUserId",
                table: "PhotoAlbum");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "PhotoAlbum",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Photo",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 60,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoAlbum_AspNetUsers_ApplicationUserId",
                table: "PhotoAlbum",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
