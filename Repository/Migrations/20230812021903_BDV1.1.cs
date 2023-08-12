using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class BDV11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Escuelas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CodigoModular = table.Column<string>(type: "text", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Escuelas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Niveles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    NNivel = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Niveles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Personas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombres = table.Column<string>(type: "text", nullable: false),
                    ApellidoPaterno = table.Column<string>(type: "text", nullable: false),
                    ApellidoMaterno = table.Column<string>(type: "text", nullable: false),
                    DNI = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PruebasPsicologicas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    Tipo = table.Column<string>(type: "text", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PruebasPsicologicas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Unidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    NUnidad = table.Column<int>(type: "integer", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Año = table.Column<int>(type: "integer", nullable: false),
                    EscuelaId = table.Column<int>(type: "integer", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unidades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Unidades_Escuelas_EscuelaId",
                        column: x => x.EscuelaId,
                        principalTable: "Escuelas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Grados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    NGrado = table.Column<int>(type: "integer", nullable: false),
                    NivelId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Grados_Niveles_NivelId",
                        column: x => x.NivelId,
                        principalTable: "Niveles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Docentes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PersonaId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Docentes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Docentes_Personas_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Personas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Estudiantes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CodigoEstudiante = table.Column<string>(type: "text", nullable: false),
                    PersonaId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estudiantes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Estudiantes_Personas_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Personas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    PersonaId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Personas_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Personas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DimensionesPsicologicas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Codigo = table.Column<string>(type: "text", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    EvaluacionPsicologicaId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DimensionesPsicologicas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DimensionesPsicologicas_PruebasPsicologicas_EvaluacionPsico~",
                        column: x => x.EvaluacionPsicologicaId,
                        principalTable: "PruebasPsicologicas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GradosEvaPsicologicas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GradoId = table.Column<int>(type: "integer", nullable: false),
                    EvaPsiId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradosEvaPsicologicas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GradosEvaPsicologicas_Grados_GradoId",
                        column: x => x.GradoId,
                        principalTable: "Grados",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GradosEvaPsicologicas_PruebasPsicologicas_EvaPsiId",
                        column: x => x.EvaPsiId,
                        principalTable: "PruebasPsicologicas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Aulas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Secccion = table.Column<string>(type: "text", nullable: false),
                    GradoId = table.Column<int>(type: "integer", nullable: false),
                    EscuelaId = table.Column<int>(type: "integer", nullable: false),
                    DocenteId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aulas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aulas_Docentes_DocenteId",
                        column: x => x.DocenteId,
                        principalTable: "Docentes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Aulas_Escuelas_EscuelaId",
                        column: x => x.EscuelaId,
                        principalTable: "Escuelas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Aulas_Grados_GradoId",
                        column: x => x.GradoId,
                        principalTable: "Grados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "EstudiantesAulas",
                columns: table => new
                {
                    EstudianteId = table.Column<int>(type: "integer", nullable: false),
                    AulaId = table.Column<int>(type: "integer", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstudiantesAulas", x => new { x.EstudianteId, x.AulaId });
                    table.ForeignKey(
                        name: "FK_EstudiantesAulas_Aulas_AulaId",
                        column: x => x.AulaId,
                        principalTable: "Aulas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EstudiantesAulas_Estudiantes_EstudianteId",
                        column: x => x.EstudianteId,
                        principalTable: "Estudiantes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EvaluacionesAula",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FechaInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false),
                    UnidadId = table.Column<int>(type: "integer", nullable: false),
                    AulaId = table.Column<int>(type: "integer", nullable: false),
                    EvaluacionPsicologicaId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluacionesAula", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EvaluacionesAula_Aulas_AulaId",
                        column: x => x.AulaId,
                        principalTable: "Aulas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EvaluacionesAula_PruebasPsicologicas_EvaluacionPsicologicaId",
                        column: x => x.EvaluacionPsicologicaId,
                        principalTable: "PruebasPsicologicas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EvaluacionesAula_Unidades_UnidadId",
                        column: x => x.UnidadId,
                        principalTable: "Unidades",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "IndicadoresPsicologicos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NombreIndicador = table.Column<string>(type: "text", nullable: false),
                    EscalaPsicologicaId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndicadoresPsicologicos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IndicadoresPsicologicos_EscalasPsicologicas_EscalaPsicologi~",
                        column: x => x.EscalaPsicologicaId,
                        principalTable: "EscalasPsicologicas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EvaluacionesEstudiante",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FechaInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false),
                    EvaluacionAulaId = table.Column<int>(type: "integer", nullable: false),
                    EstudianteId = table.Column<int>(type: "integer", nullable: false),
                    RespuestaPsicologicaId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluacionesEstudiante", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EvaluacionesEstudiante_Estudiantes_EstudianteId",
                        column: x => x.EstudianteId,
                        principalTable: "Estudiantes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EvaluacionesEstudiante_EvaluacionesAula_EvaluacionAulaId",
                        column: x => x.EvaluacionAulaId,
                        principalTable: "EvaluacionesAula",
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
                name: "IX_Aulas_EscuelaId",
                table: "Aulas",
                column: "EscuelaId");

            migrationBuilder.CreateIndex(
                name: "IX_Aulas_GradoId",
                table: "Aulas",
                column: "GradoId");

            migrationBuilder.CreateIndex(
                name: "IX_DimensionesPsicologicas_EvaluacionPsicologicaId",
                table: "DimensionesPsicologicas",
                column: "EvaluacionPsicologicaId");

            migrationBuilder.CreateIndex(
                name: "IX_Docentes_PersonaId",
                table: "Docentes",
                column: "PersonaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EscalasPsicologicas_DimensionId",
                table: "EscalasPsicologicas",
                column: "DimensionId");

            migrationBuilder.CreateIndex(
                name: "IX_Estudiantes_PersonaId",
                table: "Estudiantes",
                column: "PersonaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EstudiantesAulas_AulaId",
                table: "EstudiantesAulas",
                column: "AulaId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluacionesAula_AulaId",
                table: "EvaluacionesAula",
                column: "AulaId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluacionesAula_EvaluacionPsicologicaId",
                table: "EvaluacionesAula",
                column: "EvaluacionPsicologicaId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluacionesAula_UnidadId",
                table: "EvaluacionesAula",
                column: "UnidadId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluacionesEstudiante_EstudianteId",
                table: "EvaluacionesEstudiante",
                column: "EstudianteId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluacionesEstudiante_EvaluacionAulaId",
                table: "EvaluacionesEstudiante",
                column: "EvaluacionAulaId");

            migrationBuilder.CreateIndex(
                name: "IX_Grados_NivelId",
                table: "Grados",
                column: "NivelId");

            migrationBuilder.CreateIndex(
                name: "IX_GradosEvaPsicologicas_EvaPsiId",
                table: "GradosEvaPsicologicas",
                column: "EvaPsiId");

            migrationBuilder.CreateIndex(
                name: "IX_GradosEvaPsicologicas_GradoId",
                table: "GradosEvaPsicologicas",
                column: "GradoId");

            migrationBuilder.CreateIndex(
                name: "IX_IndicadoresPsicologicos_EscalaPsicologicaId",
                table: "IndicadoresPsicologicos",
                column: "EscalaPsicologicaId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Unidades_EscuelaId",
                table: "Unidades",
                column: "EscuelaId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_PersonaId",
                table: "Usuarios",
                column: "PersonaId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EstudiantesAulas");

            migrationBuilder.DropTable(
                name: "GradosEvaPsicologicas");

            migrationBuilder.DropTable(
                name: "RespuestasPsicologicas");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "EvaluacionesEstudiante");

            migrationBuilder.DropTable(
                name: "PreguntasPsicologicas");

            migrationBuilder.DropTable(
                name: "Estudiantes");

            migrationBuilder.DropTable(
                name: "EvaluacionesAula");

            migrationBuilder.DropTable(
                name: "IndicadoresPsicologicos");

            migrationBuilder.DropTable(
                name: "Aulas");

            migrationBuilder.DropTable(
                name: "Unidades");

            migrationBuilder.DropTable(
                name: "EscalasPsicologicas");

            migrationBuilder.DropTable(
                name: "Docentes");

            migrationBuilder.DropTable(
                name: "Grados");

            migrationBuilder.DropTable(
                name: "Escuelas");

            migrationBuilder.DropTable(
                name: "DimensionesPsicologicas");

            migrationBuilder.DropTable(
                name: "Personas");

            migrationBuilder.DropTable(
                name: "Niveles");

            migrationBuilder.DropTable(
                name: "PruebasPsicologicas");
        }
    }
}
