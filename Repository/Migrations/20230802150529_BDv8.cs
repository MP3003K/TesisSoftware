using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    public partial class BDv8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EvaluacionesAula_PruebasGrado_PruebaGradoId",
                table: "EvaluacionesAula");

            migrationBuilder.AddForeignKey(
                name: "FK_EvaluacionesAula_PruebasGrado_PruebaGradoId",
                table: "EvaluacionesAula",
                column: "PruebaGradoId",
                principalTable: "PruebasGrado",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EvaluacionesAula_PruebasGrado_PruebaGradoId",
                table: "EvaluacionesAula");

            migrationBuilder.AddForeignKey(
                name: "FK_EvaluacionesAula_PruebasGrado_PruebaGradoId",
                table: "EvaluacionesAula",
                column: "PruebaGradoId",
                principalTable: "PruebasGrado",
                principalColumn: "Id");
        }
    }
}
