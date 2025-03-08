using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class mig_added_kriterler_alankriterleri_puankriterleri_tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kriterler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kriterler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AlanKriterleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KriterId = table.Column<int>(type: "int", nullable: false),
                    AlanId = table.Column<int>(type: "int", nullable: false),
                    PozisyonId = table.Column<int>(type: "int", nullable: false),
                    MinAdet = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlanKriterleri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlanKriterleri_Alanlar_AlanId",
                        column: x => x.AlanId,
                        principalTable: "Alanlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlanKriterleri_Kriterler_KriterId",
                        column: x => x.KriterId,
                        principalTable: "Kriterler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlanKriterleri_Pozisyonlar_PozisyonId",
                        column: x => x.PozisyonId,
                        principalTable: "Pozisyonlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PuanKriterleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KriterId = table.Column<int>(type: "int", nullable: false),
                    AlanId = table.Column<int>(type: "int", nullable: false),
                    PozisyonId = table.Column<int>(type: "int", nullable: false),
                    MinPuan = table.Column<int>(type: "int", nullable: true),
                    MaxPuan = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PuanKriterleri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PuanKriterleri_Alanlar_AlanId",
                        column: x => x.AlanId,
                        principalTable: "Alanlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PuanKriterleri_Kriterler_KriterId",
                        column: x => x.KriterId,
                        principalTable: "Kriterler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PuanKriterleri_Pozisyonlar_PozisyonId",
                        column: x => x.PozisyonId,
                        principalTable: "Pozisyonlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlanKriterleri_AlanId",
                table: "AlanKriterleri",
                column: "AlanId");

            migrationBuilder.CreateIndex(
                name: "IX_AlanKriterleri_KriterId",
                table: "AlanKriterleri",
                column: "KriterId");

            migrationBuilder.CreateIndex(
                name: "IX_AlanKriterleri_PozisyonId",
                table: "AlanKriterleri",
                column: "PozisyonId");

            migrationBuilder.CreateIndex(
                name: "IX_PuanKriterleri_AlanId",
                table: "PuanKriterleri",
                column: "AlanId");

            migrationBuilder.CreateIndex(
                name: "IX_PuanKriterleri_KriterId",
                table: "PuanKriterleri",
                column: "KriterId");

            migrationBuilder.CreateIndex(
                name: "IX_PuanKriterleri_PozisyonId",
                table: "PuanKriterleri",
                column: "PozisyonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlanKriterleri");

            migrationBuilder.DropTable(
                name: "PuanKriterleri");

            migrationBuilder.DropTable(
                name: "Kriterler");
        }
    }
}
