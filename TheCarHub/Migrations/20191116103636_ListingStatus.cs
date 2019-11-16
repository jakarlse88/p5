using Microsoft.EntityFrameworkCore.Migrations;

namespace TheCarHub.Migrations
{
    public partial class ListingStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TagName",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Listing");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Tag",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Listing",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Available" });

            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Sold" });

            migrationBuilder.UpdateData(
                table: "Listing",
                keyColumn: "Id",
                keyValue: 1,
                column: "StatusId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Listing",
                keyColumn: "Id",
                keyValue: 2,
                column: "StatusId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Listing",
                keyColumn: "Id",
                keyValue: 3,
                column: "StatusId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Listing",
                keyColumn: "Id",
                keyValue: 4,
                column: "StatusId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Listing",
                keyColumn: "Id",
                keyValue: 5,
                column: "StatusId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Listing",
                keyColumn: "Id",
                keyValue: 6,
                column: "StatusId",
                value: 2);

            migrationBuilder.CreateIndex(
                name: "IX_Listing_StatusId",
                table: "Listing",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Listing_Status_StatusId",
                table: "Listing",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Listing_Status_StatusId",
                table: "Listing");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropIndex(
                name: "IX_Listing_StatusId",
                table: "Listing");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Listing");

            migrationBuilder.AddColumn<string>(
                name: "TagName",
                table: "Tag",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Listing",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
