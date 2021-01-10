using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Proiect.Migrations
{
    public partial class ExpenseCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShopId",
                table: "Expense");

            migrationBuilder.AlterColumn<decimal>(
                name: "Sum",
                table: "Expense",
                type: "decimal(6, 2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Expense",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DestinationID",
                table: "Expense",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Destination",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DestinationName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Destination", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseCategory",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpenseID = table.Column<int>(nullable: false),
                    CategoryID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseCategory", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ExpenseCategory_Category_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Category",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExpenseCategory_Expense_ExpenseID",
                        column: x => x.ExpenseID,
                        principalTable: "Expense",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Expense_DestinationID",
                table: "Expense",
                column: "DestinationID");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseCategory_CategoryID",
                table: "ExpenseCategory",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseCategory_ExpenseID",
                table: "ExpenseCategory",
                column: "ExpenseID");

            migrationBuilder.AddForeignKey(
                name: "FK_Expense_Destination_DestinationID",
                table: "Expense",
                column: "DestinationID",
                principalTable: "Destination",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expense_Destination_DestinationID",
                table: "Expense");

            migrationBuilder.DropTable(
                name: "Destination");

            migrationBuilder.DropTable(
                name: "ExpenseCategory");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Expense_DestinationID",
                table: "Expense");

            migrationBuilder.DropColumn(
                name: "DestinationID",
                table: "Expense");

            migrationBuilder.AlterColumn<int>(
                name: "Sum",
                table: "Expense",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(6, 2)");

            migrationBuilder.AlterColumn<string>(
                name: "Date",
                table: "Expense",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<decimal>(
                name: "ShopId",
                table: "Expense",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
