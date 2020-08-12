using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AprajitaRetails.Migrations
{
    public partial class BookEntry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ImportBookEntries",
                columns: table => new
                {
                    BookEntryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OnDate = table.Column<DateTime>(nullable: false),
                    LedgerBy = table.Column<int>(nullable: false),
                    LedgerTo = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    VoucherType = table.Column<int>(nullable: false),
                    Naration = table.Column<string>(nullable: true),
                    IsConsumed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportBookEntries", x => x.BookEntryId);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImportBookEntries");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1,
                column: "DateOfBirth",
                value: new DateTime(2020, 8, 9, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 2,
                column: "DateOfBirth",
                value: new DateTime(2020, 8, 9, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
