using Microsoft.EntityFrameworkCore.Migrations;

namespace TheCarHub.Migrations
{
    public partial class SingularTableNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Listings_Cars_CarForeignKey",
                table: "Listings");

            migrationBuilder.DropForeignKey(
                name: "FK_ListingTag_Listings_ListingId",
                table: "ListingTag");

            migrationBuilder.DropForeignKey(
                name: "FK_Media_Listings_ListingId",
                table: "Media");

            migrationBuilder.DropForeignKey(
                name: "FK_RepairJob_Listings_ListingId",
                table: "RepairJob");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Listings",
                table: "Listings");

            migrationBuilder.DropIndex(
                name: "IX_Listings_CarForeignKey",
                table: "Listings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cars",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "CarForeignKey",
                table: "Listings");

            migrationBuilder.RenameTable(
                name: "Listings",
                newName: "Listing");

            migrationBuilder.RenameTable(
                name: "Cars",
                newName: "Car");

            migrationBuilder.AddColumn<int>(
                name: "CarId",
                table: "Listing",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Listing",
                table: "Listing",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Car",
                table: "Car",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Listing",
                keyColumn: "Id",
                keyValue: 1,
                column: "CarId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Listing",
                keyColumn: "Id",
                keyValue: 2,
                column: "CarId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Listing",
                keyColumn: "Id",
                keyValue: 3,
                column: "CarId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Listing",
                keyColumn: "Id",
                keyValue: 4,
                column: "CarId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Listing",
                keyColumn: "Id",
                keyValue: 5,
                column: "CarId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Listing",
                keyColumn: "Id",
                keyValue: 6,
                column: "CarId",
                value: 6);

            migrationBuilder.CreateIndex(
                name: "IX_Listing_CarId",
                table: "Listing",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Listing_Car_CarId",
                table: "Listing",
                column: "CarId",
                principalTable: "Car",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ListingTag_Listing_ListingId",
                table: "ListingTag",
                column: "ListingId",
                principalTable: "Listing",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Media_Listing_ListingId",
                table: "Media",
                column: "ListingId",
                principalTable: "Listing",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RepairJob_Listing_ListingId",
                table: "RepairJob",
                column: "ListingId",
                principalTable: "Listing",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Listing_Car_CarId",
                table: "Listing");

            migrationBuilder.DropForeignKey(
                name: "FK_ListingTag_Listing_ListingId",
                table: "ListingTag");

            migrationBuilder.DropForeignKey(
                name: "FK_Media_Listing_ListingId",
                table: "Media");

            migrationBuilder.DropForeignKey(
                name: "FK_RepairJob_Listing_ListingId",
                table: "RepairJob");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Listing",
                table: "Listing");

            migrationBuilder.DropIndex(
                name: "IX_Listing_CarId",
                table: "Listing");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Car",
                table: "Car");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "Listing");

            migrationBuilder.RenameTable(
                name: "Listing",
                newName: "Listings");

            migrationBuilder.RenameTable(
                name: "Car",
                newName: "Cars");

            migrationBuilder.AddColumn<int>(
                name: "CarForeignKey",
                table: "Listings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Listings",
                table: "Listings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cars",
                table: "Cars",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Listings",
                keyColumn: "Id",
                keyValue: 1,
                column: "CarForeignKey",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Listings",
                keyColumn: "Id",
                keyValue: 2,
                column: "CarForeignKey",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Listings",
                keyColumn: "Id",
                keyValue: 3,
                column: "CarForeignKey",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Listings",
                keyColumn: "Id",
                keyValue: 4,
                column: "CarForeignKey",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Listings",
                keyColumn: "Id",
                keyValue: 5,
                column: "CarForeignKey",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Listings",
                keyColumn: "Id",
                keyValue: 6,
                column: "CarForeignKey",
                value: 6);

            migrationBuilder.CreateIndex(
                name: "IX_Listings_CarForeignKey",
                table: "Listings",
                column: "CarForeignKey");

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_Cars_CarForeignKey",
                table: "Listings",
                column: "CarForeignKey",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ListingTag_Listings_ListingId",
                table: "ListingTag",
                column: "ListingId",
                principalTable: "Listings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Media_Listings_ListingId",
                table: "Media",
                column: "ListingId",
                principalTable: "Listings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RepairJob_Listings_ListingId",
                table: "RepairJob",
                column: "ListingId",
                principalTable: "Listings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
