using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CamposDealerTest.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Venda_Cliente_ClienteIdCliente",
                table: "Venda");

            migrationBuilder.DropForeignKey(
                name: "FK_Venda_Produto_ProdutoIdProduto",
                table: "Venda");

            migrationBuilder.DropIndex(
                name: "IX_Venda_ClienteIdCliente",
                table: "Venda");

            migrationBuilder.DropColumn(
                name: "ClienteIdCliente",
                table: "Venda");

            migrationBuilder.DropColumn(
                name: "IdCliente",
                table: "Venda");

            migrationBuilder.RenameColumn(
                name: "ProdutoIdProduto",
                table: "Venda",
                newName: "ProdutoId");

            migrationBuilder.RenameColumn(
                name: "IdProduto",
                table: "Venda",
                newName: "ClienteId");

            migrationBuilder.RenameIndex(
                name: "IX_Venda_ProdutoIdProduto",
                table: "Venda",
                newName: "IX_Venda_ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_Venda_ClienteId",
                table: "Venda",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Venda_Cliente_ClienteId",
                table: "Venda",
                column: "ClienteId",
                principalTable: "Cliente",
                principalColumn: "IdCliente",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Venda_Produto_ProdutoId",
                table: "Venda",
                column: "ProdutoId",
                principalTable: "Produto",
                principalColumn: "IdProduto",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Venda_Cliente_ClienteId",
                table: "Venda");

            migrationBuilder.DropForeignKey(
                name: "FK_Venda_Produto_ProdutoId",
                table: "Venda");

            migrationBuilder.DropIndex(
                name: "IX_Venda_ClienteId",
                table: "Venda");

            migrationBuilder.RenameColumn(
                name: "ProdutoId",
                table: "Venda",
                newName: "ProdutoIdProduto");

            migrationBuilder.RenameColumn(
                name: "ClienteId",
                table: "Venda",
                newName: "IdProduto");

            migrationBuilder.RenameIndex(
                name: "IX_Venda_ProdutoId",
                table: "Venda",
                newName: "IX_Venda_ProdutoIdProduto");

            migrationBuilder.AddColumn<int>(
                name: "ClienteIdCliente",
                table: "Venda",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdCliente",
                table: "Venda",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Venda_ClienteIdCliente",
                table: "Venda",
                column: "ClienteIdCliente");

            migrationBuilder.AddForeignKey(
                name: "FK_Venda_Cliente_ClienteIdCliente",
                table: "Venda",
                column: "ClienteIdCliente",
                principalTable: "Cliente",
                principalColumn: "IdCliente",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Venda_Produto_ProdutoIdProduto",
                table: "Venda",
                column: "ProdutoIdProduto",
                principalTable: "Produto",
                principalColumn: "IdProduto",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
