using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AprajitaRetails.Migrations
{
    public partial class HSNCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "HSNCode",
                table: "RegularSaleItems",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "HSNCode1",
                table: "RegularSaleItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HSNCode",
                table: "ProductItems",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HSNList",
                columns: table => new
                {
                    HSNCode = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    Rate = table.Column<int>(nullable: false),
                    EffectiveDate = table.Column<DateTime>(nullable: false),
                    CESS = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HSNList", x => x.HSNCode);
                });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1,
                column: "DateOfBirth",
                value: new DateTime(2020, 9, 6, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 2,
                column: "DateOfBirth",
                value: new DateTime(2020, 9, 6, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.CreateIndex(
                name: "IX_RegularSaleItems_HSNCode1",
                table: "RegularSaleItems",
                column: "HSNCode1");

            migrationBuilder.AddForeignKey(
                name: "FK_RegularSaleItems_HSNList_HSNCode1",
                table: "RegularSaleItems",
                column: "HSNCode1",
                principalTable: "HSNList",
                principalColumn: "HSNCode",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegularSaleItems_HSNList_HSNCode1",
                table: "RegularSaleItems");

            migrationBuilder.DropTable(
                name: "HSNList");

            migrationBuilder.DropIndex(
                name: "IX_RegularSaleItems_HSNCode1",
                table: "RegularSaleItems");

            migrationBuilder.DropColumn(
                name: "HSNCode",
                table: "RegularSaleItems");

            migrationBuilder.DropColumn(
                name: "HSNCode1",
                table: "RegularSaleItems");

            migrationBuilder.DropColumn(
                name: "HSNCode",
                table: "ProductItems");

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
        }
    }
}
