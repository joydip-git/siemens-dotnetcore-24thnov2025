using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EFCoreDemo.Migrations
{
    /// <inheritdoc />
    public partial class InitialDbSetUp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder
                .CreateTable(
                name: "departments",
                columns: table => new
                {
                    deptid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    deptname = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_departments_deptid", x => x.deptid);
                });

            migrationBuilder
                .CreateTable(
                name: "employees",
                columns: table => new
                {
                    empid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    empname = table.Column<string>(type: "varchar(50)", nullable: false),
                    salary = table.Column<decimal>(type: "decimal(18,2)", nullable: true, defaultValue: 0.0m),
                    deptid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_employees_empid", x => x.empid);
                    table.ForeignKey(
                        name: "FK_Employee_Department",
                        column: x => x.deptid,
                        principalTable: "departments",
                        principalColumn: "deptid",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder
                .InsertData(
                table: "departments",
                columns: new[] { "deptid", "deptname" },
                values: new object[,]
                {
                    { 1, "Human Resources" },
                    { 2, "Information Technology" }
                });

            migrationBuilder
                .InsertData(
                table: "employees",
                columns: new[] { "empid", "deptid", "empname", "salary" },
                values: new object[,]
                {
                    { 1, 1, "John Doe", 60000.00m },
                    { 2, 2, "Jane Smith", 75000.00m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_employees_deptid",
                table: "employees",
                column: "deptid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "employees");

            migrationBuilder.DropTable(
                name: "departments");
        }
    }
}
