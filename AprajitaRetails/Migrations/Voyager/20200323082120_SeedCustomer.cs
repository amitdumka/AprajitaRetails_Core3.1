using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AprajitaRetails.Migrations.Voyager
{
    public partial class SeedCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Age", "City", "DateOfBirth", "FirstName", "Gender", "LastName", "MobileNo", "NoOfBills", "TotalAmount" },
                values: new object[] { 1, 0, "Dumka", new DateTime(2020, 3, 23, 0, 0, 0, 0, DateTimeKind.Local), "Cash", 0, "Sale", "1234567890", 0, 0m });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Age", "City", "DateOfBirth", "FirstName", "Gender", "LastName", "MobileNo", "NoOfBills", "TotalAmount" },
                values: new object[] { 2, 0, "Dumka", new DateTime(2020, 3, 23, 0, 0, 0, 0, DateTimeKind.Local), "Card", 0, "Sale", "1234567890", 0, 0m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 2);
        }
    }
}
