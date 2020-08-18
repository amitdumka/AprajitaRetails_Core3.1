using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AprajitaRetails.Migrations
{
    public partial class OnlineSaleReturns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OffDate",
                table: "OnlineVendor",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateTable(
                name: "OnlineSaleReturns",
                columns: table => new
                {
                    OnlineSaleReturnId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OnlineSaleId = table.Column<int>(nullable: false),
                    ReturnDate = table.Column<DateTime>(nullable: false),
                    InvNo = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    VoyagerInvoiceNo = table.Column<string>(nullable: true),
                    VoygerDate = table.Column<DateTime>(nullable: false),
                    VoyagerAmount = table.Column<decimal>(type: "money", nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    IsRecived = table.Column<bool>(nullable: false),
                    RecivedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnlineSaleReturns", x => x.OnlineSaleReturnId);
                    table.ForeignKey(
                        name: "FK_OnlineSaleReturns_OnlineSale_OnlineSaleId",
                        column: x => x.OnlineSaleId,
                        principalTable: "OnlineSale",
                        principalColumn: "OnlineSaleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1,
                column: "DateOfBirth",
                value: new DateTime(2020, 8, 19, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 2,
                column: "DateOfBirth",
                value: new DateTime(2020, 8, 19, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.CreateIndex(
                name: "IX_OnlineSaleReturns_OnlineSaleId",
                table: "OnlineSaleReturns",
                column: "OnlineSaleId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OnlineSaleReturns");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OffDate",
                table: "OnlineVendor",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1,
                column: "DateOfBirth",
                value: new DateTime(2020, 8, 12, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 2,
                column: "DateOfBirth",
                value: new DateTime(2020, 8, 12, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
