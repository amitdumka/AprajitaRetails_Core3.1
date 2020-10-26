using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace AprajitaRetails.Migrations
{
    public partial class RegisterUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "TalioringBookings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "TailoringDeliveries",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "StaffAdvanceReceipts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "StaffAdvancePayments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "SalaryPayments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "RegularInvoices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Receipts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "ProductPurchases",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "PettyCashExpenses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "PaySlips",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Payments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "OnlineSaleReturns",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "OnlineSale",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Expenses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "DueRecoverds",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "DailySales",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "CurrentSalaries",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "ChequesLogs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "CashReceipts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "CashPayments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "BankWithdrawals",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "BankDeposits",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Attendances",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "ArvindPayments",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RegisteredUsers",
                columns: table => new
                {
                    RegisteredUserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(nullable: true),
                    EmailId = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    LastLoggedIn = table.Column<DateTime>(nullable: false),
                    IsUserLoggedIn = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisteredUsers", x => x.RegisteredUserId);
                });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1,
                column: "DateOfBirth",
                value: new DateTime(2020, 9, 11, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 2,
                column: "DateOfBirth",
                value: new DateTime(2020, 9, 11, 0, 0, 0, 0, DateTimeKind.Local));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegisteredUsers");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "TalioringBookings");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "TailoringDeliveries");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "StaffAdvanceReceipts");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "StaffAdvancePayments");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "SalaryPayments");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "RegularInvoices");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Receipts");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "ProductPurchases");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "PettyCashExpenses");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "PaySlips");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "OnlineSaleReturns");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "OnlineSale");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "DueRecoverds");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "DailySales");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "CurrentSalaries");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "ChequesLogs");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "CashReceipts");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "CashPayments");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "BankWithdrawals");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "BankDeposits");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "ArvindPayments");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1,
                column: "DateOfBirth",
                value: new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 2,
                column: "DateOfBirth",
                value: new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
