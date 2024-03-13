using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _netstore.Migrations
{
    public partial class UpdatedProductTypeField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "12bc868c-ea75-45db-8991-e0f3003db090");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "43152688-0087-42b0-9f44-c4c9da9bdc3d");

            migrationBuilder.AlterColumn<string>(
                name: "ProductType",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0894b2e8-a7aa-4f8b-8c51-478ce85254a7", "933ef749-df43-4b7f-bbdf-7a7cdc51df60", "Member", "MEMBER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1d5b09de-07a4-42a0-97c6-1c1320ac246a", "5cd88865-5f3b-484a-bd84-da548871141c", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0894b2e8-a7aa-4f8b-8c51-478ce85254a7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1d5b09de-07a4-42a0-97c6-1c1320ac246a");

            migrationBuilder.AlterColumn<int>(
                name: "ProductType",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "12bc868c-ea75-45db-8991-e0f3003db090", "463814ee-3de0-44e9-85f3-8d0df90d0e52", "Member", "MEMBER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "43152688-0087-42b0-9f44-c4c9da9bdc3d", "55b04523-8040-4cc6-a26b-8ebb855a9e5f", "Admin", "ADMIN" });
        }
    }
}
