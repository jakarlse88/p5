using Microsoft.EntityFrameworkCore.Migrations;

namespace TheCarHub.Migrations.AppIdentityDb
{
    public partial class SeedIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a0d03abe-b7a5-4be8-b1a2-ac2bf2ff313e", "1655d15e-a4ed-453b-86ee-2677a78a4da3", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "7f3ffce7-91c8-4fba-a79a-5f89f82e1b28", 0, "38a72eae-ffe8-4bf5-8eac-f1b94099c246", "admin@admin.com", true, false, null, "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAEAACcQAAAAEEIfKQO3oYYcsjVzM4mXOm2ymb78OJ1UhugAYqkg2A9Sg8TaZdBFNN355bcdTO1e9w==", "123456789", false, "a43641fb-bd07-48a1-8b74-8f1c671f497b", false, "admin@admin.com" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a0d03abe-b7a5-4be8-b1a2-ac2bf2ff313e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7f3ffce7-91c8-4fba-a79a-5f89f82e1b28");
        }
    }
}
