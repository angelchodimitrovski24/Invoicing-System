using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Invoices.Web.API.Migrations
{
    public partial class SuppliersProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SuppierSupplierId",
                table: "Invoices",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    SupplierId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.SupplierId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_SuppierSupplierId",
                table: "Invoices",
                column: "SuppierSupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Suppliers_SuppierSupplierId",
                table: "Invoices",
                column: "SuppierSupplierId",
                principalTable: "Suppliers",
                principalColumn: "SupplierId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Suppliers_SuppierSupplierId",
                table: "Invoices");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_SuppierSupplierId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "SuppierSupplierId",
                table: "Invoices");
        }
    }
}
