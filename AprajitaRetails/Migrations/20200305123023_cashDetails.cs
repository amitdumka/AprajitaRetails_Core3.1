using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AprajitaRetails.Migrations
{
    public partial class cashDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CashDetail",
                columns: table => new
                {
                    CashDetailId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OnDate = table.Column<DateTime>(nullable: false),
                    TotalAmount = table.Column<decimal>(type: "money", nullable: false),
                    C2000 = table.Column<int>(nullable: false),
                    C1000 = table.Column<int>(nullable: false),
                    C500 = table.Column<int>(nullable: false),
                    C100 = table.Column<int>(nullable: false),
                    C50 = table.Column<int>(nullable: false),
                    C20 = table.Column<int>(nullable: false),
                    C10 = table.Column<int>(nullable: false),
                    C5 = table.Column<int>(nullable: false),
                    Coin10 = table.Column<int>(nullable: false),
                    Coin5 = table.Column<int>(nullable: false),
                    Coin2 = table.Column<int>(nullable: false),
                    Coin1 = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashDetail", x => x.CashDetailId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CashDetail");
        }
    }
}
