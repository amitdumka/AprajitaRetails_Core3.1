using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AprajitaRetails.Migrations.Voyager
{
    public partial class ManualInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "ManualInvoices",
            //    columns: table => new
            //    {
            //        ManualInvoiceId = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        StoreId = table.Column<int>(nullable: false),
            //        CustomerId = table.Column<int>(nullable: false),
            //        OnDate = table.Column<DateTime>(nullable: false),
            //        InvoiceNo = table.Column<string>(nullable: true),
            //        TotalItems = table.Column<int>(nullable: false),
            //        TotalQty = table.Column<double>(nullable: false),
            //        TotalBillAmount = table.Column<decimal>(type: "money", nullable: false),
            //        TotalDiscountAmount = table.Column<decimal>(type: "money", nullable: false),
            //        RoundOffAmount = table.Column<decimal>(type: "money", nullable: false),
            //        TotalTaxAmount = table.Column<decimal>(type: "money", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ManualInvoices", x => x.ManualInvoiceId);
            //        table.ForeignKey(
            //            name: "FK_ManualInvoices_Stores_StoreId",
            //            column: x => x.StoreId,
            //            principalTable: "Stores",
            //            principalColumn: "StoreId",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ManualCardDetails",
            //    columns: table => new
            //    {
            //        ManualCardDetailId = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        CardType = table.Column<int>(nullable: false),
            //        Amount = table.Column<decimal>(type: "money", nullable: false),
            //        AuthCode = table.Column<int>(nullable: false),
            //        LastDigit = table.Column<int>(nullable: false),
            //        ManualInvoiceId = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ManualCardDetails", x => x.ManualCardDetailId);
            //        table.ForeignKey(
            //            name: "FK_ManualCardDetails_ManualInvoices_ManualInvoiceId",
            //            column: x => x.ManualInvoiceId,
            //            principalTable: "ManualInvoices",
            //            principalColumn: "ManualInvoiceId",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ManualPaymentDetails",
            //    columns: table => new
            //    {
            //        ManualPaymentDetailId = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        PayMode = table.Column<int>(nullable: false),
            //        CashAmount = table.Column<decimal>(type: "money", nullable: false),
            //        CardAmount = table.Column<decimal>(type: "money", nullable: false),
            //        MixAmount = table.Column<decimal>(type: "money", nullable: false),
            //        ManualInvoiceId = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ManualPaymentDetails", x => x.ManualPaymentDetailId);
            //        table.ForeignKey(
            //            name: "FK_ManualPaymentDetails_ManualInvoices_ManualInvoiceId",
            //            column: x => x.ManualInvoiceId,
            //            principalTable: "ManualInvoices",
            //            principalColumn: "ManualInvoiceId",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ManualSaleItems",
            //    columns: table => new
            //    {
            //        ManualSaleItemId = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        ProductItemId = table.Column<int>(nullable: false),
            //        BarCode = table.Column<string>(nullable: true),
            //        Qty = table.Column<double>(nullable: false),
            //        Units = table.Column<int>(nullable: false),
            //        MRP = table.Column<decimal>(type: "money", nullable: false),
            //        BasicAmount = table.Column<decimal>(type: "money", nullable: false),
            //        Discount = table.Column<decimal>(type: "money", nullable: false),
            //        TaxAmount = table.Column<decimal>(type: "money", nullable: false),
            //        SaleTaxTypeId = table.Column<int>(nullable: true),
            //        BillAmount = table.Column<decimal>(type: "money", nullable: false),
            //        SalesPersonId = table.Column<int>(nullable: false),
            //        ManualInvoiceId = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ManualSaleItems", x => x.ManualSaleItemId);
            //        table.ForeignKey(
            //            name: "FK_ManualSaleItems_ManualInvoices_ManualInvoiceId",
            //            column: x => x.ManualInvoiceId,
            //            principalTable: "ManualInvoices",
            //            principalColumn: "ManualInvoiceId",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_ManualSaleItems_ProductItems_ProductItemId",
            //            column: x => x.ProductItemId,
            //            principalTable: "ProductItems",
            //            principalColumn: "ProductItemId",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_ManualSaleItems_SaleTaxTypes_SaleTaxTypeId",
            //            column: x => x.SaleTaxTypeId,
            //            principalTable: "SaleTaxTypes",
            //            principalColumn: "SaleTaxTypeId",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_ManualSaleItems_SalesPerson_SalesPersonId",
            //            column: x => x.SalesPersonId,
            //            principalTable: "SalesPerson",
            //            principalColumn: "SalesPersonId",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_ManualCardDetails_ManualInvoiceId",
            //    table: "ManualCardDetails",
            //    column: "ManualInvoiceId",
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_ManualInvoices_StoreId",
            //    table: "ManualInvoices",
            //    column: "StoreId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ManualPaymentDetails_ManualInvoiceId",
            //    table: "ManualPaymentDetails",
            //    column: "ManualInvoiceId",
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_ManualSaleItems_ManualInvoiceId",
            //    table: "ManualSaleItems",
            //    column: "ManualInvoiceId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ManualSaleItems_ProductItemId",
            //    table: "ManualSaleItems",
            //    column: "ProductItemId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ManualSaleItems_SaleTaxTypeId",
            //    table: "ManualSaleItems",
            //    column: "SaleTaxTypeId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ManualSaleItems_SalesPersonId",
            //    table: "ManualSaleItems",
            //    column: "SalesPersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "ManualCardDetails");

            //migrationBuilder.DropTable(
            //    name: "ManualPaymentDetails");

            //migrationBuilder.DropTable(
            //    name: "ManualSaleItems");

            //migrationBuilder.DropTable(
            //    name: "ManualInvoices");
        }
    }
}
