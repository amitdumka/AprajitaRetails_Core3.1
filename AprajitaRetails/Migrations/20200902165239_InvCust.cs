using Microsoft.EntityFrameworkCore.Migrations;

namespace AprajitaRetails.Migrations
{
    public partial class InvCust : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_RegularInvoices_CustomerId",
                table: "RegularInvoices",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_RegularInvoices_Customers_CustomerId",
                table: "RegularInvoices",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegularInvoices_Customers_CustomerId",
                table: "RegularInvoices");

            migrationBuilder.DropIndex(
                name: "IX_RegularInvoices_CustomerId",
                table: "RegularInvoices");
        }
    }
}
