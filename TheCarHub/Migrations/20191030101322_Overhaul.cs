using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TheCarHub.Migrations
{
    public partial class Overhaul : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Listings_ListingId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_MediaTag_Images_MediaId",
                table: "MediaTag");

            migrationBuilder.DropTable(
                name: "CarRepair");

            migrationBuilder.DropTable(
                name: "Repairs");

            migrationBuilder.DropIndex(
                name: "IX_Listings_CarForeignKey",
                table: "Listings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Images",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "LotDate",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "RepairCost",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "Repairs",
                table: "Listings");

            migrationBuilder.RenameTable(
                name: "Images",
                newName: "Media");

            migrationBuilder.RenameIndex(
                name: "IX_Images_ListingId",
                table: "Media",
                newName: "IX_Media_ListingId");

            migrationBuilder.AlterColumn<decimal>(
                name: "SellingPrice",
                table: "Listings",
                type: "money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "PurchasePrice",
                table: "Listings",
                type: "money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Media",
                table: "Media",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "RepairJob",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListingId = table.Column<int>(nullable: false),
                    Hours = table.Column<DateTime>(nullable: false),
                    HourlyRate = table.Column<decimal>(type: "money", nullable: false),
                    Cost = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepairJob", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RepairJob_Listings_ListingId",
                        column: x => x.ListingId,
                        principalTable: "Listings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SparePart",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Cost = table.Column<decimal>(type: "money", nullable: false),
                    RepairJobId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SparePart", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SparePart_RepairJob_RepairJobId",
                        column: x => x.RepairJobId,
                        principalTable: "RepairJob",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Listings_CarForeignKey",
                table: "Listings",
                column: "CarForeignKey");

            migrationBuilder.CreateIndex(
                name: "IX_RepairJob_ListingId",
                table: "RepairJob",
                column: "ListingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SparePart_RepairJobId",
                table: "SparePart",
                column: "RepairJobId");

            migrationBuilder.AddForeignKey(
                name: "FK_Media_Listings_ListingId",
                table: "Media",
                column: "ListingId",
                principalTable: "Listings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MediaTag_Media_MediaId",
                table: "MediaTag",
                column: "MediaId",
                principalTable: "Media",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Media_Listings_ListingId",
                table: "Media");

            migrationBuilder.DropForeignKey(
                name: "FK_MediaTag_Media_MediaId",
                table: "MediaTag");

            migrationBuilder.DropTable(
                name: "SparePart");

            migrationBuilder.DropTable(
                name: "RepairJob");

            migrationBuilder.DropIndex(
                name: "IX_Listings_CarForeignKey",
                table: "Listings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Media",
                table: "Media");

            migrationBuilder.RenameTable(
                name: "Media",
                newName: "Images");

            migrationBuilder.RenameIndex(
                name: "IX_Media_ListingId",
                table: "Images",
                newName: "IX_Images_ListingId");

            migrationBuilder.AlterColumn<decimal>(
                name: "SellingPrice",
                table: "Listings",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money");

            migrationBuilder.AlterColumn<decimal>(
                name: "PurchasePrice",
                table: "Listings",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money");

            migrationBuilder.AddColumn<DateTime>(
                name: "LotDate",
                table: "Listings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "RepairCost",
                table: "Listings",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Repairs",
                table: "Listings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Images",
                table: "Images",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Repairs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repairs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarRepair",
                columns: table => new
                {
                    CarId = table.Column<int>(type: "int", nullable: false),
                    RepairID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarRepair", x => new { x.CarId, x.RepairID });
                    table.ForeignKey(
                        name: "FK_CarRepair_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarRepair_Repairs_RepairID",
                        column: x => x.RepairID,
                        principalTable: "Repairs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Listings_CarForeignKey",
                table: "Listings",
                column: "CarForeignKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CarRepair_RepairID",
                table: "CarRepair",
                column: "RepairID");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Listings_ListingId",
                table: "Images",
                column: "ListingId",
                principalTable: "Listings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MediaTag_Images_MediaId",
                table: "MediaTag",
                column: "MediaId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
