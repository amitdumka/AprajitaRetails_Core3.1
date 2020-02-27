using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StoneWorks.Migrations
{
    public partial class StoneWorks_init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bolder",
                columns: table => new
                {
                    BolderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OnDate = table.Column<DateTime>(nullable: false),
                    VendorName = table.Column<string>(nullable: true),
                    Qty = table.Column<double>(nullable: false),
                    Rate = table.Column<decimal>(nullable: false),
                    Payment = table.Column<decimal>(nullable: false),
                    PaymentDate = table.Column<DateTime>(nullable: true),
                    Remarks = table.Column<string>(nullable: true),
                    TruckNo = table.Column<string>(nullable: true),
                    IsOwnTruck = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bolder", x => x.BolderId);
                });

            migrationBuilder.CreateTable(
                name: "ChipSales",
                columns: table => new
                {
                    ChipSalesId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OnDate = table.Column<DateTime>(nullable: false),
                    PartyName = table.Column<string>(nullable: true),
                    TruckNumber = table.Column<string>(nullable: true),
                    PartyMobileNo = table.Column<string>(nullable: true),
                    DriverName = table.Column<string>(nullable: true),
                    DriverMobileNo = table.Column<string>(nullable: true),
                    Size = table.Column<string>(nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    Rate = table.Column<decimal>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    PaidAmount = table.Column<decimal>(nullable: false),
                    ClearDate = table.Column<DateTime>(nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    BillMaker = table.Column<string>(nullable: true),
                    SlipNo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChipSales", x => x.ChipSalesId);
                });

            migrationBuilder.CreateTable(
                name: "DailyLabor",
                columns: table => new
                {
                    DailyLaborId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    OnDate = table.Column<DateTime>(nullable: false),
                    IsPresent = table.Column<bool>(nullable: false),
                    IsDailyBillable = table.Column<bool>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    ExtraAmount = table.Column<decimal>(nullable: false),
                    Remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyLabor", x => x.DailyLaborId);
                });

            migrationBuilder.CreateTable(
                name: "Fuel",
                columns: table => new
                {
                    FuelId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OnDate = table.Column<DateTime>(nullable: false),
                    PartyName = table.Column<string>(nullable: true),
                    Qty = table.Column<double>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    PaymentDate = table.Column<DateTime>(nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    IsOnVechile = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fuel", x => x.FuelId);
                });

            migrationBuilder.CreateTable(
                name: "FuelConsumtion",
                columns: table => new
                {
                    FuelConsumtionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Qty = table.Column<double>(nullable: false),
                    OnDate = table.Column<DateTime>(nullable: false),
                    AreaOfUse = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuelConsumtion", x => x.FuelConsumtionId);
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    StaffId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffName = table.Column<string>(nullable: true),
                    DateofBirth = table.Column<DateTime>(nullable: true),
                    JoiningDate = table.Column<DateTime>(nullable: false),
                    MonthlySalary = table.Column<decimal>(nullable: false),
                    IsWorking = table.Column<bool>(nullable: false),
                    LeavingDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.StaffId);
                });

            migrationBuilder.CreateTable(
                name: "Truck",
                columns: table => new
                {
                    TruckId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TruckNumber = table.Column<string>(nullable: true),
                    OwnerName = table.Column<string>(nullable: true),
                    ChasisNo = table.Column<string>(nullable: true),
                    EngineNo = table.Column<string>(nullable: true),
                    DateofRegistration = table.Column<DateTime>(nullable: false),
                    InsuranceExpiryDate = table.Column<DateTime>(nullable: false),
                    LastServiceDate = table.Column<DateTime>(nullable: false),
                    IsHired = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Truck", x => x.TruckId);
                });

            migrationBuilder.CreateTable(
                name: "StaffSalary",
                columns: table => new
                {
                    StaffSalaryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OnDate = table.Column<DateTime>(nullable: false),
                    StaffId = table.Column<int>(nullable: false),
                    PaidAmount = table.Column<decimal>(nullable: false),
                    CalcualteAmount = table.Column<decimal>(nullable: false),
                    Remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffSalary", x => x.StaffSalaryId);
                    table.ForeignKey(
                        name: "FK_StaffSalary_Staff_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staff",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HiredTruck",
                columns: table => new
                {
                    HiredTruckId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TruckId = table.Column<int>(nullable: false),
                    HiredFrom = table.Column<string>(nullable: true),
                    Rate = table.Column<decimal>(nullable: false),
                    HiredDate = table.Column<DateTime>(nullable: false),
                    SurrenderDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HiredTruck", x => x.HiredTruckId);
                    table.ForeignKey(
                        name: "FK_HiredTruck_Truck_TruckId",
                        column: x => x.TruckId,
                        principalTable: "Truck",
                        principalColumn: "TruckId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HiredTruck_TruckId",
                table: "HiredTruck",
                column: "TruckId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffSalary_StaffId",
                table: "StaffSalary",
                column: "StaffId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bolder");

            migrationBuilder.DropTable(
                name: "ChipSales");

            migrationBuilder.DropTable(
                name: "DailyLabor");

            migrationBuilder.DropTable(
                name: "Fuel");

            migrationBuilder.DropTable(
                name: "FuelConsumtion");

            migrationBuilder.DropTable(
                name: "HiredTruck");

            migrationBuilder.DropTable(
                name: "StaffSalary");

            migrationBuilder.DropTable(
                name: "Truck");

            migrationBuilder.DropTable(
                name: "Staff");
        }
    }
}
