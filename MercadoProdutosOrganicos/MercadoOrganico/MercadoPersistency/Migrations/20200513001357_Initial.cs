using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MercadoPersistency.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Enderecos",
                columns: table => new
                {
                    IdEndereco = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserID = table.Column<string>(nullable: false),
                    TipoLogradouro = table.Column<string>(maxLength: 20, nullable: false),
                    Logradouro = table.Column<string>(maxLength: 30, nullable: false),
                    Estado = table.Column<string>(maxLength: 2, nullable: false),
                    Cidade = table.Column<string>(maxLength: 25, nullable: false),
                    Bairro = table.Column<string>(maxLength: 20, nullable: false),
                    CEP = table.Column<string>(maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enderecos", x => x.IdEndereco);
                });

            migrationBuilder.CreateTable(
                name: "TiposProduto",
                columns: table => new
                {
                    IdTipoProduto = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descricao = table.Column<string>(maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposProduto", x => x.IdTipoProduto);
                });

            migrationBuilder.CreateTable(
                name: "TiposQuantidade",
                columns: table => new
                {
                    IdTipoQuantidade = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descricao = table.Column<string>(maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposQuantidade", x => x.IdTipoQuantidade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Compradores",
                columns: table => new
                {
                    IdComprador = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserID = table.Column<string>(nullable: false),
                    NomeResponsavel = table.Column<string>(maxLength: 40, nullable: false),
                    Telefone = table.Column<string>(maxLength: 15, nullable: false),
                    Email = table.Column<string>(maxLength: 30, nullable: false),
                    CPF = table.Column<string>(maxLength: 11, nullable: false),
                    IdEndereco = table.Column<int>(nullable: false),
                    IsAtivo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compradores", x => x.IdComprador);
                    table.ForeignKey(
                        name: "FK_Compradores_Enderecos_IdEndereco",
                        column: x => x.IdEndereco,
                        principalTable: "Enderecos",
                        principalColumn: "IdEndereco",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Produtores",
                columns: table => new
                {
                    IdProdutor = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserID = table.Column<string>(nullable: false),
                    RazaoSocial = table.Column<string>(maxLength: 20, nullable: false),
                    CNPJ = table.Column<string>(maxLength: 15, nullable: false),
                    IdEndereco = table.Column<int>(nullable: false),
                    IsAtivo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtores", x => x.IdProdutor);
                    table.ForeignKey(
                        name: "FK_Produtores_Enderecos_IdEndereco",
                        column: x => x.IdEndereco,
                        principalTable: "Enderecos",
                        principalColumn: "IdEndereco",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    IdPedido = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserID = table.Column<string>(nullable: false),
                    IdComprador = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.IdPedido);
                    table.ForeignKey(
                        name: "FK_Pedidos_Compradores_IdComprador",
                        column: x => x.IdComprador,
                        principalTable: "Compradores",
                        principalColumn: "IdComprador",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contatos",
                columns: table => new
                {
                    IdContato = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserID = table.Column<string>(nullable: false),
                    NomeResponsavel = table.Column<string>(maxLength: 40, nullable: false),
                    Telefone = table.Column<string>(maxLength: 15, nullable: false),
                    Email = table.Column<string>(maxLength: 30, nullable: false),
                    IdProdutor = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contatos", x => x.IdContato);
                    table.ForeignKey(
                        name: "FK_Contatos_Produtores_IdProdutor",
                        column: x => x.IdProdutor,
                        principalTable: "Produtores",
                        principalColumn: "IdProdutor",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    IdProduto = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserID = table.Column<string>(nullable: false),
                    Titulo = table.Column<string>(maxLength: 30, nullable: false),
                    Descricao = table.Column<string>(maxLength: 60, nullable: false),
                    Preco = table.Column<double>(maxLength: 30, nullable: false),
                    Quantidade = table.Column<int>(maxLength: 60, nullable: false),
                    IdProdutor = table.Column<int>(nullable: false),
                    IdTipoProduto = table.Column<int>(nullable: false),
                    IdTipoQuantidade = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.IdProduto);
                    table.ForeignKey(
                        name: "FK_Produtos_Produtores_IdProdutor",
                        column: x => x.IdProdutor,
                        principalTable: "Produtores",
                        principalColumn: "IdProdutor",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Produtos_TiposProduto_IdTipoProduto",
                        column: x => x.IdTipoProduto,
                        principalTable: "TiposProduto",
                        principalColumn: "IdTipoProduto",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Produtos_TiposQuantidade_IdTipoQuantidade",
                        column: x => x.IdTipoQuantidade,
                        principalTable: "TiposQuantidade",
                        principalColumn: "IdTipoQuantidade",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemsPedido",
                columns: table => new
                {
                    IdItemPedido = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserID = table.Column<string>(nullable: false),
                    IdProduto = table.Column<int>(nullable: false),
                    IdPedido = table.Column<int>(nullable: false),
                    SubTotal = table.Column<double>(nullable: false),
                    Quantidade = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsPedido", x => x.IdItemPedido);
                    table.ForeignKey(
                        name: "FK_ItemsPedido_Pedidos_IdPedido",
                        column: x => x.IdPedido,
                        principalTable: "Pedidos",
                        principalColumn: "IdPedido",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemsPedido_Produtos_IdProduto",
                        column: x => x.IdProduto,
                        principalTable: "Produtos",
                        principalColumn: "IdProduto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TiposProduto",
                columns: new[] { "IdTipoProduto", "Descricao" },
                values: new object[] { 1, "Frutas" });

            migrationBuilder.InsertData(
                table: "TiposProduto",
                columns: new[] { "IdTipoProduto", "Descricao" },
                values: new object[] { 2, "Verduras" });

            migrationBuilder.InsertData(
                table: "TiposProduto",
                columns: new[] { "IdTipoProduto", "Descricao" },
                values: new object[] { 3, "Legumes" });

            migrationBuilder.InsertData(
                table: "TiposProduto",
                columns: new[] { "IdTipoProduto", "Descricao" },
                values: new object[] { 4, "Grãos" });

            migrationBuilder.InsertData(
                table: "TiposQuantidade",
                columns: new[] { "IdTipoQuantidade", "Descricao" },
                values: new object[] { 1, "Unidade" });

            migrationBuilder.InsertData(
                table: "TiposQuantidade",
                columns: new[] { "IdTipoQuantidade", "Descricao" },
                values: new object[] { 2, "Quilos" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Compradores_IdEndereco",
                table: "Compradores",
                column: "IdEndereco",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contatos_IdProdutor",
                table: "Contatos",
                column: "IdProdutor");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsPedido_IdPedido",
                table: "ItemsPedido",
                column: "IdPedido");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsPedido_IdProduto",
                table: "ItemsPedido",
                column: "IdProduto");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_IdComprador",
                table: "Pedidos",
                column: "IdComprador");

            migrationBuilder.CreateIndex(
                name: "IX_Produtores_IdEndereco",
                table: "Produtores",
                column: "IdEndereco",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_IdProdutor",
                table: "Produtos",
                column: "IdProdutor");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_IdTipoProduto",
                table: "Produtos",
                column: "IdTipoProduto");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_IdTipoQuantidade",
                table: "Produtos",
                column: "IdTipoQuantidade");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Contatos");

            migrationBuilder.DropTable(
                name: "ItemsPedido");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "Compradores");

            migrationBuilder.DropTable(
                name: "Produtores");

            migrationBuilder.DropTable(
                name: "TiposProduto");

            migrationBuilder.DropTable(
                name: "TiposQuantidade");

            migrationBuilder.DropTable(
                name: "Enderecos");
        }
    }
}
