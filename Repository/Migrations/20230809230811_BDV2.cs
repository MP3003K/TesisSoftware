using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class BDV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aulas_Docentes_TutorId",
                table: "Aulas");

            migrationBuilder.DropForeignKey(
                name: "FK_EvaluacionesAula_PruebasGrado_PruebaGradoId",
                table: "EvaluacionesAula");

            migrationBuilder.DropTable(
                name: "Indicadores");

            migrationBuilder.DropTable(
                name: "PruebasGrado");

            migrationBuilder.DropTable(
                name: "Escalas");

            migrationBuilder.DropTable(
                name: "Dimensiones");

            migrationBuilder.DropIndex(
                name: "IX_Aulas_TutorId",
                table: "Aulas");

            migrationBuilder.DropColumn(
                name: "TutorId",
                table: "Aulas");

            migrationBuilder.RenameColumn(
                name: "PruebaGradoId",
                table: "EvaluacionesAula",
                newName: "EvaluacionPsicologicaId");

            migrationBuilder.RenameColumn(
                name: "Fecha",
                table: "EvaluacionesAula",
                newName: "FechaInicio");

            migrationBuilder.RenameIndex(
                name: "IX_EvaluacionesAula_PruebaGradoId",
                table: "EvaluacionesAula",
                newName: "IX_EvaluacionesAula_EvaluacionPsicologicaId");

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "EvaluacionesEstudiante",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaFin",
                table: "EvaluacionesEstudiante",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaInicio",
                table: "EvaluacionesEstudiante",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "RespuestaPsicologicaId",
                table: "EvaluacionesEstudiante",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "EvaluacionesAula",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaFin",
                table: "EvaluacionesAula",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DocenteId",
                table: "Aulas",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DimensionesPsicologicas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Codigo = table.Column<string>(type: "text", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DimensionesPsicologicas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EscalasPsicologicas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    DimensionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EscalasPsicologicas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EscalasPsicologicas_DimensionesPsicologicas_DimensionId",
                        column: x => x.DimensionId,
                        principalTable: "DimensionesPsicologicas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "IndicadoresPsicologicos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NombreIndicador = table.Column<string>(type: "text", nullable: false),
                    EscalaId = table.Column<int>(type: "integer", nullable: false),
                    EscalaId1 = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndicadoresPsicologicos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IndicadoresPsicologicos_EscalasPsicologicas_EscalaId",
                        column: x => x.EscalaId,
                        principalTable: "EscalasPsicologicas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IndicadoresPsicologicos_EscalasPsicologicas_EscalaId1",
                        column: x => x.EscalaId1,
                        principalTable: "EscalasPsicologicas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PreguntasPsicologicas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Pregunta = table.Column<string>(type: "text", nullable: false),
                    IndicadorPsicologicoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreguntasPsicologicas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreguntasPsicologicas_IndicadoresPsicologicos_IndicadorPsic~",
                        column: x => x.IndicadorPsicologicoId,
                        principalTable: "IndicadoresPsicologicos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RespuestasPsicologicas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Respuesta = table.Column<string>(type: "text", nullable: false),
                    Puntaje = table.Column<int>(type: "integer", nullable: false),
                    PreguntaPsicologicaId = table.Column<int>(type: "integer", nullable: false),
                    EvaPsiEstId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RespuestasPsicologicas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RespuestasPsicologicas_EvaluacionesEstudiante_EvaPsiEstId",
                        column: x => x.EvaPsiEstId,
                        principalTable: "EvaluacionesEstudiante",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RespuestasPsicologicas_PreguntasPsicologicas_PreguntaPsicol~",
                        column: x => x.PreguntaPsicologicaId,
                        principalTable: "PreguntasPsicologicas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aulas_DocenteId",
                table: "Aulas",
                column: "DocenteId");

            migrationBuilder.CreateIndex(
                name: "IX_EscalasPsicologicas_DimensionId",
                table: "EscalasPsicologicas",
                column: "DimensionId");

            migrationBuilder.CreateIndex(
                name: "IX_IndicadoresPsicologicos_EscalaId",
                table: "IndicadoresPsicologicos",
                column: "EscalaId");

            migrationBuilder.CreateIndex(
                name: "IX_IndicadoresPsicologicos_EscalaId1",
                table: "IndicadoresPsicologicos",
                column: "EscalaId1");

            migrationBuilder.CreateIndex(
                name: "IX_PreguntasPsicologicas_IndicadorPsicologicoId",
                table: "PreguntasPsicologicas",
                column: "IndicadorPsicologicoId");

            migrationBuilder.CreateIndex(
                name: "IX_RespuestasPsicologicas_EvaPsiEstId",
                table: "RespuestasPsicologicas",
                column: "EvaPsiEstId");

            migrationBuilder.CreateIndex(
                name: "IX_RespuestasPsicologicas_PreguntaPsicologicaId",
                table: "RespuestasPsicologicas",
                column: "PreguntaPsicologicaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Aulas_Docentes_DocenteId",
                table: "Aulas",
                column: "DocenteId",
                principalTable: "Docentes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EvaluacionesAula_PruebasPsicologicas_EvaluacionPsicologicaId",
                table: "EvaluacionesAula",
                column: "EvaluacionPsicologicaId",
                principalTable: "PruebasPsicologicas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aulas_Docentes_DocenteId",
                table: "Aulas");

            migrationBuilder.DropForeignKey(
                name: "FK_EvaluacionesAula_PruebasPsicologicas_EvaluacionPsicologicaId",
                table: "EvaluacionesAula");

            migrationBuilder.DropTable(
                name: "RespuestasPsicologicas");

            migrationBuilder.DropTable(
                name: "PreguntasPsicologicas");

            migrationBuilder.DropTable(
                name: "IndicadoresPsicologicos");

            migrationBuilder.DropTable(
                name: "EscalasPsicologicas");

            migrationBuilder.DropTable(
                name: "DimensionesPsicologicas");

            migrationBuilder.DropIndex(
                name: "IX_Aulas_DocenteId",
                table: "Aulas");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "EvaluacionesEstudiante");

            migrationBuilder.DropColumn(
                name: "FechaFin",
                table: "EvaluacionesEstudiante");

            migrationBuilder.DropColumn(
                name: "FechaInicio",
                table: "EvaluacionesEstudiante");

            migrationBuilder.DropColumn(
                name: "RespuestaPsicologicaId",
                table: "EvaluacionesEstudiante");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "EvaluacionesAula");

            migrationBuilder.DropColumn(
                name: "FechaFin",
                table: "EvaluacionesAula");

            migrationBuilder.DropColumn(
                name: "DocenteId",
                table: "Aulas");

            migrationBuilder.RenameColumn(
                name: "FechaInicio",
                table: "EvaluacionesAula",
                newName: "Fecha");

            migrationBuilder.RenameColumn(
                name: "EvaluacionPsicologicaId",
                table: "EvaluacionesAula",
                newName: "PruebaGradoId");

            migrationBuilder.RenameIndex(
                name: "IX_EvaluacionesAula_EvaluacionPsicologicaId",
                table: "EvaluacionesAula",
                newName: "IX_EvaluacionesAula_PruebaGradoId");

            migrationBuilder.AddColumn<int>(
                name: "TutorId",
                table: "Aulas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Dimensiones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Codigo = table.Column<string>(type: "text", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dimensiones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PruebasGrado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GradoId = table.Column<int>(type: "integer", nullable: false),
                    PruebaId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PruebasGrado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PruebasGrado_Grados_GradoId",
                        column: x => x.GradoId,
                        principalTable: "Grados",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PruebasGrado_PruebasPsicologicas_PruebaId",
                        column: x => x.PruebaId,
                        principalTable: "PruebasPsicologicas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Escalas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DimensionId = table.Column<int>(type: "integer", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Escalas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Escalas_Dimensiones_DimensionId",
                        column: x => x.DimensionId,
                        principalTable: "Dimensiones",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Indicadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EscalaId = table.Column<int>(type: "integer", nullable: false),
                    EvaEstudianteId = table.Column<int>(type: "integer", nullable: false),
                    NombreIndicador = table.Column<string>(type: "text", nullable: false),
                    Pregunta = table.Column<string>(type: "text", nullable: false),
                    Puntaje = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Indicadores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Indicadores_Escalas_EscalaId",
                        column: x => x.EscalaId,
                        principalTable: "Escalas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Indicadores_EvaluacionesEstudiante_EvaEstudianteId",
                        column: x => x.EvaEstudianteId,
                        principalTable: "EvaluacionesEstudiante",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aulas_TutorId",
                table: "Aulas",
                column: "TutorId");

            migrationBuilder.CreateIndex(
                name: "IX_Escalas_DimensionId",
                table: "Escalas",
                column: "DimensionId");

            migrationBuilder.CreateIndex(
                name: "IX_Indicadores_EscalaId",
                table: "Indicadores",
                column: "EscalaId");

            migrationBuilder.CreateIndex(
                name: "IX_Indicadores_EvaEstudianteId",
                table: "Indicadores",
                column: "EvaEstudianteId");

            migrationBuilder.CreateIndex(
                name: "IX_PruebasGrado_GradoId",
                table: "PruebasGrado",
                column: "GradoId");

            migrationBuilder.CreateIndex(
                name: "IX_PruebasGrado_PruebaId",
                table: "PruebasGrado",
                column: "PruebaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Aulas_Docentes_TutorId",
                table: "Aulas",
                column: "TutorId",
                principalTable: "Docentes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EvaluacionesAula_PruebasGrado_PruebaGradoId",
                table: "EvaluacionesAula",
                column: "PruebaGradoId",
                principalTable: "PruebasGrado",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
