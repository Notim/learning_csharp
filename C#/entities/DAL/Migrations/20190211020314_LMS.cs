using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class LMS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_usuario",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    login = table.Column<string>(nullable: true),
                    senha = table.Column<string>(nullable: true),
                    dtExpiracao = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_usuario", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_aluno",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    idUsuario = table.Column<int>(nullable: false),
                    nome = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    celular = table.Column<string>(nullable: true),
                    ra = table.Column<string>(nullable: true),
                    foto = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_aluno", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_aluno_tb_usuario_idUsuario",
                        column: x => x.idUsuario,
                        principalTable: "tb_usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_coordenador",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    idUsuario = table.Column<int>(nullable: false),
                    nome = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    celular = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_coordenador", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_coordenador_tb_usuario_idUsuario",
                        column: x => x.idUsuario,
                        principalTable: "tb_usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_professor",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    idUsuario = table.Column<int>(nullable: false),
                    nome = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    celular = table.Column<string>(nullable: true),
                    apelido = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_professor", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_professor_tb_usuario_idUsuario",
                        column: x => x.idUsuario,
                        principalTable: "tb_usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_aluno_celular",
                table: "tb_aluno",
                column: "celular",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_aluno_email",
                table: "tb_aluno",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_aluno_idUsuario",
                table: "tb_aluno",
                column: "idUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_tb_coordenador_celular",
                table: "tb_coordenador",
                column: "celular",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_coordenador_email",
                table: "tb_coordenador",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_coordenador_idUsuario",
                table: "tb_coordenador",
                column: "idUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_tb_professor_celular",
                table: "tb_professor",
                column: "celular",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_professor_email",
                table: "tb_professor",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_professor_idUsuario",
                table: "tb_professor",
                column: "idUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_tb_usuario_login",
                table: "tb_usuario",
                column: "login",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_aluno");

            migrationBuilder.DropTable(
                name: "tb_coordenador");

            migrationBuilder.DropTable(
                name: "tb_professor");

            migrationBuilder.DropTable(
                name: "tb_usuario");
        }
    }
}
