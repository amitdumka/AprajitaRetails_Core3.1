using Microsoft.EntityFrameworkCore.Migrations;

namespace AprajitaRetails.Migrations.Voyager
{
    public partial class Onetowthere : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ManualInvoice_ManualSalePaymentDetail_PaymentDetailManualSalePaymentDetailId",
                table: "ManualInvoice");

            migrationBuilder.DropTable(
                name: "ManualSalePaymentDetail");

            migrationBuilder.DropIndex(
                name: "IX_ManualInvoice_PaymentDetailManualSalePaymentDetailId",
                table: "ManualInvoice");

            migrationBuilder.DropColumn(
                name: "PaymentDetailManualSalePaymentDetailId",
                table: "ManualInvoice");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentDetailManualSalePaymentDetailId",
                table: "ManualInvoice",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ManualSalePaymentDetail",
                columns: table => new
                {
                    ManualSalePaymentDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardAmount = table.Column<decimal>(type: "money", nullable: false),
                    CashAmount = table.Column<decimal>(type: "money", nullable: false),
                    MixAmount = table.Column<decimal>(type: "money", nullable: false),
                    PayMode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManualSalePaymentDetail", x => x.ManualSalePaymentDetailId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ManualInvoice_PaymentDetailManualSalePaymentDetailId",
                table: "ManualInvoice",
                column: "PaymentDetailManualSalePaymentDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_ManualInvoice_ManualSalePaymentDetail_PaymentDetailManualSalePaymentDetailId",
                table: "ManualInvoice",
                column: "PaymentDetailManualSalePaymentDetailId",
                principalTable: "ManualSalePaymentDetail",
                principalColumn: "ManualSalePaymentDetailId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
