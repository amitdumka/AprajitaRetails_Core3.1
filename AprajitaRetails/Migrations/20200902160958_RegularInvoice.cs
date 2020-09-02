using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AprajitaRetails.Migrations
{
    public partial class RegularInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RegularInvoices",
                columns: table => new
                {
                    InvoiceNo = table.Column<string>(nullable: false),
                    StoreId = table.Column<int>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    OnDate = table.Column<DateTime>(nullable: false),
                    TotalItems = table.Column<int>(nullable: false),
                    TotalQty = table.Column<double>(nullable: false),
                    TotalBillAmount = table.Column<decimal>(type: "money", nullable: false),
                    TotalDiscountAmount = table.Column<decimal>(type: "money", nullable: false),
                    RoundOffAmount = table.Column<decimal>(type: "money", nullable: false),
                    TotalTaxAmount = table.Column<decimal>(type: "money", nullable: false),
                    RegularInvoiceId = table.Column<int>(nullable: false),
                    IsManualBill = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegularInvoices", x => x.InvoiceNo);
                    table.ForeignKey(
                        name: "FK_RegularInvoices_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentDetails",
                columns: table => new
                {
                    InvoiceNo = table.Column<string>(nullable: false),
                    PaymentDetailId = table.Column<int>(nullable: false),
                    PayMode = table.Column<int>(nullable: false),
                    CashAmount = table.Column<decimal>(type: "money", nullable: false),
                    CardAmount = table.Column<decimal>(type: "money", nullable: false),
                    MixAmount = table.Column<decimal>(type: "money", nullable: false),
                    IsManualBill = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentDetails", x => x.InvoiceNo);
                    table.ForeignKey(
                        name: "FK_PaymentDetails_RegularInvoices_InvoiceNo",
                        column: x => x.InvoiceNo,
                        principalTable: "RegularInvoices",
                        principalColumn: "InvoiceNo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegularSaleItems",
                columns: table => new
                {
                    RegularSaleItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductItemId = table.Column<int>(nullable: false),
                    BarCode = table.Column<string>(nullable: true),
                    Qty = table.Column<double>(nullable: false),
                    Units = table.Column<int>(nullable: false),
                    MRP = table.Column<decimal>(type: "money", nullable: false),
                    BasicAmount = table.Column<decimal>(type: "money", nullable: false),
                    Discount = table.Column<decimal>(type: "money", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "money", nullable: false),
                    SaleTaxTypeId = table.Column<int>(nullable: true),
                    BillAmount = table.Column<decimal>(type: "money", nullable: false),
                    SalesmanId = table.Column<int>(nullable: false),
                    InvoiceNo = table.Column<string>(nullable: true),
                    InvoiceNo1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegularSaleItems", x => x.RegularSaleItemId);
                    table.ForeignKey(
                        name: "FK_RegularSaleItems_RegularInvoices_InvoiceNo1",
                        column: x => x.InvoiceNo1,
                        principalTable: "RegularInvoices",
                        principalColumn: "InvoiceNo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RegularSaleItems_ProductItems_ProductItemId",
                        column: x => x.ProductItemId,
                        principalTable: "ProductItems",
                        principalColumn: "ProductItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegularSaleItems_SaleTaxTypes_SaleTaxTypeId",
                        column: x => x.SaleTaxTypeId,
                        principalTable: "SaleTaxTypes",
                        principalColumn: "SaleTaxTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RegularSaleItems_Salesmen_SalesmanId",
                        column: x => x.SalesmanId,
                        principalTable: "Salesmen",
                        principalColumn: "SalesmanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CardDetails",
                columns: table => new
                {
                    CardDetailId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardType = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    AuthCode = table.Column<int>(nullable: false),
                    LastDigit = table.Column<int>(nullable: false),
                    InvoiceNo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardDetails", x => x.CardDetailId);
                    table.ForeignKey(
                        name: "FK_CardDetails_PaymentDetails_InvoiceNo",
                        column: x => x.InvoiceNo,
                        principalTable: "PaymentDetails",
                        principalColumn: "InvoiceNo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CardDetails_InvoiceNo",
                table: "CardDetails",
                column: "InvoiceNo",
                unique: true,
                filter: "[InvoiceNo] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RegularInvoices_StoreId",
                table: "RegularInvoices",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_RegularSaleItems_InvoiceNo1",
                table: "RegularSaleItems",
                column: "InvoiceNo1");

            migrationBuilder.CreateIndex(
                name: "IX_RegularSaleItems_ProductItemId",
                table: "RegularSaleItems",
                column: "ProductItemId");

            migrationBuilder.CreateIndex(
                name: "IX_RegularSaleItems_SaleTaxTypeId",
                table: "RegularSaleItems",
                column: "SaleTaxTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RegularSaleItems_SalesmanId",
                table: "RegularSaleItems",
                column: "SalesmanId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardDetails");

            migrationBuilder.DropTable(
                name: "RegularSaleItems");

            migrationBuilder.DropTable(
                name: "PaymentDetails");

            migrationBuilder.DropTable(
                name: "RegularInvoices");
        }
    }
}
