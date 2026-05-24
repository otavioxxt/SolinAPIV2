using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SolinAPI.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_alerta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    id_pet = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    s_titulo = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    s_mensagem = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: false),
                    s_severidade = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    b_lido = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    dt_criacao = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_alerta", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_evento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    id_pet = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    s_tipo = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    s_descricao = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: true),
                    dt_evento = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    s_origem = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_evento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_historico",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    id_pet = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    s_titulo = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    s_descricao = table.Column<string>(type: "NVARCHAR2(1000)", maxLength: 1000, nullable: true),
                    s_categoria = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    s_veterinario = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: true),
                    dt_registro = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_historico", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_pet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    id_tutor = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    s_nome = table.Column<string>(type: "NVARCHAR2(80)", maxLength: 80, nullable: false),
                    s_especie = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    s_raca = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true),
                    dt_nascimento = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    s_sexo = table.Column<string>(type: "NVARCHAR2(10)", maxLength: 10, nullable: true),
                    n_peso = table.Column<decimal>(type: "DECIMAL(18, 2)", nullable: true),
                    dt_cadastro = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_pet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_tutor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    s_nome = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    s_email = table.Column<string>(type: "NVARCHAR2(150)", maxLength: 150, nullable: false),
                    s_telefone = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: true),
                    dt_cadastro = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_tutor", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_alerta");

            migrationBuilder.DropTable(
                name: "tb_evento");

            migrationBuilder.DropTable(
                name: "tb_historico");

            migrationBuilder.DropTable(
                name: "tb_pet");

            migrationBuilder.DropTable(
                name: "tb_tutor");
        }
    }
}
