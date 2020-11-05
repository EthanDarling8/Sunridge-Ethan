using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sunridge.DataAccess.Migrations
{
    public partial class DocumentsRebuild : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Lot",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LotNumber",
                table: "Lot",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaxId",
                table: "Lot",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Inventory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LostItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ExpirationDate = table.Column<bool>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LostItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LostItem_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Lot_Owner",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LotId = table.Column<int>(nullable: false),
                    OwnerId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lot_Owner", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lot_Owner_Lot_LotId",
                        column: x => x.LotId,
                        principalTable: "Lot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lot_Owner_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LotFile",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    File = table.Column<string>(nullable: true),
                    LotId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LotFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LotFile_Lot_LotId",
                        column: x => x.LotId,
                        principalTable: "Lot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lot_Inventory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LotId = table.Column<int>(nullable: false),
                    InventoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lot_Inventory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lot_Inventory_Inventory_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lot_Inventory_Lot_LotId",
                        column: x => x.LotId,
                        principalTable: "Lot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lot_OwnerFile",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    File = table.Column<string>(nullable: true),
                    Lot_OwnerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lot_OwnerFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lot_OwnerFile_Lot_Owner_Lot_OwnerId",
                        column: x => x.Lot_OwnerId,
                        principalTable: "Lot_Owner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LostItem_UserId",
                table: "LostItem",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Lot_Inventory_InventoryId",
                table: "Lot_Inventory",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Lot_Inventory_LotId",
                table: "Lot_Inventory",
                column: "LotId");

            migrationBuilder.CreateIndex(
                name: "IX_Lot_Owner_LotId",
                table: "Lot_Owner",
                column: "LotId");

            migrationBuilder.CreateIndex(
                name: "IX_Lot_Owner_OwnerId",
                table: "Lot_Owner",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Lot_OwnerFile_Lot_OwnerId",
                table: "Lot_OwnerFile",
                column: "Lot_OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_LotFile_LotId",
                table: "LotFile",
                column: "LotId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LostItem");

            migrationBuilder.DropTable(
                name: "Lot_Inventory");

            migrationBuilder.DropTable(
                name: "Lot_OwnerFile");

            migrationBuilder.DropTable(
                name: "LotFile");

            migrationBuilder.DropTable(
                name: "Inventory");

            migrationBuilder.DropTable(
                name: "Lot_Owner");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Lot");

            migrationBuilder.DropColumn(
                name: "LotNumber",
                table: "Lot");

            migrationBuilder.DropColumn(
                name: "TaxId",
                table: "Lot");
        }
    }
}
