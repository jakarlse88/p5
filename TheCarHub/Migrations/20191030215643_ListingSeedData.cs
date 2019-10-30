using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TheCarHub.Migrations
{
    public partial class ListingSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ListingStatus",
                table: "Listings");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Listings",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Listings",
                columns: new[] { "Id", "CarForeignKey", "DateCreated", "DateLastUpdated", "Description", "PurchaseDate", "PurchasePrice", "SaleDate", "SellingPrice", "Status", "Title" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m, null, 0m, null, null },
                    { 2, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m, null, 0m, null, null },
                    { 3, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m, null, 0m, null, null },
                    { 4, 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m, null, 0m, null, null },
                    { 5, 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m, null, 0m, null, null },
                    { 6, 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m, null, 0m, null, null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Listings",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Listings",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Listings",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Listings",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Listings",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Listings",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Listings");

            migrationBuilder.AddColumn<string>(
                name: "ListingStatus",
                table: "Listings",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
