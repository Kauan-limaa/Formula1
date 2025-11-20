using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Formula1.Migrations
{
    /// <inheritdoc />
    public partial class quartaMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PontuacaoPilotoTemporada",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PilotoId = table.Column<int>(type: "int", nullable: false),
                    TemporadaId = table.Column<int>(type: "int", nullable: false),
                    Pontos = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PontuacaoPilotoTemporada", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PontuacaoPilotoTemporada_Pilotos_PilotoId",
                        column: x => x.PilotoId,
                        principalTable: "Pilotos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PontuacaoPilotoTemporada_Temporas_TemporadaId",
                        column: x => x.TemporadaId,
                        principalTable: "Temporas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResultadoCorridas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PilotoId = table.Column<int>(type: "int", nullable: false),
                    TemporadaId = table.Column<int>(type: "int", nullable: false),
                    PistaId = table.Column<int>(type: "int", nullable: false),
                    Posicao = table.Column<int>(type: "int", nullable: false),
                    PontosGanhos = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultadoCorridas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResultadoCorridas_Pilotos_PilotoId",
                        column: x => x.PilotoId,
                        principalTable: "Pilotos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResultadoCorridas_Pistas_PistaId",
                        column: x => x.PistaId,
                        principalTable: "Pistas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResultadoCorridas_Temporas_TemporadaId",
                        column: x => x.TemporadaId,
                        principalTable: "Temporas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PontuacaoPilotoTemporada_PilotoId",
                table: "PontuacaoPilotoTemporada",
                column: "PilotoId");

            migrationBuilder.CreateIndex(
                name: "IX_PontuacaoPilotoTemporada_TemporadaId",
                table: "PontuacaoPilotoTemporada",
                column: "TemporadaId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultadoCorridas_PilotoId",
                table: "ResultadoCorridas",
                column: "PilotoId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultadoCorridas_PistaId",
                table: "ResultadoCorridas",
                column: "PistaId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultadoCorridas_TemporadaId",
                table: "ResultadoCorridas",
                column: "TemporadaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PontuacaoPilotoTemporada");

            migrationBuilder.DropTable(
                name: "ResultadoCorridas");
        }
    }
}
