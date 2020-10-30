using Microsoft.EntityFrameworkCore.Migrations;

namespace AprajitaRetails.Migrations
{
    public partial class bUgx1Fixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "ImportSaleRegisters",
                nullable: true
                );

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "Stocks",
                nullable: true
                );

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "ProductPurchases",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "ImportSaleItemWises",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "ImportPurchases",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "ImportInWards",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ImportInWards_Stores_StoreId",
                table: "ImportInWards",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ImportPurchases_Stores_StoreId",
                table: "ImportPurchases",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ImportSaleItemWises_Stores_StoreId",
                table: "ImportSaleItemWises",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ImportSaleRegisters_Stores_StoreId",
                table: "ImportSaleRegisters",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Stores_StoreId",
                table: "Stocks",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
               name: "FK_ProductPurchases_Stores_StoreId",
               table: "ProductPurchases",
               column: "StoreId",
               principalTable: "Stores",
               principalColumn: "StoreId",
               onDelete: ReferentialAction.Cascade);


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImportInWards_Stores_StoreId",
                table: "ImportInWards");

            migrationBuilder.DropForeignKey(
                name: "FK_ImportPurchases_Stores_StoreId",
                table: "ImportPurchases");

            migrationBuilder.DropForeignKey(
                name: "FK_ImportSaleItemWises_Stores_StoreId",
                table: "ImportSaleItemWises");

            migrationBuilder.DropForeignKey(
                name: "FK_ImportSaleRegisters_Stores_StoreId",
                table: "ImportSaleRegisters");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "ImportSaleRegisters");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "ImportSaleItemWises");
            migrationBuilder.AlterColumn<int>(
                name: "StoreId",
                table: "ImportPurchases");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "ImportInWards");
            migrationBuilder.DropColumn(
               name: "StoreId",
               table: "Stocks"
               );

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "ProductPurchases");

        }
    }
}
