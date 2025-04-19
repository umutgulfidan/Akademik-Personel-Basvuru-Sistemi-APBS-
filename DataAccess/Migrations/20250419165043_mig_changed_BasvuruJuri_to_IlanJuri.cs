using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class mig_changed_BasvuruJuri_to_IlanJuri : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RaporDosyalari_BasvuruJurileri_JuriId",
                table: "RaporDosyalari");

            migrationBuilder.DropTable(
                name: "BasvuruJurileri");

            migrationBuilder.CreateTable(
                name: "IlanJurileri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KullaniciId = table.Column<int>(type: "int", nullable: false),
                    IlanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IlanJurileri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IlanJurileri_Ilanlar_IlanId",
                        column: x => x.IlanId,
                        principalTable: "Ilanlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IlanJurileri_Users_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IlanJurileri_IlanId",
                table: "IlanJurileri",
                column: "IlanId");

            migrationBuilder.CreateIndex(
                name: "IX_IlanJurileri_KullaniciId",
                table: "IlanJurileri",
                column: "KullaniciId");

            migrationBuilder.AddForeignKey(
                name: "FK_RaporDosyalari_IlanJurileri_JuriId",
                table: "RaporDosyalari",
                column: "JuriId",
                principalTable: "IlanJurileri",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RaporDosyalari_IlanJurileri_JuriId",
                table: "RaporDosyalari");

            migrationBuilder.DropTable(
                name: "IlanJurileri");

            migrationBuilder.CreateTable(
                name: "BasvuruJurileri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BasvuruId = table.Column<int>(type: "int", nullable: false),
                    KullaniciId = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_BasvuruJurileri_BasvuruId",
                table: "BasvuruJurileri",
                column: "BasvuruId");

            migrationBuilder.CreateIndex(
                name: "IX_BasvuruJurileri_KullaniciId",
                table: "BasvuruJurileri",
                column: "KullaniciId");

            migrationBuilder.AddForeignKey(
                name: "FK_RaporDosyalari_BasvuruJurileri_JuriId",
                table: "RaporDosyalari",
                column: "JuriId",
                principalTable: "BasvuruJurileri",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
