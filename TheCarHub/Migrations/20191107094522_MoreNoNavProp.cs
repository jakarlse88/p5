using Microsoft.EntityFrameworkCore.Migrations;

namespace TheCarHub.Migrations
{
    public partial class MoreNoNavProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ListingTag_Listing_ListingId",
                table: "ListingTag");

            migrationBuilder.DropForeignKey(
                name: "FK_ListingTag_Tag_TagId",
                table: "ListingTag");

            migrationBuilder.DropForeignKey(
                name: "FK_Media_Listing_ListingId",
                table: "Media");

            migrationBuilder.DropForeignKey(
                name: "FK_MediaTag_Media_MediaId",
                table: "MediaTag");

            migrationBuilder.DropForeignKey(
                name: "FK_MediaTag_Tag_TagId",
                table: "MediaTag");

            migrationBuilder.DropForeignKey(
                name: "FK_RepairJob_Listing_ListingId",
                table: "RepairJob");

            migrationBuilder.DropIndex(
                name: "IX_RepairJob_ListingId",
                table: "RepairJob");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MediaTag",
                table: "MediaTag");

            migrationBuilder.DropIndex(
                name: "IX_MediaTag_MediaId",
                table: "MediaTag");

            migrationBuilder.DropIndex(
                name: "IX_Media_ListingId",
                table: "Media");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ListingTag",
                table: "ListingTag");

            migrationBuilder.DropIndex(
                name: "IX_ListingTag_TagId",
                table: "ListingTag");

            migrationBuilder.DropColumn(
                name: "ListingId",
                table: "RepairJob");

            migrationBuilder.DropColumn(
                name: "TagId",
                table: "MediaTag");

            migrationBuilder.DropColumn(
                name: "MediaId",
                table: "MediaTag");

            migrationBuilder.DropColumn(
                name: "ListingId",
                table: "Media");

            migrationBuilder.DropColumn(
                name: "ListingId",
                table: "ListingTag");

            migrationBuilder.DropColumn(
                name: "TagId",
                table: "ListingTag");

            migrationBuilder.AlterColumn<decimal>(
                name: "Tax",
                table: "RepairJob",
                type: "money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<int>(
                name: "ListingForeignKey",
                table: "RepairJob",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TagForeignKey",
                table: "MediaTag",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MediaForeignKey",
                table: "MediaTag",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ListingForeignKey",
                table: "Media",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ListingForeignKey",
                table: "ListingTag",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TagForeignKey",
                table: "ListingTag",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MediaTag",
                table: "MediaTag",
                columns: new[] { "TagForeignKey", "MediaForeignKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ListingTag",
                table: "ListingTag",
                columns: new[] { "ListingForeignKey", "TagForeignKey" });

            migrationBuilder.CreateIndex(
                name: "IX_RepairJob_ListingForeignKey",
                table: "RepairJob",
                column: "ListingForeignKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MediaTag_MediaForeignKey",
                table: "MediaTag",
                column: "MediaForeignKey");

            migrationBuilder.CreateIndex(
                name: "IX_Media_ListingForeignKey",
                table: "Media",
                column: "ListingForeignKey");

            migrationBuilder.CreateIndex(
                name: "IX_ListingTag_TagForeignKey",
                table: "ListingTag",
                column: "TagForeignKey");

            migrationBuilder.AddForeignKey(
                name: "FK_ListingTag_Listing_ListingForeignKey",
                table: "ListingTag",
                column: "ListingForeignKey",
                principalTable: "Listing",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ListingTag_Tag_TagForeignKey",
                table: "ListingTag",
                column: "TagForeignKey",
                principalTable: "Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Media_Listing_ListingForeignKey",
                table: "Media",
                column: "ListingForeignKey",
                principalTable: "Listing",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MediaTag_Media_MediaForeignKey",
                table: "MediaTag",
                column: "MediaForeignKey",
                principalTable: "Media",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MediaTag_Tag_TagForeignKey",
                table: "MediaTag",
                column: "TagForeignKey",
                principalTable: "Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RepairJob_Listing_ListingForeignKey",
                table: "RepairJob",
                column: "ListingForeignKey",
                principalTable: "Listing",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ListingTag_Listing_ListingForeignKey",
                table: "ListingTag");

            migrationBuilder.DropForeignKey(
                name: "FK_ListingTag_Tag_TagForeignKey",
                table: "ListingTag");

            migrationBuilder.DropForeignKey(
                name: "FK_Media_Listing_ListingForeignKey",
                table: "Media");

            migrationBuilder.DropForeignKey(
                name: "FK_MediaTag_Media_MediaForeignKey",
                table: "MediaTag");

            migrationBuilder.DropForeignKey(
                name: "FK_MediaTag_Tag_TagForeignKey",
                table: "MediaTag");

            migrationBuilder.DropForeignKey(
                name: "FK_RepairJob_Listing_ListingForeignKey",
                table: "RepairJob");

            migrationBuilder.DropIndex(
                name: "IX_RepairJob_ListingForeignKey",
                table: "RepairJob");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MediaTag",
                table: "MediaTag");

            migrationBuilder.DropIndex(
                name: "IX_MediaTag_MediaForeignKey",
                table: "MediaTag");

            migrationBuilder.DropIndex(
                name: "IX_Media_ListingForeignKey",
                table: "Media");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ListingTag",
                table: "ListingTag");

            migrationBuilder.DropIndex(
                name: "IX_ListingTag_TagForeignKey",
                table: "ListingTag");

            migrationBuilder.DropColumn(
                name: "ListingForeignKey",
                table: "RepairJob");

            migrationBuilder.DropColumn(
                name: "TagForeignKey",
                table: "MediaTag");

            migrationBuilder.DropColumn(
                name: "MediaForeignKey",
                table: "MediaTag");

            migrationBuilder.DropColumn(
                name: "ListingForeignKey",
                table: "Media");

            migrationBuilder.DropColumn(
                name: "ListingForeignKey",
                table: "ListingTag");

            migrationBuilder.DropColumn(
                name: "TagForeignKey",
                table: "ListingTag");

            migrationBuilder.AlterColumn<decimal>(
                name: "Tax",
                table: "RepairJob",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money");

            migrationBuilder.AddColumn<int>(
                name: "ListingId",
                table: "RepairJob",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TagId",
                table: "MediaTag",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MediaId",
                table: "MediaTag",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ListingId",
                table: "Media",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ListingId",
                table: "ListingTag",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TagId",
                table: "ListingTag",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MediaTag",
                table: "MediaTag",
                columns: new[] { "TagId", "MediaId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ListingTag",
                table: "ListingTag",
                columns: new[] { "ListingId", "TagId" });

            migrationBuilder.CreateIndex(
                name: "IX_RepairJob_ListingId",
                table: "RepairJob",
                column: "ListingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MediaTag_MediaId",
                table: "MediaTag",
                column: "MediaId");

            migrationBuilder.CreateIndex(
                name: "IX_Media_ListingId",
                table: "Media",
                column: "ListingId");

            migrationBuilder.CreateIndex(
                name: "IX_ListingTag_TagId",
                table: "ListingTag",
                column: "TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_ListingTag_Listing_ListingId",
                table: "ListingTag",
                column: "ListingId",
                principalTable: "Listing",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ListingTag_Tag_TagId",
                table: "ListingTag",
                column: "TagId",
                principalTable: "Tag",
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
                name: "FK_MediaTag_Media_MediaId",
                table: "MediaTag",
                column: "MediaId",
                principalTable: "Media",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MediaTag_Tag_TagId",
                table: "MediaTag",
                column: "TagId",
                principalTable: "Tag",
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
    }
}
