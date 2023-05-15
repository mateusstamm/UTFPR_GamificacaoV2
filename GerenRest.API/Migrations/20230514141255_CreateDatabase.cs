using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    CategoriaID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    Descricao = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.CategoriaID);
                });

            migrationBuilder.CreateTable(
                name: "Garcons",
                columns: table => new
                {
                    GarconID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    Sobrenome = table.Column<string>(type: "TEXT", nullable: true),
                    NumIdentificao = table.Column<int>(type: "INTEGER", nullable: true),
                    Telefone = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Garcons", x => x.GarconID);
                });

            migrationBuilder.CreateTable(
                name: "Mesas",
                columns: table => new
                {
                    MesaID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Numero = table.Column<int>(type: "INTEGER", nullable: true),
                    Ocupada = table.Column<string>(type: "TEXT", nullable: true),
                    HoraAbertura = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mesas", x => x.MesaID);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    ProdutoID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    Descricao = table.Column<string>(type: "TEXT", nullable: true),
                    Preco = table.Column<float>(type: "REAL", nullable: true),
                    CategoriaID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.ProdutoID);
                    table.ForeignKey(
                        name: "FK_Produtos_Categorias_CategoriaID",
                        column: x => x.CategoriaID,
                        principalTable: "Categorias",
                        principalColumn: "CategoriaID");
                });

            migrationBuilder.CreateTable(
                name: "Atendimentos",
                columns: table => new
                {
                    AtendimentoID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MesaID = table.Column<int>(type: "INTEGER", nullable: true),
                    GarconID = table.Column<int>(type: "INTEGER", nullable: true),
                    HorarioAtendimento = table.Column<DateTime>(type: "TEXT", nullable: true),
                    PrecoTotal = table.Column<float>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atendimentos", x => x.AtendimentoID);
                    table.ForeignKey(
                        name: "FK_Atendimentos_Garcons_GarconID",
                        column: x => x.GarconID,
                        principalTable: "Garcons",
                        principalColumn: "GarconID");
                    table.ForeignKey(
                        name: "FK_Atendimentos_Mesas_MesaID",
                        column: x => x.MesaID,
                        principalTable: "Mesas",
                        principalColumn: "MesaID");
                });

            migrationBuilder.CreateTable(
                name: "AtendimentoProduto",
                columns: table => new
                {
                    ProdutoID = table.Column<int>(type: "INTEGER", nullable: false),
                    AtendimentoID = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantidade = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AtendimentoProduto", x => new { x.AtendimentoID, x.ProdutoID });
                    table.ForeignKey(
                        name: "FK_AtendimentoProduto_Atendimentos_AtendimentoID",
                        column: x => x.AtendimentoID,
                        principalTable: "Atendimentos",
                        principalColumn: "AtendimentoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AtendimentoProduto_Produtos_ProdutoID",
                        column: x => x.ProdutoID,
                        principalTable: "Produtos",
                        principalColumn: "ProdutoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AtendimentoProduto_ProdutoID",
                table: "AtendimentoProduto",
                column: "ProdutoID");

            migrationBuilder.CreateIndex(
                name: "IX_Atendimentos_GarconID",
                table: "Atendimentos",
                column: "GarconID");

            migrationBuilder.CreateIndex(
                name: "IX_Atendimentos_MesaID",
                table: "Atendimentos",
                column: "MesaID");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_CategoriaID",
                table: "Produtos",
                column: "CategoriaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AtendimentoProduto");

            migrationBuilder.DropTable(
                name: "Atendimentos");

            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "Garcons");

            migrationBuilder.DropTable(
                name: "Mesas");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
