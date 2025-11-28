using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IMS.Plugins.EFCoreSqlServer.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InventoryTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PONumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductionNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InventoryId = table.Column<int>(type: "int", nullable: false),
                    QuantityBefore = table.Column<int>(type: "int", nullable: false),
                    ActivityType = table.Column<int>(type: "int", nullable: false),
                    QuantityAfter = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DoneBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryTransactions_Inventories_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductInventories",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    InventoryId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductInventories", x => new { x.ProductId, x.InventoryId });
                    table.ForeignKey(
                        name: "FK_ProductInventories_Inventories_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductInventories_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SONumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductionNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    QuantityBefore = table.Column<int>(type: "int", nullable: false),
                    ActivityType = table.Column<int>(type: "int", nullable: false),
                    QuantityAfter = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DoneBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductTransactions_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Inventories",
                columns: new[] { "Id", "Name", "Price", "Quantity" },
                values: new object[,]
                {
                    { 1, "Aluminum Frame", 250.00m, 50 },
                    { 2, "Carbon Frame", 600.00m, 30 },
                    { 3, "Wheel Set 30\"", 150.00m, 100 },
                    { 4, "Wheel 20\"", 40.00m, 120 },
                    { 5, "Wheel 26\"", 75.00m, 150 },
                    { 6, "Disc Brake Set", 120.00m, 80 },
                    { 7, "Electric Motor Kit", 800.00m, 25 },
                    { 8, "Gear System 21-Speed", 180.00m, 70 },
                    { 9, "Gear System 7-Speed", 80.00m, 90 },
                    { 10, "Suspension Fork", 200.00m, 45 },
                    { 11, "Battery Pack", 400.00m, 30 },
                    { 12, "Pedals", 35.00m, 200 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Price", "Quantity" },
                values: new object[,]
                {
                    { 1, "Mountain Trail Pro", 1299.99m, 0 },
                    { 2, "City Commuter Elite", 899.99m, 0 },
                    { 3, "Road Racer X500", 1899.99m, 0 },
                    { 4, "Electric Cruiser Plus", 2499.99m, 0 },
                    { 5, "Kids Explorer 20", 549.99m, 0 }
                });

            migrationBuilder.InsertData(
                table: "ProductInventories",
                columns: new[] { "InventoryId", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 3, 1, 1 },
                    { 6, 1, 1 },
                    { 8, 1, 1 },
                    { 10, 1, 1 },
                    { 12, 1, 2 },
                    { 1, 2, 1 },
                    { 5, 2, 2 },
                    { 6, 2, 1 },
                    { 9, 2, 1 },
                    { 12, 2, 2 },
                    { 2, 3, 1 },
                    { 3, 3, 1 },
                    { 6, 3, 1 },
                    { 8, 3, 1 },
                    { 12, 3, 2 },
                    { 1, 4, 1 },
                    { 5, 4, 2 },
                    { 6, 4, 1 },
                    { 7, 4, 1 },
                    { 9, 4, 1 },
                    { 11, 4, 1 },
                    { 12, 4, 2 },
                    { 1, 5, 1 },
                    { 4, 5, 2 },
                    { 9, 5, 1 },
                    { 12, 5, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransactions_InventoryId",
                table: "InventoryTransactions",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInventories_InventoryId",
                table: "ProductInventories",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTransactions_ProductId",
                table: "ProductTransactions",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InventoryTransactions");

            migrationBuilder.DropTable(
                name: "ProductInventories");

            migrationBuilder.DropTable(
                name: "ProductTransactions");

            migrationBuilder.DropTable(
                name: "Inventories");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
