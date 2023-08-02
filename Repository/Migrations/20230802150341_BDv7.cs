using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    public partial class BDv7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grados_Niveles_NivelId",
                table: "Grados");

            migrationBuilder.DropForeignKey(
                name: "FK_Pixes_Banks_BankId",
                table: "Pixes");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Pixes_PixId",
                table: "Transactions");

            migrationBuilder.AddForeignKey(
                name: "FK_Grados_Niveles_NivelId",
                table: "Grados",
                column: "NivelId",
                principalTable: "Niveles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pixes_Banks_BankId",
                table: "Pixes",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Pixes_PixId",
                table: "Transactions",
                column: "PixId",
                principalTable: "Pixes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grados_Niveles_NivelId",
                table: "Grados");

            migrationBuilder.DropForeignKey(
                name: "FK_Pixes_Banks_BankId",
                table: "Pixes");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Pixes_PixId",
                table: "Transactions");

            migrationBuilder.AddForeignKey(
                name: "FK_Grados_Niveles_NivelId",
                table: "Grados",
                column: "NivelId",
                principalTable: "Niveles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pixes_Banks_BankId",
                table: "Pixes",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Pixes_PixId",
                table: "Transactions",
                column: "PixId",
                principalTable: "Pixes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
