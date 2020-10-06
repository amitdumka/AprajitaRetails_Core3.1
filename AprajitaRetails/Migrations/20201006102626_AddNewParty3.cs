using Microsoft.EntityFrameworkCore.Migrations;

namespace AprajitaRetails.Migrations
{
    public partial class AddNewParty3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeName",
                table: "LedgerTypes");

            migrationBuilder.AddColumn<string>(
                name: "LedgerNameType",
                table: "LedgerTypes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LedgerNameType",
                table: "LedgerTypes");

            migrationBuilder.AddColumn<string>(
                name: "TypeName",
                table: "LedgerTypes",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
