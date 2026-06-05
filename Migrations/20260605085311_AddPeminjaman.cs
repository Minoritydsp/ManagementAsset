using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManagementAsset.Migrations
{
    /// <inheritdoc />
    public partial class AddPeminjaman : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Peminjamans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssetId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApprovedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Keperluan = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TanggalPinjam = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TanggalDisetujui = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TanggalKembali = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CatatanAdmin = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Peminjamans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Peminjamans_AspNetUsers_ApprovedByUserId",
                        column: x => x.ApprovedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Peminjamans_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Peminjamans_Assets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Peminjamans_ApprovedByUserId",
                table: "Peminjamans",
                column: "ApprovedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Peminjamans_AssetId",
                table: "Peminjamans",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_Peminjamans_UserId",
                table: "Peminjamans",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Peminjamans");
        }
    }
}
