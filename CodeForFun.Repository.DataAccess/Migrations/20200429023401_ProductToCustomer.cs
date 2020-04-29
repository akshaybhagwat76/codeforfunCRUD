using Microsoft.EntityFrameworkCore.Migrations;

namespace CodeForFun.Repository.DataAccess.Migrations
{
    public partial class ProductToCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsToCustomers",
                table: "ProductsToCustomers");

            migrationBuilder.AlterColumn<int>(
                name: "ProductsToCustomerId",
                table: "ProductsToCustomers",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsToCustomers",
                table: "ProductsToCustomers",
                column: "ProductsToCustomerId");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleID", "Name" },
                values: new object[] { 1, "Admin" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleID", "Name" },
                values: new object[] { 2, "User" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductsToCustomers_CustomerId",
                table: "ProductsToCustomers",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsToCustomers",
                table: "ProductsToCustomers");

            migrationBuilder.DropIndex(
                name: "IX_ProductsToCustomers_CustomerId",
                table: "ProductsToCustomers");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleID",
                keyValue: 2);

            migrationBuilder.AlterColumn<int>(
                name: "ProductsToCustomerId",
                table: "ProductsToCustomers",
                type: "int",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsToCustomers",
                table: "ProductsToCustomers",
                columns: new[] { "CustomerId", "ProductId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
