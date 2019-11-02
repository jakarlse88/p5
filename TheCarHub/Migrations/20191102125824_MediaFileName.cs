using Microsoft.EntityFrameworkCore.Migrations;

namespace TheCarHub.Migrations
{
    public partial class MediaFileName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Media");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Media",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Media");

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Media",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
