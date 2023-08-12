using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class BDV15 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DimensionesPsicologicas_PruebasPsicologicas_EvaluacionPsico~",
                table: "DimensionesPsicologicas");

            migrationBuilder.DropForeignKey(
                name: "FK_EvaluacionesAula_Aulas_AulaId",
                table: "EvaluacionesAula");

            migrationBuilder.DropForeignKey(
                name: "FK_EvaluacionesAula_PruebasPsicologicas_EvaluacionPsicologicaId",
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
                name: "FK_GradosEvaPsicologicas_PruebasPsicologicas_EvaPsiId",
                table: "GradosEvaPsicologicas");

            migrationBuilder.DropForeignKey(
                name: "FK_RespuestasPsicologicas_EvaluacionesEstudiante_EvaPsiEstId",
                table: "RespuestasPsicologicas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PruebasPsicologicas",
                table: "PruebasPsicologicas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EvaluacionesEstudiante",
                table: "EvaluacionesEstudiante");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EvaluacionesAula",
                table: "EvaluacionesAula");

            migrationBuilder.RenameTable(
                name: "PruebasPsicologicas",
                newName: "EvaluacionesPsicologicas");

            migrationBuilder.RenameTable(
                name: "EvaluacionesEstudiante",
                newName: "EvaluacionesPsicologicasEstudiante");

            migrationBuilder.RenameTable(
                name: "EvaluacionesAula",
                newName: "EvaluacionesPsicologicasAula");

            migrationBuilder.RenameIndex(
                name: "IX_EvaluacionesEstudiante_EvaluacionAulaId",
                table: "EvaluacionesPsicologicasEstudiante",
                newName: "IX_EvaluacionesPsicologicasEstudiante_EvaluacionAulaId");

            migrationBuilder.RenameIndex(
                name: "IX_EvaluacionesEstudiante_EstudianteId",
                table: "EvaluacionesPsicologicasEstudiante",
                newName: "IX_EvaluacionesPsicologicasEstudiante_EstudianteId");

            migrationBuilder.RenameIndex(
                name: "IX_EvaluacionesAula_UnidadId",
                table: "EvaluacionesPsicologicasAula",
                newName: "IX_EvaluacionesPsicologicasAula_UnidadId");

            migrationBuilder.RenameIndex(
                name: "IX_EvaluacionesAula_EvaluacionPsicologicaId",
                table: "EvaluacionesPsicologicasAula",
                newName: "IX_EvaluacionesPsicologicasAula_EvaluacionPsicologicaId");

            migrationBuilder.RenameIndex(
                name: "IX_EvaluacionesAula_AulaId",
                table: "EvaluacionesPsicologicasAula",
                newName: "IX_EvaluacionesPsicologicasAula_AulaId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaInicio",
                table: "EvaluacionesPsicologicasAula",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaFin",
                table: "EvaluacionesPsicologicasAula",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EvaluacionesPsicologicas",
                table: "EvaluacionesPsicologicas",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EvaluacionesPsicologicasEstudiante",
                table: "EvaluacionesPsicologicasEstudiante",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EvaluacionesPsicologicasAula",
                table: "EvaluacionesPsicologicasAula",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DimensionesPsicologicas_EvaluacionesPsicologicas_Evaluacion~",
                table: "DimensionesPsicologicas",
                column: "EvaluacionPsicologicaId",
                principalTable: "EvaluacionesPsicologicas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EvaluacionesPsicologicasAula_Aulas_AulaId",
                table: "EvaluacionesPsicologicasAula",
                column: "AulaId",
                principalTable: "Aulas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EvaluacionesPsicologicasAula_EvaluacionesPsicologicas_Evalu~",
                table: "EvaluacionesPsicologicasAula",
                column: "EvaluacionPsicologicaId",
                principalTable: "EvaluacionesPsicologicas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EvaluacionesPsicologicasAula_Unidades_UnidadId",
                table: "EvaluacionesPsicologicasAula",
                column: "UnidadId",
                principalTable: "Unidades",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EvaluacionesPsicologicasEstudiante_Estudiantes_EstudianteId",
                table: "EvaluacionesPsicologicasEstudiante",
                column: "EstudianteId",
                principalTable: "Estudiantes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EvaluacionesPsicologicasEstudiante_EvaluacionesPsicologicas~",
                table: "EvaluacionesPsicologicasEstudiante",
                column: "EvaluacionAulaId",
                principalTable: "EvaluacionesPsicologicasAula",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GradosEvaPsicologicas_EvaluacionesPsicologicas_EvaPsiId",
                table: "GradosEvaPsicologicas",
                column: "EvaPsiId",
                principalTable: "EvaluacionesPsicologicas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RespuestasPsicologicas_EvaluacionesPsicologicasEstudiante_E~",
                table: "RespuestasPsicologicas",
                column: "EvaPsiEstId",
                principalTable: "EvaluacionesPsicologicasEstudiante",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DimensionesPsicologicas_EvaluacionesPsicologicas_Evaluacion~",
                table: "DimensionesPsicologicas");

            migrationBuilder.DropForeignKey(
                name: "FK_EvaluacionesPsicologicasAula_Aulas_AulaId",
                table: "EvaluacionesPsicologicasAula");

            migrationBuilder.DropForeignKey(
                name: "FK_EvaluacionesPsicologicasAula_EvaluacionesPsicologicas_Evalu~",
                table: "EvaluacionesPsicologicasAula");

            migrationBuilder.DropForeignKey(
                name: "FK_EvaluacionesPsicologicasAula_Unidades_UnidadId",
                table: "EvaluacionesPsicologicasAula");

            migrationBuilder.DropForeignKey(
                name: "FK_EvaluacionesPsicologicasEstudiante_Estudiantes_EstudianteId",
                table: "EvaluacionesPsicologicasEstudiante");

            migrationBuilder.DropForeignKey(
                name: "FK_EvaluacionesPsicologicasEstudiante_EvaluacionesPsicologicas~",
                table: "EvaluacionesPsicologicasEstudiante");

            migrationBuilder.DropForeignKey(
                name: "FK_GradosEvaPsicologicas_EvaluacionesPsicologicas_EvaPsiId",
                table: "GradosEvaPsicologicas");

            migrationBuilder.DropForeignKey(
                name: "FK_RespuestasPsicologicas_EvaluacionesPsicologicasEstudiante_E~",
                table: "RespuestasPsicologicas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EvaluacionesPsicologicasEstudiante",
                table: "EvaluacionesPsicologicasEstudiante");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EvaluacionesPsicologicasAula",
                table: "EvaluacionesPsicologicasAula");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EvaluacionesPsicologicas",
                table: "EvaluacionesPsicologicas");

            migrationBuilder.RenameTable(
                name: "EvaluacionesPsicologicasEstudiante",
                newName: "EvaluacionesEstudiante");

            migrationBuilder.RenameTable(
                name: "EvaluacionesPsicologicasAula",
                newName: "EvaluacionesAula");

            migrationBuilder.RenameTable(
                name: "EvaluacionesPsicologicas",
                newName: "PruebasPsicologicas");

            migrationBuilder.RenameIndex(
                name: "IX_EvaluacionesPsicologicasEstudiante_EvaluacionAulaId",
                table: "EvaluacionesEstudiante",
                newName: "IX_EvaluacionesEstudiante_EvaluacionAulaId");

            migrationBuilder.RenameIndex(
                name: "IX_EvaluacionesPsicologicasEstudiante_EstudianteId",
                table: "EvaluacionesEstudiante",
                newName: "IX_EvaluacionesEstudiante_EstudianteId");

            migrationBuilder.RenameIndex(
                name: "IX_EvaluacionesPsicologicasAula_UnidadId",
                table: "EvaluacionesAula",
                newName: "IX_EvaluacionesAula_UnidadId");

            migrationBuilder.RenameIndex(
                name: "IX_EvaluacionesPsicologicasAula_EvaluacionPsicologicaId",
                table: "EvaluacionesAula",
                newName: "IX_EvaluacionesAula_EvaluacionPsicologicaId");

            migrationBuilder.RenameIndex(
                name: "IX_EvaluacionesPsicologicasAula_AulaId",
                table: "EvaluacionesAula",
                newName: "IX_EvaluacionesAula_AulaId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaInicio",
                table: "EvaluacionesAula",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaFin",
                table: "EvaluacionesAula",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EvaluacionesEstudiante",
                table: "EvaluacionesEstudiante",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EvaluacionesAula",
                table: "EvaluacionesAula",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PruebasPsicologicas",
                table: "PruebasPsicologicas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DimensionesPsicologicas_PruebasPsicologicas_EvaluacionPsico~",
                table: "DimensionesPsicologicas",
                column: "EvaluacionPsicologicaId",
                principalTable: "PruebasPsicologicas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EvaluacionesAula_Aulas_AulaId",
                table: "EvaluacionesAula",
                column: "AulaId",
                principalTable: "Aulas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EvaluacionesAula_PruebasPsicologicas_EvaluacionPsicologicaId",
                table: "EvaluacionesAula",
                column: "EvaluacionPsicologicaId",
                principalTable: "PruebasPsicologicas",
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
                name: "FK_GradosEvaPsicologicas_PruebasPsicologicas_EvaPsiId",
                table: "GradosEvaPsicologicas",
                column: "EvaPsiId",
                principalTable: "PruebasPsicologicas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RespuestasPsicologicas_EvaluacionesEstudiante_EvaPsiEstId",
                table: "RespuestasPsicologicas",
                column: "EvaPsiEstId",
                principalTable: "EvaluacionesEstudiante",
                principalColumn: "Id");
        }
    }
}
