using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class RemoveRelaciEnderPacie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pacientes_Enderecos_EnderecoId",
                table: "Pacientes");

            migrationBuilder.DropIndex(
                name: "IX_Pacientes_EnderecoId",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "EnderecoId",
                table: "Pacientes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EnderecoId",
                table: "Pacientes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_EnderecoId",
                table: "Pacientes",
                column: "EnderecoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pacientes_Enderecos_EnderecoId",
                table: "Pacientes",
                column: "EnderecoId",
                principalTable: "Enderecos",
                principalColumn: "EnderecoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
