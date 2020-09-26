using Microsoft.EntityFrameworkCore.Migrations;

namespace CommentoIntegrationTest.Migrations
{
    public partial class ModelRepair : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fe70eeac-e31a-4ca4-88f7-a1a2ea58339b");

            migrationBuilder.AlterColumn<int>(
                name: "Age",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ecfc1009-d1d2-4899-9123-8fa8edc42822", "6c92f7cc-b25a-4bb3-b6f3-6e363f9eee79", "RegisteredUser", "REGISTEREDUSER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ecfc1009-d1d2-4899-9123-8fa8edc42822");

            migrationBuilder.AlterColumn<string>(
                name: "Age",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fe70eeac-e31a-4ca4-88f7-a1a2ea58339b", "9bea455a-4ca0-43fd-b3e8-d364ceaddfbf", "RegisteredUser", "REGISTEREDUSER" });
        }
    }
}
