using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace entities.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_classroom",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    desc = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_classroom", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_person",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(nullable: true),
                    birthday = table.Column<DateTime>(nullable: true),
                    idClassroom = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_person", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_person_tb_classroom_idClassroom",
                        column: x => x.idClassroom,
                        principalTable: "tb_classroom",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_person_idClassroom",
                table: "tb_person",
                column: "idClassroom");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_person");

            migrationBuilder.DropTable(
                name: "tb_classroom");
        }
    }
}
