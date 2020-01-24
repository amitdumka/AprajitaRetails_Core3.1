using Microsoft.EntityFrameworkCore.Migrations;

namespace AprajitaRetails.Migrations.Voyager
{
    public partial class SaleItemWise : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StoreID",
                table: "Stores",
                newName: "StoreId");

            migrationBuilder.RenameColumn(
                name: "CustomerID",
                table: "Customers",
                newName: "CustomerId");

            migrationBuilder.AddColumn<bool>(
                name: "IsLocal",
                table: "ImportSaleItemWises",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVatBill",
                table: "ImportSaleItemWises",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDataConsumed",
                table: "ImportInWards",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLocal",
                table: "ImportSaleItemWises");

            migrationBuilder.DropColumn(
                name: "IsVatBill",
                table: "ImportSaleItemWises");

            migrationBuilder.DropColumn(
                name: "IsDataConsumed",
                table: "ImportInWards");

            migrationBuilder.RenameColumn(
                name: "StoreId",
                table: "Stores",
                newName: "StoreID");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Customers",
                newName: "CustomerID");
        }
    }
}
