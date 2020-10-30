using Microsoft.EntityFrameworkCore.Migrations;

namespace AprajitaRetails.Migrations
{
    public partial class EDCPaymentsFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "PointRedeemeds",
                type: "money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "MixPayments",
                type: "money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "MixPayments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "CouponPayments",
                type: "money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "CardTranscations",
                type: "money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "CardTranscations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MixPayments_StoreId",
                table: "MixPayments",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_CardTranscations_StoreId",
                table: "CardTranscations",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_CardTranscations_Stores_StoreId",
                table: "CardTranscations",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "StoreId",
                onUpdate: ReferentialAction.NoAction,
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_MixPayments_Stores_StoreId",
                table: "MixPayments",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "StoreId",
                onUpdate: ReferentialAction.NoAction,
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardTranscations_Stores_StoreId",
                table: "CardTranscations");

            migrationBuilder.DropForeignKey(
                name: "FK_MixPayments_Stores_StoreId",
                table: "MixPayments");

            migrationBuilder.DropIndex(
                name: "IX_MixPayments_StoreId",
                table: "MixPayments");

            migrationBuilder.DropIndex(
                name: "IX_CardTranscations_StoreId",
                table: "CardTranscations");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "MixPayments");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "CardTranscations");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "PointRedeemeds",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "MixPayments",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "CouponPayments",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "CardTranscations",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money");
        }
    }
}
