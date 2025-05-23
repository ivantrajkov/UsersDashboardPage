using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserAuthentication.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "PasswordHash", "RefreshToken", "RefreshTokenExpiryTime", "Role", "Username" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "AQAAAAIAAYagAAAAEH9MUVqZAd1HJosVdpILz2HzVbafsV1UNW7uBb6ZlquyAnoT7hV9y/FtkldxdkhgzQ==", null, null, 1, "admin" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "AQAAAAIAAYagAAAAEEGKNzLpcJjG40N9LOWG+6BZg8mHNvR6XLtut5Vm8AQRx4y7NnVw05rT193He3UwGw==", null, null, 0, "user1" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "AQAAAAIAAYagAAAAEFHBUMJsZL1Jj+wR8Szfr/fDa51V4XTp2xANQslpxsicfFHWvlxXB+E0oxaxDUmE3g==", null, null, 0, "user2" },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "AQAAAAIAAYagAAAAENfqHUq9MwyEP9BAY9x+rvumK8ZR4b07d5SXCkutS69KPo2F8NGjzrPrAOUYxd66Qw==", null, null, 0, "user3" },
                    { new Guid("55555555-5555-5555-5555-555555555555"), "AQAAAAIAAYagAAAAEPGaYIJS4Uhqg0Sb31S+g932GOvjKPZrdQvtQmGeNzfsGdWgAnI/7DQ9iKtqW2Xwdw==", null, null, 0, "user4" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"));
        }
    }
}
