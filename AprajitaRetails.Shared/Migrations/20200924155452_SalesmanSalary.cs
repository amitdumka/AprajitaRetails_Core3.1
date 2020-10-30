using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace AprajitaRetails.Migrations
{
    public partial class SalesmanSalary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StoreLocationId",
                table: "Salesmen");

            migrationBuilder.DropColumn(
                name: "SundaySalary",
                table: "CurrentSalaries");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Salesmen",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsTailoring",
                table: "CurrentSalaries",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFullMonth",
                table: "CurrentSalaries",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1,
                column: "DateOfBirth",
                value: new DateTime(2020, 9, 24, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 2,
                column: "DateOfBirth",
                value: new DateTime(2020, 9, 24, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.CreateIndex(
                name: "IX_Salesmen_EmployeeId",
                table: "Salesmen",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Salesmen_Employees_EmployeeId",
                table: "Salesmen",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Salesmen_Employees_EmployeeId",
                table: "Salesmen");

            migrationBuilder.DropIndex(
                name: "IX_Salesmen_EmployeeId",
                table: "Salesmen");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Salesmen");

            migrationBuilder.DropColumn(
                name: "IsFullMonth",
                table: "CurrentSalaries");

            migrationBuilder.AddColumn<int>(
                name: "StoreLocationId",
                table: "Salesmen",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<bool>(
                name: "IsTailoring",
                table: "CurrentSalaries",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AddColumn<decimal>(
                name: "SundaySalary",
                table: "CurrentSalaries",
                type: "money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1,
                column: "DateOfBirth",
                value: new DateTime(2020, 9, 21, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 2,
                column: "DateOfBirth",
                value: new DateTime(2020, 9, 21, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
