using Microsoft.EntityFrameworkCore.Migrations;

namespace Sunridge.DataAccess.Migrations
{
    public partial class DocumentFileKeywordInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Keywords",
                table: "DocumentFile");

            migrationBuilder.CreateTable(
                name: "DocumentFileKeyword",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Keyword = table.Column<string>(nullable: false),
                    DocumentFileId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentFileKeyword", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentFileKeyword_DocumentFile_DocumentFileId",
                        column: x => x.DocumentFileId,
                        principalTable: "DocumentFile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentFileKeyword_DocumentFileId",
                table: "DocumentFileKeyword",
                column: "DocumentFileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentFileKeyword");

            migrationBuilder.AddColumn<string>(
                name: "Keywords",
                table: "DocumentFile",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
