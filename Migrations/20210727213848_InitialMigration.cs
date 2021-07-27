using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoApi.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TodoItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssignTo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoItems", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "TodoItems",
                columns: new[] { "Id", "AssignTo", "City", "DueDate", "Name", "Priority", "Status" },
                values: new object[] { new Guid("36a12bfa-d2c4-4dc9-9906-eae63b4319a0"), "a1@gmail.com", "city1", new DateTime(2021, 7, 28, 0, 38, 48, 55, DateTimeKind.Local).AddTicks(5786), "Todo 1", 2, 1 });

            migrationBuilder.InsertData(
                table: "TodoItems",
                columns: new[] { "Id", "AssignTo", "City", "DueDate", "Name", "Priority", "Status" },
                values: new object[] { new Guid("36a12bfa-1111-2222-3333-eae63b4319a0"), "a2@gmail.com", "city2", new DateTime(2021, 7, 28, 0, 38, 48, 57, DateTimeKind.Local).AddTicks(4992), "Todo 2", 0, 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TodoItems");
        }
    }
}
