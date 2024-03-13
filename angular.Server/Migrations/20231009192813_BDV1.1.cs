using Microsoft.EntityFrameworkCore.Migrations;

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
                name: "MateriasAcademicas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MateriasAcademicas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompetenciasAcademicas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MateriaAcademicaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetenciasAcademicas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompetenciasAcademicas_MateriasAcademicas_MateriaAcademicaId",
                        column: x => x.MateriaAcademicaId,
                        principalTable: "MateriasAcademicas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EvaluacionesCompetenciasEstudiante",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AulaId = table.Column<int>(type: "int", nullable: false),
                    UnidadId = table.Column<int>(type: "int", nullable: false),
                    CompetenciaAcademicaId = table.Column<int>(type: "int", nullable: false),
                    EstudianteId = table.Column<int>(type: "int", nullable: false),
                    Nota = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluacionesCompetenciasEstudiante", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EvaluacionesCompetenciasEstudiante_Aulas_AulaId",
                        column: x => x.AulaId,
                        principalTable: "Aulas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EvaluacionesCompetenciasEstudiante_CompetenciasAcademicas_CompetenciaAcademicaId",
                        column: x => x.CompetenciaAcademicaId,
                        principalTable: "CompetenciasAcademicas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EvaluacionesCompetenciasEstudiante_Estudiantes_EstudianteId",
                        column: x => x.EstudianteId,
                        principalTable: "Estudiantes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EvaluacionesCompetenciasEstudiante_Unidades_UnidadId",
                        column: x => x.UnidadId,
                        principalTable: "Unidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompetenciasAcademicas_MateriaAcademicaId",
                table: "CompetenciasAcademicas",
                column: "MateriaAcademicaId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluacionesCompetenciasEstudiante_AulaId",
                table: "EvaluacionesCompetenciasEstudiante",
                column: "AulaId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluacionesCompetenciasEstudiante_CompetenciaAcademicaId",
                table: "EvaluacionesCompetenciasEstudiante",
                column: "CompetenciaAcademicaId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluacionesCompetenciasEstudiante_EstudianteId",
                table: "EvaluacionesCompetenciasEstudiante",
                column: "EstudianteId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluacionesCompetenciasEstudiante_UnidadId",
                table: "EvaluacionesCompetenciasEstudiante",
                column: "UnidadId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EvaluacionesCompetenciasEstudiante");

            migrationBuilder.DropTable(
                name: "CompetenciasAcademicas");

            migrationBuilder.DropTable(
                name: "MateriasAcademicas");
        }
    }
}
