using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AprajitaRetails.Migrations
{
    public partial class Newaccounting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable (
                name: "Apps");

            //migrationBuilder.DropTable (
            //    name: "BankTranscations");

            //migrationBuilder.DropTable (
            //    name: "ExpenseVochers");

            migrationBuilder.DropTable(
                name: "LedgerMasters");

            //migrationBuilder.DropTable (
            //    name: "PaymentVochers");

            //migrationBuilder.DropTable (
            //    name: "ReceiptVochers");

            //migrationBuilder.DropTable (
            //    name: "BankAccounts");

            migrationBuilder.DropTable(
                name: "LedgerEntries");

            migrationBuilder.DropTable(
                name: "Parties");

            migrationBuilder.DropTable(
                name: "LedgerTypes");



            migrationBuilder.CreateTable(
                name: "Apps",
                columns: table => new
                {
                    AppInfoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Version = table.Column<string>(nullable: true),
                    PublishDate = table.Column<DateTime>(nullable: false),
                    UpdateOn = table.Column<DateTime>(nullable: false),
                    DatabaseVersion = table.Column<string>(nullable: true),
                    IsEffective = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apps", x => x.AppInfoId);
                });

            migrationBuilder.CreateTable(
                name: "BankAccounts",
                columns: table => new
                {
                    BankAccountId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankId = table.Column<int>(nullable: false),
                    Account = table.Column<string>(nullable: true),
                    BranchName = table.Column<string>(nullable: true),
                    AccountType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccounts", x => x.BankAccountId);
                    table.ForeignKey(
                        name: "FK_BankAccounts_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "BankId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LedgerTypes",
                columns: table => new
                {
                    LedgerTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LedgerNameType = table.Column<string>(nullable: true),
                    Category = table.Column<int>(nullable: false),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LedgerTypes", x => x.LedgerTypeId);
                });

            migrationBuilder.CreateTable(
                name: "BankTranscations",
                columns: table => new
                {
                    BankTranscationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OnDate = table.Column<DateTime>(nullable: false),
                    BankAccountId = table.Column<int>(nullable: false),
                    InAmount = table.Column<decimal>(type: "money", nullable: false),
                    OutAmount = table.Column<decimal>(type: "money", nullable: false),
                    ChequeNo = table.Column<string>(nullable: true),
                    InNameOf = table.Column<string>(nullable: true),
                    SignedBy = table.Column<string>(nullable: true),
                    ApprovedBy = table.Column<string>(nullable: true),
                    IsInHouse = table.Column<bool>(nullable: false),
                    PaymentModes = table.Column<int>(nullable: false),
                    PaymentDetails = table.Column<string>(nullable: true),
                    Remarks = table.Column<string>(nullable: true),
                    StoreId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankTranscations", x => x.BankTranscationId);
                    table.ForeignKey(
                        name: "FK_BankTranscations_BankAccounts_BankAccountId",
                        column: x => x.BankAccountId,
                        principalTable: "BankAccounts",
                        principalColumn: "BankAccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BankTranscations_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Cascade);
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
                    LedgerTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parties", x => x.PartyId);
                    table.ForeignKey(
                        name: "FK_Parties_LedgerTypes_LedgerTypeId",
                        column: x => x.LedgerTypeId,
                        principalTable: "LedgerTypes",
                        principalColumn: "LedgerTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LedgerEntries",
                columns: table => new
                {
                    LedgerEntryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartyId = table.Column<int>(nullable: false),
                    EntryDate = table.Column<DateTime>(nullable: false),
                    EntryType = table.Column<int>(nullable: false),
                    ReferanceId = table.Column<int>(nullable: false),
                    VoucherType = table.Column<int>(nullable: false),
                    Particulars = table.Column<string>(nullable: true),
                    AmountIn = table.Column<decimal>(type: "money", nullable: false),
                    AmountOut = table.Column<decimal>(type: "money", nullable: false),
                    LedgerEntryRefId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LedgerEntries", x => x.LedgerEntryId);
                    table.ForeignKey(
                        name: "FK_LedgerEntries_Parties_PartyId",
                        column: x => x.PartyId,
                        principalTable: "Parties",
                        principalColumn: "PartyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LedgerMasters",
                columns: table => new
                {
                    LedgerMasterId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartyId = table.Column<int>(nullable: false),
                    CreatingDate = table.Column<DateTime>(nullable: false),
                    LedgerTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LedgerMasters", x => x.LedgerMasterId);
                    table.ForeignKey(
                        name: "FK_LedgerMasters_LedgerTypes_LedgerTypeId",
                        column: x => x.LedgerTypeId,
                        principalTable: "LedgerTypes",
                        principalColumn: "LedgerTypeId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_LedgerMasters_Parties_PartyId",
                        column: x => x.PartyId,
                        principalTable: "Parties",
                        principalColumn: "PartyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseVochers",
                columns: table => new
                {
                    ExpenseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OnDate = table.Column<DateTime>(nullable: false),
                    PayMode = table.Column<int>(nullable: false),
                    BankAccountId = table.Column<int>(nullable: true),
                    PaymentDetails = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    PartyId = table.Column<int>(nullable: true),
                    LedgerEnteryId = table.Column<int>(nullable: true),
                    IsCash = table.Column<bool>(nullable: false),
                    StoreId = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    LedgerEntryId = table.Column<int>(nullable: true),
                    Particulars = table.Column<string>(nullable: true),
                    PartyName = table.Column<string>(nullable: true),
                    EmployeeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseVochers", x => x.ExpenseId);
                    table.ForeignKey(
                        name: "FK_ExpenseVochers_BankAccounts_BankAccountId",
                        column: x => x.BankAccountId,
                        principalTable: "BankAccounts",
                        principalColumn: "BankAccountId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExpenseVochers_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExpenseVochers_LedgerEntries_LedgerEntryId",
                        column: x => x.LedgerEntryId,
                        principalTable: "LedgerEntries",
                        principalColumn: "LedgerEntryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExpenseVochers_Parties_PartyId",
                        column: x => x.PartyId,
                        principalTable: "Parties",
                        principalColumn: "PartyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExpenseVochers_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentVochers",
                columns: table => new
                {
                    PaymentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OnDate = table.Column<DateTime>(nullable: false),
                    PayMode = table.Column<int>(nullable: false),
                    BankAccountId = table.Column<int>(nullable: true),
                    PaymentDetails = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    PartyId = table.Column<int>(nullable: true),
                    LedgerEnteryId = table.Column<int>(nullable: true),
                    IsCash = table.Column<bool>(nullable: false),
                    StoreId = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    LedgerEntryId = table.Column<int>(nullable: true),
                    PartyName = table.Column<string>(nullable: true),
                    PaymentSlipNo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentVochers", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_PaymentVochers_BankAccounts_BankAccountId",
                        column: x => x.BankAccountId,
                        principalTable: "BankAccounts",
                        principalColumn: "BankAccountId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentVochers_LedgerEntries_LedgerEntryId",
                        column: x => x.LedgerEntryId,
                        principalTable: "LedgerEntries",
                        principalColumn: "LedgerEntryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentVochers_Parties_PartyId",
                        column: x => x.PartyId,
                        principalTable: "Parties",
                        principalColumn: "PartyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentVochers_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReceiptVochers",
                columns: table => new
                {
                    ReceiptId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OnDate = table.Column<DateTime>(nullable: false),
                    PayMode = table.Column<int>(nullable: false),
                    BankAccountId = table.Column<int>(nullable: true),
                    PaymentDetails = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    PartyId = table.Column<int>(nullable: true),
                    LedgerEnteryId = table.Column<int>(nullable: true),
                    IsCash = table.Column<bool>(nullable: false),
                    StoreId = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    LedgerEntryId = table.Column<int>(nullable: true),
                    PartyName = table.Column<string>(nullable: true),
                    RecieptSlipNo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptVochers", x => x.ReceiptId);
                    table.ForeignKey(
                        name: "FK_ReceiptVochers_BankAccounts_BankAccountId",
                        column: x => x.BankAccountId,
                        principalTable: "BankAccounts",
                        principalColumn: "BankAccountId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiptVochers_LedgerEntries_LedgerEntryId",
                        column: x => x.LedgerEntryId,
                        principalTable: "LedgerEntries",
                        principalColumn: "LedgerEntryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiptVochers_Parties_PartyId",
                        column: x => x.PartyId,
                        principalTable: "Parties",
                        principalColumn: "PartyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiptVochers_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_BankId",
                table: "BankAccounts",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_BankTranscations_BankAccountId",
                table: "BankTranscations",
                column: "BankAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_BankTranscations_StoreId",
                table: "BankTranscations",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseVochers_BankAccountId",
                table: "ExpenseVochers",
                column: "BankAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseVochers_EmployeeId",
                table: "ExpenseVochers",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseVochers_LedgerEntryId",
                table: "ExpenseVochers",
                column: "LedgerEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseVochers_PartyId",
                table: "ExpenseVochers",
                column: "PartyId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseVochers_StoreId",
                table: "ExpenseVochers",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_LedgerEntries_PartyId",
                table: "LedgerEntries",
                column: "PartyId");

            migrationBuilder.CreateIndex(
                name: "IX_LedgerMasters_LedgerTypeId",
                table: "LedgerMasters",
                column: "LedgerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LedgerMasters_PartyId",
                table: "LedgerMasters",
                column: "PartyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parties_LedgerTypeId",
                table: "Parties",
                column: "LedgerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentVochers_BankAccountId",
                table: "PaymentVochers",
                column: "BankAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentVochers_LedgerEntryId",
                table: "PaymentVochers",
                column: "LedgerEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentVochers_PartyId",
                table: "PaymentVochers",
                column: "PartyId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentVochers_StoreId",
                table: "PaymentVochers",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptVochers_BankAccountId",
                table: "ReceiptVochers",
                column: "BankAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptVochers_LedgerEntryId",
                table: "ReceiptVochers",
                column: "LedgerEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptVochers_PartyId",
                table: "ReceiptVochers",
                column: "PartyId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptVochers_StoreId",
                table: "ReceiptVochers",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_BankSettings_Banks_BankId",
                table: "BankSettings",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "BankId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankSettings_Banks_BankId",
                table: "BankSettings");

            migrationBuilder.DropTable(
                name: "Apps");

            migrationBuilder.DropTable(
                name: "BankTranscations");

            migrationBuilder.DropTable(
                name: "ExpenseVochers");

            migrationBuilder.DropTable(
                name: "LedgerMasters");

            migrationBuilder.DropTable(
                name: "PaymentVochers");

            migrationBuilder.DropTable(
                name: "ReceiptVochers");

            migrationBuilder.DropTable(
                name: "BankAccounts");

            migrationBuilder.DropTable(
                name: "LedgerEntries");

            migrationBuilder.DropTable(
                name: "Parties");

            migrationBuilder.DropTable(
                name: "LedgerTypes");
        }
    }
}
