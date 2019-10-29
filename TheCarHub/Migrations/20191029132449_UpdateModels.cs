using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TheCarHub.Migrations
{
    public partial class UpdateModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Listings_ListingEntityId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Listings_Cars_CarForeignKey",
                table: "Listings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Listings",
                table: "Listings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Images",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_ListingEntityId",
                table: "Images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cars",
                table: "Cars");

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

            migrationBuilder.DropColumn(
                name: "ListingId",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "ImageEntityId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ListingEntityId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "LotDate",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "PurchaseDate",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "PurchasePrice",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "RepairCost",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Repairs",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "SaleDate",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "SellingPrice",
                table: "Cars");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Listings",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "LotDate",
                table: "Listings",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "PurchaseDate",
                table: "Listings",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "PurchasePrice",
                table: "Listings",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RepairCost",
                table: "Listings",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Repairs",
                table: "Listings",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SaleDate",
                table: "Listings",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SellingPrice",
                table: "Listings",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Images",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ListingId",
                table: "Images",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Cars",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Listings",
                table: "Listings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Images",
                table: "Images",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cars",
                table: "Cars",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Repairs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(nullable: true),
                    Cost = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repairs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TagName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarRepair",
                columns: table => new
                {
                    CarId = table.Column<int>(nullable: false),
                    RepairID = table.Column<int>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "ListingTag",
                columns: table => new
                {
                    ListingId = table.Column<int>(nullable: false),
                    TagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListingTag", x => new { x.ListingId, x.TagId });
                    table.ForeignKey(
                        name: "FK_ListingTag_Listings_ListingId",
                        column: x => x.ListingId,
                        principalTable: "Listings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ListingTag_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MediaTag",
                columns: table => new
                {
                    TagId = table.Column<int>(nullable: false),
                    MediaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaTag", x => new { x.TagId, x.MediaId });
                    table.ForeignKey(
                        name: "FK_MediaTag_Images_MediaId",
                        column: x => x.MediaId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MediaTag_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "Make", "Model", "Trim", "VIN", "Year" },
                values: new object[,]
                {
                    { 1, "Mazda", "Miata", "LE", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(1991) },
                    { 2, "Jeep", "Liberty", "Sport", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(2007) },
                    { 3, "Ford", "Explorer", "XLT", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(2017) },
                    { 4, "Honda", "Civic", "LX", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(2008) },
                    { 5, "Volkswagen", "GTI", "S", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(2016) },
                    { 6, "Ford", "Edge", "SEL", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(2013) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_ListingId",
                table: "Images",
                column: "ListingId");

            migrationBuilder.CreateIndex(
                name: "IX_CarRepair_RepairID",
                table: "CarRepair",
                column: "RepairID");

            migrationBuilder.CreateIndex(
                name: "IX_ListingTag_TagId",
                table: "ListingTag",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_MediaTag_MediaId",
                table: "MediaTag",
                column: "MediaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Listings_ListingId",
                table: "Images",
                column: "ListingId",
                principalTable: "Listings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_Cars_CarForeignKey",
                table: "Listings",
                column: "CarForeignKey",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Listings_ListingId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Listings_Cars_CarForeignKey",
                table: "Listings");

            migrationBuilder.DropTable(
                name: "CarRepair");

            migrationBuilder.DropTable(
                name: "ListingTag");

            migrationBuilder.DropTable(
                name: "MediaTag");

            migrationBuilder.DropTable(
                name: "Repairs");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Listings",
                table: "Listings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Images",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_ListingId",
                table: "Images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cars",
                table: "Cars");

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "LotDate",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "PurchaseDate",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "PurchasePrice",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "RepairCost",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "Repairs",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "SaleDate",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "SellingPrice",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ListingId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Cars");

            migrationBuilder.AddColumn<int>(
                name: "ListingId",
                table: "Listings",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ImageEntityId",
                table: "Images",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ListingEntityId",
                table: "Images",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CarId",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "LotDate",
                table: "Cars",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "PurchaseDate",
                table: "Cars",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "PurchasePrice",
                table: "Cars",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RepairCost",
                table: "Cars",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Repairs",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SaleDate",
                table: "Cars",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SellingPrice",
                table: "Cars",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Listings",
                table: "Listings",
                column: "ListingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Images",
                table: "Images",
                column: "ImageEntityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cars",
                table: "Cars",
                column: "CarId");

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "CarId", "LotDate", "Make", "Model", "PurchaseDate", "PurchasePrice", "RepairCost", "Repairs", "SaleDate", "SellingPrice", "Trim", "VIN", "Year" },
                values: new object[,]
                {
                    { 1, new DateTime(2019, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mazda", "Miata", new DateTime(2019, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 1800m, 7600m, "Full restoration", new DateTime(2019, 4, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 9900m, "LE", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(1991) },
                    { 2, new DateTime(2019, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jeep", "Liberty", new DateTime(2019, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 4500m, 350m, "Front wheel bearings", null, 5350m, "Sport", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(2007) },
                    { 3, new DateTime(2019, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ford", "Explorer", new DateTime(2019, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 24350m, 1100m, "Tyres, brakes", null, 25950m, "XLT", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(2017) },
                    { 4, new DateTime(2019, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Honda", "Civic", new DateTime(2019, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 4000m, 475m, "Ac, brakes", new DateTime(2019, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 4975m, "LX", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(2008) },
                    { 5, new DateTime(2019, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Volkswagen", "GTI", new DateTime(2019, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 15250m, 440m, "Tyres", new DateTime(2019, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 16190m, "S", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(2016) },
                    { 6, new DateTime(2019, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ford", "Edge", new DateTime(2019, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 10990m, 950m, "Tyres, brakes, AC", new DateTime(2019, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 12440m, "SEL", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(2013) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_ListingEntityId",
                table: "Images",
                column: "ListingEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Listings_ListingEntityId",
                table: "Images",
                column: "ListingEntityId",
                principalTable: "Listings",
                principalColumn: "ListingId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_Cars_CarForeignKey",
                table: "Listings",
                column: "CarForeignKey",
                principalTable: "Cars",
                principalColumn: "CarId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
