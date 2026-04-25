using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reparaciones.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Direccion = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Electrodomesticos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Marca = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Modelo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NumeroSerie = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Tipo = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Electrodomesticos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Repuestos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repuestos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tecnicos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Especialidad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tecnicos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reparaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaRecepcion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaEntrega = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Diagnostico = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CostoManoObra = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    ElectrodomesticoId = table.Column<int>(type: "int", nullable: false),
                    TecnicoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reparaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reparaciones_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reparaciones_Electrodomesticos_ElectrodomesticoId",
                        column: x => x.ElectrodomesticoId,
                        principalTable: "Electrodomesticos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reparaciones_Tecnicos_TecnicoId",
                        column: x => x.TecnicoId,
                        principalTable: "Tecnicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Garantias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReparacionId = table.Column<int>(type: "int", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Garantias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Garantias_Reparaciones_ReparacionId",
                        column: x => x.ReparacionId,
                        principalTable: "Reparaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReparacionRepuestos",
                columns: table => new
                {
                    ReparacionId = table.Column<int>(type: "int", nullable: false),
                    RepuestoId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReparacionRepuestos", x => new { x.ReparacionId, x.RepuestoId });
                    table.ForeignKey(
                        name: "FK_ReparacionRepuestos_Reparaciones_ReparacionId",
                        column: x => x.ReparacionId,
                        principalTable: "Reparaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReparacionRepuestos_Repuestos_RepuestoId",
                        column: x => x.RepuestoId,
                        principalTable: "Repuestos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Garantias_ReparacionId",
                table: "Garantias",
                column: "ReparacionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reparaciones_ClienteId",
                table: "Reparaciones",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Reparaciones_ElectrodomesticoId",
                table: "Reparaciones",
                column: "ElectrodomesticoId");

            migrationBuilder.CreateIndex(
                name: "IX_Reparaciones_TecnicoId",
                table: "Reparaciones",
                column: "TecnicoId");

            migrationBuilder.CreateIndex(
                name: "IX_ReparacionRepuestos_RepuestoId",
                table: "ReparacionRepuestos",
                column: "RepuestoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Garantias");

            migrationBuilder.DropTable(
                name: "ReparacionRepuestos");

            migrationBuilder.DropTable(
                name: "Reparaciones");

            migrationBuilder.DropTable(
                name: "Repuestos");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Electrodomesticos");

            migrationBuilder.DropTable(
                name: "Tecnicos");
        }
    }
}
