using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace AprajitaRetails.Migrations
{
    public partial class voybill : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VBInvoices",
                columns: table => new
                {
                    VBInvoiceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceNumber = table.Column<string>(nullable: true),
                    OnDate = table.Column<DateTime>(nullable: false),
                    StoreId = table.Column<int>(nullable: false),
                    BillType = table.Column<string>(nullable: true),
                    BillAmount = table.Column<decimal>(nullable: false),
                    BillGrossAmount = table.Column<decimal>(nullable: false),
                    DiscountAmount = table.Column<decimal>(nullable: false),
                    CustomerMobile = table.Column<string>(nullable: true),
                    CustomerName = table.Column<string>(nullable: true),
                    Tailoring = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VBInvoices", x => x.VBInvoiceId);
                });

            migrationBuilder.CreateTable(
                name: "VBLineItems",
                columns: table => new
                {
                    VBLineItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialNo = table.Column<int>(nullable: false),
                    LineItemType = table.Column<string>(nullable: true),
                    ItemCode = table.Column<string>(nullable: true),
                    Qty = table.Column<double>(nullable: false),
                    Rate = table.Column<decimal>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    DiscountAmount = table.Column<decimal>(nullable: false),
                    LineTotalAmount = table.Column<decimal>(nullable: false),
                    VBInvoiceid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VBLineItems", x => x.VBLineItemId);
                    table.ForeignKey(
                        name: "FK_VBLineItems_VBInvoices_VBInvoiceid",
                        column: x => x.VBInvoiceid,
                        principalTable: "VBInvoices",
                        principalColumn: "VBInvoiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VBPaymentDetails",
                columns: table => new
                {
                    VBPaymentDetailId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mode = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    VBInvoiceid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VBPaymentDetails", x => x.VBPaymentDetailId);
                    table.ForeignKey(
                        name: "FK_VBPaymentDetails_VBInvoices_VBInvoiceid",
                        column: x => x.VBInvoiceid,
                        principalTable: "VBInvoices",
                        principalColumn: "VBInvoiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VBLineItems_VBInvoiceid",
                table: "VBLineItems",
                column: "VBInvoiceid");

            migrationBuilder.CreateIndex(
                name: "IX_VBPaymentDetails_VBInvoiceid",
                table: "VBPaymentDetails",
                column: "VBInvoiceid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VBLineItems");

            migrationBuilder.DropTable(
                name: "VBPaymentDetails");

            migrationBuilder.DropTable(
                name: "VBInvoices");
        }
    }
}
