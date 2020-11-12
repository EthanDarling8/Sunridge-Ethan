using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sunridge.DataAccess.Migrations
{
    public partial class FireInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "FireInfo");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "FireInfo");

            migrationBuilder.DropColumn(
                name: "File",
                table: "FireInfo");

            migrationBuilder.DropColumn(
                name: "Link1",
                table: "FireInfo");

            migrationBuilder.DropColumn(
                name: "Link2",
                table: "FireInfo");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "FireInfo");

            migrationBuilder.DropColumn(
                name: "PostDate",
                table: "FireInfo");

            migrationBuilder.AddColumn<bool>(
                name: "Archived",
                table: "FireInfo",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Attachment",
                table: "FireInfo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "FireInfo",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DisplayDate",
                table: "FireInfo",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Archived",
                table: "FireInfo");

            migrationBuilder.DropColumn(
                name: "Attachment",
                table: "FireInfo");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "FireInfo");

            migrationBuilder.DropColumn(
                name: "DisplayDate",
                table: "FireInfo");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "FireInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "FireInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "File",
                table: "FireInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Link1",
                table: "FireInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Link2",
                table: "FireInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "FireInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostDate",
                table: "FireInfo",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
