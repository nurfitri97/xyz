using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XyzSystem.Migrations
{
    /// <inheritdoc />
    public partial class New : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryProduct_Categories_CategoryId",
                table: "CategoryProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryProduct_Products_ProductCode",
                table: "CategoryProduct");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "CategoryProduct",
                newName: "ProductsProductCode");

            migrationBuilder.RenameColumn(
                name: "ProductCode",
                table: "CategoryProduct",
                newName: "CategoriesCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryProduct_CategoryId",
                table: "CategoryProduct",
                newName: "IX_CategoryProduct_ProductsProductCode");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryProduct_Categories_CategoriesCategoryId",
                table: "CategoryProduct",
                column: "CategoriesCategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryProduct_Products_ProductsProductCode",
                table: "CategoryProduct",
                column: "ProductsProductCode",
                principalTable: "Products",
                principalColumn: "ProductCode",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryProduct_Categories_CategoriesCategoryId",
                table: "CategoryProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryProduct_Products_ProductsProductCode",
                table: "CategoryProduct");

            migrationBuilder.RenameColumn(
                name: "ProductsProductCode",
                table: "CategoryProduct",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "CategoriesCategoryId",
                table: "CategoryProduct",
                newName: "ProductCode");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryProduct_ProductsProductCode",
                table: "CategoryProduct",
                newName: "IX_CategoryProduct_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryProduct_Categories_CategoryId",
                table: "CategoryProduct",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryProduct_Products_ProductCode",
                table: "CategoryProduct",
                column: "ProductCode",
                principalTable: "Products",
                principalColumn: "ProductCode",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
