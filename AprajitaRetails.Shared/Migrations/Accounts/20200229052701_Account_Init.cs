using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AprajitaRetails.Migrations.Accounts
{// This file is processed
    public partial class Account_Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Parties",
            //    columns: table => new
            //    {
            //        PartyId = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        PartyName = table.Column<string>(nullable: true),
            //        OpenningDate = table.Column<DateTime>(nullable: false),
            //        OpenningBalance = table.Column<decimal>(type: "money", nullable: false),
            //        Address = table.Column<string>(nullable: true),
            //        PANNo = table.Column<string>(nullable: true),
            //        GSTNo = table.Column<string>(nullable: true),
            //        LedgerType = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Parties", x => x.PartyId);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Masters",
            //    columns: table => new
            //    {
            //        LedgerMasterId = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        PartyId = table.Column<int>(nullable: false),
            //        CreatingDate = table.Column<DateTime>(nullable: false),
            //        LedgerType = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Masters", x => x.LedgerMasterId);
            //        table.ForeignKey(
            //            name: "FK_Masters_Parties_PartyId",
            //            column: x => x.PartyId,
            //            principalTable: "Parties",
            //            principalColumn: "PartyId",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Masters_PartyId",
            //    table: "Masters",
            //    column: "PartyId",
            //    unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "Masters");

            //migrationBuilder.DropTable(
            //    name: "Parties");
        }
    }
}
