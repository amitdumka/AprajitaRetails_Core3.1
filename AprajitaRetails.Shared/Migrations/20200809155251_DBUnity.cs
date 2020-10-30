using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace AprajitaRetails.Migrations
{
    public partial class DBUnity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardPaymentDetails_SalePaymentDetails_CardPaymentDetailId",
                table: "CardPaymentDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_SalePaymentDetails_SaleInvoices_SalePaymentDetailId",
                table: "SalePaymentDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SalePaymentDetails",
                table: "SalePaymentDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CardPaymentDetails",
                table: "CardPaymentDetails");

            migrationBuilder.RenameTable(
                name: "SalePaymentDetails",
                newName: "SalePaymentDetail");

            migrationBuilder.RenameTable(
                name: "CardPaymentDetails",
                newName: "CardPaymentDetail");

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "TalioringBookings",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "TailoringDeliveries",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PartyId",
                table: "StaffAdvanceReceipts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "StaffAdvanceReceipts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PartyId",
                table: "StaffAdvancePayments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "StaffAdvancePayments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "Salesmen",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StoreLocationId",
                table: "Salesmen",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "SalaryPayments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PartyId",
                table: "Receipts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "Receipts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "PettyCashExpenses",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsTailoring",
                table: "PaySlips",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PartyId",
                table: "Payments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "Payments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "MonthEnds",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PartyId",
                table: "Expenses",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "Expenses",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "EndOfDays",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsTailors",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "DuesLists",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "DueRecoverds",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "DailySales",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsTailoring",
                table: "CurrentSalaries",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "CashReceipts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "CashPayments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "CashInHands",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "CashInBanks",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "CashDetail",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "BankWithdrawals",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "BankDeposits",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsTailoring",
                table: "Attendances",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "Attendances",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SalePaymentDetail",
                table: "SalePaymentDetail",
                column: "SalePaymentDetailId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CardPaymentDetail",
                table: "CardPaymentDetail",
                column: "CardPaymentDetailId");

            migrationBuilder.CreateTable(
                name: "AttendancesImport",
                columns: table => new
                {
                    AttendanceVMId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeName = table.Column<string>(nullable: true),
                    AttDate = table.Column<DateTime>(nullable: false),
                    EntryTime = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    IsTailoring = table.Column<bool>(nullable: true),
                    StoreCode = table.Column<int>(nullable: false),
                    IsDataConsumed = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendancesImport", x => x.AttendanceVMId);
                });

            migrationBuilder.CreateTable(
                name: "OnlineVendor",
                columns: table => new
                {
                    OnlineVendorId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VendorName = table.Column<string>(nullable: true),
                    OnDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    OffDate = table.Column<DateTime>(nullable: false),
                    Reason = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnlineVendor", x => x.OnlineVendorId);
                });

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
                name: "OnlineSale",
                columns: table => new
                {
                    OnlineSaleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SaleDate = table.Column<DateTime>(nullable: false),
                    InvNo = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    VoyagerInvoiceNo = table.Column<string>(nullable: true),
                    VoygerDate = table.Column<DateTime>(nullable: false),
                    VoyagerAmount = table.Column<decimal>(type: "money", nullable: false),
                    ShippingMode = table.Column<string>(nullable: true),
                    VendorFee = table.Column<decimal>(type: "money", nullable: false),
                    ProfitValue = table.Column<decimal>(type: "money", nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    OnlineVendorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnlineSale", x => x.OnlineSaleId);
                    table.ForeignKey(
                        name: "FK_OnlineSale_OnlineVendor_OnlineVendorId",
                        column: x => x.OnlineVendorId,
                        principalTable: "OnlineVendor",
                        principalColumn: "OnlineVendorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BasicLedgerEntries",
                columns: table => new
                {
                    BasicLedgerEntryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartyId = table.Column<int>(nullable: false),
                    EntryDate = table.Column<DateTime>(nullable: false),
                    EntryType = table.Column<int>(nullable: false),
                    ReferanceId = table.Column<int>(nullable: false),
                    Particulars = table.Column<string>(nullable: true),
                    AmountIn = table.Column<decimal>(type: "money", nullable: false),
                    AmountOut = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasicLedgerEntries", x => x.BasicLedgerEntryId);
                    table.ForeignKey(
                        name: "FK_BasicLedgerEntries_Parties_PartyId",
                        column: x => x.PartyId,
                        principalTable: "Parties",
                        principalColumn: "PartyId",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1,
                column: "DateOfBirth",
                value: new DateTime(2020, 8, 9, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 2,
                column: "DateOfBirth",
                value: new DateTime(2020, 8, 9, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.InsertData(
                table: "SalesPerson",
                columns: new[] { "SalesPersonId", "SalesmanName" },
                values: new object[] { 4, "Bikash Kumar Sah" });

            migrationBuilder.UpdateData(
                table: "Salesmen",
                keyColumn: "SalesmanId",
                keyValue: 1,
                column: "StoreId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Salesmen",
                keyColumn: "SalesmanId",
                keyValue: 2,
                column: "StoreId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Salesmen",
                keyColumn: "SalesmanId",
                keyValue: 3,
                column: "StoreId",
                value: 1);

            migrationBuilder.InsertData(
                table: "Salesmen",
                columns: new[] { "SalesmanId", "SalesmanName", "StoreId", "StoreLocationId" },
                values: new object[] { 4, "Bikash Kumar Sah", 1, 0 });

            migrationBuilder.CreateIndex(
                name: "IX_TalioringBookings_StoreId",
                table: "TalioringBookings",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_TailoringDeliveries_StoreId",
                table: "TailoringDeliveries",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffAdvanceReceipts_StoreId",
                table: "StaffAdvanceReceipts",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffAdvancePayments_StoreId",
                table: "StaffAdvancePayments",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Salesmen_StoreId",
                table: "Salesmen",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_SalaryPayments_StoreId",
                table: "SalaryPayments",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_StoreId",
                table: "Receipts",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_PettyCashExpenses_StoreId",
                table: "PettyCashExpenses",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_StoreId",
                table: "Payments",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_MonthEnds_StoreId",
                table: "MonthEnds",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_StoreId",
                table: "Expenses",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_EndOfDays_StoreId",
                table: "EndOfDays",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_StoreId",
                table: "Employees",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_DuesLists_StoreId",
                table: "DuesLists",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_DueRecoverds_StoreId",
                table: "DueRecoverds",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_DailySales_StoreId",
                table: "DailySales",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_CashReceipts_StoreId",
                table: "CashReceipts",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_CashPayments_StoreId",
                table: "CashPayments",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_CashInHands_StoreId",
                table: "CashInHands",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_CashInBanks_StoreId",
                table: "CashInBanks",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_CashDetail_StoreId",
                table: "CashDetail",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_BankWithdrawals_StoreId",
                table: "BankWithdrawals",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_BankDeposits_StoreId",
                table: "BankDeposits",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_StoreId",
                table: "Attendances",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_BasicLedgerEntries_PartyId",
                table: "BasicLedgerEntries",
                column: "PartyId");

            migrationBuilder.CreateIndex(
                name: "IX_Masters_PartyId",
                table: "Masters",
                column: "PartyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OnlineSale_OnlineVendorId",
                table: "OnlineSale",
                column: "OnlineVendorId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Stores_StoreId",
                table: "Attendances",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BankDeposits_Stores_StoreId",
                table: "BankDeposits",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BankWithdrawals_Stores_StoreId",
                table: "BankWithdrawals",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CardPaymentDetail_SalePaymentDetail_CardPaymentDetailId",
                table: "CardPaymentDetail",
                column: "CardPaymentDetailId",
                principalTable: "SalePaymentDetail",
                principalColumn: "SalePaymentDetailId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CashDetail_Stores_StoreId",
                table: "CashDetail",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CashInBanks_Stores_StoreId",
                table: "CashInBanks",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CashInHands_Stores_StoreId",
                table: "CashInHands",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CashPayments_Stores_StoreId",
                table: "CashPayments",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CashReceipts_Stores_StoreId",
                table: "CashReceipts",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DailySales_Stores_StoreId",
                table: "DailySales",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DueRecoverds_Stores_StoreId",
                table: "DueRecoverds",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DuesLists_Stores_StoreId",
                table: "DuesLists",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Stores_StoreId",
                table: "Employees",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EndOfDays_Stores_StoreId",
                table: "EndOfDays",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Stores_StoreId",
                table: "Expenses",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MonthEnds_Stores_StoreId",
                table: "MonthEnds",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Stores_StoreId",
                table: "Payments",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PettyCashExpenses_Stores_StoreId",
                table: "PettyCashExpenses",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_Stores_StoreId",
                table: "Receipts",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalaryPayments_Stores_StoreId",
                table: "SalaryPayments",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalePaymentDetail_SaleInvoices_SalePaymentDetailId",
                table: "SalePaymentDetail",
                column: "SalePaymentDetailId",
                principalTable: "SaleInvoices",
                principalColumn: "SaleInvoiceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Salesmen_Stores_StoreId",
                table: "Salesmen",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StaffAdvancePayments_Stores_StoreId",
                table: "StaffAdvancePayments",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StaffAdvanceReceipts_Stores_StoreId",
                table: "StaffAdvanceReceipts",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TailoringDeliveries_Stores_StoreId",
                table: "TailoringDeliveries",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TalioringBookings_Stores_StoreId",
                table: "TalioringBookings",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Stores_StoreId",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_BankDeposits_Stores_StoreId",
                table: "BankDeposits");

            migrationBuilder.DropForeignKey(
                name: "FK_BankWithdrawals_Stores_StoreId",
                table: "BankWithdrawals");

            migrationBuilder.DropForeignKey(
                name: "FK_CardPaymentDetail_SalePaymentDetail_CardPaymentDetailId",
                table: "CardPaymentDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_CashDetail_Stores_StoreId",
                table: "CashDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_CashInBanks_Stores_StoreId",
                table: "CashInBanks");

            migrationBuilder.DropForeignKey(
                name: "FK_CashInHands_Stores_StoreId",
                table: "CashInHands");

            migrationBuilder.DropForeignKey(
                name: "FK_CashPayments_Stores_StoreId",
                table: "CashPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_CashReceipts_Stores_StoreId",
                table: "CashReceipts");

            migrationBuilder.DropForeignKey(
                name: "FK_DailySales_Stores_StoreId",
                table: "DailySales");

            migrationBuilder.DropForeignKey(
                name: "FK_DueRecoverds_Stores_StoreId",
                table: "DueRecoverds");

            migrationBuilder.DropForeignKey(
                name: "FK_DuesLists_Stores_StoreId",
                table: "DuesLists");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Stores_StoreId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_EndOfDays_Stores_StoreId",
                table: "EndOfDays");

            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Stores_StoreId",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_MonthEnds_Stores_StoreId",
                table: "MonthEnds");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Stores_StoreId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_PettyCashExpenses_Stores_StoreId",
                table: "PettyCashExpenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_Stores_StoreId",
                table: "Receipts");

            migrationBuilder.DropForeignKey(
                name: "FK_SalaryPayments_Stores_StoreId",
                table: "SalaryPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_SalePaymentDetail_SaleInvoices_SalePaymentDetailId",
                table: "SalePaymentDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_Salesmen_Stores_StoreId",
                table: "Salesmen");

            migrationBuilder.DropForeignKey(
                name: "FK_StaffAdvancePayments_Stores_StoreId",
                table: "StaffAdvancePayments");

            migrationBuilder.DropForeignKey(
                name: "FK_StaffAdvanceReceipts_Stores_StoreId",
                table: "StaffAdvanceReceipts");

            migrationBuilder.DropForeignKey(
                name: "FK_TailoringDeliveries_Stores_StoreId",
                table: "TailoringDeliveries");

            migrationBuilder.DropForeignKey(
                name: "FK_TalioringBookings_Stores_StoreId",
                table: "TalioringBookings");

            migrationBuilder.DropTable(
                name: "AttendancesImport");

            migrationBuilder.DropTable(
                name: "BasicLedgerEntries");

            migrationBuilder.DropTable(
                name: "Masters");

            migrationBuilder.DropTable(
                name: "OnlineSale");

            migrationBuilder.DropTable(
                name: "RegularCardDetails");

            migrationBuilder.DropTable(
                name: "RegularPaymentDetails");

            migrationBuilder.DropTable(
                name: "RegularSaleItems");

            migrationBuilder.DropTable(
                name: "Parties");

            migrationBuilder.DropTable(
                name: "OnlineVendor");

            migrationBuilder.DropTable(
                name: "RegularInvoices");

            migrationBuilder.DropIndex(
                name: "IX_TalioringBookings_StoreId",
                table: "TalioringBookings");

            migrationBuilder.DropIndex(
                name: "IX_TailoringDeliveries_StoreId",
                table: "TailoringDeliveries");

            migrationBuilder.DropIndex(
                name: "IX_StaffAdvanceReceipts_StoreId",
                table: "StaffAdvanceReceipts");

            migrationBuilder.DropIndex(
                name: "IX_StaffAdvancePayments_StoreId",
                table: "StaffAdvancePayments");

            migrationBuilder.DropIndex(
                name: "IX_Salesmen_StoreId",
                table: "Salesmen");

            migrationBuilder.DropIndex(
                name: "IX_SalaryPayments_StoreId",
                table: "SalaryPayments");

            migrationBuilder.DropIndex(
                name: "IX_Receipts_StoreId",
                table: "Receipts");

            migrationBuilder.DropIndex(
                name: "IX_PettyCashExpenses_StoreId",
                table: "PettyCashExpenses");

            migrationBuilder.DropIndex(
                name: "IX_Payments_StoreId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_MonthEnds_StoreId",
                table: "MonthEnds");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_StoreId",
                table: "Expenses");

            migrationBuilder.DropIndex(
                name: "IX_EndOfDays_StoreId",
                table: "EndOfDays");

            migrationBuilder.DropIndex(
                name: "IX_Employees_StoreId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_DuesLists_StoreId",
                table: "DuesLists");

            migrationBuilder.DropIndex(
                name: "IX_DueRecoverds_StoreId",
                table: "DueRecoverds");

            migrationBuilder.DropIndex(
                name: "IX_DailySales_StoreId",
                table: "DailySales");

            migrationBuilder.DropIndex(
                name: "IX_CashReceipts_StoreId",
                table: "CashReceipts");

            migrationBuilder.DropIndex(
                name: "IX_CashPayments_StoreId",
                table: "CashPayments");

            migrationBuilder.DropIndex(
                name: "IX_CashInHands_StoreId",
                table: "CashInHands");

            migrationBuilder.DropIndex(
                name: "IX_CashInBanks_StoreId",
                table: "CashInBanks");

            migrationBuilder.DropIndex(
                name: "IX_CashDetail_StoreId",
                table: "CashDetail");

            migrationBuilder.DropIndex(
                name: "IX_BankWithdrawals_StoreId",
                table: "BankWithdrawals");

            migrationBuilder.DropIndex(
                name: "IX_BankDeposits_StoreId",
                table: "BankDeposits");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_StoreId",
                table: "Attendances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SalePaymentDetail",
                table: "SalePaymentDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CardPaymentDetail",
                table: "CardPaymentDetail");

            migrationBuilder.DeleteData(
                table: "SalesPerson",
                keyColumn: "SalesPersonId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Salesmen",
                keyColumn: "SalesmanId",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "TalioringBookings");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "TailoringDeliveries");

            migrationBuilder.DropColumn(
                name: "PartyId",
                table: "StaffAdvanceReceipts");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "StaffAdvanceReceipts");

            migrationBuilder.DropColumn(
                name: "PartyId",
                table: "StaffAdvancePayments");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "StaffAdvancePayments");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "Salesmen");

            migrationBuilder.DropColumn(
                name: "StoreLocationId",
                table: "Salesmen");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "SalaryPayments");

            migrationBuilder.DropColumn(
                name: "PartyId",
                table: "Receipts");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "Receipts");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "PettyCashExpenses");

            migrationBuilder.DropColumn(
                name: "IsTailoring",
                table: "PaySlips");

            migrationBuilder.DropColumn(
                name: "PartyId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "MonthEnds");

            migrationBuilder.DropColumn(
                name: "PartyId",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "EndOfDays");

            migrationBuilder.DropColumn(
                name: "IsTailors",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "DuesLists");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "DueRecoverds");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "DailySales");

            migrationBuilder.DropColumn(
                name: "IsTailoring",
                table: "CurrentSalaries");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "CashReceipts");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "CashPayments");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "CashInHands");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "CashInBanks");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "CashDetail");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "BankWithdrawals");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "BankDeposits");

            migrationBuilder.DropColumn(
                name: "IsTailoring",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "Attendances");

            migrationBuilder.RenameTable(
                name: "SalePaymentDetail",
                newName: "SalePaymentDetails");

            migrationBuilder.RenameTable(
                name: "CardPaymentDetail",
                newName: "CardPaymentDetails");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SalePaymentDetails",
                table: "SalePaymentDetails",
                column: "SalePaymentDetailId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CardPaymentDetails",
                table: "CardPaymentDetails",
                column: "CardPaymentDetailId");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1,
                column: "DateOfBirth",
                value: new DateTime(2020, 3, 23, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 2,
                column: "DateOfBirth",
                value: new DateTime(2020, 3, 23, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.AddForeignKey(
                name: "FK_CardPaymentDetails_SalePaymentDetails_CardPaymentDetailId",
                table: "CardPaymentDetails",
                column: "CardPaymentDetailId",
                principalTable: "SalePaymentDetails",
                principalColumn: "SalePaymentDetailId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SalePaymentDetails_SaleInvoices_SalePaymentDetailId",
                table: "SalePaymentDetails",
                column: "SalePaymentDetailId",
                principalTable: "SaleInvoices",
                principalColumn: "SaleInvoiceId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
