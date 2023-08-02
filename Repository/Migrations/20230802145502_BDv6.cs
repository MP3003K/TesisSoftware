using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    public partial class BDv6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aulas_Docentes_TutorId",
                table: "Aulas");

            migrationBuilder.DropForeignKey(
                name: "FK_Aulas_Escuelas_EscuelaId",
                table: "Aulas");

            migrationBuilder.DropForeignKey(
                name: "FK_Escalas_Dimensiones_DimensionId",
                table: "Escalas");

            migrationBuilder.DropForeignKey(
                name: "FK_EvaluacionesAula_Aulas_AulaId",
                table: "EvaluacionesAula");

            migrationBuilder.DropForeignKey(
                name: "FK_EvaluacionesAula_PruebasGrado_PruebaGradoId",
                table: "EvaluacionesAula");

            migrationBuilder.DropForeignKey(
                name: "FK_EvaluacionesAula_Unidades_UnidadId",
                table: "EvaluacionesAula");

            migrationBuilder.DropForeignKey(
                name: "FK_EvaluacionesEstudiante_Estudiantes_EstudianteId",
                table: "EvaluacionesEstudiante");

            migrationBuilder.DropForeignKey(
                name: "FK_EvaluacionesEstudiante_EvaluacionesAula_EvaluacionAulaId",
                table: "EvaluacionesEstudiante");

            migrationBuilder.DropForeignKey(
                name: "FK_Indicadores_Escalas_EscalaId",
                table: "Indicadores");

            migrationBuilder.DropForeignKey(
                name: "FK_Indicadores_EvaluacionesEstudiante_EvaEstudianteId",
                table: "Indicadores");

            migrationBuilder.DropForeignKey(
                name: "FK_PruebasGrado_Grados_GradoId",
                table: "PruebasGrado");

            migrationBuilder.DropForeignKey(
                name: "FK_PruebasGrado_PruebasPsicologicas_PruebaId",
                table: "PruebasGrado");

            migrationBuilder.DropForeignKey(
                name: "FK_Unidades_Escuelas_EscuelaId",
                table: "Unidades");

            migrationBuilder.AddForeignKey(
                name: "FK_Aulas_Docentes_TutorId",
                table: "Aulas",
                column: "TutorId",
                principalTable: "Docentes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Aulas_Escuelas_EscuelaId",
                table: "Aulas",
                column: "EscuelaId",
                principalTable: "Escuelas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Escalas_Dimensiones_DimensionId",
                table: "Escalas",
                column: "DimensionId",
                principalTable: "Dimensiones",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EvaluacionesAula_Aulas_AulaId",
                table: "EvaluacionesAula",
                column: "AulaId",
                principalTable: "Aulas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EvaluacionesAula_PruebasGrado_PruebaGradoId",
                table: "EvaluacionesAula",
                column: "PruebaGradoId",
                principalTable: "PruebasGrado",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EvaluacionesAula_Unidades_UnidadId",
                table: "EvaluacionesAula",
                column: "UnidadId",
                principalTable: "Unidades",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EvaluacionesEstudiante_Estudiantes_EstudianteId",
                table: "EvaluacionesEstudiante",
                column: "EstudianteId",
                principalTable: "Estudiantes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EvaluacionesEstudiante_EvaluacionesAula_EvaluacionAulaId",
                table: "EvaluacionesEstudiante",
                column: "EvaluacionAulaId",
                principalTable: "EvaluacionesAula",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Indicadores_Escalas_EscalaId",
                table: "Indicadores",
                column: "EscalaId",
                principalTable: "Escalas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Indicadores_EvaluacionesEstudiante_EvaEstudianteId",
                table: "Indicadores",
                column: "EvaEstudianteId",
                principalTable: "EvaluacionesEstudiante",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PruebasGrado_Grados_GradoId",
                table: "PruebasGrado",
                column: "GradoId",
                principalTable: "Grados",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PruebasGrado_PruebasPsicologicas_PruebaId",
                table: "PruebasGrado",
                column: "PruebaId",
                principalTable: "PruebasPsicologicas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Unidades_Escuelas_EscuelaId",
                table: "Unidades",
                column: "EscuelaId",
                principalTable: "Escuelas",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aulas_Docentes_TutorId",
                table: "Aulas");

            migrationBuilder.DropForeignKey(
                name: "FK_Aulas_Escuelas_EscuelaId",
                table: "Aulas");

            migrationBuilder.DropForeignKey(
                name: "FK_Escalas_Dimensiones_DimensionId",
                table: "Escalas");

            migrationBuilder.DropForeignKey(
                name: "FK_EvaluacionesAula_Aulas_AulaId",
                table: "EvaluacionesAula");

            migrationBuilder.DropForeignKey(
                name: "FK_EvaluacionesAula_PruebasGrado_PruebaGradoId",
                table: "EvaluacionesAula");

            migrationBuilder.DropForeignKey(
                name: "FK_EvaluacionesAula_Unidades_UnidadId",
                table: "EvaluacionesAula");

            migrationBuilder.DropForeignKey(
                name: "FK_EvaluacionesEstudiante_Estudiantes_EstudianteId",
                table: "EvaluacionesEstudiante");

            migrationBuilder.DropForeignKey(
                name: "FK_EvaluacionesEstudiante_EvaluacionesAula_EvaluacionAulaId",
                table: "EvaluacionesEstudiante");

            migrationBuilder.DropForeignKey(
                name: "FK_Indicadores_Escalas_EscalaId",
                table: "Indicadores");

            migrationBuilder.DropForeignKey(
                name: "FK_Indicadores_EvaluacionesEstudiante_EvaEstudianteId",
                table: "Indicadores");

            migrationBuilder.DropForeignKey(
                name: "FK_PruebasGrado_Grados_GradoId",
                table: "PruebasGrado");

            migrationBuilder.DropForeignKey(
                name: "FK_PruebasGrado_PruebasPsicologicas_PruebaId",
                table: "PruebasGrado");

            migrationBuilder.DropForeignKey(
                name: "FK_Unidades_Escuelas_EscuelaId",
                table: "Unidades");

            migrationBuilder.AddForeignKey(
                name: "FK_Aulas_Docentes_TutorId",
                table: "Aulas",
                column: "TutorId",
                principalTable: "Docentes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Aulas_Escuelas_EscuelaId",
                table: "Aulas",
                column: "EscuelaId",
                principalTable: "Escuelas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Escalas_Dimensiones_DimensionId",
                table: "Escalas",
                column: "DimensionId",
                principalTable: "Dimensiones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EvaluacionesAula_Aulas_AulaId",
                table: "EvaluacionesAula",
                column: "AulaId",
                principalTable: "Aulas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EvaluacionesAula_PruebasGrado_PruebaGradoId",
                table: "EvaluacionesAula",
                column: "PruebaGradoId",
                principalTable: "PruebasGrado",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EvaluacionesAula_Unidades_UnidadId",
                table: "EvaluacionesAula",
                column: "UnidadId",
                principalTable: "Unidades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EvaluacionesEstudiante_Estudiantes_EstudianteId",
                table: "EvaluacionesEstudiante",
                column: "EstudianteId",
                principalTable: "Estudiantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EvaluacionesEstudiante_EvaluacionesAula_EvaluacionAulaId",
                table: "EvaluacionesEstudiante",
                column: "EvaluacionAulaId",
                principalTable: "EvaluacionesAula",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Indicadores_Escalas_EscalaId",
                table: "Indicadores",
                column: "EscalaId",
                principalTable: "Escalas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Indicadores_EvaluacionesEstudiante_EvaEstudianteId",
                table: "Indicadores",
                column: "EvaEstudianteId",
                principalTable: "EvaluacionesEstudiante",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PruebasGrado_Grados_GradoId",
                table: "PruebasGrado",
                column: "GradoId",
                principalTable: "Grados",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PruebasGrado_PruebasPsicologicas_PruebaId",
                table: "PruebasGrado",
                column: "PruebaId",
                principalTable: "PruebasPsicologicas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Unidades_Escuelas_EscuelaId",
                table: "Unidades",
                column: "EscuelaId",
                principalTable: "Escuelas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
