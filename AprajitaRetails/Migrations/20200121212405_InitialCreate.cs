using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace AprajitaRetails.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //Voyager DB Innit
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
            //Accounts Database
            migrationBuilder.CreateTable(
               name: "Parties",
               columns: table => new
               {
                   PartyId = table.Column<int>(nullable: false)
                       .Annotation("SqlServer:Identity", "1, 1"),
                   PartyName = table.Column<string>(nullable: true),
                   OpenningDate = table.Column<DateTime>(nullable: false),
                   OpenningBalance = table.Column<decimal>(type: "money", nullable: false),
                   Address = table.Column<string>(nullable: true),
                   PANNo = table.Column<string>(nullable: true),
                   GSTNo = table.Column<string>(nullable: true),
                   LedgerType = table.Column<int>(nullable: false)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_Parties", x => x.PartyId);
               });

            migrationBuilder.CreateTable(
                name: "Masters",
                columns: table => new
                {
                    LedgerMasterId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartyId = table.Column<int>(nullable: false),
                    CreatingDate = table.Column<DateTime>(nullable: false),
                    LedgerType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Masters", x => x.LedgerMasterId);
                    table.ForeignKey(
                        name: "FK_Masters_Parties_PartyId",
                        column: x => x.PartyId,
                        principalTable: "Parties",
                        principalColumn: "PartyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Masters_PartyId",
                table: "Masters",
                column: "PartyId",
                unique: true);

            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    BankId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.BankId);
                });

            migrationBuilder.CreateTable(
                name: "CashInBanks",
                columns: table => new
                {
                    CashInBankId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CIBDate = table.Column<DateTime>(nullable: false),
                    OpenningBalance = table.Column<decimal>(nullable: false),
                    ClosingBalance = table.Column<decimal>(nullable: false),
                    CashIn = table.Column<decimal>(nullable: false),
                    CashOut = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashInBanks", x => x.CashInBankId);
                });

            migrationBuilder.CreateTable(
                name: "CashInHands",
                columns: table => new
                {
                    CashInHandId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CIHDate = table.Column<DateTime>(nullable: false),
                    OpenningBalance = table.Column<decimal>(nullable: false),
                    ClosingBalance = table.Column<decimal>(nullable: false),
                    CashIn = table.Column<decimal>(nullable: false),
                    CashOut = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashInHands", x => x.CashInHandId);
                });

            migrationBuilder.CreateTable(
                name: "ChequesLogs",
                columns: table => new
                {
                    ChequesLogId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankName = table.Column<string>(nullable: true),
                    AccountNumber = table.Column<string>(nullable: true),
                    ChequesDate = table.Column<DateTime>(nullable: true),
                    DepositDate = table.Column<DateTime>(nullable: true),
                    ClearedDate = table.Column<DateTime>(nullable: true),
                    IssuedBy = table.Column<string>(nullable: true),
                    IssuedTo = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    IsPDC = table.Column<bool>(nullable: false),
                    IsIssuedByAprajitaRetails = table.Column<bool>(nullable: false),
                    IsDepositedOnAprajitaRetails = table.Column<bool>(nullable: false),
                    Remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChequesLogs", x => x.ChequesLogId);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffName = table.Column<string>(nullable: true),
                    MobileNo = table.Column<string>(nullable: true),
                    JoiningDate = table.Column<DateTime>(nullable: false),
                    LeavingDate = table.Column<DateTime>(nullable: true),
                    IsWorking = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "EndOfDays",
                columns: table => new
                {
                    EndOfDayId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EOD_Date = table.Column<DateTime>(nullable: false),
                    Shirting = table.Column<float>(nullable: false),
                    Suiting = table.Column<float>(nullable: false),
                    USPA = table.Column<int>(nullable: false),
                    FM_Arrow = table.Column<int>(nullable: false),
                    RWT = table.Column<int>(nullable: false),
                    Access = table.Column<int>(nullable: false),
                    CashInHand = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndOfDays", x => x.EndOfDayId);
                });

            migrationBuilder.CreateTable(
                name: "IncomeExpensesReport",
                columns: table => new
                {
                    IncomeExpensesReportId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OnDate = table.Column<DateTime>(nullable: false),
                    TotalSale = table.Column<decimal>(type: "money", nullable: false),
                    TotalTailoringSale = table.Column<decimal>(type: "money", nullable: false),
                    TotalManualSale = table.Column<decimal>(type: "money", nullable: false),
                    TotalRecipts = table.Column<decimal>(type: "money", nullable: false),
                    TotalCashRecipts = table.Column<decimal>(type: "money", nullable: false),
                    TotalOtherIncome = table.Column<decimal>(type: "money", nullable: false),
                    TotalStaffPayments = table.Column<decimal>(type: "money", nullable: false),
                    TotalTailoringPayments = table.Column<decimal>(type: "money", nullable: false),
                    TotalExpenses = table.Column<decimal>(type: "money", nullable: false),
                    TotalHomeExpenses = table.Column<decimal>(type: "money", nullable: false),
                    TotalPayments = table.Column<decimal>(type: "money", nullable: false),
                    TotalCashPayments = table.Column<decimal>(type: "money", nullable: false),
                    TotalOthersExpenses = table.Column<decimal>(type: "money", nullable: false),
                    TotalDues = table.Column<decimal>(type: "money", nullable: false),
                    TotalRecovery = table.Column<decimal>(type: "money", nullable: false),
                    TotalPendingDues = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeExpensesReport", x => x.IncomeExpensesReportId);
                });

            migrationBuilder.CreateTable(
                name: "MonthEnds",
                columns: table => new
                {
                    MonthEndId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntryDate = table.Column<DateTime>(nullable: false),
                    Month = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    TotalBill = table.Column<double>(nullable: false),
                    TotalFabric = table.Column<double>(nullable: false),
                    TotalRMZ = table.Column<double>(nullable: false),
                    TotalAccess = table.Column<double>(nullable: false),
                    TotalOthers = table.Column<double>(nullable: false),
                    TotalAmountFabric = table.Column<decimal>(nullable: false),
                    TotalAmountRMZ = table.Column<decimal>(nullable: false),
                    TotalAmountAccess = table.Column<decimal>(nullable: false),
                    TotalAmountOthers = table.Column<decimal>(nullable: false),
                    TotalSaleIncome = table.Column<decimal>(nullable: false),
                    TotalTailoringIncome = table.Column<decimal>(nullable: false),
                    TotalOtherIncome = table.Column<decimal>(nullable: false),
                    TotalInward = table.Column<decimal>(nullable: false),
                    TotalInwardByAmitKumar = table.Column<decimal>(nullable: false),
                    TotalInwardOthers = table.Column<decimal>(nullable: false),
                    TotalDues = table.Column<decimal>(nullable: false),
                    TotalDuesOfMonth = table.Column<decimal>(nullable: false),
                    TotalDuesRecovered = table.Column<decimal>(nullable: false),
                    TotalExpenses = table.Column<decimal>(nullable: false),
                    TotalOnBookExpenes = table.Column<decimal>(nullable: false),
                    TotalCashExpenses = table.Column<decimal>(nullable: false),
                    TotalSalary = table.Column<decimal>(nullable: false),
                    TotalTailoringExpenses = table.Column<decimal>(nullable: false),
                    TotalTrimsAndOtherExpenses = table.Column<decimal>(nullable: false),
                    TotalHomeExpenses = table.Column<decimal>(nullable: false),
                    TotalOtherHomeExpenses = table.Column<decimal>(nullable: false),
                    TotalOtherExpenses = table.Column<decimal>(nullable: false),
                    TotalPayments = table.Column<decimal>(nullable: false),
                    TotalRecipts = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonthEnds", x => x.MonthEndId);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PayDate = table.Column<DateTime>(nullable: false),
                    PaymentPartry = table.Column<string>(nullable: true),
                    PayMode = table.Column<int>(nullable: false),
                    PaymentDetails = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    PaymentSlipNo = table.Column<string>(nullable: true),
                    Remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                });

            migrationBuilder.CreateTable(
                name: "Receipts",
                columns: table => new
                {
                    ReceiptId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecieptDate = table.Column<DateTime>(nullable: false),
                    ReceiptFrom = table.Column<string>(nullable: true),
                    PayMode = table.Column<int>(nullable: false),
                    ReceiptDetails = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    RecieptSlipNo = table.Column<string>(nullable: true),
                    Remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receipts", x => x.ReceiptId);
                });

            migrationBuilder.CreateTable(
                name: "Salesmen",
                columns: table => new
                {
                    SalesmanId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalesmanName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salesmen", x => x.SalesmanId);
                });

            migrationBuilder.CreateTable(
                name: "Suspenses",
                columns: table => new
                {
                    SuspenseAccountId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntryDate = table.Column<DateTime>(nullable: false),
                    ReferanceDetails = table.Column<string>(nullable: true),
                    InAmount = table.Column<decimal>(type: "money", nullable: false),
                    OutAmount = table.Column<decimal>(nullable: false),
                    IsCleared = table.Column<bool>(nullable: false),
                    ClearedDetails = table.Column<string>(nullable: true),
                    ReviewBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suspenses", x => x.SuspenseAccountId);
                });

            migrationBuilder.CreateTable(
                name: "TailoringEmployees",
                columns: table => new
                {
                    TailoringEmployeeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffName = table.Column<string>(nullable: true),
                    MobileNo = table.Column<string>(nullable: true),
                    JoiningDate = table.Column<DateTime>(nullable: false),
                    LeavingDate = table.Column<DateTime>(nullable: true),
                    IsWorking = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TailoringEmployees", x => x.TailoringEmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "TalioringBookings",
                columns: table => new
                {
                    TalioringBookingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingDate = table.Column<DateTime>(nullable: false),
                    CustName = table.Column<string>(nullable: true),
                    DeliveryDate = table.Column<DateTime>(nullable: false),
                    TryDate = table.Column<DateTime>(nullable: false),
                    BookingSlipNo = table.Column<string>(nullable: true),
                    TotalAmount = table.Column<decimal>(type: "money", nullable: false),
                    TotalQty = table.Column<int>(nullable: false),
                    ShirtQty = table.Column<int>(nullable: false),
                    ShirtPrice = table.Column<decimal>(type: "money", nullable: false),
                    PantQty = table.Column<int>(nullable: false),
                    PantPrice = table.Column<decimal>(type: "money", nullable: false),
                    CoatQty = table.Column<int>(nullable: false),
                    CoatPrice = table.Column<decimal>(type: "money", nullable: false),
                    KurtaQty = table.Column<int>(nullable: false),
                    KurtaPrice = table.Column<decimal>(type: "money", nullable: false),
                    BundiQty = table.Column<int>(nullable: false),
                    BundiPrice = table.Column<decimal>(type: "money", nullable: false),
                    Others = table.Column<int>(nullable: false),
                    OthersPrice = table.Column<decimal>(type: "money", nullable: false),
                    IsDelivered = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TalioringBookings", x => x.TalioringBookingId);
                });

            migrationBuilder.CreateTable(
                name: "TranscationModes",
                columns: table => new
                {
                    TranscationModeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Transcation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TranscationModes", x => x.TranscationModeId);
                });

            migrationBuilder.CreateTable(
                name: "AccountNumbers",
                columns: table => new
                {
                    AccountNumberId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankId = table.Column<int>(nullable: false),
                    Account = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountNumbers", x => x.AccountNumberId);
                    table.ForeignKey(
                        name: "FK_AccountNumbers_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "BankId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attendances",
                columns: table => new
                {
                    AttendanceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(nullable: false),
                    AttDate = table.Column<DateTime>(nullable: false),
                    EntryTime = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendances", x => x.AttendanceId);
                    table.ForeignKey(
                        name: "FK_Attendances_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CurrentSalaries",
                columns: table => new
                {
                    CurrentSalaryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(nullable: false),
                    BasicSalary = table.Column<decimal>(type: "money", nullable: false),
                    SundaySalary = table.Column<decimal>(type: "money", nullable: false),
                    LPRate = table.Column<decimal>(nullable: false),
                    IncentiveRate = table.Column<decimal>(nullable: false),
                    IncentiveTarget = table.Column<decimal>(type: "money", nullable: false),
                    WOWBillRate = table.Column<decimal>(nullable: false),
                    WOWBillTarget = table.Column<decimal>(type: "money", nullable: false),
                    IsSundayBillable = table.Column<bool>(nullable: false),
                    EffectiveDate = table.Column<DateTime>(nullable: false),
                    CloseDate = table.Column<DateTime>(nullable: false),
                    IsEffective = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentSalaries", x => x.CurrentSalaryId);
                    table.ForeignKey(
                        name: "FK_CurrentSalaries_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    ExpenseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpDate = table.Column<DateTime>(nullable: false),
                    Particulars = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    PayMode = table.Column<int>(nullable: false),
                    PaymentDetails = table.Column<string>(nullable: true),
                    EmployeeId = table.Column<int>(nullable: false),
                    PaidTo = table.Column<string>(nullable: true),
                    Remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.ExpenseId);
                    table.ForeignKey(
                        name: "FK_Expenses_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PettyCashExpenses",
                columns: table => new
                {
                    PettyCashExpenseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpDate = table.Column<DateTime>(nullable: false),
                    Particulars = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    EmployeeId = table.Column<int>(nullable: false),
                    PaidTo = table.Column<string>(nullable: true),
                    Remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PettyCashExpenses", x => x.PettyCashExpenseId);
                    table.ForeignKey(
                        name: "FK_PettyCashExpenses_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalaryPayments",
                columns: table => new
                {
                    SalaryPaymentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(nullable: false),
                    SalaryMonth = table.Column<string>(nullable: true),
                    SalaryComponet = table.Column<int>(nullable: false),
                    PaymentDate = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    PayMode = table.Column<int>(nullable: false),
                    Details = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryPayments", x => x.SalaryPaymentId);
                    table.ForeignKey(
                        name: "FK_SalaryPayments_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StaffAdvancePayments",
                columns: table => new
                {
                    StaffAdvancePaymentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(nullable: false),
                    PaymentDate = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    PayMode = table.Column<int>(nullable: false),
                    Details = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffAdvancePayments", x => x.StaffAdvancePaymentId);
                    table.ForeignKey(
                        name: "FK_StaffAdvancePayments_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StaffAdvanceReceipts",
                columns: table => new
                {
                    StaffAdvanceReceiptId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(nullable: false),
                    ReceiptDate = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    PayMode = table.Column<int>(nullable: false),
                    Details = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffAdvanceReceipts", x => x.StaffAdvanceReceiptId);
                    table.ForeignKey(
                        name: "FK_StaffAdvanceReceipts_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DailySales",
                columns: table => new
                {
                    DailySaleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SaleDate = table.Column<DateTime>(nullable: false),
                    InvNo = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    PayMode = table.Column<int>(nullable: false),
                    CashAmount = table.Column<decimal>(type: "money", nullable: false),
                    SalesmanId = table.Column<int>(nullable: false),
                    IsDue = table.Column<bool>(nullable: false),
                    IsManualBill = table.Column<bool>(nullable: false),
                    IsTailoringBill = table.Column<bool>(nullable: false),
                    IsSaleReturn = table.Column<bool>(nullable: false),
                    Remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailySales", x => x.DailySaleId);
                    table.ForeignKey(
                        name: "FK_DailySales_Salesmen_SalesmanId",
                        column: x => x.SalesmanId,
                        principalTable: "Salesmen",
                        principalColumn: "SalesmanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TailorAttendances",
                columns: table => new
                {
                    TailorAttendanceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TailoringEmployeeId = table.Column<int>(nullable: false),
                    AttDate = table.Column<DateTime>(nullable: false),
                    EntryTime = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TailorAttendances", x => x.TailorAttendanceId);
                    table.ForeignKey(
                        name: "FK_TailorAttendances_TailoringEmployees_TailoringEmployeeId",
                        column: x => x.TailoringEmployeeId,
                        principalTable: "TailoringEmployees",
                        principalColumn: "TailoringEmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TailoringSalaryPayments",
                columns: table => new
                {
                    TailoringSalaryPaymentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TailoringEmployeeId = table.Column<int>(nullable: false),
                    SalaryMonth = table.Column<string>(nullable: true),
                    SalaryComponet = table.Column<int>(nullable: false),
                    PaymentDate = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    PayMode = table.Column<int>(nullable: false),
                    Details = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TailoringSalaryPayments", x => x.TailoringSalaryPaymentId);
                    table.ForeignKey(
                        name: "FK_TailoringSalaryPayments_TailoringEmployees_TailoringEmployeeId",
                        column: x => x.TailoringEmployeeId,
                        principalTable: "TailoringEmployees",
                        principalColumn: "TailoringEmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TailoringStaffAdvancePayments",
                columns: table => new
                {
                    TailoringStaffAdvancePaymentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TailoringEmployeeId = table.Column<int>(nullable: false),
                    PaymentDate = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    PayMode = table.Column<int>(nullable: false),
                    Details = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TailoringStaffAdvancePayments", x => x.TailoringStaffAdvancePaymentId);
                    table.ForeignKey(
                        name: "FK_TailoringStaffAdvancePayments_TailoringEmployees_TailoringEmployeeId",
                        column: x => x.TailoringEmployeeId,
                        principalTable: "TailoringEmployees",
                        principalColumn: "TailoringEmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TailoringStaffAdvanceReceipts",
                columns: table => new
                {
                    TailoringStaffAdvanceReceiptId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TailoringEmployeeId = table.Column<int>(nullable: false),
                    ReceiptDate = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    PayMode = table.Column<int>(nullable: false),
                    Details = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TailoringStaffAdvanceReceipts", x => x.TailoringStaffAdvanceReceiptId);
                    table.ForeignKey(
                        name: "FK_TailoringStaffAdvanceReceipts_TailoringEmployees_TailoringEmployeeId",
                        column: x => x.TailoringEmployeeId,
                        principalTable: "TailoringEmployees",
                        principalColumn: "TailoringEmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TailoringDeliveries",
                columns: table => new
                {
                    TalioringDeliveryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeliveryDate = table.Column<DateTime>(nullable: false),
                    TalioringBookingId = table.Column<int>(nullable: false),
                    InvNo = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    Remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TailoringDeliveries", x => x.TalioringDeliveryId);
                    table.ForeignKey(
                        name: "FK_TailoringDeliveries_TalioringBookings_TalioringBookingId",
                        column: x => x.TalioringBookingId,
                        principalTable: "TalioringBookings",
                        principalColumn: "TalioringBookingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CashPayments",
                columns: table => new
                {
                    CashPaymentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentDate = table.Column<DateTime>(nullable: false),
                    TranscationModeId = table.Column<int>(nullable: false),
                    PaidTo = table.Column<string>(nullable: false),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    SlipNo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashPayments", x => x.CashPaymentId);
                    table.ForeignKey(
                        name: "FK_CashPayments_TranscationModes_TranscationModeId",
                        column: x => x.TranscationModeId,
                        principalTable: "TranscationModes",
                        principalColumn: "TranscationModeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CashReceipts",
                columns: table => new
                {
                    CashReceiptId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InwardDate = table.Column<DateTime>(nullable: false),
                    TranscationModeId = table.Column<int>(nullable: false),
                    ReceiptFrom = table.Column<string>(nullable: false),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    SlipNo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashReceipts", x => x.CashReceiptId);
                    table.ForeignKey(
                        name: "FK_CashReceipts_TranscationModes_TranscationModeId",
                        column: x => x.TranscationModeId,
                        principalTable: "TranscationModes",
                        principalColumn: "TranscationModeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BankDeposits",
                columns: table => new
                {
                    BankDepositId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepoDate = table.Column<DateTime>(nullable: false),
                    AccountNumberId = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    PayMode = table.Column<int>(nullable: false),
                    Details = table.Column<string>(nullable: true),
                    Remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankDeposits", x => x.BankDepositId);
                    table.ForeignKey(
                        name: "FK_BankDeposits_AccountNumbers_AccountNumberId",
                        column: x => x.AccountNumberId,
                        principalTable: "AccountNumbers",
                        principalColumn: "AccountNumberId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BankWithdrawals",
                columns: table => new
                {
                    BankWithdrawalId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepoDate = table.Column<DateTime>(nullable: false),
                    AccountNumberId = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    ChequeNo = table.Column<string>(nullable: true),
                    SignedBy = table.Column<string>(nullable: true),
                    ApprovedBy = table.Column<string>(nullable: true),
                    InNameOf = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankWithdrawals", x => x.BankWithdrawalId);
                    table.ForeignKey(
                        name: "FK_BankWithdrawals_AccountNumbers_AccountNumberId",
                        column: x => x.AccountNumberId,
                        principalTable: "AccountNumbers",
                        principalColumn: "AccountNumberId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaySlips",
                columns: table => new
                {
                    PaySlipId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OnDate = table.Column<DateTime>(nullable: false),
                    Month = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false),
                    CurrentSalaryId = table.Column<int>(nullable: true),
                    BasicSalary = table.Column<decimal>(type: "money", nullable: false),
                    NoOfDaysPresent = table.Column<int>(nullable: false),
                    TotalSale = table.Column<decimal>(type: "money", nullable: false),
                    SaleIncentive = table.Column<decimal>(type: "money", nullable: false),
                    WOWBillAmount = table.Column<decimal>(type: "money", nullable: false),
                    WOWBillIncentive = table.Column<decimal>(type: "money", nullable: false),
                    LastPcsAmount = table.Column<decimal>(type: "money", nullable: false),
                    LastPCsIncentive = table.Column<decimal>(type: "money", nullable: false),
                    OthersIncentive = table.Column<decimal>(type: "money", nullable: false),
                    GrossSalary = table.Column<decimal>(type: "money", nullable: false),
                    StandardDeductions = table.Column<decimal>(type: "money", nullable: false),
                    TDSDeductions = table.Column<decimal>(type: "money", nullable: false),
                    PFDeductions = table.Column<decimal>(type: "money", nullable: false),
                    AdvanceDeducations = table.Column<decimal>(type: "money", nullable: false),
                    OtherDeductions = table.Column<decimal>(type: "money", nullable: false),
                    Remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaySlips", x => x.PaySlipId);
                    table.ForeignKey(
                        name: "FK_PaySlips_CurrentSalaries_CurrentSalaryId",
                        column: x => x.CurrentSalaryId,
                        principalTable: "CurrentSalaries",
                        principalColumn: "CurrentSalaryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaySlips_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DuesLists",
                columns: table => new
                {
                    DuesListId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(nullable: false),
                    IsRecovered = table.Column<bool>(nullable: false),
                    RecoveryDate = table.Column<DateTime>(nullable: true),
                    DailySaleId = table.Column<int>(nullable: false),
                    IsPartialRecovery = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DuesLists", x => x.DuesListId);
                    table.ForeignKey(
                        name: "FK_DuesLists_DailySales_DailySaleId",
                        column: x => x.DailySaleId,
                        principalTable: "DailySales",
                        principalColumn: "DailySaleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DueRecoverds",
                columns: table => new
                {
                    DueRecoverdId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaidDate = table.Column<DateTime>(nullable: false),
                    DuesListId = table.Column<int>(nullable: false),
                    AmountPaid = table.Column<decimal>(type: "money", nullable: false),
                    IsPartialPayment = table.Column<bool>(nullable: false),
                    Modes = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DueRecoverds", x => x.DueRecoverdId);
                    table.ForeignKey(
                        name: "FK_DueRecoverds_DuesLists_DuesListId",
                        column: x => x.DuesListId,
                        principalTable: "DuesLists",
                        principalColumn: "DuesListId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountNumbers_BankId",
                table: "AccountNumbers",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_EmployeeId",
                table: "Attendances",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_BankDeposits_AccountNumberId",
                table: "BankDeposits",
                column: "AccountNumberId");

            migrationBuilder.CreateIndex(
                name: "IX_BankWithdrawals_AccountNumberId",
                table: "BankWithdrawals",
                column: "AccountNumberId");

            migrationBuilder.CreateIndex(
                name: "IX_CashInBanks_CIBDate",
                table: "CashInBanks",
                column: "CIBDate",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CashInHands_CIHDate",
                table: "CashInHands",
                column: "CIHDate",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CashPayments_TranscationModeId",
                table: "CashPayments",
                column: "TranscationModeId");

            migrationBuilder.CreateIndex(
                name: "IX_CashReceipts_TranscationModeId",
                table: "CashReceipts",
                column: "TranscationModeId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrentSalaries_EmployeeId",
                table: "CurrentSalaries",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_DailySales_SalesmanId",
                table: "DailySales",
                column: "SalesmanId");

            migrationBuilder.CreateIndex(
                name: "IX_DueRecoverds_DuesListId",
                table: "DueRecoverds",
                column: "DuesListId");

            migrationBuilder.CreateIndex(
                name: "IX_DuesLists_DailySaleId",
                table: "DuesLists",
                column: "DailySaleId");

            migrationBuilder.CreateIndex(
                name: "IX_EndOfDays_EOD_Date",
                table: "EndOfDays",
                column: "EOD_Date",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_EmployeeId",
                table: "Expenses",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PaySlips_CurrentSalaryId",
                table: "PaySlips",
                column: "CurrentSalaryId");

            migrationBuilder.CreateIndex(
                name: "IX_PaySlips_EmployeeId",
                table: "PaySlips",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PettyCashExpenses_EmployeeId",
                table: "PettyCashExpenses",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_SalaryPayments_EmployeeId",
                table: "SalaryPayments",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffAdvancePayments_EmployeeId",
                table: "StaffAdvancePayments",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffAdvanceReceipts_EmployeeId",
                table: "StaffAdvanceReceipts",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TailorAttendances_TailoringEmployeeId",
                table: "TailorAttendances",
                column: "TailoringEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TailoringDeliveries_TalioringBookingId",
                table: "TailoringDeliveries",
                column: "TalioringBookingId");

            migrationBuilder.CreateIndex(
                name: "IX_TailoringSalaryPayments_TailoringEmployeeId",
                table: "TailoringSalaryPayments",
                column: "TailoringEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TailoringStaffAdvancePayments_TailoringEmployeeId",
                table: "TailoringStaffAdvancePayments",
                column: "TailoringEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TailoringStaffAdvanceReceipts_TailoringEmployeeId",
                table: "TailoringStaffAdvanceReceipts",
                column: "TailoringEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TranscationModes_Transcation",
                table: "TranscationModes",
                column: "Transcation",
                unique: true,
                filter: "[Transcation] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendances");

            migrationBuilder.DropTable(
                name: "BankDeposits");

            migrationBuilder.DropTable(
                name: "BankWithdrawals");

            migrationBuilder.DropTable(
                name: "CashInBanks");

            migrationBuilder.DropTable(
                name: "CashInHands");

            migrationBuilder.DropTable(
                name: "CashPayments");

            migrationBuilder.DropTable(
                name: "CashReceipts");

            migrationBuilder.DropTable(
                name: "ChequesLogs");

            migrationBuilder.DropTable(
                name: "DueRecoverds");

            migrationBuilder.DropTable(
                name: "EndOfDays");

            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "IncomeExpensesReport");

            migrationBuilder.DropTable(
                name: "MonthEnds");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "PaySlips");

            migrationBuilder.DropTable(
                name: "PettyCashExpenses");

            migrationBuilder.DropTable(
                name: "Receipts");

            migrationBuilder.DropTable(
                name: "SalaryPayments");

            migrationBuilder.DropTable(
                name: "StaffAdvancePayments");

            migrationBuilder.DropTable(
                name: "StaffAdvanceReceipts");

            migrationBuilder.DropTable(
                name: "Suspenses");

            migrationBuilder.DropTable(
                name: "TailorAttendances");

            migrationBuilder.DropTable(
                name: "TailoringDeliveries");

            migrationBuilder.DropTable(
                name: "TailoringSalaryPayments");

            migrationBuilder.DropTable(
                name: "TailoringStaffAdvancePayments");

            migrationBuilder.DropTable(
                name: "TailoringStaffAdvanceReceipts");

            migrationBuilder.DropTable(
                name: "AccountNumbers");

            migrationBuilder.DropTable(
                name: "TranscationModes");

            migrationBuilder.DropTable(
                name: "DuesLists");

            migrationBuilder.DropTable(
                name: "CurrentSalaries");

            migrationBuilder.DropTable(
                name: "TalioringBookings");

            migrationBuilder.DropTable(
                name: "TailoringEmployees");

            migrationBuilder.DropTable(
                name: "Banks");

            migrationBuilder.DropTable(
                name: "DailySales");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Salesmen");

            migrationBuilder.DropTable(
               name: "Masters");

            migrationBuilder.DropTable(
                name: "Parties");

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
