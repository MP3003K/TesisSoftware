using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class BDV10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accesos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accesos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Escuelas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoModular = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Escuelas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EvaluacionesPsicologicas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CantPreguntas = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluacionesPsicologicas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Niveles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NNivel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Niveles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Personas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApellidoPaterno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApellidoMaterno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DNI = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Unidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NUnidad = table.Column<int>(type: "int", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Año = table.Column<int>(type: "int", nullable: false),
                    EscuelaId = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                name: "DimensionesPsicologicas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EvaluacionPsicologicaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DimensionesPsicologicas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DimensionesPsicologicas_EvaluacionesPsicologicas_EvaluacionPsicologicaId",
                        column: x => x.EvaluacionPsicologicaId,
                        principalTable: "EvaluacionesPsicologicas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Grados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NGrado = table.Column<int>(type: "int", nullable: false),
                    NivelId = table.Column<int>(type: "int", nullable: false)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonaId = table.Column<int>(type: "int", nullable: false)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoEstudiante = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonaId = table.Column<int>(type: "int", nullable: false)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonaId = table.Column<int>(type: "int", nullable: false)
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
                name: "RolesAccesos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RolId = table.Column<int>(type: "int", nullable: false),
                    AccesoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolesAccesos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolesAccesos_Accesos_AccesoId",
                        column: x => x.AccesoId,
                        principalTable: "Accesos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RolesAccesos_Roles_RolId",
                        column: x => x.RolId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EscalasPsicologicas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DimensionId = table.Column<int>(type: "int", nullable: false)
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
                name: "GradosEvaPsicologicas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GradoId = table.Column<int>(type: "int", nullable: false),
                    EvaPsiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradosEvaPsicologicas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GradosEvaPsicologicas_EvaluacionesPsicologicas_EvaPsiId",
                        column: x => x.EvaPsiId,
                        principalTable: "EvaluacionesPsicologicas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GradosEvaPsicologicas_Grados_GradoId",
                        column: x => x.GradoId,
                        principalTable: "Grados",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Aulas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Secccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GradoId = table.Column<int>(type: "int", nullable: false),
                    EscuelaId = table.Column<int>(type: "int", nullable: false),
                    DocenteId = table.Column<int>(type: "int", nullable: true)
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
                name: "RolesUsuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    RolId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolesUsuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolesUsuarios_Roles_RolId",
                        column: x => x.RolId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RolesUsuarios_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "IndicadoresPsicologicos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreIndicador = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EscalaPsicologicaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndicadoresPsicologicos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IndicadoresPsicologicos_EscalasPsicologicas_EscalaPsicologicaId",
                        column: x => x.EscalaPsicologicaId,
                        principalTable: "EscalasPsicologicas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EstudiantesAulas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EstudianteId = table.Column<int>(type: "int", nullable: false),
                    AulaId = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Anio = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstudiantesAulas", x => x.Id);
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
                name: "EvaluacionesPsicologicasAula",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "(No inicio= N, En proceso = P, Finalizo= F)"),
                    UnidadId = table.Column<int>(type: "int", nullable: false),
                    AulaId = table.Column<int>(type: "int", nullable: false),
                    EvaluacionPsicologicaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluacionesPsicologicasAula", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EvaluacionesPsicologicasAula_Aulas_AulaId",
                        column: x => x.AulaId,
                        principalTable: "Aulas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EvaluacionesPsicologicasAula_EvaluacionesPsicologicas_EvaluacionPsicologicaId",
                        column: x => x.EvaluacionPsicologicaId,
                        principalTable: "EvaluacionesPsicologicas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EvaluacionesPsicologicasAula_Unidades_UnidadId",
                        column: x => x.UnidadId,
                        principalTable: "Unidades",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PreguntasPsicologicas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pregunta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IndicadorPsicologicoId = table.Column<int>(type: "int", nullable: false),
                    NPregunta = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreguntasPsicologicas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreguntasPsicologicas_IndicadoresPsicologicos_IndicadorPsicologicoId",
                        column: x => x.IndicadorPsicologicoId,
                        principalTable: "IndicadoresPsicologicos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EvaluacionesPsicologicasEstudiante",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "(No inicio= N, En proceso = P, Finalizo= F)"),
                    EvaluacionAulaId = table.Column<int>(type: "int", nullable: false),
                    EstudianteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluacionesPsicologicasEstudiante", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EvaluacionesPsicologicasEstudiante_Estudiantes_EstudianteId",
                        column: x => x.EstudianteId,
                        principalTable: "Estudiantes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EvaluacionesPsicologicasEstudiante_EvaluacionesPsicologicasAula_EvaluacionAulaId",
                        column: x => x.EvaluacionAulaId,
                        principalTable: "EvaluacionesPsicologicasAula",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RespuestasPsicologicas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Respuesta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreguntaPsicologicaId = table.Column<int>(type: "int", nullable: false),
                    EvaPsiEstId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RespuestasPsicologicas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RespuestasPsicologicas_EvaluacionesPsicologicasEstudiante_EvaPsiEstId",
                        column: x => x.EvaPsiEstId,
                        principalTable: "EvaluacionesPsicologicasEstudiante",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RespuestasPsicologicas_PreguntasPsicologicas_PreguntaPsicologicaId",
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
                name: "IX_EstudiantesAulas_EstudianteId",
                table: "EstudiantesAulas",
                column: "EstudianteId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluacionesPsicologicasAula_AulaId",
                table: "EvaluacionesPsicologicasAula",
                column: "AulaId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluacionesPsicologicasAula_EvaluacionPsicologicaId",
                table: "EvaluacionesPsicologicasAula",
                column: "EvaluacionPsicologicaId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluacionesPsicologicasAula_UnidadId",
                table: "EvaluacionesPsicologicasAula",
                column: "UnidadId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluacionesPsicologicasEstudiante_EstudianteId",
                table: "EvaluacionesPsicologicasEstudiante",
                column: "EstudianteId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluacionesPsicologicasEstudiante_EvaluacionAulaId",
                table: "EvaluacionesPsicologicasEstudiante",
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
                name: "IX_RolesAccesos_AccesoId",
                table: "RolesAccesos",
                column: "AccesoId");

            migrationBuilder.CreateIndex(
                name: "IX_RolesAccesos_RolId",
                table: "RolesAccesos",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_RolesUsuarios_RolId",
                table: "RolesUsuarios",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_RolesUsuarios_UsuarioId",
                table: "RolesUsuarios",
                column: "UsuarioId");

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
                name: "RolesAccesos");

            migrationBuilder.DropTable(
                name: "RolesUsuarios");

            migrationBuilder.DropTable(
                name: "EvaluacionesPsicologicasEstudiante");

            migrationBuilder.DropTable(
                name: "PreguntasPsicologicas");

            migrationBuilder.DropTable(
                name: "Accesos");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Estudiantes");

            migrationBuilder.DropTable(
                name: "EvaluacionesPsicologicasAula");

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
                name: "EvaluacionesPsicologicas");
        }
    }
}
