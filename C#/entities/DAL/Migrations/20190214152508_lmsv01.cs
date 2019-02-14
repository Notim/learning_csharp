using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class lmsv01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Aluno");

            migrationBuilder.EnsureSchema(
                name: "Coordenador");

            migrationBuilder.EnsureSchema(
                name: "Disciplina");

            migrationBuilder.EnsureSchema(
                name: "Professor");

            migrationBuilder.EnsureSchema(
                name: "Usuario");

            migrationBuilder.CreateTable(
                name: "tb_status_disciplina",
                schema: "Disciplina",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    dtCadastro = table.Column<DateTime>(nullable: false),
                    dtAlteracao = table.Column<DateTime>(nullable: true),
                    dtExclusao = table.Column<DateTime>(nullable: true),
                    idUsuarioCadastro = table.Column<int>(nullable: false),
                    idUsuarioAlteracao = table.Column<int>(nullable: true),
                    idUsuarioExclusao = table.Column<int>(nullable: true),
                    ativo = table.Column<string>(nullable: true),
                    descricao = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_status_disciplina", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_usuario",
                schema: "Usuario",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    dtCadastro = table.Column<DateTime>(nullable: false),
                    dtAlteracao = table.Column<DateTime>(nullable: true),
                    dtExclusao = table.Column<DateTime>(nullable: true),
                    idUsuarioCadastro = table.Column<int>(nullable: false),
                    idUsuarioAlteracao = table.Column<int>(nullable: true),
                    idUsuarioExclusao = table.Column<int>(nullable: true),
                    ativo = table.Column<string>(nullable: true),
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
                schema: "Aluno",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    dtCadastro = table.Column<DateTime>(nullable: false),
                    dtAlteracao = table.Column<DateTime>(nullable: true),
                    dtExclusao = table.Column<DateTime>(nullable: true),
                    idUsuarioCadastro = table.Column<int>(nullable: false),
                    idUsuarioAlteracao = table.Column<int>(nullable: true),
                    idUsuarioExclusao = table.Column<int>(nullable: true),
                    ativo = table.Column<string>(nullable: true),
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
                        principalSchema: "Usuario",
                        principalTable: "tb_usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_coordenador",
                schema: "Coordenador",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    dtCadastro = table.Column<DateTime>(nullable: false),
                    dtAlteracao = table.Column<DateTime>(nullable: true),
                    dtExclusao = table.Column<DateTime>(nullable: true),
                    idUsuarioCadastro = table.Column<int>(nullable: false),
                    idUsuarioAlteracao = table.Column<int>(nullable: true),
                    idUsuarioExclusao = table.Column<int>(nullable: true),
                    ativo = table.Column<string>(nullable: true),
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
                        principalSchema: "Usuario",
                        principalTable: "tb_usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_professor",
                schema: "Professor",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    dtCadastro = table.Column<DateTime>(nullable: false),
                    dtAlteracao = table.Column<DateTime>(nullable: true),
                    dtExclusao = table.Column<DateTime>(nullable: true),
                    idUsuarioCadastro = table.Column<int>(nullable: false),
                    idUsuarioAlteracao = table.Column<int>(nullable: true),
                    idUsuarioExclusao = table.Column<int>(nullable: true),
                    ativo = table.Column<string>(nullable: true),
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
                        principalSchema: "Usuario",
                        principalTable: "tb_usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_disciplina",
                schema: "Disciplina",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    dtCadastro = table.Column<DateTime>(nullable: false),
                    dtAlteracao = table.Column<DateTime>(nullable: true),
                    dtExclusao = table.Column<DateTime>(nullable: true),
                    idUsuarioCadastro = table.Column<int>(nullable: false),
                    idUsuarioAlteracao = table.Column<int>(nullable: true),
                    idUsuarioExclusao = table.Column<int>(nullable: true),
                    ativo = table.Column<string>(nullable: true),
                    idCoordenador = table.Column<int>(nullable: false),
                    idStatusDisciplina = table.Column<int>(nullable: false),
                    nome = table.Column<string>(maxLength: 50, nullable: true),
                    data = table.Column<DateTime>(nullable: false),
                    planoEnsino = table.Column<string>(maxLength: 200, nullable: true),
                    cargaHoraria = table.Column<int>(nullable: false),
                    competencias = table.Column<string>(maxLength: 500, nullable: true),
                    habilidades = table.Column<string>(maxLength: 100, nullable: true),
                    emenda = table.Column<string>(maxLength: 100, nullable: true),
                    conteudoProgramatico = table.Column<string>(maxLength: 10, nullable: true),
                    bibliografiaBasica = table.Column<string>(maxLength: 100, nullable: true),
                    bibliografiaComplementar = table.Column<string>(maxLength: 100, nullable: true),
                    percentualPratico = table.Column<string>(maxLength: 10, nullable: true),
                    percentualTeorico = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_disciplina", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_disciplina_tb_coordenador_idCoordenador",
                        column: x => x.idCoordenador,
                        principalSchema: "Coordenador",
                        principalTable: "tb_coordenador",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_disciplina_tb_status_disciplina_idStatusDisciplina",
                        column: x => x.idStatusDisciplina,
                        principalSchema: "Disciplina",
                        principalTable: "tb_status_disciplina",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_aluno_celular",
                schema: "Aluno",
                table: "tb_aluno",
                column: "celular",
                unique: true,
                filter: "[celular] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tb_aluno_email",
                schema: "Aluno",
                table: "tb_aluno",
                column: "email",
                unique: true,
                filter: "[email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tb_aluno_idUsuario",
                schema: "Aluno",
                table: "tb_aluno",
                column: "idUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_tb_coordenador_celular",
                schema: "Coordenador",
                table: "tb_coordenador",
                column: "celular",
                unique: true,
                filter: "[celular] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tb_coordenador_email",
                schema: "Coordenador",
                table: "tb_coordenador",
                column: "email",
                unique: true,
                filter: "[email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tb_coordenador_idUsuario",
                schema: "Coordenador",
                table: "tb_coordenador",
                column: "idUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_tb_disciplina_idCoordenador",
                schema: "Disciplina",
                table: "tb_disciplina",
                column: "idCoordenador");

            migrationBuilder.CreateIndex(
                name: "IX_tb_disciplina_idStatusDisciplina",
                schema: "Disciplina",
                table: "tb_disciplina",
                column: "idStatusDisciplina");

            migrationBuilder.CreateIndex(
                name: "IX_tb_disciplina_nome",
                schema: "Disciplina",
                table: "tb_disciplina",
                column: "nome",
                unique: true,
                filter: "[nome] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tb_status_disciplina_descricao",
                schema: "Disciplina",
                table: "tb_status_disciplina",
                column: "descricao",
                unique: true,
                filter: "[descricao] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tb_professor_celular",
                schema: "Professor",
                table: "tb_professor",
                column: "celular",
                unique: true,
                filter: "[celular] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tb_professor_email",
                schema: "Professor",
                table: "tb_professor",
                column: "email",
                unique: true,
                filter: "[email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tb_professor_idUsuario",
                schema: "Professor",
                table: "tb_professor",
                column: "idUsuario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_aluno",
                schema: "Aluno");

            migrationBuilder.DropTable(
                name: "tb_disciplina",
                schema: "Disciplina");

            migrationBuilder.DropTable(
                name: "tb_professor",
                schema: "Professor");

            migrationBuilder.DropTable(
                name: "tb_coordenador",
                schema: "Coordenador");

            migrationBuilder.DropTable(
                name: "tb_status_disciplina",
                schema: "Disciplina");

            migrationBuilder.DropTable(
                name: "tb_usuario",
                schema: "Usuario");
        }
    }
}
