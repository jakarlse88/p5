using Microsoft.EntityFrameworkCore.Migrations;

namespace TheCarHub.Migrations
{
    public partial class ListingEntityRepairJob : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RepairJob_ListingId",
                table: "RepairJob");

            migrationBuilder.CreateIndex(
                name: "IX_RepairJob_ListingId",
                table: "RepairJob",
                column: "ListingId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RepairJob_ListingId",
                table: "RepairJob");

            migrationBuilder.CreateIndex(
                name: "IX_RepairJob_ListingId",
                table: "RepairJob",
                column: "ListingId");
        }
    }
}
