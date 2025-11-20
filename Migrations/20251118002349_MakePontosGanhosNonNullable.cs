using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Formula1.Migrations
{
    /// <inheritdoc />
    public partial class MakePontosGanhosNonNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PontosGanhos",
                table: "ResultadoCorridas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PontosGanhos",
                table: "ResultadoCorridas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
