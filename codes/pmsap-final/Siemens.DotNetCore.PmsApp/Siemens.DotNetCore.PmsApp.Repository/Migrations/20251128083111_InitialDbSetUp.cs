using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Siemens.DotNetCore.PmsApp.Repository.Migrations
{
    /// <inheritdoc />
    public partial class InitialDbSetUp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    productid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productname = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: true, defaultValue: 0.0m),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.productid);
                });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "productid", "description", "price", "productname" },
                values: new object[,]
                {
                    { 1, "A high-performance laptop.", 1200.00m, "Laptop" },
                    { 2, "A latest model smartphone.", 800.00m, "Smartphone" },
                    { 3, "A lightweight tablet.", 500.00m, "Tablet" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "products");
        }
    }
}
