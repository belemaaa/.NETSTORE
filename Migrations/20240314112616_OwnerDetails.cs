using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _netstore.Migrations
{
    public partial class OwnerDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0894b2e8-a7aa-4f8b-8c51-478ce85254a7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1d5b09de-07a4-42a0-97c6-1c1320ac246a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d38b5d92-e1df-4706-8161-a6fc5198e0f6", "5fb9ed48-7286-4ec6-aead-3a0cf76b8835", "Member", "MEMBER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d96b4db8-9f2d-45bb-80b1-815fb81d8490", "95e14dea-02ee-48a7-839b-768edbf4244a", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d38b5d92-e1df-4706-8161-a6fc5198e0f6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d96b4db8-9f2d-45bb-80b1-815fb81d8490");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0894b2e8-a7aa-4f8b-8c51-478ce85254a7", "933ef749-df43-4b7f-bbdf-7a7cdc51df60", "Member", "MEMBER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1d5b09de-07a4-42a0-97c6-1c1320ac246a", "5cd88865-5f3b-484a-bd84-da548871141c", "Admin", "ADMIN" });
        }
    }
}
