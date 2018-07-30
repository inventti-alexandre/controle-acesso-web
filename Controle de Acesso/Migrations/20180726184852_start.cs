using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ControleAcesso.Migrations
{
    public partial class start : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Eventos",
                columns: table => new
                {
                    EventoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DataFim = table.Column<DateTime>(nullable: false),
                    DataInicio = table.Column<DateTime>(nullable: false),
                    Descricao = table.Column<string>(nullable: true),
                    Logo = table.Column<string>(nullable: true),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventos", x => x.EventoID);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    StatusID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Bloquear = table.Column<bool>(nullable: false),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.StatusID);
                });

            migrationBuilder.CreateTable(
                name: "TipoPresenca",
                columns: table => new
                {
                    TipoPresencaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true),
                    PrivilegiosElevados = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoPresenca", x => x.TipoPresencaID);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuarioID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    Senha = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioID);
                });

            migrationBuilder.CreateTable(
                name: "Cursos",
                columns: table => new
                {
                    CursoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DataFim = table.Column<DateTime>(nullable: false),
                    DataInicio = table.Column<DateTime>(nullable: false),
                    Descricao = table.Column<string>(nullable: true),
                    EventoID = table.Column<int>(nullable: false),
                    Logo = table.Column<string>(nullable: true),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cursos", x => x.CursoID);
                    table.ForeignKey(
                        name: "FK_Cursos_Eventos_EventoID",
                        column: x => x.EventoID,
                        principalTable: "Eventos",
                        principalColumn: "EventoID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Pessoas",
                columns: table => new
                {
                    PessoaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Bairro = table.Column<string>(nullable: true),
                    CEP = table.Column<string>(nullable: true),
                    CNPJ = table.Column<string>(nullable: true),
                    CPF = table.Column<string>(nullable: false),
                    Cargo = table.Column<string>(nullable: false),
                    Celular = table.Column<string>(nullable: true),
                    Cidade = table.Column<string>(nullable: true),
                    Complemento = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: false),
                    Empresa = table.Column<string>(nullable: false),
                    Endereco = table.Column<string>(nullable: true),
                    Nome = table.Column<string>(nullable: false),
                    NomeCracha = table.Column<string>(nullable: false),
                    Numero = table.Column<string>(nullable: true),
                    StatusID = table.Column<int>(nullable: false),
                    Telefone = table.Column<string>(nullable: false),
                    UF = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoas", x => x.PessoaID);
                    table.ForeignKey(
                        name: "FK_Pessoas_Status_StatusID",
                        column: x => x.StatusID,
                        principalTable: "Status",
                        principalColumn: "StatusID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "CursoPresencas",
                columns: table => new
                {
                    CursoPresencaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CursoID = table.Column<int>(nullable: false),
                    PessoaID = table.Column<int>(nullable: false),
                    StatusID = table.Column<int>(nullable: false),
                    TipoPresencaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CursoPresencas", x => x.CursoPresencaID);
                    table.ForeignKey(
                        name: "FK_CursoPresencas_Cursos_CursoID",
                        column: x => x.CursoID,
                        principalTable: "Cursos",
                        principalColumn: "CursoID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CursoPresencas_Pessoas_PessoaID",
                        column: x => x.PessoaID,
                        principalTable: "Pessoas",
                        principalColumn: "PessoaID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CursoPresencas_Status_StatusID",
                        column: x => x.StatusID,
                        principalTable: "Status",
                        principalColumn: "StatusID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CursoPresencas_TipoPresenca_TipoPresencaID",
                        column: x => x.TipoPresencaID,
                        principalTable: "TipoPresenca",
                        principalColumn: "TipoPresencaID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CursoPresencas_CursoID",
                table: "CursoPresencas",
                column: "CursoID");

            migrationBuilder.CreateIndex(
                name: "IX_CursoPresencas_PessoaID",
                table: "CursoPresencas",
                column: "PessoaID");

            migrationBuilder.CreateIndex(
                name: "IX_CursoPresencas_StatusID",
                table: "CursoPresencas",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_CursoPresencas_TipoPresencaID",
                table: "CursoPresencas",
                column: "TipoPresencaID");

            migrationBuilder.CreateIndex(
                name: "IX_Cursos_EventoID",
                table: "Cursos",
                column: "EventoID");

            migrationBuilder.CreateIndex(
                name: "IX_Pessoas_StatusID",
                table: "Pessoas",
                column: "StatusID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CursoPresencas");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Cursos");

            migrationBuilder.DropTable(
                name: "Pessoas");

            migrationBuilder.DropTable(
                name: "TipoPresenca");

            migrationBuilder.DropTable(
                name: "Eventos");

            migrationBuilder.DropTable(
                name: "Status");
        }
    }
}
