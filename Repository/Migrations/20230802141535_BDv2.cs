using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    public partial class BDv2 : Migration
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
                name: "FK_Docentes_Personas_PersonaId",
                table: "Docentes");

            migrationBuilder.DropForeignKey(
                name: "FK_Escalas_Dimensiones_DimensionId",
                table: "Escalas");

            migrationBuilder.DropForeignKey(
                name: "FK_Estudiantes_Aulas_AulaId",
                table: "Estudiantes");

            migrationBuilder.DropForeignKey(
                name: "FK_Estudiantes_Personas_PersonaId",
                table: "Estudiantes");

            migrationBuilder.DropForeignKey(
                name: "FK_EvalucionAulas_Aulas_AulaId",
                table: "EvalucionAulas");

            migrationBuilder.DropForeignKey(
                name: "FK_EvalucionAulas_PruebaGrados_PruebaGradoId",
                table: "EvalucionAulas");

            migrationBuilder.DropForeignKey(
                name: "FK_EvalucionAulas_Unidades_UnidadId",
                table: "EvalucionAulas");

            migrationBuilder.DropForeignKey(
                name: "FK_Grados_Niveles_NivelId",
                table: "Grados");

            migrationBuilder.DropForeignKey(
                name: "FK_Indicadores_Escalas_EscalaId1",
                table: "Indicadores");

            migrationBuilder.DropForeignKey(
                name: "FK_PruebaGrados_Grados_GradoId",
                table: "PruebaGrados");

            migrationBuilder.DropForeignKey(
                name: "FK_PruebaGrados_PruebaPsicologicas_PruebaPsicologicaId",
                table: "PruebaGrados");

            migrationBuilder.DropForeignKey(
                name: "FK_Unidades_Escuelas_EscuelaId",
                table: "Unidades");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Personas_PersonaId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_PersonaId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Indicadores_EscalaId1",
                table: "Indicadores");

            migrationBuilder.DropIndex(
                name: "IX_Estudiantes_PersonaId",
                table: "Estudiantes");

            migrationBuilder.DropIndex(
                name: "IX_Docentes_PersonaId",
                table: "Docentes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PruebaPsicologicas",
                table: "PruebaPsicologicas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PruebaGrados",
                table: "PruebaGrados");

            migrationBuilder.DropIndex(
                name: "IX_PruebaGrados_PruebaPsicologicaId",
                table: "PruebaGrados");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EvalucionAulas",
                table: "EvalucionAulas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EvaluacionEstudiantes",
                table: "EvaluacionEstudiantes");

            migrationBuilder.DropColumn(
                name: "EscalaId1",
                table: "Indicadores");

            migrationBuilder.DropColumn(
                name: "PruebaPsicologicaId",
                table: "PruebaGrados");

            migrationBuilder.RenameTable(
                name: "PruebaPsicologicas",
                newName: "PruebasPsicologicas");

            migrationBuilder.RenameTable(
                name: "PruebaGrados",
                newName: "PruebasGrado");

            migrationBuilder.RenameTable(
                name: "EvalucionAulas",
                newName: "EvaluacionesAula");

            migrationBuilder.RenameTable(
                name: "EvaluacionEstudiantes",
                newName: "EvaluacionesEstudiante");

            migrationBuilder.RenameIndex(
                name: "IX_PruebaGrados_GradoId",
                table: "PruebasGrado",
                newName: "IX_PruebasGrado_GradoId");

            migrationBuilder.RenameIndex(
                name: "IX_EvalucionAulas_UnidadId",
                table: "EvaluacionesAula",
                newName: "IX_EvaluacionesAula_UnidadId");

            migrationBuilder.RenameIndex(
                name: "IX_EvalucionAulas_PruebaGradoId",
                table: "EvaluacionesAula",
                newName: "IX_EvaluacionesAula_PruebaGradoId");

            migrationBuilder.RenameIndex(
                name: "IX_EvalucionAulas_AulaId",
                table: "EvaluacionesAula",
                newName: "IX_EvaluacionesAula_AulaId");

            migrationBuilder.RenameColumn(
                name: "Alumno_id",
                table: "EvaluacionesEstudiante",
                newName: "EstudianteId");

            migrationBuilder.AlterColumn<int>(
                name: "EscalaId",
                table: "Indicadores",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "EvaEstudianteId",
                table: "Indicadores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PruebasPsicologicas",
                table: "PruebasPsicologicas",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PruebasGrado",
                table: "PruebasGrado",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EvaluacionesAula",
                table: "EvaluacionesAula",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EvaluacionesEstudiante",
                table: "EvaluacionesEstudiante",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_PersonaId",
                table: "Usuarios",
                column: "PersonaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Indicadores_EscalaId",
                table: "Indicadores",
                column: "EscalaId");

            migrationBuilder.CreateIndex(
                name: "IX_Indicadores_EvaEstudianteId",
                table: "Indicadores",
                column: "EvaEstudianteId");

            migrationBuilder.CreateIndex(
                name: "IX_Estudiantes_PersonaId",
                table: "Estudiantes",
                column: "PersonaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Docentes_PersonaId",
                table: "Docentes",
                column: "PersonaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PruebasGrado_PruebaId",
                table: "PruebasGrado",
                column: "PruebaId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluacionesEstudiante_EstudianteId",
                table: "EvaluacionesEstudiante",
                column: "EstudianteId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluacionesEstudiante_EvaluacionAulaId",
                table: "EvaluacionesEstudiante",
                column: "EvaluacionAulaId");

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
                name: "FK_Docentes_Personas_PersonaId",
                table: "Docentes",
                column: "PersonaId",
                principalTable: "Personas",
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
                name: "FK_Estudiantes_Aulas_AulaId",
                table: "Estudiantes",
                column: "AulaId",
                principalTable: "Aulas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Estudiantes_Personas_PersonaId",
                table: "Estudiantes",
                column: "PersonaId",
                principalTable: "Personas",
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
                name: "FK_Grados_Niveles_NivelId",
                table: "Grados",
                column: "NivelId",
                principalTable: "Niveles",
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

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Personas_PersonaId",
                table: "Usuarios",
                column: "PersonaId",
                principalTable: "Personas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
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
                name: "FK_Docentes_Personas_PersonaId",
                table: "Docentes");

            migrationBuilder.DropForeignKey(
                name: "FK_Escalas_Dimensiones_DimensionId",
                table: "Escalas");

            migrationBuilder.DropForeignKey(
                name: "FK_Estudiantes_Aulas_AulaId",
                table: "Estudiantes");

            migrationBuilder.DropForeignKey(
                name: "FK_Estudiantes_Personas_PersonaId",
                table: "Estudiantes");

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
                name: "FK_Grados_Niveles_NivelId",
                table: "Grados");

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

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Personas_PersonaId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_PersonaId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Indicadores_EscalaId",
                table: "Indicadores");

            migrationBuilder.DropIndex(
                name: "IX_Indicadores_EvaEstudianteId",
                table: "Indicadores");

            migrationBuilder.DropIndex(
                name: "IX_Estudiantes_PersonaId",
                table: "Estudiantes");

            migrationBuilder.DropIndex(
                name: "IX_Docentes_PersonaId",
                table: "Docentes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PruebasPsicologicas",
                table: "PruebasPsicologicas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PruebasGrado",
                table: "PruebasGrado");

            migrationBuilder.DropIndex(
                name: "IX_PruebasGrado_PruebaId",
                table: "PruebasGrado");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EvaluacionesEstudiante",
                table: "EvaluacionesEstudiante");

            migrationBuilder.DropIndex(
                name: "IX_EvaluacionesEstudiante_EstudianteId",
                table: "EvaluacionesEstudiante");

            migrationBuilder.DropIndex(
                name: "IX_EvaluacionesEstudiante_EvaluacionAulaId",
                table: "EvaluacionesEstudiante");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EvaluacionesAula",
                table: "EvaluacionesAula");

            migrationBuilder.DropColumn(
                name: "EvaEstudianteId",
                table: "Indicadores");

            migrationBuilder.RenameTable(
                name: "PruebasPsicologicas",
                newName: "PruebaPsicologicas");

            migrationBuilder.RenameTable(
                name: "PruebasGrado",
                newName: "PruebaGrados");

            migrationBuilder.RenameTable(
                name: "EvaluacionesEstudiante",
                newName: "EvaluacionEstudiantes");

            migrationBuilder.RenameTable(
                name: "EvaluacionesAula",
                newName: "EvalucionAulas");

            migrationBuilder.RenameIndex(
                name: "IX_PruebasGrado_GradoId",
                table: "PruebaGrados",
                newName: "IX_PruebaGrados_GradoId");

            migrationBuilder.RenameColumn(
                name: "EstudianteId",
                table: "EvaluacionEstudiantes",
                newName: "Alumno_id");

            migrationBuilder.RenameIndex(
                name: "IX_EvaluacionesAula_UnidadId",
                table: "EvalucionAulas",
                newName: "IX_EvalucionAulas_UnidadId");

            migrationBuilder.RenameIndex(
                name: "IX_EvaluacionesAula_PruebaGradoId",
                table: "EvalucionAulas",
                newName: "IX_EvalucionAulas_PruebaGradoId");

            migrationBuilder.RenameIndex(
                name: "IX_EvaluacionesAula_AulaId",
                table: "EvalucionAulas",
                newName: "IX_EvalucionAulas_AulaId");

            migrationBuilder.AlterColumn<string>(
                name: "EscalaId",
                table: "Indicadores",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "EscalaId1",
                table: "Indicadores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PruebaPsicologicaId",
                table: "PruebaGrados",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PruebaPsicologicas",
                table: "PruebaPsicologicas",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PruebaGrados",
                table: "PruebaGrados",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EvaluacionEstudiantes",
                table: "EvaluacionEstudiantes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EvalucionAulas",
                table: "EvalucionAulas",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_PersonaId",
                table: "Usuarios",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_Indicadores_EscalaId1",
                table: "Indicadores",
                column: "EscalaId1");

            migrationBuilder.CreateIndex(
                name: "IX_Estudiantes_PersonaId",
                table: "Estudiantes",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_Docentes_PersonaId",
                table: "Docentes",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_PruebaGrados_PruebaPsicologicaId",
                table: "PruebaGrados",
                column: "PruebaPsicologicaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Aulas_Docentes_TutorId",
                table: "Aulas",
                column: "TutorId",
                principalTable: "Docentes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Aulas_Escuelas_EscuelaId",
                table: "Aulas",
                column: "EscuelaId",
                principalTable: "Escuelas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Docentes_Personas_PersonaId",
                table: "Docentes",
                column: "PersonaId",
                principalTable: "Personas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Escalas_Dimensiones_DimensionId",
                table: "Escalas",
                column: "DimensionId",
                principalTable: "Dimensiones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Estudiantes_Aulas_AulaId",
                table: "Estudiantes",
                column: "AulaId",
                principalTable: "Aulas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Estudiantes_Personas_PersonaId",
                table: "Estudiantes",
                column: "PersonaId",
                principalTable: "Personas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EvalucionAulas_Aulas_AulaId",
                table: "EvalucionAulas",
                column: "AulaId",
                principalTable: "Aulas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EvalucionAulas_PruebaGrados_PruebaGradoId",
                table: "EvalucionAulas",
                column: "PruebaGradoId",
                principalTable: "PruebaGrados",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EvalucionAulas_Unidades_UnidadId",
                table: "EvalucionAulas",
                column: "UnidadId",
                principalTable: "Unidades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grados_Niveles_NivelId",
                table: "Grados",
                column: "NivelId",
                principalTable: "Niveles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Indicadores_Escalas_EscalaId1",
                table: "Indicadores",
                column: "EscalaId1",
                principalTable: "Escalas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PruebaGrados_Grados_GradoId",
                table: "PruebaGrados",
                column: "GradoId",
                principalTable: "Grados",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PruebaGrados_PruebaPsicologicas_PruebaPsicologicaId",
                table: "PruebaGrados",
                column: "PruebaPsicologicaId",
                principalTable: "PruebaPsicologicas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Unidades_Escuelas_EscuelaId",
                table: "Unidades",
                column: "EscuelaId",
                principalTable: "Escuelas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Personas_PersonaId",
                table: "Usuarios",
                column: "PersonaId",
                principalTable: "Personas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
