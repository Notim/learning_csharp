﻿// <auto-generated />
using System;
using DAL.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DAL.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20190217014856_Start")]
    partial class Start
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0-preview.19074.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DAL.Entities.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("active");

                    b.Property<string>("city");

                    b.Property<DateTime>("createdDate");

                    b.Property<string>("email");

                    b.Property<DateTime?>("excludeDate");

                    b.Property<string>("name");

                    b.Property<string>("state");

                    b.Property<string>("surname");

                    b.Property<string>("telephoneNumber");

                    b.HasKey("id");

                    b.ToTable("tb_user","User");
                });
#pragma warning restore 612, 618
        }
    }
}
