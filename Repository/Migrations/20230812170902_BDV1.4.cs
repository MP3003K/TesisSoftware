using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class BDV14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EstudiantesAulas",
                table: "EstudiantesAulas");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "EstudiantesAulas",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EstudiantesAulas",
                table: "EstudiantesAulas",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_EstudiantesAulas_EstudianteId",
                table: "EstudiantesAulas",
                column: "EstudianteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EstudiantesAulas",
                table: "EstudiantesAulas");

            migrationBuilder.DropIndex(
                name: "IX_EstudiantesAulas_EstudianteId",
                table: "EstudiantesAulas");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "EstudiantesAulas",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EstudiantesAulas",
                table: "EstudiantesAulas",
                columns: new[] { "EstudianteId", "AulaId" });
        }
    }
}
