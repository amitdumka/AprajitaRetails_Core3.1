using Microsoft.EntityFrameworkCore.Migrations;

namespace AprajitaRetails.Migrations
{
    public partial class BankAccInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Tags",
                table: "Todo",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(3)",
                oldMaxLength: 3,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "BankAccountInfos",
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
                    table.PrimaryKey("PK_BankAccountInfos", x => x.BankAccountInfoId);
                    table.ForeignKey(
                        name: "FK_BankAccountInfos_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "BankId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountSecurityInfos",
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
                    table.PrimaryKey("PK_AccountSecurityInfos", x => x.BankAccountSecurityInfoId);
                    table.ForeignKey(
                        name: "FK_AccountSecurityInfos_BankAccountInfos_BankAccountSecurityInfoId",
                        column: x => x.BankAccountSecurityInfoId,
                        principalTable: "BankAccountInfos",
                        principalColumn: "BankAccountInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankAccountInfos_BankId",
                table: "BankAccountInfos",
                column: "BankId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountSecurityInfos");

            migrationBuilder.DropTable(
                name: "BankAccountInfos");

            migrationBuilder.AlterColumn<string>(
                name: "Tags",
                table: "Todo",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 10,
                oldNullable: true);
        }
    }
}
