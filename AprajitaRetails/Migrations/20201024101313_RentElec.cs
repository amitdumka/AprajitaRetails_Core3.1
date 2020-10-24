using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AprajitaRetails.Migrations
{
    public partial class RentElec : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ElectricityConnections",
                columns: table => new
                {
                    ElectricityConnectionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationName = table.Column<string>(nullable: true),
                    ConnectioName = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    PinCode = table.Column<string>(nullable: true),
                    ConsumerNumber = table.Column<string>(nullable: true),
                    ConusumerId = table.Column<string>(nullable: true),
                    Connection = table.Column<int>(nullable: false),
                    ConnectinDate = table.Column<DateTime>(nullable: false),
                    DisconnectionDate = table.Column<DateTime>(nullable: true),
                    KVLoad = table.Column<int>(nullable: false),
                    OwnedMetter = table.Column<bool>(nullable: false),
                    TotalConnectionCharges = table.Column<decimal>(type: "money", nullable: false),
                    SecurityDeposit = table.Column<decimal>(type: "money", nullable: false),
                    Remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectricityConnections", x => x.ElectricityConnectionId);
                });

            migrationBuilder.CreateTable(
                name: "RentedLocations",
                columns: table => new
                {
                    RentedLocationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlaceName = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    OnDate = table.Column<DateTime>(nullable: false),
                    VacatedDate = table.Column<DateTime>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    OwnerName = table.Column<string>(nullable: true),
                    MobileNo = table.Column<string>(nullable: true),
                    RentAmount = table.Column<decimal>(nullable: false),
                    AdvanceAmount = table.Column<decimal>(nullable: false),
                    IsRented = table.Column<bool>(nullable: false),
                    RentType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentedLocations", x => x.RentedLocationId);
                });

            migrationBuilder.CreateTable(
                name: "EletricityBills",
                columns: table => new
                {
                    EletricityBillId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ElectricityConnectionId = table.Column<int>(nullable: false),
                    BillNumber = table.Column<string>(nullable: true),
                    BillDate = table.Column<DateTime>(nullable: false),
                    MeterReadingDate = table.Column<DateTime>(nullable: false),
                    CurrentMeterReading = table.Column<double>(nullable: false),
                    TotalUnit = table.Column<double>(nullable: false),
                    CurrentAmount = table.Column<decimal>(type: "money", nullable: false),
                    ArrearAmount = table.Column<decimal>(type: "money", nullable: false),
                    NetDemand = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EletricityBills", x => x.EletricityBillId);
                    table.ForeignKey(
                        name: "FK_EletricityBills_ElectricityConnections_ElectricityConnectionId",
                        column: x => x.ElectricityConnectionId,
                        principalTable: "ElectricityConnections",
                        principalColumn: "ElectricityConnectionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rents",
                columns: table => new
                {
                    RentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RentLocationId = table.Column<int>(nullable: false),
                    LocationRentedLocationId = table.Column<int>(nullable: true),
                    RentType = table.Column<int>(nullable: false),
                    OnDate = table.Column<DateTime>(nullable: false),
                    Period = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    Mode = table.Column<int>(nullable: false),
                    PaymentDetails = table.Column<string>(nullable: true),
                    Remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rents", x => x.RentId);
                    table.ForeignKey(
                        name: "FK_Rents_RentedLocations_LocationRentedLocationId",
                        column: x => x.LocationRentedLocationId,
                        principalTable: "RentedLocations",
                        principalColumn: "RentedLocationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BillPayments",
                columns: table => new
                {
                    EBillPaymentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentDate = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    Mode = table.Column<int>(nullable: false),
                    PaymentDetails = table.Column<string>(nullable: true),
                    Remarks = table.Column<string>(nullable: true),
                    IsPartialPayment = table.Column<bool>(nullable: false),
                    IsBillCleared = table.Column<bool>(nullable: false),
                    BillEletricityBillId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillPayments", x => x.EBillPaymentId);
                    table.ForeignKey(
                        name: "FK_BillPayments_EletricityBills_BillEletricityBillId",
                        column: x => x.BillEletricityBillId,
                        principalTable: "EletricityBills",
                        principalColumn: "EletricityBillId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillPayments_BillEletricityBillId",
                table: "BillPayments",
                column: "BillEletricityBillId");

            migrationBuilder.CreateIndex(
                name: "IX_EletricityBills_ElectricityConnectionId",
                table: "EletricityBills",
                column: "ElectricityConnectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Rents_LocationRentedLocationId",
                table: "Rents",
                column: "LocationRentedLocationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillPayments");

            migrationBuilder.DropTable(
                name: "Rents");

            migrationBuilder.DropTable(
                name: "EletricityBills");

            migrationBuilder.DropTable(
                name: "RentedLocations");

            migrationBuilder.DropTable(
                name: "ElectricityConnections");
        }
    }
}
