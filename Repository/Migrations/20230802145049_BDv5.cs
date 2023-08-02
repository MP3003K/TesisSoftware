using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    public partial class BDv5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estudiantes_Aulas_AulaId",
                table: "Estudiantes");

            migrationBuilder.AddForeignKey(
                name: "FK_Estudiantes_Aulas_AulaId",
                table: "Estudiantes",
                column: "AulaId",
                principalTable: "Aulas",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estudiantes_Aulas_AulaId",
                table: "Estudiantes");

            migrationBuilder.AddForeignKey(
                name: "FK_Estudiantes_Aulas_AulaId",
                table: "Estudiantes",
                column: "AulaId",
                principalTable: "Aulas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
