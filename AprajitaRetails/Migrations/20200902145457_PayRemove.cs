using Microsoft.EntityFrameworkCore.Migrations;

namespace AprajitaRetails.Migrations
{
    public partial class PayRemove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardDetails");

            migrationBuilder.DropTable(
                name: "PaymentDetails");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentDetails",
                columns: table => new
                {
                    InvoiceNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CardAmount = table.Column<decimal>(type: "money", nullable: false),
                    CashAmount = table.Column<decimal>(type: "money", nullable: false),
                    IsManualBill = table.Column<bool>(type: "bit", nullable: false),
                    MixAmount = table.Column<decimal>(type: "money", nullable: false),
                    PayMode = table.Column<int>(type: "int", nullable: false),
                    PaymentDetailId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentDetails", x => x.InvoiceNo);
                });

            migrationBuilder.CreateTable(
                name: "CardDetails",
                columns: table => new
                {
                    CardDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    AuthCode = table.Column<int>(type: "int", nullable: false),
                    CardType = table.Column<int>(type: "int", nullable: false),
                    InvoiceNo = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LastDigit = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardDetails", x => x.CardDetailId);
                    table.ForeignKey(
                        name: "FK_CardDetails_PaymentDetails_InvoiceNo",
                        column: x => x.InvoiceNo,
                        principalTable: "PaymentDetails",
                        principalColumn: "InvoiceNo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CardDetails_InvoiceNo",
                table: "CardDetails",
                column: "InvoiceNo",
                unique: true,
                filter: "[InvoiceNo] IS NOT NULL");
        }
    }
}
