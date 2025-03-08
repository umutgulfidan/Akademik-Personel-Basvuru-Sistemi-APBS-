using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class mig_added_all_basvuru_tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BasvuruDurumlari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasvuruDurumlari", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IlanBasvurulari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IlanId = table.Column<int>(type: "int", nullable: false),
                    BasvuranId = table.Column<int>(type: "int", nullable: false),
                    BasvuruDurumuId = table.Column<int>(type: "int", nullable: false),
                    BasvuruTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IlanBasvurulari", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IlanBasvurulari_BasvuruDurumlari_BasvuruDurumuId",
                        column: x => x.BasvuruDurumuId,
                        principalTable: "BasvuruDurumlari",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IlanBasvurulari_Ilanlar_IlanId",
                        column: x => x.IlanId,
                        principalTable: "Ilanlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IlanBasvurulari_Users_BasvuranId",
                        column: x => x.BasvuranId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BasvuruDosyalari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BasvuruId = table.Column<int>(type: "int", nullable: false),
                    DosyaYolu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YuklenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasvuruDosyalari", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BasvuruDosyalari_IlanBasvurulari_BasvuruId",
                        column: x => x.BasvuruId,
                        principalTable: "IlanBasvurulari",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BasvuruJurileri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KullaniciId = table.Column<int>(type: "int", nullable: false),
                    BasvuruId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasvuruJurileri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BasvuruJurileri_IlanBasvurulari_BasvuruId",
                        column: x => x.BasvuruId,
                        principalTable: "IlanBasvurulari",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BasvuruJurileri_Users_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RaporDosyalari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BasvuruId = table.Column<int>(type: "int", nullable: false),
                    JuriId = table.Column<int>(type: "int", nullable: false),
                    DosyaYolu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YuklenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaporDosyalari", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RaporDosyalari_BasvuruJurileri_JuriId",
                        column: x => x.JuriId,
                        principalTable: "BasvuruJurileri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RaporDosyalari_IlanBasvurulari_BasvuruId",
                        column: x => x.BasvuruId,
                        principalTable: "IlanBasvurulari",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BasvuruDosyalari_BasvuruId",
                table: "BasvuruDosyalari",
                column: "BasvuruId");

            migrationBuilder.CreateIndex(
                name: "IX_BasvuruJurileri_BasvuruId",
                table: "BasvuruJurileri",
                column: "BasvuruId");

            migrationBuilder.CreateIndex(
                name: "IX_BasvuruJurileri_KullaniciId",
                table: "BasvuruJurileri",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_IlanBasvurulari_BasvuranId",
                table: "IlanBasvurulari",
                column: "BasvuranId");

            migrationBuilder.CreateIndex(
                name: "IX_IlanBasvurulari_BasvuruDurumuId",
                table: "IlanBasvurulari",
                column: "BasvuruDurumuId");

            migrationBuilder.CreateIndex(
                name: "IX_IlanBasvurulari_IlanId",
                table: "IlanBasvurulari",
                column: "IlanId");

            migrationBuilder.CreateIndex(
                name: "IX_RaporDosyalari_BasvuruId",
                table: "RaporDosyalari",
                column: "BasvuruId");

            migrationBuilder.CreateIndex(
                name: "IX_RaporDosyalari_JuriId",
                table: "RaporDosyalari",
                column: "JuriId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BasvuruDosyalari");

            migrationBuilder.DropTable(
                name: "RaporDosyalari");

            migrationBuilder.DropTable(
                name: "BasvuruJurileri");

            migrationBuilder.DropTable(
                name: "IlanBasvurulari");

            migrationBuilder.DropTable(
                name: "BasvuruDurumlari");
        }
    }
}
