﻿// <auto-generated />
using ControleAcesso.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace ControleAcesso.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ControleAcesso.Models.Curso", b =>
                {
                    b.Property<int>("CursoID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DataFim");

                    b.Property<DateTime>("DataInicio");

                    b.Property<string>("Descricao");

                    b.Property<int>("EventoID");

                    b.Property<string>("Logo");

                    b.Property<string>("Nome");

                    b.HasKey("CursoID");

                    b.HasIndex("EventoID");

                    b.ToTable("Cursos");
                });

            modelBuilder.Entity("ControleAcesso.Models.CursoPresenca", b =>
                {
                    b.Property<int>("CursoPresencaID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CursoID");

                    b.Property<int>("PessoaID");

                    b.Property<int>("StatusID");

                    b.Property<int>("TipoPresencaID");

                    b.HasKey("CursoPresencaID");

                    b.HasIndex("CursoID");

                    b.HasIndex("PessoaID");

                    b.HasIndex("StatusID");

                    b.HasIndex("TipoPresencaID");

                    b.ToTable("CursoPresencas");
                });

            modelBuilder.Entity("ControleAcesso.Models.Evento", b =>
                {
                    b.Property<int>("EventoID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Banner");

                    b.Property<DateTime>("DataFim");

                    b.Property<DateTime>("DataInicio");

                    b.Property<string>("Descricao");

                    b.Property<string>("Logo");

                    b.Property<string>("Nome");

                    b.HasKey("EventoID");

                    b.ToTable("Eventos");
                });

            modelBuilder.Entity("ControleAcesso.Models.Pessoa", b =>
                {
                    b.Property<int>("PessoaID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Bairro");

                    b.Property<string>("CEP");

                    b.Property<string>("CNPJ");

                    b.Property<string>("CPF")
                        .IsRequired();

                    b.Property<string>("Cargo")
                        .IsRequired();

                    b.Property<string>("Celular");

                    b.Property<string>("Cidade");

                    b.Property<string>("Complemento");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Empresa")
                        .IsRequired();

                    b.Property<string>("Endereco");

                    b.Property<string>("Nome")
                        .IsRequired();

                    b.Property<string>("NomeCracha")
                        .IsRequired();

                    b.Property<string>("Numero");

                    b.Property<int>("StatusID");

                    b.Property<string>("Telefone")
                        .IsRequired();

                    b.Property<string>("UF");

                    b.HasKey("PessoaID");

                    b.HasIndex("StatusID");

                    b.ToTable("Pessoas");
                });

            modelBuilder.Entity("ControleAcesso.Models.Status", b =>
                {
                    b.Property<int>("StatusID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Bloquear");

                    b.Property<string>("Nome");

                    b.HasKey("StatusID");

                    b.ToTable("Status");
                });

            modelBuilder.Entity("ControleAcesso.Models.TipoPresenca", b =>
                {
                    b.Property<int>("TipoPresencaID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nome");

                    b.Property<bool>("PrivilegiosElevados");

                    b.HasKey("TipoPresencaID");

                    b.ToTable("TipoPresenca");
                });

            modelBuilder.Entity("ControleAcesso.Models.TipoUsuario", b =>
                {
                    b.Property<int>("TipoUsuarioID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Administrador");

                    b.Property<string>("Nome");

                    b.HasKey("TipoUsuarioID");

                    b.ToTable("TipoUsuario");
                });

            modelBuilder.Entity("ControleAcesso.Models.Usuario", b =>
                {
                    b.Property<int>("UsuarioID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DataCadastro");

                    b.Property<string>("Nome");

                    b.Property<string>("Senha");

                    b.Property<int?>("TipoUsuarioID");

                    b.Property<string>("Username");

                    b.HasKey("UsuarioID");

                    b.HasIndex("TipoUsuarioID");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("ControleAcesso.Models.Curso", b =>
                {
                    b.HasOne("ControleAcesso.Models.Evento", "Evento")
                        .WithMany()
                        .HasForeignKey("EventoID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ControleAcesso.Models.CursoPresenca", b =>
                {
                    b.HasOne("ControleAcesso.Models.Curso", "Curso")
                        .WithMany()
                        .HasForeignKey("CursoID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ControleAcesso.Models.Pessoa", "Pessoa")
                        .WithMany()
                        .HasForeignKey("PessoaID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ControleAcesso.Models.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ControleAcesso.Models.TipoPresenca", "TipoPresenca")
                        .WithMany()
                        .HasForeignKey("TipoPresencaID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ControleAcesso.Models.Pessoa", b =>
                {
                    b.HasOne("ControleAcesso.Models.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ControleAcesso.Models.Usuario", b =>
                {
                    b.HasOne("ControleAcesso.Models.TipoUsuario", "TipoUsuario")
                        .WithMany()
                        .HasForeignKey("TipoUsuarioID");
                });
#pragma warning restore 612, 618
        }
    }
}
