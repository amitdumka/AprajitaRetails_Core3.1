using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace AprajitaRetails.Migrations
{
    public partial class BankSystem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountType",
                table: "AccountNumbers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BankStatements",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountNumberId = table.Column<int>(nullable: false),
                    OnDateValue = table.Column<DateTime>(nullable: false),
                    OnDateTranscation = table.Column<DateTime>(nullable: false),
                    ChequeNumber = table.Column<string>(nullable: true),
                    TransactionRemarks = table.Column<string>(nullable: true),
                    WithdrawalAmount = table.Column<decimal>(nullable: false),
                    DepositAmount = table.Column<decimal>(nullable: false),
                    Balance = table.Column<decimal>(nullable: false),
                    Remark = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankStatements", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BankStatements_AccountNumbers_AccountNumberId",
                        column: x => x.AccountNumberId,
                        principalTable: "AccountNumbers",
                        principalColumn: "AccountNumberId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1,
                column: "DateOfBirth",
                value: new DateTime(2020, 9, 27, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 2,
                column: "DateOfBirth",
                value: new DateTime(2020, 9, 27, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.CreateIndex(
                name: "IX_BankStatements_AccountNumberId",
                table: "BankStatements",
                column: "AccountNumberId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankStatements");

            migrationBuilder.DropColumn(
                name: "AccountType",
                table: "AccountNumbers");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1,
                column: "DateOfBirth",
                value: new DateTime(2020, 9, 26, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 2,
                column: "DateOfBirth",
                value: new DateTime(2020, 9, 26, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
