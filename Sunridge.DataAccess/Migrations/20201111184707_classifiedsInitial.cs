using Microsoft.EntityFrameworkCore.Migrations;

namespace Sunridge.DataAccess.Migrations
{
    public partial class classifiedsInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClassifiedsCategory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassifiedsCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClassifiedsItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: false),
                    Images = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Website = table.Column<string>(nullable: true),
                    TimeAdded = table.Column<string>(nullable: true),
                    OwnerId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassifiedsItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassifiedsItem_ClassifiedsCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ClassifiedsCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassifiedsItem_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ViewCount",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Count = table.Column<int>(nullable: false),
                    ClassifiedsItemId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViewCount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ViewCount_ClassifiedsItem_ClassifiedsItemId",
                        column: x => x.ClassifiedsItemId,
                        principalTable: "ClassifiedsItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassifiedsItem_CategoryId",
                table: "ClassifiedsItem",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassifiedsItem_OwnerId",
                table: "ClassifiedsItem",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ViewCount_ClassifiedsItemId",
                table: "ViewCount",
                column: "ClassifiedsItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ViewCount");

            migrationBuilder.DropTable(
                name: "ClassifiedsItem");

            migrationBuilder.DropTable(
                name: "ClassifiedsCategory");
        }
    }
}
