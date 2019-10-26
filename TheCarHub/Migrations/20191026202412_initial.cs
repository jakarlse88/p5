using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TheCarHub.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    CarId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                });

            migrationBuilder.CreateTable(
                name: "Listings",
                columns: table => new
                {
                    ListingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarForeignKey = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ListingStatus = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateLastUpdated = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Listings", x => x.ListingId);
                    table.ForeignKey(
                        name: "FK_Listings_Cars_CarForeignKey",
                        column: x => x.CarForeignKey,
                        principalTable: "Cars",
                        principalColumn: "CarId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    ImageEntityId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<byte[]>(nullable: true),
                    Caption = table.Column<string>(nullable: true),
                    ListingEntityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ImageEntityId);
                    table.ForeignKey(
                        name: "FK_Images_Listings_ListingEntityId",
                        column: x => x.ListingEntityId,
                        principalTable: "Listings",
                        principalColumn: "ListingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "CarId", "LotDate", "Make", "Model", "PurchaseDate", "PurchasePrice", "RepairCost", "Repairs", "SaleDate", "SellingPrice", "Trim", "VIN", "Year" },
                values: new object[,]
                {
                    { 1, new DateTime(2019, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mazda", "Miata", new DateTime(2019, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 1800m, 7600m, "Full restoration", new DateTime(2019, 4, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 9900m, "LE", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(1991) },
                    { 2, new DateTime(2019, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jeep", "Liberty", new DateTime(2019, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 4500m, 350m, "Front wheel bearings", null, 5350m, "Sport", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(2007) },
                    { 3, new DateTime(2019, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ford", "Explorer", new DateTime(2019, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 24350m, 1100m, "Tyres, brakes", null, 25950m, "XLT", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(2017) },
                    { 4, new DateTime(2019, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Honda", "Civic", new DateTime(2019, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 4000m, 475m, "Ac, brakes", new DateTime(2019, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 4975m, "LX", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(2008) },
                    { 5, new DateTime(2019, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Volkswagen", "GTI", new DateTime(2019, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 15250m, 440m, "Tyres", new DateTime(2019, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 16190m, "S", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(2016) },
                    { 6, new DateTime(2019, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ford", "Edge", new DateTime(2019, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 10990m, 950m, "Tyres, brakes, AC", new DateTime(2019, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 12440m, "SEL", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(2013) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_ListingEntityId",
                table: "Images",
                column: "ListingEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Listings_CarForeignKey",
                table: "Listings",
                column: "CarForeignKey",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Listings");

            migrationBuilder.DropTable(
                name: "Cars");
        }
    }
}
