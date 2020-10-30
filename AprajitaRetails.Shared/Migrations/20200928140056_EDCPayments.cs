using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace AprajitaRetails.Migrations
{
    public partial class EDCPayments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EDCTranscationId",
                table: "DailySales",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MixAndCouponPaymentId",
                table: "DailySales",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CardMachine",
                columns: table => new
                {
                    EDCId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TID = table.Column<int>(nullable: false),
                    EDCName = table.Column<string>(nullable: true),
                    AccountNumberId = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: true),
                    IsWorking = table.Column<bool>(nullable: false),
                    MID = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    StoreId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardMachine", x => x.EDCId);
                    table.ForeignKey(
                        name: "FK_CardMachine_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CouponPayments",
                columns: table => new
                {
                    CouponPaymentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CouponNumber = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    OnDate = table.Column<DateTime>(nullable: false),
                    DailySaleId = table.Column<int>(nullable: false),
                    InvoiceNumber = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CouponPayments", x => x.CouponPaymentId);
                    table.ForeignKey(
                        name: "FK_CouponPayments_DailySales_DailySaleId",
                        column: x => x.DailySaleId,
                        principalTable: "DailySales",
                        principalColumn: "DailySaleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MixPayments",
                columns: table => new
                {
                    MixAndCouponPaymentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceNumber = table.Column<string>(nullable: true),
                    OnDate = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    ModeMixAndCouponPaymentId = table.Column<int>(nullable: true),
                    Details = table.Column<string>(nullable: true),
                    Remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MixPayments", x => x.MixAndCouponPaymentId);
                    table.ForeignKey(
                        name: "FK_MixPayments_MixPayments_ModeMixAndCouponPaymentId",
                        column: x => x.ModeMixAndCouponPaymentId,
                        principalTable: "MixPayments",
                        principalColumn: "MixAndCouponPaymentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PointRedeemeds",
                columns: table => new
                {
                    PointRedeemedId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerMobileNumber = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    OnDate = table.Column<DateTime>(nullable: false),
                    DailySaleId = table.Column<int>(nullable: false),
                    InvoiceNumber = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PointRedeemeds", x => x.PointRedeemedId);
                    table.ForeignKey(
                        name: "FK_PointRedeemeds_DailySales_DailySaleId",
                        column: x => x.DailySaleId,
                        principalTable: "DailySales",
                        principalColumn: "DailySaleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CardTranscations",
                columns: table => new
                {
                    EDCTranscationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EDCId = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    OnDate = table.Column<DateTime>(nullable: false),
                    CardEndingNumber = table.Column<string>(nullable: true),
                    CardTypes = table.Column<int>(nullable: false),
                    InvoiceNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardTranscations", x => x.EDCTranscationId);
                    table.ForeignKey(
                        name: "FK_CardTranscations_CardMachine_EDCId",
                        column: x => x.EDCId,
                        principalTable: "CardMachine",
                        principalColumn: "EDCId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1,
                column: "DateOfBirth",
                value: new DateTime(2016, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 2,
                column: "DateOfBirth",
                value: new DateTime(2016, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_DailySales_EDCTranscationId",
                table: "DailySales",
                column: "EDCTranscationId");

            migrationBuilder.CreateIndex(
                name: "IX_DailySales_MixAndCouponPaymentId",
                table: "DailySales",
                column: "MixAndCouponPaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_CardMachine_StoreId",
                table: "CardMachine",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_CardTranscations_EDCId",
                table: "CardTranscations",
                column: "EDCId");

            migrationBuilder.CreateIndex(
                name: "IX_CouponPayments_DailySaleId",
                table: "CouponPayments",
                column: "DailySaleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MixPayments_ModeMixAndCouponPaymentId",
                table: "MixPayments",
                column: "ModeMixAndCouponPaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_PointRedeemeds_DailySaleId",
                table: "PointRedeemeds",
                column: "DailySaleId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DailySales_CardTranscations_EDCTranscationId",
                table: "DailySales",
                column: "EDCTranscationId",
                principalTable: "CardTranscations",
                principalColumn: "EDCTranscationId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DailySales_MixPayments_MixAndCouponPaymentId",
                table: "DailySales",
                column: "MixAndCouponPaymentId",
                principalTable: "MixPayments",
                principalColumn: "MixAndCouponPaymentId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DailySales_CardTranscations_EDCTranscationId",
                table: "DailySales");

            migrationBuilder.DropForeignKey(
                name: "FK_DailySales_MixPayments_MixAndCouponPaymentId",
                table: "DailySales");

            migrationBuilder.DropTable(
                name: "CardTranscations");

            migrationBuilder.DropTable(
                name: "CouponPayments");

            migrationBuilder.DropTable(
                name: "MixPayments");

            migrationBuilder.DropTable(
                name: "PointRedeemeds");

            migrationBuilder.DropTable(
                name: "CardMachine");

            migrationBuilder.DropIndex(
                name: "IX_DailySales_EDCTranscationId",
                table: "DailySales");

            migrationBuilder.DropIndex(
                name: "IX_DailySales_MixAndCouponPaymentId",
                table: "DailySales");

            migrationBuilder.DropColumn(
                name: "EDCTranscationId",
                table: "DailySales");

            migrationBuilder.DropColumn(
                name: "MixAndCouponPaymentId",
                table: "DailySales");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1,
                column: "DateOfBirth",
                value: new DateTime(2020, 9, 28, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 2,
                column: "DateOfBirth",
                value: new DateTime(2020, 9, 28, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
