using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AprajitaRetails.Migrations
{
    public partial class SaleTax : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1,
                column: "DateOfBirth",
                value: new DateTime(2020, 9, 4, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 2,
                column: "DateOfBirth",
                value: new DateTime(2020, 9, 4, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.InsertData(
                table: "SaleTaxTypes",
                columns: new[] { "SaleTaxTypeId", "CompositeRate", "TaxName", "TaxType" },
                values: new object[,]
                {
                    { 1, 5m, "Local Output GST@ 5%  ", 0 },
                    { 2, 12m, "Local Output GST@ 12%  ", 0 },
                    { 3, 5m, "Output IGST@ 5%  ", 3 },
                    { 4, 12m, "Output IGST@ 12%  ", 3 },
                    { 5, 5m, "Output Vat@ 12%  ", 4 },
                    { 6, 12m, "Output VAT@ 5%  ", 4 },
                    { 7, 5m, "Output Vat Free  ", 4 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SaleTaxTypes",
                keyColumn: "SaleTaxTypeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SaleTaxTypes",
                keyColumn: "SaleTaxTypeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SaleTaxTypes",
                keyColumn: "SaleTaxTypeId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SaleTaxTypes",
                keyColumn: "SaleTaxTypeId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "SaleTaxTypes",
                keyColumn: "SaleTaxTypeId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "SaleTaxTypes",
                keyColumn: "SaleTaxTypeId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "SaleTaxTypes",
                keyColumn: "SaleTaxTypeId",
                keyValue: 7);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1,
                column: "DateOfBirth",
                value: new DateTime(2020, 9, 2, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 2,
                column: "DateOfBirth",
                value: new DateTime(2020, 9, 2, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
