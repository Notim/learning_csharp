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

                    b.ToTable("tb_aluno");
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

                    b.ToTable("tb_coordenador");
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

                    b.ToTable("tb_professor");
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

                    b.ToTable("tb_usuario");
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
