using Microsoft.EntityFrameworkCore.Migrations;

namespace TheCarHub.Migrations
{
    public partial class MediaFilePath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Media");

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Media",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Media");

            migrationBuilder.AddColumn<byte[]>(
                name: "Content",
                table: "Media",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
