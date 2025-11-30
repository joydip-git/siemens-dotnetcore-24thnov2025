using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Siemens.DotNetCore.PmsApp.Repository.Migrations
{
    /// <inheritdoc />
    public partial class UserTableSetUp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    userid = table.Column<string>(type: "varchar(50)", nullable: false),
                    password = table.Column<string>(type: "varchar(12)", nullable: false),
                    Mobile = table.Column<long>(type: "bigint", nullable: true),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.userid);
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "userid", "DateOfBirth", "Mobile", "password" },
                values: new object[] { "joydip@gmail.com", null, null, "Joydip@123" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
