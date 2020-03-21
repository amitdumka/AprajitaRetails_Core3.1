using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AprajitaRetails.Migrations.Voyager
{
    public partial class Innit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArvindPayments",
                columns: table => new
                {
                    ArvindPaymentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Arvind = table.Column<int>(nullable: false),
                    OnDate = table.Column<DateTime>(nullable: false),
                    InvoiceNo = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    BankDetails = table.Column<string>(nullable: true),
                    Remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArvindPayments", x => x.ArvindPaymentId);
                });

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    BrandId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandName = table.Column<string>(nullable: true),
                    BCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.BrandId);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(nullable: true),
                    IsPrimaryCategory = table.Column<bool>(nullable: false),
                    IsSecondaryCategory = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Age = table.Column<int>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    MobileNo = table.Column<string>(nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    NoOfBills = table.Column<int>(nullable: false),
                    TotalAmount = table.Column<decimal>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseTaxTypes",
                columns: table => new
                {
                    PurchaseTaxTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaxName = table.Column<string>(nullable: true),
                    TaxType = table.Column<int>(nullable: false),
                    CompositeRate = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseTaxTypes", x => x.PurchaseTaxTypeId);
                });

            migrationBuilder.CreateTable(
                name: "SaleInvoices",
                columns: table => new
                {
                    SaleInvoiceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                    table.PrimaryKey("PK_SaleInvoices", x => x.SaleInvoiceId);
                });

            migrationBuilder.CreateTable(
                name: "SalesPerson",
                columns: table => new
                {
                    SalesPersonId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalesmanName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesPerson", x => x.SalesPersonId);
                });

            migrationBuilder.CreateTable(
                name: "SaleTaxTypes",
                columns: table => new
                {
                    SaleTaxTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaxName = table.Column<string>(nullable: true),
                    TaxType = table.Column<int>(nullable: false),
                    CompositeRate = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleTaxTypes", x => x.SaleTaxTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Stores",
                columns: table => new
                {
                    StoreId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreCode = table.Column<string>(nullable: true),
                    StoreName = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    PinCode = table.Column<string>(nullable: true),
                    PhoneNo = table.Column<string>(nullable: true),
                    StoreManagerName = table.Column<string>(nullable: true),
                    StoreManagerPhoneNo = table.Column<string>(nullable: true),
                    PanNo = table.Column<string>(nullable: true),
                    GSTNO = table.Column<string>(nullable: true),
                    NoOfEmployees = table.Column<int>(nullable: false),
                    OpeningDate = table.Column<DateTime>(nullable: false),
                    ClosingDate = table.Column<DateTime>(nullable: true),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stores", x => x.StoreId);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    SupplierID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SuppilerName = table.Column<string>(nullable: true),
                    Warehouse = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.SupplierID);
                });

            migrationBuilder.CreateTable(
                name: "ProductItems",
                columns: table => new
                {
                    ProductItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Barcode = table.Column<string>(nullable: true),
                    BrandId = table.Column<int>(nullable: false),
                    StyleCode = table.Column<string>(nullable: true),
                    ProductName = table.Column<string>(nullable: true),
                    ItemDesc = table.Column<string>(nullable: true),
                    Categorys = table.Column<int>(nullable: false),
                    MainCategoryCategoryId = table.Column<int>(nullable: true),
                    ProductCategoryCategoryId = table.Column<int>(nullable: true),
                    ProductTypeCategoryId = table.Column<int>(nullable: true),
                    MRP = table.Column<decimal>(nullable: false),
                    TaxRate = table.Column<decimal>(nullable: false),
                    Cost = table.Column<decimal>(nullable: false),
                    Size = table.Column<int>(nullable: false),
                    Units = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductItems", x => x.ProductItemId);
                    table.ForeignKey(
                        name: "FK_ProductItems_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "BrandId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductItems_Categories_MainCategoryCategoryId",
                        column: x => x.MainCategoryCategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductItems_Categories_ProductCategoryCategoryId",
                        column: x => x.ProductCategoryCategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductItems_Categories_ProductTypeCategoryId",
                        column: x => x.ProductTypeCategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalePaymentDetails",
                columns: table => new
                {
                    SalePaymentDetailId = table.Column<int>(nullable: false),
                    PayMode = table.Column<int>(nullable: false),
                    CashAmount = table.Column<decimal>(type: "money", nullable: false),
                    CardAmount = table.Column<decimal>(type: "money", nullable: false),
                    MixAmount = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalePaymentDetails", x => x.SalePaymentDetailId);
                    table.ForeignKey(
                        name: "FK_SalePaymentDetails_SaleInvoices_SalePaymentDetailId",
                        column: x => x.SalePaymentDetailId,
                        principalTable: "SaleInvoices",
                        principalColumn: "SaleInvoiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImportInWards",
                columns: table => new
                {
                    ImportInWardId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InWardNo = table.Column<string>(nullable: true),
                    InWardDate = table.Column<DateTime>(type: "DateTime2", nullable: false),
                    InvoiceNo = table.Column<string>(nullable: true),
                    InvoiceDate = table.Column<DateTime>(type: "DateTime2", nullable: false),
                    PartyName = table.Column<string>(nullable: true),
                    TotalQty = table.Column<decimal>(nullable: false),
                    TotalMRPValue = table.Column<decimal>(nullable: false),
                    TotalCost = table.Column<decimal>(nullable: false),
                    IsDataConsumed = table.Column<bool>(nullable: false),
                    StoreId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportInWards", x => x.ImportInWardId);
                    table.ForeignKey(
                        name: "FK_ImportInWards_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImportPurchases",
                columns: table => new
                {
                    ImportPurchaseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GRNNo = table.Column<string>(nullable: true),
                    GRNDate = table.Column<DateTime>(nullable: false),
                    InvoiceNo = table.Column<string>(nullable: true),
                    InvoiceDate = table.Column<DateTime>(nullable: false),
                    SupplierName = table.Column<string>(nullable: true),
                    Barcode = table.Column<string>(nullable: true),
                    ProductName = table.Column<string>(nullable: true),
                    StyleCode = table.Column<string>(nullable: true),
                    ItemDesc = table.Column<string>(nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    MRP = table.Column<decimal>(type: "money", nullable: false),
                    MRPValue = table.Column<decimal>(type: "money", nullable: false),
                    Cost = table.Column<decimal>(type: "money", nullable: false),
                    CostValue = table.Column<decimal>(type: "money", nullable: false),
                    TaxAmt = table.Column<decimal>(type: "money", nullable: false),
                    IsVatBill = table.Column<bool>(nullable: false),
                    IsLocal = table.Column<bool>(nullable: false),
                    IsDataConsumed = table.Column<bool>(nullable: false),
                    StoreId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportPurchases", x => x.ImportPurchaseId);
                    table.ForeignKey(
                        name: "FK_ImportPurchases_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImportSaleItemWises",
                columns: table => new
                {
                    ImportSaleItemWiseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceDate = table.Column<DateTime>(nullable: false),
                    InvoiceNo = table.Column<string>(nullable: true),
                    InvoiceType = table.Column<string>(nullable: true),
                    BrandName = table.Column<string>(nullable: true),
                    ProductName = table.Column<string>(nullable: true),
                    ItemDesc = table.Column<string>(nullable: true),
                    HSNCode = table.Column<string>(nullable: true),
                    Barcode = table.Column<string>(nullable: true),
                    StyleCode = table.Column<string>(nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    MRP = table.Column<decimal>(type: "money", nullable: false),
                    Discount = table.Column<decimal>(type: "money", nullable: false),
                    BasicRate = table.Column<decimal>(type: "money", nullable: false),
                    Tax = table.Column<decimal>(type: "money", nullable: false),
                    SGST = table.Column<decimal>(type: "money", nullable: false),
                    CGST = table.Column<decimal>(type: "money", nullable: false),
                    LineTotal = table.Column<decimal>(type: "money", nullable: false),
                    RoundOff = table.Column<decimal>(type: "money", nullable: false),
                    BillAmnt = table.Column<decimal>(type: "money", nullable: false),
                    PaymentType = table.Column<string>(nullable: true),
                    Saleman = table.Column<string>(nullable: true),
                    IsDataConsumed = table.Column<bool>(nullable: false),
                    IsVatBill = table.Column<bool>(nullable: false),
                    IsLocal = table.Column<bool>(nullable: false),
                    StoreId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportSaleItemWises", x => x.ImportSaleItemWiseId);
                    table.ForeignKey(
                        name: "FK_ImportSaleItemWises_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImportSaleRegisters",
                columns: table => new
                {
                    ImportSaleRegisterId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceNo = table.Column<string>(nullable: true),
                    InvoiceType = table.Column<string>(nullable: true),
                    InvoiceDate = table.Column<string>(nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    MRP = table.Column<decimal>(nullable: false),
                    Discount = table.Column<decimal>(nullable: false),
                    BasicRate = table.Column<decimal>(nullable: false),
                    Tax = table.Column<decimal>(nullable: false),
                    RoundOff = table.Column<decimal>(nullable: false),
                    BillAmnt = table.Column<decimal>(nullable: false),
                    PaymentType = table.Column<string>(nullable: true),
                    IsConsumed = table.Column<bool>(nullable: false),
                    StoreId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportSaleRegisters", x => x.ImportSaleRegisterId);
                    table.ForeignKey(
                        name: "FK_ImportSaleRegisters_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductPurchases",
                columns: table => new
                {
                    ProductPurchaseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InWardNo = table.Column<string>(nullable: true),
                    InWardDate = table.Column<DateTime>(nullable: false),
                    PurchaseDate = table.Column<DateTime>(nullable: false),
                    InvoiceNo = table.Column<string>(nullable: true),
                    TotalQty = table.Column<double>(nullable: false),
                    TotalBasicAmount = table.Column<decimal>(type: "money", nullable: false),
                    ShippingCost = table.Column<decimal>(type: "money", nullable: false),
                    TotalTax = table.Column<decimal>(type: "money", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "money", nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    SupplierID = table.Column<int>(nullable: false),
                    IsPaid = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPurchases", x => x.ProductPurchaseId);
                    table.ForeignKey(
                        name: "FK_ProductPurchases_Suppliers_SupplierID",
                        column: x => x.SupplierID,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SaleItems",
                columns: table => new
                {
                    SaleItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SaleInvoiceId = table.Column<int>(nullable: false),
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
                    SalesPersonId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleItems", x => x.SaleItemId);
                    table.ForeignKey(
                        name: "FK_SaleItems_ProductItems_ProductItemId",
                        column: x => x.ProductItemId,
                        principalTable: "ProductItems",
                        principalColumn: "ProductItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaleItems_SaleInvoices_SaleInvoiceId",
                        column: x => x.SaleInvoiceId,
                        principalTable: "SaleInvoices",
                        principalColumn: "SaleInvoiceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaleItems_SaleTaxTypes_SaleTaxTypeId",
                        column: x => x.SaleTaxTypeId,
                        principalTable: "SaleTaxTypes",
                        principalColumn: "SaleTaxTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SaleItems_SalesPerson_SalesPersonId",
                        column: x => x.SalesPersonId,
                        principalTable: "SalesPerson",
                        principalColumn: "SalesPersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    StockID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductItemId = table.Column<int>(nullable: false),
                    Quantity = table.Column<double>(nullable: false),
                    SaleQty = table.Column<double>(nullable: false),
                    PurchaseQty = table.Column<double>(nullable: false),
                    Units = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.StockID);
                    table.ForeignKey(
                        name: "FK_Stocks_ProductItems_ProductItemId",
                        column: x => x.ProductItemId,
                        principalTable: "ProductItems",
                        principalColumn: "ProductItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CardPaymentDetails",
                columns: table => new
                {
                    CardPaymentDetailId = table.Column<int>(nullable: false),
                    SaleInvoiceId = table.Column<int>(nullable: false),
                    CardType = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    AuthCode = table.Column<int>(nullable: false),
                    LastDigit = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardPaymentDetails", x => x.CardPaymentDetailId);
                    table.ForeignKey(
                        name: "FK_CardPaymentDetails_SalePaymentDetails_CardPaymentDetailId",
                        column: x => x.CardPaymentDetailId,
                        principalTable: "SalePaymentDetails",
                        principalColumn: "SalePaymentDetailId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseItems",
                columns: table => new
                {
                    PurchaseItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductPurchaseId = table.Column<int>(nullable: false),
                    ProductItemId = table.Column<int>(nullable: false),
                    Barcode = table.Column<string>(nullable: true),
                    Qty = table.Column<double>(nullable: false),
                    Unit = table.Column<int>(nullable: false),
                    Cost = table.Column<decimal>(type: "money", nullable: false),
                    TaxAmout = table.Column<decimal>(type: "money", nullable: false),
                    PurchaseTaxTypeId = table.Column<int>(nullable: true),
                    CostValue = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseItems", x => x.PurchaseItemId);
                    table.ForeignKey(
                        name: "FK_PurchaseItems_ProductItems_ProductItemId",
                        column: x => x.ProductItemId,
                        principalTable: "ProductItems",
                        principalColumn: "ProductItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseItems_ProductPurchases_ProductPurchaseId",
                        column: x => x.ProductPurchaseId,
                        principalTable: "ProductPurchases",
                        principalColumn: "ProductPurchaseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseItems_PurchaseTaxTypes_PurchaseTaxTypeId",
                        column: x => x.PurchaseTaxTypeId,
                        principalTable: "PurchaseTaxTypes",
                        principalColumn: "PurchaseTaxTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_ImportInWards_StoreId",
                table: "ImportInWards",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportPurchases_StoreId",
                table: "ImportPurchases",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportSaleItemWises_StoreId",
                table: "ImportSaleItemWises",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportSaleRegisters_StoreId",
                table: "ImportSaleRegisters",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductItems_BrandId",
                table: "ProductItems",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductItems_MainCategoryCategoryId",
                table: "ProductItems",
                column: "MainCategoryCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductItems_ProductCategoryCategoryId",
                table: "ProductItems",
                column: "ProductCategoryCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductItems_ProductTypeCategoryId",
                table: "ProductItems",
                column: "ProductTypeCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPurchases_SupplierID",
                table: "ProductPurchases",
                column: "SupplierID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseItems_ProductItemId",
                table: "PurchaseItems",
                column: "ProductItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseItems_ProductPurchaseId",
                table: "PurchaseItems",
                column: "ProductPurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseItems_PurchaseTaxTypeId",
                table: "PurchaseItems",
                column: "PurchaseTaxTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleItems_ProductItemId",
                table: "SaleItems",
                column: "ProductItemId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleItems_SaleInvoiceId",
                table: "SaleItems",
                column: "SaleInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleItems_SaleTaxTypeId",
                table: "SaleItems",
                column: "SaleTaxTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleItems_SalesPersonId",
                table: "SaleItems",
                column: "SalesPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_ProductItemId",
                table: "Stocks",
                column: "ProductItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArvindPayments");

            migrationBuilder.DropTable(
                name: "CardPaymentDetails");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "ImportInWards");

            migrationBuilder.DropTable(
                name: "ImportPurchases");

            migrationBuilder.DropTable(
                name: "ImportSaleItemWises");

            migrationBuilder.DropTable(
                name: "ImportSaleRegisters");

            migrationBuilder.DropTable(
                name: "PurchaseItems");

            migrationBuilder.DropTable(
                name: "SaleItems");

            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.DropTable(
                name: "SalePaymentDetails");

            migrationBuilder.DropTable(
                name: "Stores");

            migrationBuilder.DropTable(
                name: "ProductPurchases");

            migrationBuilder.DropTable(
                name: "PurchaseTaxTypes");

            migrationBuilder.DropTable(
                name: "SaleTaxTypes");

            migrationBuilder.DropTable(
                name: "SalesPerson");

            migrationBuilder.DropTable(
                name: "ProductItems");

            migrationBuilder.DropTable(
                name: "SaleInvoices");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
