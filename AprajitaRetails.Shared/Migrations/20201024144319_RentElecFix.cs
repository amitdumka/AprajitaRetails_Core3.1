using Microsoft.EntityFrameworkCore.Migrations;

namespace AprajitaRetails.Migrations
{
    public partial class RentElecFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillPayments_EletricityBills_BillEletricityBillId",
                table: "BillPayments");

            migrationBuilder.DropIndex(
                name: "IX_BillPayments_BillEletricityBillId",
                table: "BillPayments");

            migrationBuilder.DropColumn(
                name: "BillEletricityBillId",
                table: "BillPayments");

            migrationBuilder.AddColumn<int>(
                name: "EletricityBillId",
                table: "BillPayments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BillPayments_EletricityBillId",
                table: "BillPayments",
                column: "EletricityBillId");

            migrationBuilder.AddForeignKey(
                name: "FK_BillPayments_EletricityBills_EletricityBillId",
                table: "BillPayments",
                column: "EletricityBillId",
                principalTable: "EletricityBills",
                principalColumn: "EletricityBillId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillPayments_EletricityBills_EletricityBillId",
                table: "BillPayments");

            migrationBuilder.DropIndex(
                name: "IX_BillPayments_EletricityBillId",
                table: "BillPayments");

            migrationBuilder.DropColumn(
                name: "EletricityBillId",
                table: "BillPayments");

            migrationBuilder.AddColumn<int>(
                name: "BillEletricityBillId",
                table: "BillPayments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BillPayments_BillEletricityBillId",
                table: "BillPayments",
                column: "BillEletricityBillId");

            migrationBuilder.AddForeignKey(
                name: "FK_BillPayments_EletricityBills_BillEletricityBillId",
                table: "BillPayments",
                column: "BillEletricityBillId",
                principalTable: "EletricityBills",
                principalColumn: "EletricityBillId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
