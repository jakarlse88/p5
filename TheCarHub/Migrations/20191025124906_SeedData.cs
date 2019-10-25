using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TheCarHub.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Listings",
                columns: new[] { "ListingItemId", "DateCreated", "DateLastUpdated", "Description", "ListingStatus" },
                values: new object[,]
                {
                    { 1, new DateTime(2019, 10, 25, 14, 49, 6, 272, DateTimeKind.Local).AddTicks(4370), null, "", "Available" },
                    { 2, new DateTime(2019, 10, 25, 14, 49, 6, 279, DateTimeKind.Local).AddTicks(2010), null, "", "Available" },
                    { 3, new DateTime(2019, 10, 25, 14, 49, 6, 279, DateTimeKind.Local).AddTicks(2070), null, "", "Available" },
                    { 4, new DateTime(2019, 10, 25, 14, 49, 6, 279, DateTimeKind.Local).AddTicks(2080), null, "", "Available" },
                    { 5, new DateTime(2019, 10, 25, 14, 49, 6, 279, DateTimeKind.Local).AddTicks(2080), null, "", "Available" },
                    { 6, new DateTime(2019, 10, 25, 14, 49, 6, 279, DateTimeKind.Local).AddTicks(2090), null, "", "Available" }
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "CarId", "ListingItemId", "LotDate", "Make", "Model", "PurchaseDate", "PurchasePrice", "RepairCost", "Repairs", "SaleDate", "SellingPrice", "Trim", "VIN", "Year" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2019, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mazda", "Miata", new DateTime(2019, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 1800m, 7600m, "Full restoration", new DateTime(2019, 4, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 9900m, "LE", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(1991) },
                    { 2, 2, new DateTime(2019, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jeep", "Liberty", new DateTime(2019, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 4500m, 350m, "Front wheel bearings", null, 5350m, "Sport", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(2007) },
                    { 3, 3, new DateTime(2019, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ford", "Explorer", new DateTime(2019, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 24350m, 1100m, "Tyres, brakes", null, 25950m, "XLT", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(2017) },
                    { 4, 4, new DateTime(2019, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Honda", "Civic", new DateTime(2019, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 4000m, 475m, "Ac, brakes", new DateTime(2019, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 4975m, "LX", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(2008) },
                    { 5, 5, new DateTime(2019, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Volkswagen", "GTI", new DateTime(2019, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 15250m, 440m, "Tyres", new DateTime(2019, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 16190m, "S", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(2016) },
                    { 6, 6, new DateTime(2019, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ford", "Edge", new DateTime(2019, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 10990m, 950m, "Tyres, brakes, AC", new DateTime(2019, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 12440m, "SEL", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(2013) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Listings",
                keyColumn: "ListingItemId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Listings",
                keyColumn: "ListingItemId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Listings",
                keyColumn: "ListingItemId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Listings",
                keyColumn: "ListingItemId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Listings",
                keyColumn: "ListingItemId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Listings",
                keyColumn: "ListingItemId",
                keyValue: 6);
        }
    }
}
