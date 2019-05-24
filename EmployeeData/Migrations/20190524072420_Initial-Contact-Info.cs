using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CompanyData.Migrations
{
    public partial class InitialContactInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    department_id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    name = table.Column<string>(nullable: true),
                    modified_date = table.Column<DateTime>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.department_id);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    employee_id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    name = table.Column<string>(nullable: true),
                    home_phone = table.Column<string>(nullable: true),
                    cell_phone = table.Column<string>(nullable: true),
                    address = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    created_date = table.Column<DateTime>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    status = table.Column<int>(nullable: false),
                    departmentId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.employee_id);
                    table.ForeignKey(
                        name: "FK_Employee_Department_departmentId",
                        column: x => x.departmentId,
                        principalTable: "Department",
                        principalColumn: "department_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employee_departmentId",
                table: "Employee",
                column: "departmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Department");
        }
    }
}
