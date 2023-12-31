using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UsersAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedUsersTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "DateTime", "Email", "FirstName", "LastName", "Login", "Password", "Phone", "Updated" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "myevropa1@gmail.com", "Dmitry", "Vagner", "Vagner", "123456", "+420776544634", new DateTime(2023, 12, 31, 13, 41, 56, 146, DateTimeKind.Local).AddTicks(5610) },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "myevropa1@gmail.com", "Dmitry", "Vagner", "Vagner", "123456", "+420776544634", new DateTime(2023, 12, 31, 13, 41, 56, 146, DateTimeKind.Local).AddTicks(5687) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "Users");
        }
    }
}
