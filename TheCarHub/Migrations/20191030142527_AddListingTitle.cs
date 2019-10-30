using Microsoft.EntityFrameworkCore.Migrations;

namespace TheCarHub.Migrations
{
    public partial class AddListingTitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Listings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Listings");
        }
    }
}
