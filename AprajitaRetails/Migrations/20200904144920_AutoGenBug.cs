using Microsoft.EntityFrameworkCore.Migrations;

namespace AprajitaRetails.Migrations
{
    public partial class AutoGenBug : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegularInvoiceId",
                table: "RegularInvoices");
            
            migrationBuilder.DropColumn(
                name: "PaymentDetailId",
                table: "PaymentDetails");

            migrationBuilder.AddColumn<int>(
                name: "RegularInvoiceId",
                table: "RegularInvoices",
                nullable: false)//,
                //oldClrType: typeof(int),
                //oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "PaymentDetailId",
                table: "PaymentDetails",
                nullable: false)//,
                //oldClrType: typeof(int),
                //oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "CardCode",
                table: "CardDetails",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardCode",
                table: "CardDetails");

            migrationBuilder.AlterColumn<int>(
                name: "RegularInvoiceId",
                table: "RegularInvoices",
                type: "int",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "PaymentDetailId",
                table: "PaymentDetails",
                type: "int",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:Identity", "1, 1");
        }
    }
}
