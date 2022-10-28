using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RenegociacaoAPI.Migrations
{
    public partial class CriandoBaseRenegociacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cpf = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contratos",
                columns: table => new
                {
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    NumContrato = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Plano = table.Column<int>(type: "int", nullable: false),
                    DataContratacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Multa = table.Column<double>(type: "float", nullable: false),
                    Juros = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contratos", x => new { x.ClienteId, x.NumContrato });
                    table.UniqueConstraint("AK_Contratos_NumContrato", x => x.NumContrato);
                    table.ForeignKey(
                        name: "FK_Contratos_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Parcelas",
                columns: table => new
                {
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    NumContrato = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NumParcela = table.Column<int>(type: "int", nullable: false),
                    Vencimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parcelas", x => new { x.ClienteId, x.NumContrato, x.NumParcela });
                    table.ForeignKey(
                        name: "FK_Parcelas_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Parcelas_Contratos_NumContrato",
                        column: x => x.NumContrato,
                        principalTable: "Contratos",
                        principalColumn: "NumContrato");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Parcelas_NumContrato",
                table: "Parcelas",
                column: "NumContrato");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Parcelas");

            migrationBuilder.DropTable(
                name: "Contratos");

            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
