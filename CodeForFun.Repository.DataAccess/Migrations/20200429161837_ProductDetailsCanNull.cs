using Microsoft.EntityFrameworkCore.Migrations;

namespace CodeForFun.Repository.DataAccess.Migrations
{
    public partial class ProductDetailsCanNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductDetails_Products",
                table: "ProductDetails");

            migrationBuilder.AddColumn<int>(
                name: "ProductDetailId",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ProductDetails",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDetails_Products_Id",
                table: "ProductDetails",
                column: "Id",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductDetails_Products_Id",
                table: "ProductDetails");

            migrationBuilder.DropColumn(
                name: "ProductDetailId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDetails_Products",
                table: "ProductDetails",
                column: "Id",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
