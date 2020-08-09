using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace AprajitaRetails.Migrations
{
    public partial class todo_public : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "Todo",
                nullable: false,
                defaultValue: false);
            ////StoreId
            //migrationBuilder.AddColumn<int>(
            //   name: "StoreId",
            //   table: "Stocks",
            //   nullable: false,
            //   defaultValue: 0);

            //migrationBuilder.AddColumn<int>(
            //    name: "StoreId",
            //    table: "ProductPurchases",
            //    nullable: false,
            //    defaultValue: 0);

            //migrationBuilder.CreateIndex(
            //    name: "IX_Stocks_StoreId",
            //    table: "Stocks",
            //    column: "StoreId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ProductPurchases_StoreId",
            //    table: "ProductPurchases",
            //    column: "StoreId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_ProductPurchases_Stores_StoreId",
            //    table: "ProductPurchases",
            //    column: "StoreId",
            //    principalTable: "Stores",
            //    principalColumn: "StoreId",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Stocks_Stores_StoreId",
            //    table: "Stocks",
            //    column: "StoreId",
            //    principalTable: "Stores",
            //    principalColumn: "StoreId",
            //    onDelete: ReferentialAction.Cascade);

            ////RegularInvoice
            //migrationBuilder.CreateTable(
            //   name: "RegularInvoices",
            //   columns: table => new
            //   {
            //       RegularInvoiceId = table.Column<int>(nullable: false)
            //           .Annotation("SqlServer:Identity", "1, 1"),
            //       StoreId = table.Column<int>(nullable: false),
            //       CustomerId = table.Column<int>(nullable: false),
            //       OnDate = table.Column<DateTime>(nullable: false),
            //       InvoiceNo = table.Column<string>(nullable: true),
            //       TotalItems = table.Column<int>(nullable: false),
            //       TotalQty = table.Column<double>(nullable: false),
            //       TotalBillAmount = table.Column<decimal>(type: "money", nullable: false),
            //       TotalDiscountAmount = table.Column<decimal>(type: "money", nullable: false),
            //       RoundOffAmount = table.Column<decimal>(type: "money", nullable: false),
            //       TotalTaxAmount = table.Column<decimal>(type: "money", nullable: false)
            //   },
            //   constraints: table =>
            //   {
            //       table.PrimaryKey("PK_RegularInvoices", x => x.RegularInvoiceId);
            //       table.ForeignKey(
            //           name: "FK_RegularInvoices_Stores_StoreId",
            //           column: x => x.StoreId,
            //           principalTable: "Stores",
            //           principalColumn: "StoreId",
            //           onDelete: ReferentialAction.Cascade);
            //   });

            //migrationBuilder.CreateTable(
            //    name: "RegularCardDetails",
            //    columns: table => new
            //    {
            //        RegularCardDetailId = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        CardType = table.Column<int>(nullable: false),
            //        Amount = table.Column<decimal>(type: "money", nullable: false),
            //        AuthCode = table.Column<int>(nullable: false),
            //        LastDigit = table.Column<int>(nullable: false),
            //        RegularInvoiceId = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_RegularCardDetails", x => x.RegularCardDetailId);
            //        table.ForeignKey(
            //            name: "FK_RegularCardDetails_RegularInvoices_RegularInvoiceId",
            //            column: x => x.RegularInvoiceId,
            //            principalTable: "RegularInvoices",
            //            principalColumn: "RegularInvoiceId",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "RegularPaymentDetails",
            //    columns: table => new
            //    {
            //        RegularPaymentDetailId = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        PayMode = table.Column<int>(nullable: false),
            //        CashAmount = table.Column<decimal>(type: "money", nullable: false),
            //        CardAmount = table.Column<decimal>(type: "money", nullable: false),
            //        MixAmount = table.Column<decimal>(type: "money", nullable: false),
            //        RegularInvoiceId = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_RegularPaymentDetails", x => x.RegularPaymentDetailId);
            //        table.ForeignKey(
            //            name: "FK_RegularPaymentDetails_RegularInvoices_RegularInvoiceId",
            //            column: x => x.RegularInvoiceId,
            //            principalTable: "RegularInvoices",
            //            principalColumn: "RegularInvoiceId",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "RegularSaleItems",
            //    columns: table => new
            //    {
            //        RegularSaleItemId = table.Column<int>(nullable: false)
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
            //        RegularInvoiceId = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_RegularSaleItems", x => x.RegularSaleItemId);
            //        table.ForeignKey(
            //            name: "FK_RegularSaleItems_ProductItems_ProductItemId",
            //            column: x => x.ProductItemId,
            //            principalTable: "ProductItems",
            //            principalColumn: "ProductItemId",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_RegularSaleItems_RegularInvoices_RegularInvoiceId",
            //            column: x => x.RegularInvoiceId,
            //            principalTable: "RegularInvoices",
            //            principalColumn: "RegularInvoiceId",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_RegularSaleItems_SaleTaxTypes_SaleTaxTypeId",
            //            column: x => x.SaleTaxTypeId,
            //            principalTable: "SaleTaxTypes",
            //            principalColumn: "SaleTaxTypeId",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_RegularSaleItems_SalesPerson_SalesPersonId",
            //            column: x => x.SalesPersonId,
            //            principalTable: "SalesPerson",
            //            principalColumn: "SalesPersonId",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_RegularCardDetails_RegularInvoiceId",
            //    table: "RegularCardDetails",
            //    column: "RegularInvoiceId",
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_RegularInvoices_StoreId",
            //    table: "RegularInvoices",
            //    column: "StoreId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_RegularPaymentDetails_RegularInvoiceId",
            //    table: "RegularPaymentDetails",
            //    column: "RegularInvoiceId",
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_RegularSaleItems_ProductItemId",
            //    table: "RegularSaleItems",
            //    column: "ProductItemId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_RegularSaleItems_RegularInvoiceId",
            //    table: "RegularSaleItems",
            //    column: "RegularInvoiceId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_RegularSaleItems_SaleTaxTypeId",
            //    table: "RegularSaleItems",
            //    column: "SaleTaxTypeId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_RegularSaleItems_SalesPersonId",
            //    table: "RegularSaleItems",
            //    column: "SalesPersonId");

            ////Somebugs
            //migrationBuilder.AlterColumn<decimal>(
            //   name: "TaxRate",
            //   table: "ProductItems",
            //   type: "money",
            //   nullable: false,
            //   oldClrType: typeof(decimal),
            //   oldType: "decimal(18,2)");

            //migrationBuilder.AlterColumn<decimal>(
            //    name: "MRP",
            //    table: "ProductItems",
            //    type: "money",
            //    nullable: false,
            //    oldClrType: typeof(decimal),
            //    oldType: "decimal(18,2)");

            //migrationBuilder.AlterColumn<decimal>(
            //    name: "Cost",
            //    table: "ProductItems",
            //    type: "money",
            //    nullable: false,
            //    oldClrType: typeof(decimal),
            //    oldType: "decimal(18,2)");

            //migrationBuilder.AlterColumn<decimal>(
            //    name: "TotalMRPValue",
            //    table: "ImportInWards",
            //    type: "money",
            //    nullable: false,
            //    oldClrType: typeof(decimal),
            //    oldType: "decimal(18,2)");

            //migrationBuilder.AlterColumn<decimal>(
            //    name: "TotalCost",
            //    table: "ImportInWards",
            //    type: "money",
            //    nullable: false,
            //    oldClrType: typeof(decimal),
            //    oldType: "decimal(18,2)");

            //migrationBuilder.AlterColumn<decimal>(
            //    name: "TotalAmount",
            //    table: "Customers",
            //    type: "money",
            //    nullable: false,
            //    oldClrType: typeof(decimal),
            //    oldType: "decimal(18,2)");
            ////Somebugs2
            //migrationBuilder.AlterColumn<decimal>(
            //   name: "Tax",
            //   table: "ImportSaleRegisters",
            //   type: "money",
            //   nullable: false,
            //   oldClrType: typeof(decimal),
            //   oldType: "decimal(18,2)");

            //migrationBuilder.AlterColumn<decimal>(
            //    name: "RoundOff",
            //    table: "ImportSaleRegisters",
            //    type: "money",
            //    nullable: false,
            //    oldClrType: typeof(decimal),
            //    oldType: "decimal(18,2)");

            //migrationBuilder.AlterColumn<decimal>(
            //    name: "MRP",
            //    table: "ImportSaleRegisters",
            //    type: "money",
            //    nullable: false,
            //    oldClrType: typeof(decimal),
            //    oldType: "decimal(18,2)");

            //migrationBuilder.AlterColumn<decimal>(
            //    name: "Discount",
            //    table: "ImportSaleRegisters",
            //    type: "money",
            //    nullable: false,
            //    oldClrType: typeof(decimal),
            //    oldType: "decimal(18,2)");

            //migrationBuilder.AlterColumn<decimal>(
            //    name: "BillAmnt",
            //    table: "ImportSaleRegisters",
            //    type: "money",
            //    nullable: false,
            //    oldClrType: typeof(decimal),
            //    oldType: "decimal(18,2)");

            //migrationBuilder.AlterColumn<decimal>(
            //    name: "BasicRate",
            //    table: "ImportSaleRegisters",
            //    type: "money",
            //    nullable: false,
            //    oldClrType: typeof(decimal),
            //    oldType: "decimal(18,2)");

            //manualInovoice
            //migrationBuilder.CreateTable(
            //   name: "ManualInvoices",
            //   columns: table => new
            //   {
            //       ManualInvoiceId = table.Column<int>(nullable: false)
            //           .Annotation("SqlServer:Identity", "1, 1"),
            //       StoreId = table.Column<int>(nullable: false),
            //       CustomerId = table.Column<int>(nullable: false),
            //       OnDate = table.Column<DateTime>(nullable: false),
            //       InvoiceNo = table.Column<string>(nullable: true),
            //       TotalItems = table.Column<int>(nullable: false),
            //       TotalQty = table.Column<double>(nullable: false),
            //       TotalBillAmount = table.Column<decimal>(type: "money", nullable: false),
            //       TotalDiscountAmount = table.Column<decimal>(type: "money", nullable: false),
            //       RoundOffAmount = table.Column<decimal>(type: "money", nullable: false),
            //       TotalTaxAmount = table.Column<decimal>(type: "money", nullable: false)
            //   },
            //   constraints: table =>
            //   {
            //       table.PrimaryKey("PK_ManualInvoices", x => x.ManualInvoiceId);
            //       table.ForeignKey(
            //           name: "FK_ManualInvoices_Stores_StoreId",
            //           column: x => x.StoreId,
            //           principalTable: "Stores",
            //           principalColumn: "StoreId",
            //           onDelete: ReferentialAction.Cascade);
            //   });

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
            //SeedCustomer
            //migrationBuilder.InsertData(
            //    table: "Customers",
            //    columns: new[] { "CustomerId", "Age", "City", "DateOfBirth", "FirstName", "Gender", "LastName", "MobileNo", "NoOfBills", "TotalAmount" },
            //    values: new object[] { 1, 0, "Dumka", new DateTime(2020, 3, 23, 0, 0, 0, 0, DateTimeKind.Local), "Cash", 0, "Sale", "1234567890", 0, 0m });

            //migrationBuilder.InsertData(
            //    table: "Customers",
            //    columns: new[] { "CustomerId", "Age", "City", "DateOfBirth", "FirstName", "Gender", "LastName", "MobileNo", "NoOfBills", "TotalAmount" },
            //    values: new object[] { 2, 0, "Dumka", new DateTime(2020, 3, 23, 0, 0, 0, 0, DateTimeKind.Local), "Card", 0, "Sale", "1234567890", 0, 0m });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "Todo");
            ////StoreID
            //migrationBuilder.DropForeignKey(
            //  name: "FK_ProductPurchases_Stores_StoreId",
            //  table: "ProductPurchases");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Stocks_Stores_StoreId",
            //    table: "Stocks");

            //migrationBuilder.DropIndex(
            //    name: "IX_Stocks_StoreId",
            //    table: "Stocks");

            //migrationBuilder.DropIndex(
            //    name: "IX_ProductPurchases_StoreId",
            //    table: "ProductPurchases");

            //migrationBuilder.DropColumn(
            //    name: "StoreId",
            //    table: "Stocks");

            //migrationBuilder.DropColumn(
            //    name: "StoreId",
            //    table: "ProductPurchases");

            ////regularInvoice
            //migrationBuilder.DropTable(
            //    name: "RegularCardDetails");

            //migrationBuilder.DropTable(
            //    name: "RegularPaymentDetails");

            //migrationBuilder.DropTable(
            //    name: "RegularSaleItems");

            //migrationBuilder.DropTable(
            //    name: "RegularInvoices");

            ////somebugs
            //migrationBuilder.AlterColumn<decimal>(
            //   name: "TaxRate",
            //   table: "ProductItems",
            //   type: "decimal(18,2)",
            //   nullable: false,
            //   oldClrType: typeof(decimal),
            //   oldType: "money");

            //migrationBuilder.AlterColumn<decimal>(
            //    name: "MRP",
            //    table: "ProductItems",
            //    type: "decimal(18,2)",
            //    nullable: false,
            //    oldClrType: typeof(decimal),
            //    oldType: "money");

            //migrationBuilder.AlterColumn<decimal>(
            //    name: "Cost",
            //    table: "ProductItems",
            //    type: "decimal(18,2)",
            //    nullable: false,
            //    oldClrType: typeof(decimal),
            //    oldType: "money");

            //migrationBuilder.AlterColumn<decimal>(
            //    name: "TotalMRPValue",
            //    table: "ImportInWards",
            //    type: "decimal(18,2)",
            //    nullable: false,
            //    oldClrType: typeof(decimal),
            //    oldType: "money");

            //migrationBuilder.AlterColumn<decimal>(
            //    name: "TotalCost",
            //    table: "ImportInWards",
            //    type: "decimal(18,2)",
            //    nullable: false,
            //    oldClrType: typeof(decimal),
            //    oldType: "money");

            //migrationBuilder.AlterColumn<decimal>(
            //    name: "TotalAmount",
            //    table: "Customers",
            //    type: "decimal(18,2)",
            //    nullable: false,
            //    oldClrType: typeof(decimal),
            //    oldType: "money");
            ////somebugs2
            //migrationBuilder.AlterColumn<decimal>(
            //  name: "Tax",
            //  table: "ImportSaleRegisters",
            //  type: "decimal(18,2)",
            //  nullable: false,
            //  oldClrType: typeof(decimal),
            //  oldType: "money");

            //migrationBuilder.AlterColumn<decimal>(
            //    name: "RoundOff",
            //    table: "ImportSaleRegisters",
            //    type: "decimal(18,2)",
            //    nullable: false,
            //    oldClrType: typeof(decimal),
            //    oldType: "money");

            //migrationBuilder.AlterColumn<decimal>(
            //    name: "MRP",
            //    table: "ImportSaleRegisters",
            //    type: "decimal(18,2)",
            //    nullable: false,
            //    oldClrType: typeof(decimal),
            //    oldType: "money");

            //migrationBuilder.AlterColumn<decimal>(
            //    name: "Discount",
            //    table: "ImportSaleRegisters",
            //    type: "decimal(18,2)",
            //    nullable: false,
            //    oldClrType: typeof(decimal),
            //    oldType: "money");

            //migrationBuilder.AlterColumn<decimal>(
            //    name: "BillAmnt",
            //    table: "ImportSaleRegisters",
            //    type: "decimal(18,2)",
            //    nullable: false,
            //    oldClrType: typeof(decimal),
            //    oldType: "money");

            //migrationBuilder.AlterColumn<decimal>(
            //    name: "BasicRate",
            //    table: "ImportSaleRegisters",
            //    type: "decimal(18,2)",
            //    nullable: false,
            //    oldClrType: typeof(decimal),
            //    oldType: "money");

            //manual Invoie
            //migrationBuilder.DropTable(
            //  name: "ManualCardDetails");

            //migrationBuilder.DropTable(
            //    name: "ManualPaymentDetails");

            //migrationBuilder.DropTable(
            //    name: "ManualSaleItems");

            //migrationBuilder.DropTable(
            //    name: "ManualInvoices");
            ////Seed Customer
            //migrationBuilder.DeleteData(
            //   table: "Customers",
            //   keyColumn: "CustomerId",
            //   keyValue: 1);

            //migrationBuilder.DeleteData(
            //    table: "Customers",
            //    keyColumn: "CustomerId",
            //    keyValue: 2);
        }
    }
}
