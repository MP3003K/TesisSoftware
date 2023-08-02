using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    public partial class BDv4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Docentes_Personas_PersonaId",
                table: "Docentes");

            migrationBuilder.DropForeignKey(
                name: "FK_Estudiantes_Personas_PersonaId",
                table: "Estudiantes");

            migrationBuilder.AddForeignKey(
                name: "FK_Docentes_Personas_PersonaId",
                table: "Docentes",
                column: "PersonaId",
                principalTable: "Personas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Estudiantes_Personas_PersonaId",
                table: "Estudiantes",
                column: "PersonaId",
                principalTable: "Personas",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Docentes_Personas_PersonaId",
                table: "Docentes");

            migrationBuilder.DropForeignKey(
                name: "FK_Estudiantes_Personas_PersonaId",
                table: "Estudiantes");

            migrationBuilder.AddForeignKey(
                name: "FK_Docentes_Personas_PersonaId",
                table: "Docentes",
                column: "PersonaId",
                principalTable: "Personas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Estudiantes_Personas_PersonaId",
                table: "Estudiantes",
                column: "PersonaId",
                principalTable: "Personas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
