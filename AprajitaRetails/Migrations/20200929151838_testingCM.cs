using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AprajitaRetails.Migrations
{
    public partial class testingCM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CardMachine",
                columns: new[] { "EDCId", "AccountNumberId", "EDCName", "EndDate", "IsWorking", "MID", "Remark", "StartDate", "StoreId", "TID" },
                values: new object[] { 1, 1, "SBI_EDC_CC", null, true, "NEEDTOENTER", "AUTOADDED FOR Testing Purpose", new DateTime(2016, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 101 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CardMachine",
                keyColumn: "EDCId",
                keyValue: 1);
        }
    }
}
