using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ManagementAsset.Migrations
{
    /// <inheritdoc />
    public partial class SeedAssets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Assets",
                columns: new[] { "Id", "AssetCode", "AssignedToUserId", "CategoryId", "CreatedAt", "Description", "LocationId", "Name", "PurchaseDate", "PurchasePrice", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "AST-001", null, 1, new DateTime(2023, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, "Laptop Dell Inspiron 15", new DateTime(2023, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 9500000m, 0, null },
                    { 2, "AST-002", null, 1, new DateTime(2023, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, "Laptop Lenovo ThinkPad", new DateTime(2023, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 12000000m, 1, null },
                    { 3, "AST-003", null, 1, new DateTime(2023, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2, "Printer Canon G2020", new DateTime(2023, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2300000m, 0, null },
                    { 4, "AST-004", null, 2, new DateTime(2022, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, "Meja Kerja Kayu", new DateTime(2022, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1500000m, 0, null },
                    { 5, "AST-005", null, 2, new DateTime(2022, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, "Kursi Ergonomis", new DateTime(2022, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 2800000m, 0, null },
                    { 6, "AST-006", null, 2, new DateTime(2022, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2, "Lemari Arsip", new DateTime(2022, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1200000m, 0, null },
                    { 7, "AST-007", null, 3, new DateTime(2021, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, "Motor Honda Vario", new DateTime(2021, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 21000000m, 0, null },
                    { 8, "AST-008", null, 3, new DateTime(2020, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2, "Mobil Toyota Avanza", new DateTime(2020, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 185000000m, 2, null },
                    { 9, "AST-009", null, 1, new DateTime(2023, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, "Monitor LG 24 inch", new DateTime(2023, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 3200000m, 0, null },
                    { 10, "AST-010", null, 1, new DateTime(2022, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2, "Proyektor Epson", new DateTime(2022, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 7500000m, 0, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
