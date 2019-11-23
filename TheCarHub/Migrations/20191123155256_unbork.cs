using Microsoft.EntityFrameworkCore.Migrations;

namespace TheCarHub.Migrations
{
    public partial class unbork : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CarId1",
                table: "Listing",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Listing_CarId1",
                table: "Listing",
                column: "CarId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Listing_Car_CarId1",
                table: "Listing",
                column: "CarId1",
                principalTable: "Car",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Listing_Car_CarId1",
                table: "Listing");

            migrationBuilder.DropIndex(
                name: "IX_Listing_CarId1",
                table: "Listing");

            migrationBuilder.DropColumn(
                name: "CarId1",
                table: "Listing");
        }
    }
}
