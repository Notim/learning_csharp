﻿// <auto-generated />
using System;
using DAL.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DAL.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0-preview.19074.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DAL.Entities.Aluno", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ativo");

                    b.Property<string>("celular");

                    b.Property<DateTime?>("dtAlteracao");

                    b.Property<DateTime>("dtCadastro");

                    b.Property<DateTime?>("dtExclusao");

                    b.Property<string>("email");

                    b.Property<string>("foto");

                    b.Property<int>("idUsuario");

                    b.Property<int?>("idUsuarioAlteracao");

                    b.Property<int>("idUsuarioCadastro");

                    b.Property<int?>("idUsuarioExclusao");

                    b.Property<string>("nome");

                    b.Property<string>("ra");

                    b.HasKey("id");

                    b.HasIndex("celular")
                        .IsUnique()
                        .HasFilter("[celular] IS NOT NULL");

                    b.HasIndex("email")
                        .IsUnique()
                        .HasFilter("[email] IS NOT NULL");

                    b.HasIndex("idUsuario");

                    b.ToTable("tb_aluno","Aluno");
                });

            modelBuilder.Entity("DAL.Entities.Coordenador", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ativo");

                    b.Property<string>("celular");

                    b.Property<DateTime?>("dtAlteracao");

                    b.Property<DateTime>("dtCadastro");

                    b.Property<DateTime?>("dtExclusao");

                    b.Property<string>("email");

                    b.Property<int>("idUsuario");

                    b.Property<int?>("idUsuarioAlteracao");

                    b.Property<int>("idUsuarioCadastro");

                    b.Property<int?>("idUsuarioExclusao");

                    b.Property<string>("nome");

                    b.HasKey("id");

                    b.HasIndex("celular")
                        .IsUnique()
                        .HasFilter("[celular] IS NOT NULL");

                    b.HasIndex("email")
                        .IsUnique()
                        .HasFilter("[email] IS NOT NULL");

                    b.HasIndex("idUsuario");

                    b.ToTable("tb_coordenador","Coordenador");
                });

            modelBuilder.Entity("DAL.Entities.Disciplina", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ativo");

                    b.Property<string>("bibliografiaBasica")
                        .HasMaxLength(100);

                    b.Property<string>("bibliografiaComplementar")
                        .HasMaxLength(100);

                    b.Property<int>("cargaHoraria");

                    b.Property<string>("competencias")
                        .HasMaxLength(500);

                    b.Property<string>("conteudoProgramatico")
                        .HasMaxLength(10);

                    b.Property<DateTime>("data");

                    b.Property<DateTime?>("dtAlteracao");

                    b.Property<DateTime>("dtCadastro");

                    b.Property<DateTime?>("dtExclusao");

                    b.Property<string>("emenda")
                        .HasMaxLength(100);

                    b.Property<string>("habilidades")
                        .HasMaxLength(100);

                    b.Property<int>("idCoordenador");

                    b.Property<int>("idStatusDisciplina");

                    b.Property<int?>("idUsuarioAlteracao");

                    b.Property<int>("idUsuarioCadastro");

                    b.Property<int?>("idUsuarioExclusao");

                    b.Property<string>("nome")
                        .HasMaxLength(50);

                    b.Property<string>("percentualPratico")
                        .HasMaxLength(10);

                    b.Property<string>("percentualTeorico")
                        .HasMaxLength(10);

                    b.Property<string>("planoEnsino")
                        .HasMaxLength(200);

                    b.HasKey("id");

                    b.HasIndex("idCoordenador");

                    b.HasIndex("idStatusDisciplina");

                    b.HasIndex("nome")
                        .IsUnique()
                        .HasFilter("[nome] IS NOT NULL");

                    b.ToTable("tb_disciplina","Disciplina");
                });

            modelBuilder.Entity("DAL.Entities.Professor", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("apelido");

                    b.Property<string>("ativo");

                    b.Property<string>("celular");

                    b.Property<DateTime?>("dtAlteracao");

                    b.Property<DateTime>("dtCadastro");

                    b.Property<DateTime?>("dtExclusao");

                    b.Property<string>("email");

                    b.Property<int>("idUsuario");

                    b.Property<int?>("idUsuarioAlteracao");

                    b.Property<int>("idUsuarioCadastro");

                    b.Property<int?>("idUsuarioExclusao");

                    b.Property<string>("nome");

                    b.HasKey("id");

                    b.HasIndex("celular")
                        .IsUnique()
                        .HasFilter("[celular] IS NOT NULL");

                    b.HasIndex("email")
                        .IsUnique()
                        .HasFilter("[email] IS NOT NULL");

                    b.HasIndex("idUsuario");

                    b.ToTable("tb_professor","Professor");
                });

            modelBuilder.Entity("DAL.Entities.StatusDisciplina", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ativo");

                    b.Property<string>("descricao")
                        .HasMaxLength(50);

                    b.Property<DateTime?>("dtAlteracao");

                    b.Property<DateTime>("dtCadastro");

                    b.Property<DateTime?>("dtExclusao");

                    b.Property<int?>("idUsuarioAlteracao");

                    b.Property<int>("idUsuarioCadastro");

                    b.Property<int?>("idUsuarioExclusao");

                    b.HasKey("id");

                    b.HasIndex("descricao")
                        .IsUnique()
                        .HasFilter("[descricao] IS NOT NULL");

                    b.ToTable("tb_status_disciplina","Disciplina");
                });

            modelBuilder.Entity("DAL.Entities.Usuario", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ativo");

                    b.Property<DateTime?>("dtAlteracao");

                    b.Property<DateTime>("dtCadastro");

                    b.Property<DateTime?>("dtExclusao");

                    b.Property<DateTime>("dtExpiracao");

                    b.Property<int?>("idUsuarioAlteracao");

                    b.Property<int>("idUsuarioCadastro");

                    b.Property<int?>("idUsuarioExclusao");

                    b.Property<string>("login");

                    b.Property<string>("senha");

                    b.HasKey("id");

                    b.ToTable("tb_usuario","Usuario");
                });

            modelBuilder.Entity("DAL.Entities.Aluno", b =>
                {
                    b.HasOne("DAL.Entities.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("idUsuario")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("DAL.Entities.Coordenador", b =>
                {
                    b.HasOne("DAL.Entities.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("idUsuario")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("DAL.Entities.Disciplina", b =>
                {
                    b.HasOne("DAL.Entities.Coordenador", "Coordenador")
                        .WithMany()
                        .HasForeignKey("idCoordenador")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DAL.Entities.StatusDisciplina", "StatusDisciplina")
                        .WithMany()
                        .HasForeignKey("idStatusDisciplina")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("DAL.Entities.Professor", b =>
                {
                    b.HasOne("DAL.Entities.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("idUsuario")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}