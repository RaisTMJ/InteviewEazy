using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recipe.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FinalSeedFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("dc6f83b7-6c9c-4032-b699-a84566b8d6ed"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "FirstName", "LastName", "PasswordHash", "ProfilePicture", "UpdatedAt" },
                values: new object[] { new Guid("c9120612-921c-4b7c-a5a9-c59714e8064f"), new DateTime(2025, 1, 1, 10, 0, 0, 0, DateTimeKind.Utc), "admin@example.com", "Admin", "User", "$2a$11$DTjutr9lesxUJqX.05P1we7.9eIZ5aXALiDMaJj8bdLi87NgD0PV2", null, new DateTime(2025, 1, 1, 10, 0, 0, 0, DateTimeKind.Utc) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c9120612-921c-4b7c-a5a9-c59714e8064f"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "FirstName", "LastName", "PasswordHash", "ProfilePicture", "UpdatedAt" },
                values: new object[] { new Guid("dc6f83b7-6c9c-4032-b699-a84566b8d6ed"), new DateTime(2025, 10, 2, 12, 52, 26, 90, DateTimeKind.Utc).AddTicks(395), "admin@example.com", "Admin", "User", "$2a$11$DTjutr9lesxUJqX.05P1we7.9eIZ5aXALiDMaJj8bdLi87NgD0PV2", null, new DateTime(2025, 10, 2, 12, 52, 26, 90, DateTimeKind.Utc).AddTicks(399) });
        }
    }
}
