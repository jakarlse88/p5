using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TheCarHub.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Listings",
                columns: table => new
                {
                    ListingItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    ListingStatus = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateLastUpdated = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Listings", x => x.ListingItemId);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    CarId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListingItemId = table.Column<int>(nullable: false),
                    VIN = table.Column<string>(nullable: true),
                    Year = table.Column<DateTime>(nullable: false),
                    Make = table.Column<string>(nullable: true),
                    Model = table.Column<string>(nullable: true),
                    Trim = table.Column<string>(nullable: true),
                    PurchaseDate = table.Column<DateTime>(nullable: false),
                    PurchasePrice = table.Column<decimal>(nullable: false),
                    Repairs = table.Column<string>(nullable: true),
                    RepairCost = table.Column<decimal>(nullable: false),
                    LotDate = table.Column<DateTime>(nullable: false),
                    SellingPrice = table.Column<decimal>(nullable: false),
                    SaleDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.CarId);
                    table.ForeignKey(
                        name: "FK_Cars_Listings_ListingItemId",
                        column: x => x.ListingItemId,
                        principalTable: "Listings",
                        principalColumn: "ListingItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    ImageItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageLocation = table.Column<string>(nullable: true),
                    ListingItemId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ImageItemId);
                    table.ForeignKey(
                        name: "FK_Images_Listings_ListingItemId",
                        column: x => x.ListingItemId,
                        principalTable: "Listings",
                        principalColumn: "ListingItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_ListingItemId",
                table: "Cars",
                column: "ListingItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_ListingItemId",
                table: "Images",
                column: "ListingItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Listings");
        }
    }
}
