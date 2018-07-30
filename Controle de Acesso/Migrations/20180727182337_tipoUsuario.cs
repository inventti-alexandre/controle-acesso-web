using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ControleAcesso.Migrations
{
    public partial class tipoUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TipoUsuarioID",
                table: "Usuarios",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TipoUsuario",
                columns: table => new
                {
                    TipoUsuarioID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Administrador = table.Column<bool>(nullable: false),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoUsuario", x => x.TipoUsuarioID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_TipoUsuarioID",
                table: "Usuarios",
                column: "TipoUsuarioID");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_TipoUsuario_TipoUsuarioID",
                table: "Usuarios",
                column: "TipoUsuarioID",
                principalTable: "TipoUsuario",
                principalColumn: "TipoUsuarioID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_TipoUsuario_TipoUsuarioID",
                table: "Usuarios");

            migrationBuilder.DropTable(
                name: "TipoUsuario");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_TipoUsuarioID",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "TipoUsuarioID",
                table: "Usuarios");
        }
    }
}
