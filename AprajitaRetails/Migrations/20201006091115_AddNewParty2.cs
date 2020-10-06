using Microsoft.EntityFrameworkCore.Migrations;

namespace AprajitaRetails.Migrations
{
    public partial class AddNewParty2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LedgerMasters_LedgerTypes_LedgerTypeId",
                table: "LedgerMasters");

            migrationBuilder.AlterColumn<int>(
                name: "LedgerTypeId",
                table: "LedgerMasters",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LedgerMasters_LedgerTypes_LedgerTypeId",
                table: "LedgerMasters",
                column: "LedgerTypeId",
                principalTable: "LedgerTypes",
                principalColumn: "LedgerTypeId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LedgerMasters_LedgerTypes_LedgerTypeId",
                table: "LedgerMasters");

            migrationBuilder.AlterColumn<int>(
                name: "LedgerTypeId",
                table: "LedgerMasters",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_LedgerMasters_LedgerTypes_LedgerTypeId",
                table: "LedgerMasters",
                column: "LedgerTypeId",
                principalTable: "LedgerTypes",
                principalColumn: "LedgerTypeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
