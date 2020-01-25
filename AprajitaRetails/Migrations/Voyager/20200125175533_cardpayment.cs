using Microsoft.EntityFrameworkCore.Migrations;

namespace AprajitaRetails.Migrations.Voyager
{
    public partial class cardpayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ManualSalePaymentDetail",
                columns: table => new
                {
                    ManualSalePaymentDetailId = table.Column<int>(nullable: false),
                    PayMode = table.Column<int>(nullable: false),
                    CashAmount = table.Column<decimal>(type: "money", nullable: false),
                    CardAmount = table.Column<decimal>(type: "money", nullable: false),
                    MixAmount = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManualSalePaymentDetail", x => x.ManualSalePaymentDetailId);
                    table.ForeignKey(
                        name: "FK_ManualSalePaymentDetail_ManualInvoice_ManualSalePaymentDetailId",
                        column: x => x.ManualSalePaymentDetailId,
                        principalTable: "ManualInvoice",
                        principalColumn: "ManualInvoiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ManualCardPaymentDetail",
                columns: table => new
                {
                    ManualCardPaymentDetailId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ManualSaleInvoiceId = table.Column<int>(nullable: false),
                    CardType = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    AuthCode = table.Column<int>(nullable: false),
                    LastDigit = table.Column<int>(nullable: false),
                    ManualSalePaymentDetailId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManualCardPaymentDetail", x => x.ManualCardPaymentDetailId);
                    table.ForeignKey(
                        name: "FK_ManualCardPaymentDetail_ManualSalePaymentDetail_ManualSalePaymentDetailId",
                        column: x => x.ManualSalePaymentDetailId,
                        principalTable: "ManualSalePaymentDetail",
                        principalColumn: "ManualSalePaymentDetailId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ManualCardPaymentDetail_ManualSalePaymentDetailId",
                table: "ManualCardPaymentDetail",
                column: "ManualSalePaymentDetailId",
                unique: true,
                filter: "[ManualSalePaymentDetailId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ManualCardPaymentDetail");

            migrationBuilder.DropTable(
                name: "ManualSalePaymentDetail");
        }
    }
}
