using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Formula1.Migrations
{
    /// <inheritdoc />
    public partial class equipeNoResultado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EquipeId",
                table: "ResultadoCorridas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ResultadoCorridas_EquipeId",
                table: "ResultadoCorridas",
                column: "EquipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ResultadoCorridas_Equipes_EquipeId",
                table: "ResultadoCorridas",
                column: "EquipeId",
                principalTable: "Equipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResultadoCorridas_Equipes_EquipeId",
                table: "ResultadoCorridas");

            migrationBuilder.DropIndex(
                name: "IX_ResultadoCorridas_EquipeId",
                table: "ResultadoCorridas");

            migrationBuilder.DropColumn(
                name: "EquipeId",
                table: "ResultadoCorridas");
        }
    }
}
