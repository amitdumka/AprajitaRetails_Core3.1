using Microsoft.EntityFrameworkCore.Migrations;

namespace AprajitaRetails.Migrations
{
    public partial class DailySale_matchwithyoy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsMatchedWithVOy",
                table: "DailySales",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMatchedWithVOy",
                table: "DailySales");
        }
    }
}
