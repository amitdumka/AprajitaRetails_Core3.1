using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AprajitaRetails.Migrations.Voyager
{
    public partial class RegularInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RegularInvoices",
                columns: table => new
                {
                    RegularInvoiceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreId = table.Column<int>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    OnDate = table.Column<DateTime>(nullable: false),
                    InvoiceNo = table.Column<string>(nullable: true),
                    TotalItems = table.Column<int>(nullable: false),
                    TotalQty = table.Column<double>(nullable: false),
                    TotalBillAmount = table.Column<decimal>(type: "money", nullable: false),
                    TotalDiscountAmount = table.Column<decimal>(type: "money", nullable: false),
                    RoundOffAmount = table.Column<decimal>(type: "money", nullable: false),
                    TotalTaxAmount = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegularInvoices", x => x.RegularInvoiceId);
                    table.ForeignKey(
                        name: "FK_RegularInvoices_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegularCardDetails",
                columns: table => new
                {
                    RegularCardDetailId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardType = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    AuthCode = table.Column<int>(nullable: false),
                    LastDigit = table.Column<int>(nullable: false),
                    RegularInvoiceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegularCardDetails", x => x.RegularCardDetailId);
                    table.ForeignKey(
                        name: "FK_RegularCardDetails_RegularInvoices_RegularInvoiceId",
                        column: x => x.RegularInvoiceId,
                        principalTable: "RegularInvoices",
                        principalColumn: "RegularInvoiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegularPaymentDetails",
                columns: table => new
                {
                    RegularPaymentDetailId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PayMode = table.Column<int>(nullable: false),
                    CashAmount = table.Column<decimal>(type: "money", nullable: false),
                    CardAmount = table.Column<decimal>(type: "money", nullable: false),
                    MixAmount = table.Column<decimal>(type: "money", nullable: false),
                    RegularInvoiceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegularPaymentDetails", x => x.RegularPaymentDetailId);
                    table.ForeignKey(
                        name: "FK_RegularPaymentDetails_RegularInvoices_RegularInvoiceId",
                        column: x => x.RegularInvoiceId,
                        principalTable: "RegularInvoices",
                        principalColumn: "RegularInvoiceId",
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
                    SalesPersonId = table.Column<int>(nullable: false),
                    RegularInvoiceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegularSaleItems", x => x.RegularSaleItemId);
                    table.ForeignKey(
                        name: "FK_RegularSaleItems_ProductItems_ProductItemId",
                        column: x => x.ProductItemId,
                        principalTable: "ProductItems",
                        principalColumn: "ProductItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegularSaleItems_RegularInvoices_RegularInvoiceId",
                        column: x => x.RegularInvoiceId,
                        principalTable: "RegularInvoices",
                        principalColumn: "RegularInvoiceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegularSaleItems_SaleTaxTypes_SaleTaxTypeId",
                        column: x => x.SaleTaxTypeId,
                        principalTable: "SaleTaxTypes",
                        principalColumn: "SaleTaxTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RegularSaleItems_SalesPerson_SalesPersonId",
                        column: x => x.SalesPersonId,
                        principalTable: "SalesPerson",
                        principalColumn: "SalesPersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegularCardDetails_RegularInvoiceId",
                table: "RegularCardDetails",
                column: "RegularInvoiceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RegularInvoices_StoreId",
                table: "RegularInvoices",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_RegularPaymentDetails_RegularInvoiceId",
                table: "RegularPaymentDetails",
                column: "RegularInvoiceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RegularSaleItems_ProductItemId",
                table: "RegularSaleItems",
                column: "ProductItemId");

            migrationBuilder.CreateIndex(
                name: "IX_RegularSaleItems_RegularInvoiceId",
                table: "RegularSaleItems",
                column: "RegularInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_RegularSaleItems_SaleTaxTypeId",
                table: "RegularSaleItems",
                column: "SaleTaxTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RegularSaleItems_SalesPersonId",
                table: "RegularSaleItems",
                column: "SalesPersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegularCardDetails");

            migrationBuilder.DropTable(
                name: "RegularPaymentDetails");

            migrationBuilder.DropTable(
                name: "RegularSaleItems");

            migrationBuilder.DropTable(
                name: "RegularInvoices");
        }
    }
}
