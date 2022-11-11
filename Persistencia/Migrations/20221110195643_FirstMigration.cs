using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistencia.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pacientes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoDocumentoId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    VcDocumento = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    VcPrimerNombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    VcSegundoNombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    VcPrimerApellido = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    VcSegundoApellido = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Nacionalidad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DtFechaNacimineto = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PacienteAfiliacion",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PacienteId = table.Column<long>(type: "bigint", nullable: false),
                    DtFechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioId = table.Column<long>(type: "bigint", nullable: false),
                    RegimenId = table.Column<long>(type: "bigint", nullable: false),
                    AseguradoraId = table.Column<long>(type: "bigint", nullable: false),
                    VcOtraAseguradora = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PacienteAfiliacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pacientes_Afiliacion",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PacienteCaso",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PacienteId = table.Column<long>(type: "bigint", maxLength: 20, nullable: false),
                    VcNumeroCaso = table.Column<int>(type: "int", maxLength: 20, nullable: false),
                    BComfirmadoSifilisGestacional = table.Column<bool>(type: "bit", maxLength: 20, nullable: false),
                    ClasificacionSifilis = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DtFecha = table.Column<DateTime>(type: "datetime2", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PacienteCaso", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PacienteCaso_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PacienteContacto",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PacienteId = table.Column<long>(type: "bigint", nullable: false),
                    DtFechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioId = table.Column<long>(type: "bigint", nullable: false),
                    PaidId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartamentoId = table.Column<long>(type: "bigint", nullable: false),
                    LocalidadId = table.Column<long>(type: "bigint", nullable: false),
                    UpzId = table.Column<long>(type: "bigint", nullable: false),
                    BarrioId = table.Column<long>(type: "bigint", nullable: false),
                    VcDireccionPrincipal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VcDireccionSecundaria = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VcTelefono1 = table.Column<int>(type: "int", nullable: false),
                    VcTelefono2 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PacienteContacto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pacientes_Contacto",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PacienteDiagnostico",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PacienteCasoId = table.Column<long>(type: "bigint", maxLength: 20, nullable: false),
                    DtFechaDiagnostico = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IpsDiagnosticaId = table.Column<long>(type: "bigint", maxLength: 20, nullable: false),
                    FormulaObstetricaId = table.Column<long>(type: "bigint", maxLength: 20, nullable: false),
                    CondicionActualId = table.Column<long>(type: "bigint", maxLength: 200, nullable: false),
                    VcEdadGestacional = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    AntecedenteItsId = table.Column<long>(type: "bigint", maxLength: 20, nullable: false),
                    TipoAntecedenteId = table.Column<long>(type: "bigint", maxLength: 20, nullable: false),
                    VcDescripcionAntecedente = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    AntecedentePenicilinaId = table.Column<long>(type: "bigint", maxLength: 20, nullable: false),
                    BRequierePruebaNoTreponemica = table.Column<bool>(type: "bit", nullable: false),
                    TipoPruebaNoTriponemica = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ResultadoPruebaNoTriponemica = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DtResultadoPruebaNoTreponemica = table.Column<DateTime>(type: "datetime2", maxLength: 20, nullable: false),
                    ModificacionDefinicionCasoId = table.Column<long>(type: "bigint", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PacienteDiagnostico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PacienteDiagnostico_PacienteCaso_PacienteCasoId",
                        column: x => x.PacienteCasoId,
                        principalTable: "PacienteCaso",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PacienteDiagnosticoPruebaTreponemica",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PacienteDiagnosticoId = table.Column<long>(type: "bigint", nullable: false),
                    TipoPruebaTriponemica = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BResultadoPruebaTreponemica = table.Column<bool>(type: "bit", nullable: false),
                    DtResultadoPruebaTreponemica = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PacienteDiagnosticoPruebaTreponemica", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pacientes_Diagnostico",
                        column: x => x.PacienteDiagnosticoId,
                        principalTable: "PacienteDiagnostico",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PacienteAfiliacion_PacienteId",
                table: "PacienteAfiliacion",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_PacienteCaso_PacienteId",
                table: "PacienteCaso",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_PacienteContacto_PacienteId",
                table: "PacienteContacto",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_PacienteDiagnostico_PacienteCasoId",
                table: "PacienteDiagnostico",
                column: "PacienteCasoId");

            migrationBuilder.CreateIndex(
                name: "IX_PacienteDiagnosticoPruebaTreponemica_PacienteDiagnosticoId",
                table: "PacienteDiagnosticoPruebaTreponemica",
                column: "PacienteDiagnosticoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PacienteAfiliacion");

            migrationBuilder.DropTable(
                name: "PacienteContacto");

            migrationBuilder.DropTable(
                name: "PacienteDiagnosticoPruebaTreponemica");

            migrationBuilder.DropTable(
                name: "PacienteDiagnostico");

            migrationBuilder.DropTable(
                name: "PacienteCaso");

            migrationBuilder.DropTable(
                name: "Pacientes");
        }
    }
}
