using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HBSIS_Padawan.Sistema.Boletim.Repository.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cursos",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "Varchar(50)", nullable: false),
                    Situacao = table.Column<string>(nullable: false, defaultValue: "Ativo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cursos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Materias",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "Varchar(50)", nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", nullable: false),
                    Cadastro = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    Status = table.Column<string>(nullable: false, defaultValue: "Ativo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(maxLength: 50, nullable: false),
                    Senha = table.Column<string>(nullable: false),
                    Tipo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Alunos",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CursoId = table.Column<long>(nullable: false),
                    Nome = table.Column<string>(maxLength: 20, nullable: false),
                    Sobrenome = table.Column<string>(maxLength: 20, nullable: false),
                    Cpf = table.Column<string>(nullable: false),
                    Nascimento = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alunos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alunos_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CursoMateria",
                columns: table => new
                {
                    CursoId = table.Column<long>(nullable: false),
                    MateriaId = table.Column<long>(nullable: false),
                    Id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CursoMateria", x => new { x.CursoId, x.MateriaId });
                    table.UniqueConstraint("AK_CursoMateria_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CursoMateria_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CursoMateria_Materias_MateriaId",
                        column: x => x.MateriaId,
                        principalTable: "Materias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CursoMateriaAluno",
                columns: table => new
                {
                    CursoMateriaId = table.Column<long>(nullable: false),
                    AlunoId = table.Column<long>(nullable: false),
                    Nota = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CursoMateriaAluno", x => new { x.AlunoId, x.CursoMateriaId });
                    table.ForeignKey(
                        name: "FK_CursoMateriaAluno_Alunos_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Alunos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CursoMateriaAluno_CursoMateria_CursoMateriaId",
                        column: x => x.CursoMateriaId,
                        principalTable: "CursoMateria",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alunos_CursoId",
                table: "Alunos",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_CursoMateria_MateriaId",
                table: "CursoMateria",
                column: "MateriaId");

            migrationBuilder.CreateIndex(
                name: "IX_CursoMateriaAluno_CursoMateriaId",
                table: "CursoMateriaAluno",
                column: "CursoMateriaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CursoMateriaAluno");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Alunos");

            migrationBuilder.DropTable(
                name: "CursoMateria");

            migrationBuilder.DropTable(
                name: "Cursos");

            migrationBuilder.DropTable(
                name: "Materias");
        }
    }
}
