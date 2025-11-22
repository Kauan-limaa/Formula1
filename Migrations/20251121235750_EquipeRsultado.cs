using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Formula1.Migrations
{
    /// <inheritdoc />
    public partial class EquipeRsultado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PontuacaoEquipeTemporada",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EquipeId = table.Column<int>(type: "int", nullable: false),
                    TemporadaId = table.Column<int>(type: "int", nullable: false),
                    Pontos = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PontuacaoEquipeTemporada", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PontuacaoEquipeTemporada_Equipes_EquipeId",
                        column: x => x.EquipeId,
                        principalTable: "Equipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PontuacaoEquipeTemporada_Temporas_TemporadaId",
                        column: x => x.TemporadaId,
                        principalTable: "Temporas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PontuacaoEquipeTemporada_EquipeId",
                table: "PontuacaoEquipeTemporada",
                column: "EquipeId");

            migrationBuilder.CreateIndex(
                name: "IX_PontuacaoEquipeTemporada_TemporadaId",
                table: "PontuacaoEquipeTemporada",
                column: "TemporadaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PontuacaoEquipeTemporada");
        }
    }
}
