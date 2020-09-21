using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AprajitaRetails.Migrations
{
    public partial class EmpUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TailorAttendances");

            migrationBuilder.DropTable(
                name: "TailoringSalaryPayments");

            migrationBuilder.DropTable(
                name: "TailoringStaffAdvancePayments");

            migrationBuilder.DropTable(
                name: "TailoringStaffAdvanceReceipts");

            migrationBuilder.DropTable(
                name: "TailoringEmployees");

            migrationBuilder.DeleteData(
                table: "SalesPerson",
                keyColumn: "SalesPersonId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SalesPerson",
                keyColumn: "SalesPersonId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SalesPerson",
                keyColumn: "SalesPersonId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SalesPerson",
                keyColumn: "SalesPersonId",
                keyValue: 4);

            migrationBuilder.CreateTable(
                name: "EmployeeUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    IsWorking = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeUsers_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1,
                column: "DateOfBirth",
                value: new DateTime(2020, 9, 21, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 2,
                column: "DateOfBirth",
                value: new DateTime(2020, 9, 21, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeUsers_EmployeeId",
                table: "EmployeeUsers",
                column: "EmployeeId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeUsers");

            migrationBuilder.CreateTable(
                name: "TailoringEmployees",
                columns: table => new
                {
                    TailoringEmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsWorking = table.Column<bool>(type: "bit", nullable: false),
                    JoiningDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LeavingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MobileNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StaffName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TailoringEmployees", x => x.TailoringEmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "TailorAttendances",
                columns: table => new
                {
                    TailorAttendanceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EntryTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TailoringEmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TailorAttendances", x => x.TailorAttendanceId);
                    table.ForeignKey(
                        name: "FK_TailorAttendances_TailoringEmployees_TailoringEmployeeId",
                        column: x => x.TailoringEmployeeId,
                        principalTable: "TailoringEmployees",
                        principalColumn: "TailoringEmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TailoringSalaryPayments",
                columns: table => new
                {
                    TailoringSalaryPaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PayMode = table.Column<int>(type: "int", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SalaryComponet = table.Column<int>(type: "int", nullable: false),
                    SalaryMonth = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TailoringEmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TailoringSalaryPayments", x => x.TailoringSalaryPaymentId);
                    table.ForeignKey(
                        name: "FK_TailoringSalaryPayments_TailoringEmployees_TailoringEmployeeId",
                        column: x => x.TailoringEmployeeId,
                        principalTable: "TailoringEmployees",
                        principalColumn: "TailoringEmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TailoringStaffAdvancePayments",
                columns: table => new
                {
                    TailoringStaffAdvancePaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PayMode = table.Column<int>(type: "int", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TailoringEmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TailoringStaffAdvancePayments", x => x.TailoringStaffAdvancePaymentId);
                    table.ForeignKey(
                        name: "FK_TailoringStaffAdvancePayments_TailoringEmployees_TailoringEmployeeId",
                        column: x => x.TailoringEmployeeId,
                        principalTable: "TailoringEmployees",
                        principalColumn: "TailoringEmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TailoringStaffAdvanceReceipts",
                columns: table => new
                {
                    TailoringStaffAdvanceReceiptId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PayMode = table.Column<int>(type: "int", nullable: false),
                    ReceiptDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TailoringEmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TailoringStaffAdvanceReceipts", x => x.TailoringStaffAdvanceReceiptId);
                    table.ForeignKey(
                        name: "FK_TailoringStaffAdvanceReceipts_TailoringEmployees_TailoringEmployeeId",
                        column: x => x.TailoringEmployeeId,
                        principalTable: "TailoringEmployees",
                        principalColumn: "TailoringEmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1,
                column: "DateOfBirth",
                value: new DateTime(2020, 9, 12, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 2,
                column: "DateOfBirth",
                value: new DateTime(2020, 9, 12, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.InsertData(
                table: "SalesPerson",
                columns: new[] { "SalesPersonId", "SalesmanName" },
                values: new object[,]
                {
                    { 1, "Sanjeev Mishra" },
                    { 2, "Mukesh Mandal" },
                    { 3, "Manager" },
                    { 4, "Bikash Kumar Sah" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TailorAttendances_TailoringEmployeeId",
                table: "TailorAttendances",
                column: "TailoringEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TailoringSalaryPayments_TailoringEmployeeId",
                table: "TailoringSalaryPayments",
                column: "TailoringEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TailoringStaffAdvancePayments_TailoringEmployeeId",
                table: "TailoringStaffAdvancePayments",
                column: "TailoringEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TailoringStaffAdvanceReceipts_TailoringEmployeeId",
                table: "TailoringStaffAdvanceReceipts",
                column: "TailoringEmployeeId");
        }
    }
}
