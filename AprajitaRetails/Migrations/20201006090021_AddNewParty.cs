using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AprajitaRetails.Migrations
{
    public partial class AddNewParty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Apps",
                columns: table => new
                {
                    AppInfoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Version = table.Column<string>(nullable: true),
                    PublishDate = table.Column<DateTime>(nullable: false),
                    UpdateOn = table.Column<DateTime>(nullable: false),
                    DatabaseVersion = table.Column<string>(nullable: true),
                    IsEffective = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apps", x => x.AppInfoId);
                });

            migrationBuilder.CreateTable(
                name: "LedgerTypes",
                columns: table => new
                {
                    LedgerTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(nullable: true),
                    Category = table.Column<int>(nullable: false),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LedgerTypes", x => x.LedgerTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Parties",
                columns: table => new
                {
                    PartyId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartyName = table.Column<string>(nullable: true),
                    OpenningDate = table.Column<DateTime>(nullable: false),
                    OpenningBalance = table.Column<decimal>(type: "money", nullable: false),
                    Address = table.Column<string>(nullable: true),
                    PANNo = table.Column<string>(nullable: true),
                    GSTNo = table.Column<string>(nullable: true),
                    LedgerTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parties", x => x.PartyId);
                    table.ForeignKey(
                        name: "FK_Parties_LedgerTypes_LedgerTypeId",
                        column: x => x.LedgerTypeId,
                        principalTable: "LedgerTypes",
                        principalColumn: "LedgerTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LedgerEntries",
                columns: table => new
                {
                    LedgerEntryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartyId = table.Column<int>(nullable: false),
                    EntryDate = table.Column<DateTime>(nullable: false),
                    EntryType = table.Column<int>(nullable: false),
                    ReferanceId = table.Column<int>(nullable: false),
                    VoucherType = table.Column<int>(nullable: false),
                    Particulars = table.Column<string>(nullable: true),
                    AmountIn = table.Column<decimal>(type: "money", nullable: false),
                    AmountOut = table.Column<decimal>(type: "money", nullable: false),
                    LedgerEntryRefId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LedgerEntries", x => x.LedgerEntryId);
                    table.ForeignKey(
                        name: "FK_LedgerEntries_Parties_PartyId",
                        column: x => x.PartyId,
                        principalTable: "Parties",
                        principalColumn: "PartyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LedgerMasters",
                columns: table => new
                {
                    LedgerMasterId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartyId = table.Column<int>(nullable: false),
                    CreatingDate = table.Column<DateTime>(nullable: false),
                    LedgerTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LedgerMasters", x => x.LedgerMasterId);
                    table.ForeignKey(
                        name: "FK_LedgerMasters_LedgerTypes_LedgerTypeId",
                        column: x => x.LedgerTypeId,
                        principalTable: "LedgerTypes",
                        principalColumn: "LedgerTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LedgerMasters_Parties_PartyId",
                        column: x => x.PartyId,
                        principalTable: "Parties",
                        principalColumn: "PartyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LedgerEntries_PartyId",
                table: "LedgerEntries",
                column: "PartyId");

            migrationBuilder.CreateIndex(
                name: "IX_LedgerMasters_LedgerTypeId",
                table: "LedgerMasters",
                column: "LedgerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LedgerMasters_PartyId",
                table: "LedgerMasters",
                column: "PartyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parties_LedgerTypeId",
                table: "Parties",
                column: "LedgerTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Apps");

            migrationBuilder.DropTable(
                name: "LedgerEntries");

            migrationBuilder.DropTable(
                name: "LedgerMasters");

            migrationBuilder.DropTable(
                name: "Parties");

            migrationBuilder.DropTable(
                name: "LedgerTypes");
        }
    }
}
