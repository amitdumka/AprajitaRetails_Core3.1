using Microsoft.EntityFrameworkCore.Migrations;

namespace AprajitaRetails.Migrations.Voyager
{
    public partial class defaultSeedAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SalesPerson",
                columns: new[] { "SalesPersonId", "SalesmanName" },
                values: new object[] { 1, "Sanjeev Mishra" });

            migrationBuilder.InsertData(
                table: "SalesPerson",
                columns: new[] { "SalesPersonId", "SalesmanName" },
                values: new object[] { 2, "Mukesh Mandal" });

            migrationBuilder.InsertData(
                table: "SalesPerson",
                columns: new[] { "SalesPersonId", "SalesmanName" },
                values: new object[] { 3, "Manager" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SalesPerson",
                keyColumn: "SalesPersonId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SalesPerson",
                keyColumn: "SalesPersonId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SalesPerson",
                keyColumn: "SalesPersonId",
                keyValue: 3);
        }
    }
}
