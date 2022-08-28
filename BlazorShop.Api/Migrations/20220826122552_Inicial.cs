using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorShop.Api.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IconCSS = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeUsuario = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ImagemUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    CategoriaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produtos_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Carrinhos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carrinhos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carrinhos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarrinhoItens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarrinhoId = table.Column<int>(type: "int", nullable: false),
                    ProdutoId = table.Column<int>(type: "int", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarrinhoItens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarrinhoItens_Carrinhos_CarrinhoId",
                        column: x => x.CarrinhoId,
                        principalTable: "Carrinhos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarrinhoItens_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "IconCSS", "Nome" },
                values: new object[,]
                {
                    { 1, "fas fa-spa", "Beleza" },
                    { 2, "fas fa-couch", "Moveis" },
                    { 3, "fas fa-headphones", "Eletronicos" },
                    { 4, "fas fa-shoe-prints", "Calcados" }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "NomeUsuario" },
                values: new object[,]
                {
                    { 1, "Macoratti" },
                    { 2, "Janice" }
                });

            migrationBuilder.InsertData(
                table: "Carrinhos",
                columns: new[] { "Id", "UsuarioId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "Produtos",
                columns: new[] { "Id", "CategoriaId", "Descricao", "ImagemUrl", "Nome", "Preco", "Quantidade" },
                values: new object[,]
                {
                    { 1, 1, "Um kit fornecido pela Natura, contendo produtos para cuidados com a pele", "/Imagens/Beleza/Beleza1.png", "Glossier - Beleza Kit", 100m, 100 },
                    { 2, 1, "Um kit fornecido pela Curology, contendo produtos para cuidados com a pele", "/Imagens/Beleza/Beleza2.png", "Curology - Kit para Pele", 50m, 45 },
                    { 3, 1, "Um kit fornecido pela Glossier, contendo produtos para cuidados com a pele", "/Imagens/Beleza/Beleza3.png", "Óleo de Coco Orgânico", 20m, 30 },
                    { 4, 1, "Um kit fornecido pela Curology, contendo produtos para cuidados com a pele", "/Imagens/Beleza/Beleza4.png", "Schwarzkopf - Kit de cuidados com a pele e cabelo", 50m, 60 },
                    { 5, 1, "Kit de cuidados com a pele, contendo produtos para cuidados com a pele e cabelos", "/Imagens/Beleza/Beleza5.png", "Kit de cuidados com a pele", 30m, 85 },
                    { 6, 3, "Air Pods - fones de ouvido sem fio intra-auriculares", "/Imagens/Eletronicos/eletronico1.png", "Fones de ouvidos", 100m, 120 },
                    { 7, 3, "Fones de ouvido dourados na orelha - esses fones de ouvido não são sem fio", "/Imagens/Eletronicos/eletronico2.png", "Fones de ouvido dourados", 40m, 200 },
                    { 8, 3, "Fones de ouvido pretos na orelha - esses fones de ouvido não são sem fio", "/Imagens/Eletronicos/eletronico3.png", "Fones de ouvido pretos", 40m, 300 },
                    { 9, 3, "Câmera Digital Sennheiser - Câmera digital de alta qualidade fornecida pela Sennheiser - inclui tripé", "/Imagens/Eletronicos/eletronico4.png", "Câmera digital Sennheiser com tripé", 600m, 20 },
                    { 10, 3, "Canon Digital Camera - Câmera digital de alta qualidade fornecida pela Canon", "/Imagens/Eletronicos/eletronico5.png", "Câmera Digital Canon", 500m, 15 },
                    { 11, 3, "Gameboy - Fornecido por Nintendo", "/Imagens/Eletronicos/tecnologia6.png", "Nintendo Gameboy", 100m, 60 },
                    { 12, 2, "Cadeira de escritório em couro preto muito confortável", "/Imagens/Moveis/moveis1.png", "Cadeira de escritório de couro preto", 50m, 212 },
                    { 13, 2, "Cadeira de escritório em couro rosa muito confortável", "/Imagens/Moveis/moveis2.png", "Cadeira de escritório de couro rosa", 50m, 112 },
                    { 14, 2, "Poltrona muito confortável", "/Imagens/Moveis/moveis3.png", "Espreguiçadeira", 70m, 90 },
                    { 15, 2, "Poltrona prateada muito confortável", "/Imagens/Moveis/moveis4.png", "Silver Lounge Chair", 120m, 95 },
                    { 16, 2, "Abajur de mesa de porcelana branco e azul", "/Imagens/Moveis/moveis6.png", "Luminária de mesa de porcelana", 15m, 100 },
                    { 17, 2, "Abajur de mesa de escritório", "/Imagens/Moveis/moveis7.png", "Office Table Lamp", 20m, 73 },
                    { 18, 4, "Tênis Puma confortáveis na maioria dos tamanhos", "/Imagens/Calcados/calcado1.png", "Tênis Puma", 100m, 50 },
                    { 19, 4, "Tênis coloridos - disponíveis na maioria dos tamanhos", "/Imagens/Calcados/calcado2.png", "Tênis Colodiros", 150m, 60 },
                    { 20, 4, "Tênis Nike azul - disponível na maioria dos tamanhos", "/Imagens/Calcados/calcado3.png", "Tênis Nike Azul", 200m, 70 },
                    { 21, 4, "Treinadores Hummel coloridos - disponíveis na maioria dos tamanhos", "/Imagens/Calcados/calcado4.png", "Tênis Hummel Coloridos", 120m, 120 },
                    { 22, 4, "Tênis Nike vermelho - disponível na maioria dos tamanhos", "/Imagens/Calcados/calcado5.png", "Tênis Nike Vermelho", 200m, 100 },
                    { 23, 4, "Sandálias Birkenstock - disponíveis na maioria dos tamanhos", "/Imagens/Calcados/calcado6.png", "Sandálidas Birkenstock", 50m, 150 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarrinhoItens_CarrinhoId",
                table: "CarrinhoItens",
                column: "CarrinhoId");

            migrationBuilder.CreateIndex(
                name: "IX_CarrinhoItens_ProdutoId",
                table: "CarrinhoItens",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_Carrinhos_UsuarioId",
                table: "Carrinhos",
                column: "UsuarioId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_CategoriaId",
                table: "Produtos",
                column: "CategoriaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarrinhoItens");

            migrationBuilder.DropTable(
                name: "Carrinhos");

            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
