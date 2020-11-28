using Microsoft.EntityFrameworkCore.Migrations;

namespace Sunridge.DataAccess.Migrations
{
    public partial class Documents3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentFileKeyword");

            migrationBuilder.AddColumn<string>(
                name: "Keywords",
                table: "DocumentFile",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Keywords",
                table: "DocumentFile");

            migrationBuilder.CreateTable(
                name: "DocumentFileKeyword",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentFileId = table.Column<int>(type: "int", nullable: false),
                    Keyword = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
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
    }
}
