using Microsoft.EntityFrameworkCore.Migrations;

namespace AprajitaRetails.Migrations
{
    public partial class defaultSeedAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Banks",
                columns: new[] { "BankId", "BankName" },
                values: new object[,]
                {
                    { 1, "SBI" },
                    { 2, "ICICI" },
                    { 3, "Bandhan Bank" },
                    { 4, "PNB" },
                    { 5, "BOB" },
                    { 6, "Axis" },
                    { 7, "HDFC" }
                });

            migrationBuilder.InsertData(
                table: "Salesmen",
                columns: new[] { "SalesmanId", "SalesmanName" },
                values: new object[,]
                {
                    { 3, "Manager" },
                    { 2, "Mukesh Mandal" },
                    { 1, "Sanjeev Mishra" }
                });

            migrationBuilder.InsertData(
                table: "TranscationModes",
                columns: new[] { "TranscationModeId", "Transcation" },
                values: new object[,]
                {
                    { 1, "Home Expenses" },
                    { 2, "Other Home Expenses" },
                    { 3, "Mukesh(Home Staff)" },
                    { 4, "Amit Kumar" },
                    { 5, "Amit Kumar Expenses" },
                    { 6, "CashIn" },
                    { 7, "CashOut" },
                    { 8, "Regular" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Salesmen",
                keyColumn: "SalesmanId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Salesmen",
                keyColumn: "SalesmanId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Salesmen",
                keyColumn: "SalesmanId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TranscationModes",
                keyColumn: "TranscationModeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TranscationModes",
                keyColumn: "TranscationModeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TranscationModes",
                keyColumn: "TranscationModeId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TranscationModes",
                keyColumn: "TranscationModeId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TranscationModes",
                keyColumn: "TranscationModeId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "TranscationModes",
                keyColumn: "TranscationModeId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "TranscationModes",
                keyColumn: "TranscationModeId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "TranscationModes",
                keyColumn: "TranscationModeId",
                keyValue: 8);
        }
    }
}
