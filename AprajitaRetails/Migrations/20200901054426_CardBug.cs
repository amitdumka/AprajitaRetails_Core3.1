using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AprajitaRetails.Migrations
{
    public partial class CardBug : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "PaymentDetail");

            migrationBuilder.DropTable(
                name: "RegularSaleItems");

            //migrationBuilder.DropTable(
            //    name: "CardDetail");

            migrationBuilder.DropTable(
                name: "RegularInvoices");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1,
                column: "DateOfBirth",
                value: new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 2,
                column: "DateOfBirth",
                value: new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Local));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RegularInvoices",
                columns: table => new
                {
                    RegularInvoiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    InvoiceNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RoundOffAmount = table.Column<decimal>(type: "money", nullable: false),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    TotalBillAmount = table.Column<decimal>(type: "money", nullable: false),
                    TotalDiscountAmount = table.Column<decimal>(type: "money", nullable: false),
                    TotalItems = table.Column<int>(type: "int", nullable: false),
                    TotalQty = table.Column<double>(type: "float", nullable: false),
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
                name: "CardDetail",
                columns: table => new
                {
                    CardDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    AuthCode = table.Column<int>(type: "int", nullable: false),
                    CardType = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastDigit = table.Column<int>(type: "int", nullable: false),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    RegularCardDetailId = table.Column<int>(type: "int", nullable: true),
                    RegularInvoiceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardDetail", x => x.CardDetailId);
                    table.ForeignKey(
                        name: "FK_CardDetail_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CardDetail_RegularInvoices_RegularInvoiceId",
                        column: x => x.RegularInvoiceId,
                        principalTable: "RegularInvoices",
                        principalColumn: "RegularInvoiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegularSaleItems",
                columns: table => new
                {
                    RegularSaleItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BarCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BasicAmount = table.Column<decimal>(type: "money", nullable: false),
                    BillAmount = table.Column<decimal>(type: "money", nullable: false),
                    Discount = table.Column<decimal>(type: "money", nullable: false),
                    MRP = table.Column<decimal>(type: "money", nullable: false),
                    ProductItemId = table.Column<int>(type: "int", nullable: false),
                    Qty = table.Column<double>(type: "float", nullable: false),
                    RegularInvoiceId = table.Column<int>(type: "int", nullable: false),
                    SaleTaxTypeId = table.Column<int>(type: "int", nullable: true),
                    SalesPersonId = table.Column<int>(type: "int", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "money", nullable: false),
                    Units = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "PaymentDetail",
                columns: table => new
                {
                    PaymentDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardAmount = table.Column<decimal>(type: "money", nullable: false),
                    CardDetailId = table.Column<int>(type: "int", nullable: true),
                    CashAmount = table.Column<decimal>(type: "money", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvoiceNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsManualBill = table.Column<bool>(type: "bit", nullable: false),
                    MixAmount = table.Column<decimal>(type: "money", nullable: false),
                    PayMode = table.Column<int>(type: "int", nullable: false),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    RegularInvoiceId = table.Column<int>(type: "int", nullable: true),
                    RegularPaymentDetailId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentDetail", x => x.PaymentDetailId);
                    table.ForeignKey(
                        name: "FK_PaymentDetail_CardDetail_CardDetailId",
                        column: x => x.CardDetailId,
                        principalTable: "CardDetail",
                        principalColumn: "CardDetailId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentDetail_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaymentDetail_RegularInvoices_RegularInvoiceId",
                        column: x => x.RegularInvoiceId,
                        principalTable: "RegularInvoices",
                        principalColumn: "RegularInvoiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1,
                column: "DateOfBirth",
                value: new DateTime(2020, 8, 31, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 2,
                column: "DateOfBirth",
                value: new DateTime(2020, 8, 31, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.CreateIndex(
                name: "IX_CardDetail_StoreId",
                table: "CardDetail",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_CardDetail_RegularInvoiceId",
                table: "CardDetail",
                column: "RegularInvoiceId",
                unique: true,
                filter: "[RegularInvoiceId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentDetail_CardDetailId",
                table: "PaymentDetail",
                column: "CardDetailId",
                unique: true,
                filter: "[CardDetailId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentDetail_StoreId",
                table: "PaymentDetail",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentDetail_RegularInvoiceId",
                table: "PaymentDetail",
                column: "RegularInvoiceId",
                unique: true,
                filter: "[RegularInvoiceId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RegularInvoices_StoreId",
                table: "RegularInvoices",
                column: "StoreId");

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
    }
}
