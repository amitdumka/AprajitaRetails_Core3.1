using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AprajitaRetails.Migrations
{
    public partial class BankSetting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "WithdrawalAmount",
                table: "BankStatements",
                type: "money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "DepositAmount",
                table: "BankStatements",
                type: "money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Balance",
                table: "BankStatements",
                type: "money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.CreateTable(
                name: "Bank",
                columns: table => new
                {
                    BankId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bank", x => x.BankId);
                });

            migrationBuilder.CreateTable(
                name: "AccountNumber",
                columns: table => new
                {
                    AccountNumberId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankId = table.Column<int>(nullable: false),
                    Account = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountNumber", x => x.AccountNumberId);
                    table.ForeignKey(
                        name: "FK_AccountNumber_Bank_BankId",
                        column: x => x.BankId,
                        principalTable: "Bank",
                        principalColumn: "BankId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BankAccountInfo",
                columns: table => new
                {
                    BankAccountInfoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountHolder = table.Column<string>(nullable: true),
                    AccountNumber = table.Column<string>(nullable: true),
                    BankId = table.Column<int>(nullable: false),
                    BranchName = table.Column<string>(nullable: true),
                    IFSCCode = table.Column<string>(nullable: true),
                    AccountType = table.Column<int>(nullable: false),
                    IsClientAccount = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccountInfo", x => x.BankAccountInfoId);
                    table.ForeignKey(
                        name: "FK_BankAccountInfo_Bank_BankId",
                        column: x => x.BankId,
                        principalTable: "Bank",
                        principalColumn: "BankId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BankSettings",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SettingName = table.Column<string>(nullable: true),
                    BankId = table.Column<int>(nullable: false),
                    StartRow = table.Column<int>(nullable: false),
                    StartCol = table.Column<int>(nullable: false),
                    EndRow = table.Column<int>(nullable: false),
                    EndCol = table.Column<int>(nullable: false),
                    TransDate = table.Column<int>(nullable: false),
                    ValueDate = table.Column<int>(nullable: false),
                    ChequeNumber = table.Column<int>(nullable: false),
                    Trans = table.Column<int>(nullable: false),
                    InAmount = table.Column<int>(nullable: false),
                    OutAmount = table.Column<int>(nullable: false),
                    BalAmount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankSettings", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BankSettings_Bank_BankId",
                        column: x => x.BankId,
                        principalTable: "Bank",
                        principalColumn: "BankId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BankDeposit",
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
                    table.PrimaryKey("PK_BankDeposit", x => x.BankDepositId);
                    table.ForeignKey(
                        name: "FK_BankDeposit_AccountNumber_AccountNumberId",
                        column: x => x.AccountNumberId,
                        principalTable: "AccountNumber",
                        principalColumn: "AccountNumberId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BankWithdrawal",
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
                    table.PrimaryKey("PK_BankWithdrawal", x => x.BankWithdrawalId);
                    table.ForeignKey(
                        name: "FK_BankWithdrawal_AccountNumber_AccountNumberId",
                        column: x => x.AccountNumberId,
                        principalTable: "AccountNumber",
                        principalColumn: "AccountNumberId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BankAccountSecurityInfo",
                columns: table => new
                {
                    BankAccountSecurityInfoId = table.Column<int>(nullable: false),
                    CustomerId = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    TaxPassword = table.Column<string>(nullable: true),
                    ExtraPassword = table.Column<string>(nullable: true),
                    ATMCardNumber = table.Column<string>(nullable: true),
                    ExpiryDate = table.Column<string>(nullable: true),
                    CVVNo = table.Column<int>(nullable: false),
                    ATMPin = table.Column<int>(nullable: false),
                    TPIN = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccountSecurityInfo", x => x.BankAccountSecurityInfoId);
                    table.ForeignKey(
                        name: "FK_BankAccountSecurityInfo_BankAccountInfo_BankAccountSecurityInfoId",
                        column: x => x.BankAccountSecurityInfoId,
                        principalTable: "BankAccountInfo",
                        principalColumn: "BankAccountInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccSettings",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankSettingId = table.Column<int>(nullable: false),
                    AccountNumberId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccSettings", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AccSettings_BankSettings_BankSettingId",
                        column: x => x.BankSettingId,
                        principalTable: "BankSettings",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountNumber_BankId",
                table: "AccountNumber",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_AccSettings_BankSettingId",
                table: "AccSettings",
                column: "BankSettingId");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccountInfo_BankId",
                table: "BankAccountInfo",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_BankDeposit_AccountNumberId",
                table: "BankDeposit",
                column: "AccountNumberId");

            migrationBuilder.CreateIndex(
                name: "IX_BankSettings_BankId",
                table: "BankSettings",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_BankWithdrawal_AccountNumberId",
                table: "BankWithdrawal",
                column: "AccountNumberId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccSettings");

            migrationBuilder.DropTable(
                name: "BankAccountSecurityInfo");

            migrationBuilder.DropTable(
                name: "BankDeposit");

            migrationBuilder.DropTable(
                name: "BankWithdrawal");

            migrationBuilder.DropTable(
                name: "BankSettings");

            migrationBuilder.DropTable(
                name: "BankAccountInfo");

            migrationBuilder.DropTable(
                name: "AccountNumber");

            migrationBuilder.DropTable(
                name: "Bank");

            migrationBuilder.AlterColumn<decimal>(
                name: "WithdrawalAmount",
                table: "BankStatements",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money");

            migrationBuilder.AlterColumn<decimal>(
                name: "DepositAmount",
                table: "BankStatements",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money");

            migrationBuilder.AlterColumn<decimal>(
                name: "Balance",
                table: "BankStatements",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money");
        }
    }
}
