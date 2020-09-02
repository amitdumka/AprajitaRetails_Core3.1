using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AprajitaRetails.Migrations
{
    public partial class PayId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardDetails_PaymentDetails_PaymentDetailId",
                table: "CardDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentDetails",
                table: "PaymentDetails");

            migrationBuilder.DropIndex(
                name: "IX_CardDetails_PaymentDetailId",
                table: "CardDetails");

            migrationBuilder.DropColumn(
                name: "PaymentDetailId",
                table: "CardDetails");

            migrationBuilder.AlterColumn<string>(
                name: "InvoiceNo",
                table: "PaymentDetails",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PaymentDetailId",
                table: "PaymentDetails",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "InvoiceNo",
                table: "CardDetails",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentDetails",
                table: "PaymentDetails",
                column: "InvoiceNo");

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

            migrationBuilder.CreateIndex(
                name: "IX_CardDetails_InvoiceNo",
                table: "CardDetails",
                column: "InvoiceNo",
                unique: true,
                filter: "[InvoiceNo] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_CardDetails_PaymentDetails_InvoiceNo",
                table: "CardDetails",
                column: "InvoiceNo",
                principalTable: "PaymentDetails",
                principalColumn: "InvoiceNo",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardDetails_PaymentDetails_InvoiceNo",
                table: "CardDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentDetails",
                table: "PaymentDetails");

            migrationBuilder.DropIndex(
                name: "IX_CardDetails_InvoiceNo",
                table: "CardDetails");

            migrationBuilder.DropColumn(
                name: "InvoiceNo",
                table: "CardDetails");

            migrationBuilder.AlterColumn<int>(
                name: "PaymentDetailId",
                table: "PaymentDetails",
                type: "int",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "InvoiceNo",
                table: "PaymentDetails",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "PaymentDetailId",
                table: "CardDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentDetails",
                table: "PaymentDetails",
                column: "PaymentDetailId");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1,
                column: "DateOfBirth",
                value: new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 2,
                column: "DateOfBirth",
                value: new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.CreateIndex(
                name: "IX_CardDetails_PaymentDetailId",
                table: "CardDetails",
                column: "PaymentDetailId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CardDetails_PaymentDetails_PaymentDetailId",
                table: "CardDetails",
                column: "PaymentDetailId",
                principalTable: "PaymentDetails",
                principalColumn: "PaymentDetailId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
