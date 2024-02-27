using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace desafio_backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class basepedidos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "person",
                keyColumn: "PersonId",
                keyValue: 4);

            migrationBuilder.CreateTable(
                name: "estadoDelPedido",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_estadoDelPedido", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "pedidos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    numeroDePedido = table.Column<int>(type: "int", nullable: true),
                    cicloDelPedido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    codigoDeContratoInterno = table.Column<long>(type: "bigint", nullable: true),
                    estadoDelPedido = table.Column<int>(type: "int", nullable: true),
                    cuentaCorriente = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    cuando = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pedidos", x => x.id);
                    table.ForeignKey(
                        name: "FK_pedidos_estadoDelPedido",
                        column: x => x.estadoDelPedido,
                        principalTable: "estadoDelPedido",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_pedidos_estadoDelPedido",
                table: "pedidos",
                column: "estadoDelPedido");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pedidos");

            migrationBuilder.DropTable(
                name: "estadoDelPedido");

            migrationBuilder.InsertData(
                table: "person",
                columns: new[] { "PersonId", "Apellido", "Nombre" },
                values: new object[] { 4, "apellidoTest4", "nombreTest4" });
        }
    }
}
