using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _netstore.Migrations
{
    public partial class Identity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4b8f2bd8-e14d-4c1b-b79b-e8624c1f753a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e5b234f0-c63c-484c-a2a1-3d93e3c47e71");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fca07470-b0f2-4cad-b8b5-dc0acacea3e0", "b96c963a-f1fb-4970-b9e4-e55d48cf8ea9", "Member", "MEMBER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fd2e5371-2c26-4ef5-940a-7b7122c14422", "b724bfc2-be27-4dc2-be6a-ff282ee4d58c", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fca07470-b0f2-4cad-b8b5-dc0acacea3e0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fd2e5371-2c26-4ef5-940a-7b7122c14422");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4b8f2bd8-e14d-4c1b-b79b-e8624c1f753a", "ab94f839-18dc-4b26-800c-188d0d2044f1", "Member", "MEMBER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e5b234f0-c63c-484c-a2a1-3d93e3c47e71", "d8ecd81e-cfc2-472f-8d48-960c5a11f333", "Admin", "ADMIN" });
        }
    }
}
