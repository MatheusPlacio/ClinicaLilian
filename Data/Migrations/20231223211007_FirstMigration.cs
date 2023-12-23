using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Enderecos",
                columns: table => new
                {
                    EnderecoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Logradouro = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Complemento = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    Numero = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    Cep = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    Bairro = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    localidade = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    UF = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enderecos", x => x.EnderecoId);
                });

            migrationBuilder.CreateTable(
                name: "Funcionarios",
                columns: table => new
                {
                    FuncionarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SobreNome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Idade = table.Column<int>(type: "int", maxLength: 2, nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "GETDATE()"),
                    CPF = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Celular = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Especialidade = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionarios", x => x.FuncionarioId);
                });

            migrationBuilder.CreateTable(
                name: "Procedimentos",
                columns: table => new
                {
                    ProcedimentoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeProcedimento = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Procedimentos", x => x.ProcedimentoId);
                });

            migrationBuilder.CreateTable(
                name: "Pacientes",
                columns: table => new
                {
                    PacienteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    SobreNome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Idade = table.Column<int>(type: "int", maxLength: 2, nullable: true),
                    DataDeNascimento = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "GETDATE()"),
                    Genero = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Celular = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Profissao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnderecoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacientes", x => x.PacienteId);
                    table.ForeignKey(
                        name: "FK_Pacientes_Enderecos_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "Enderecos",
                        principalColumn: "EnderecoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Agendamentos",
                columns: table => new
                {
                    AgendamentoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataHoraMarcada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sessoes = table.Column<int>(type: "int", nullable: false),
                    Observacao = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    FuncionarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agendamentos", x => x.AgendamentoId);
                    table.ForeignKey(
                        name: "FK_Agendamentos_Funcionarios_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionarios",
                        principalColumn: "FuncionarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AgendamentosPacientes",
                columns: table => new
                {
                    AgendamentosPacientesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AgendamentoId = table.Column<int>(type: "int", nullable: false),
                    PacienteId = table.Column<int>(type: "int", nullable: false),
                    DataHoraMarcada = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgendamentosPacientes", x => x.AgendamentosPacientesId);
                    table.ForeignKey(
                        name: "FK_AgendamentosPacientes_Agendamentos_AgendamentoId",
                        column: x => x.AgendamentoId,
                        principalTable: "Agendamentos",
                        principalColumn: "AgendamentoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AgendamentosPacientes_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "PacienteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProcedimentosAgendamentos",
                columns: table => new
                {
                    ProcedimentoAgendamentoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataHoraMarcada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProcedimentoId = table.Column<int>(type: "int", nullable: false),
                    AgendamentoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcedimentosAgendamentos", x => x.ProcedimentoAgendamentoId);
                    table.ForeignKey(
                        name: "FK_ProcedimentosAgendamentos_Agendamentos_AgendamentoId",
                        column: x => x.AgendamentoId,
                        principalTable: "Agendamentos",
                        principalColumn: "AgendamentoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProcedimentosAgendamentos_Procedimentos_ProcedimentoId",
                        column: x => x.ProcedimentoId,
                        principalTable: "Procedimentos",
                        principalColumn: "ProcedimentoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Procedimentos",
                columns: new[] { "ProcedimentoId", "NomeProcedimento" },
                values: new object[,]
                {
                    { 1, "Limpeza de pele" },
                    { 2, "Design de sobrancelhas" },
                    { 3, "Design de sobrancelha c/ henna ou tintura" },
                    { 4, "Depilação a laser" },
                    { 5, "RadioFrequencia" },
                    { 6, "Pelling químico" },
                    { 7, "Micropigmentação de sobrancelha" },
                    { 8, "Preenchimento Labial" },
                    { 9, "Botox" },
                    { 10, "Bioestimulador" },
                    { 11, "Bioestimulador" },
                    { 12, "Aplicação de enzimas" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agendamentos_FuncionarioId",
                table: "Agendamentos",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_AgendamentosPacientes_AgendamentoId",
                table: "AgendamentosPacientes",
                column: "AgendamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_AgendamentosPacientes_PacienteId",
                table: "AgendamentosPacientes",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionarios_CPF",
                table: "Funcionarios",
                column: "CPF");

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_Celular",
                table: "Pacientes",
                column: "Celular",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_CPF",
                table: "Pacientes",
                column: "CPF",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_EnderecoId",
                table: "Pacientes",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcedimentosAgendamentos_AgendamentoId",
                table: "ProcedimentosAgendamentos",
                column: "AgendamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcedimentosAgendamentos_ProcedimentoId",
                table: "ProcedimentosAgendamentos",
                column: "ProcedimentoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgendamentosPacientes");

            migrationBuilder.DropTable(
                name: "ProcedimentosAgendamentos");

            migrationBuilder.DropTable(
                name: "Pacientes");

            migrationBuilder.DropTable(
                name: "Agendamentos");

            migrationBuilder.DropTable(
                name: "Procedimentos");

            migrationBuilder.DropTable(
                name: "Enderecos");

            migrationBuilder.DropTable(
                name: "Funcionarios");
        }
    }
}
