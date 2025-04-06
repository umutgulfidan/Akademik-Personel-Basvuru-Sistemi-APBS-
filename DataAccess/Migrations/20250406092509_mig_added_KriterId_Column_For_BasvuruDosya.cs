using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class mig_added_KriterId_Column_For_BasvuruDosya : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KriterId",
                table: "BasvuruDosyalari",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BasvuruDosyalari_KriterId",
                table: "BasvuruDosyalari",
                column: "KriterId");

            migrationBuilder.AddForeignKey(
                name: "FK_BasvuruDosyalari_Kriterler_KriterId",
                table: "BasvuruDosyalari",
                column: "KriterId",
                principalTable: "Kriterler",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasvuruDosyalari_Kriterler_KriterId",
                table: "BasvuruDosyalari");

            migrationBuilder.DropIndex(
                name: "IX_BasvuruDosyalari_KriterId",
                table: "BasvuruDosyalari");

            migrationBuilder.DropColumn(
                name: "KriterId",
                table: "BasvuruDosyalari");
        }
    }
}
