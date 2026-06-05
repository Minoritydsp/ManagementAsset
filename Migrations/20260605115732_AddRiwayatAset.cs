using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManagementAsset.Migrations
{
    /// <inheritdoc />
    public partial class AddRiwayatAset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RiwayatAsets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssetId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Aksi = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    StatusLama = table.Column<int>(type: "int", nullable: true),
                    StatusBaru = table.Column<int>(type: "int", nullable: true),
                    Keterangan = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TanggalAksi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiwayatAsets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RiwayatAsets_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RiwayatAsets_Assets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RiwayatAsets_AssetId",
                table: "RiwayatAsets",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_RiwayatAsets_UserId",
                table: "RiwayatAsets",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RiwayatAsets");
        }
    }
}
