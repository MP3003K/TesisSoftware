using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class BDV110 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accesos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accesos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RolesAccesos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RolId = table.Column<int>(type: "integer", nullable: false),
                    AccesoId = table.Column<int>(type: "integer", nullable: false)
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
                name: "RolesUsuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    RolId = table.Column<int>(type: "integer", nullable: false)
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RolesAccesos");

            migrationBuilder.DropTable(
                name: "RolesUsuarios");

            migrationBuilder.DropTable(
                name: "Accesos");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
