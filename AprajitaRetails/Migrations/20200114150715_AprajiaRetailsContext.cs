using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AprajitaRetails.Migrations
{
    public partial class AprajiaRetailsContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    BankId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.BankId);
                });

            migrationBuilder.CreateTable(
                name: "CashInBanks",
                columns: table => new
                {
                    CashInBankId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CIBDate = table.Column<DateTime>(nullable: false),
                    OpenningBalance = table.Column<decimal>(nullable: false),
                    ClosingBalance = table.Column<decimal>(nullable: false),
                    CashIn = table.Column<decimal>(nullable: false),
                    CashOut = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashInBanks", x => x.CashInBankId);
                });

            migrationBuilder.CreateTable(
                name: "CashInHands",
                columns: table => new
                {
                    CashInHandId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CIHDate = table.Column<DateTime>(nullable: false),
                    OpenningBalance = table.Column<decimal>(nullable: false),
                    ClosingBalance = table.Column<decimal>(nullable: false),
                    CashIn = table.Column<decimal>(nullable: false),
                    CashOut = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashInHands", x => x.CashInHandId);
                });

            migrationBuilder.CreateTable(
                name: "ChequesLogs",
                columns: table => new
                {
                    ChequesLogId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankName = table.Column<string>(nullable: true),
                    AccountNumber = table.Column<string>(nullable: true),
                    ChequesDate = table.Column<DateTime>(nullable: true),
                    DepositDate = table.Column<DateTime>(nullable: true),
                    ClearedDate = table.Column<DateTime>(nullable: true),
                    IssuedBy = table.Column<string>(nullable: true),
                    IssuedTo = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    IsPDC = table.Column<bool>(nullable: false),
                    IsIssuedByAprajitaRetails = table.Column<bool>(nullable: false),
                    IsDepositedOnAprajitaRetails = table.Column<bool>(nullable: false),
                    Remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChequesLogs", x => x.ChequesLogId);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffName = table.Column<string>(nullable: true),
                    MobileNo = table.Column<string>(nullable: true),
                    JoiningDate = table.Column<DateTime>(nullable: false),
                    LeavingDate = table.Column<DateTime>(nullable: true),
                    IsWorking = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "EndOfDays",
                columns: table => new
                {
                    EndOfDayId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EOD_Date = table.Column<DateTime>(nullable: false),
                    Shirting = table.Column<float>(nullable: false),
                    Suiting = table.Column<float>(nullable: false),
                    USPA = table.Column<int>(nullable: false),
                    FM_Arrow = table.Column<int>(nullable: false),
                    RWT = table.Column<int>(nullable: false),
                    Access = table.Column<int>(nullable: false),
                    CashInHand = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndOfDays", x => x.EndOfDayId);
                });

            migrationBuilder.CreateTable(
                name: "MonthEnds",
                columns: table => new
                {
                    MonthEndId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntryDate = table.Column<DateTime>(nullable: false),
                    Month = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    TotalBill = table.Column<double>(nullable: false),
                    TotalFabric = table.Column<double>(nullable: false),
                    TotalRMZ = table.Column<double>(nullable: false),
                    TotalAccess = table.Column<double>(nullable: false),
                    TotalOthers = table.Column<double>(nullable: false),
                    TotalAmountFabric = table.Column<decimal>(nullable: false),
                    TotalAmountRMZ = table.Column<decimal>(nullable: false),
                    TotalAmountAccess = table.Column<decimal>(nullable: false),
                    TotalAmountOthers = table.Column<decimal>(nullable: false),
                    TotalSaleIncome = table.Column<decimal>(nullable: false),
                    TotalTailoringIncome = table.Column<decimal>(nullable: false),
                    TotalOtherIncome = table.Column<decimal>(nullable: false),
                    TotalInward = table.Column<decimal>(nullable: false),
                    TotalInwardByAmitKumar = table.Column<decimal>(nullable: false),
                    TotalInwardOthers = table.Column<decimal>(nullable: false),
                    TotalDues = table.Column<decimal>(nullable: false),
                    TotalDuesOfMonth = table.Column<decimal>(nullable: false),
                    TotalDuesRecovered = table.Column<decimal>(nullable: false),
                    TotalExpenses = table.Column<decimal>(nullable: false),
                    TotalOnBookExpenes = table.Column<decimal>(nullable: false),
                    TotalCashExpenses = table.Column<decimal>(nullable: false),
                    TotalSalary = table.Column<decimal>(nullable: false),
                    TotalTailoringExpenses = table.Column<decimal>(nullable: false),
                    TotalTrimsAndOtherExpenses = table.Column<decimal>(nullable: false),
                    TotalHomeExpenses = table.Column<decimal>(nullable: false),
                    TotalOtherHomeExpenses = table.Column<decimal>(nullable: false),
                    TotalOtherExpenses = table.Column<decimal>(nullable: false),
                    TotalPayments = table.Column<decimal>(nullable: false),
                    TotalRecipts = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonthEnds", x => x.MonthEndId);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PayDate = table.Column<DateTime>(nullable: false),
                    PaymentPartry = table.Column<string>(nullable: true),
                    PayMode = table.Column<int>(nullable: false),
                    PaymentDetails = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    PaymentSlipNo = table.Column<string>(nullable: true),
                    Remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                });

            migrationBuilder.CreateTable(
                name: "Receipts",
                columns: table => new
                {
                    ReceiptId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecieptDate = table.Column<DateTime>(nullable: false),
                    ReceiptFrom = table.Column<string>(nullable: true),
                    PayMode = table.Column<int>(nullable: false),
                    ReceiptDetails = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    RecieptSlipNo = table.Column<string>(nullable: true),
                    Remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receipts", x => x.ReceiptId);
                });

            migrationBuilder.CreateTable(
                name: "Salesmen",
                columns: table => new
                {
                    SalesmanId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalesmanName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salesmen", x => x.SalesmanId);
                });

            migrationBuilder.CreateTable(
                name: "Suspenses",
                columns: table => new
                {
                    SuspenseAccountId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntryDate = table.Column<DateTime>(nullable: false),
                    ReferanceDetails = table.Column<string>(nullable: true),
                    InAmount = table.Column<decimal>(type: "money", nullable: false),
                    OutAmount = table.Column<decimal>(nullable: false),
                    IsCleared = table.Column<bool>(nullable: false),
                    ClearedDetails = table.Column<string>(nullable: true),
                    ReviewBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suspenses", x => x.SuspenseAccountId);
                });

            migrationBuilder.CreateTable(
                name: "TailoringEmployees",
                columns: table => new
                {
                    TailoringEmployeeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffName = table.Column<string>(nullable: true),
                    MobileNo = table.Column<string>(nullable: true),
                    JoiningDate = table.Column<DateTime>(nullable: false),
                    LeavingDate = table.Column<DateTime>(nullable: true),
                    IsWorking = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TailoringEmployees", x => x.TailoringEmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "TalioringBookings",
                columns: table => new
                {
                    TalioringBookingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingDate = table.Column<DateTime>(nullable: false),
                    CustName = table.Column<string>(nullable: true),
                    DeliveryDate = table.Column<DateTime>(nullable: false),
                    TryDate = table.Column<DateTime>(nullable: false),
                    BookingSlipNo = table.Column<string>(nullable: true),
                    TotalAmount = table.Column<decimal>(type: "money", nullable: false),
                    TotalQty = table.Column<int>(nullable: false),
                    ShirtQty = table.Column<int>(nullable: false),
                    ShirtPrice = table.Column<decimal>(type: "money", nullable: false),
                    PantQty = table.Column<int>(nullable: false),
                    PantPrice = table.Column<decimal>(type: "money", nullable: false),
                    CoatQty = table.Column<int>(nullable: false),
                    CoatPrice = table.Column<decimal>(type: "money", nullable: false),
                    KurtaQty = table.Column<int>(nullable: false),
                    KurtaPrice = table.Column<decimal>(type: "money", nullable: false),
                    BundiQty = table.Column<int>(nullable: false),
                    BundiPrice = table.Column<decimal>(type: "money", nullable: false),
                    Others = table.Column<int>(nullable: false),
                    OthersPrice = table.Column<decimal>(type: "money", nullable: false),
                    IsDelivered = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TalioringBookings", x => x.TalioringBookingId);
                });

            migrationBuilder.CreateTable(
                name: "TranscationModes",
                columns: table => new
                {
                    TranscationModeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Transcation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TranscationModes", x => x.TranscationModeId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountNumbers",
                columns: table => new
                {
                    AccountNumberId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankId = table.Column<int>(nullable: false),
                    Account = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountNumbers", x => x.AccountNumberId);
                    table.ForeignKey(
                        name: "FK_AccountNumbers_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "BankId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attendances",
                columns: table => new
                {
                    AttendanceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(nullable: false),
                    AttDate = table.Column<DateTime>(nullable: false),
                    EntryTime = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendances", x => x.AttendanceId);
                    table.ForeignKey(
                        name: "FK_Attendances_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CurrentSalaries",
                columns: table => new
                {
                    CurrentSalaryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(nullable: false),
                    BasicSalary = table.Column<decimal>(type: "money", nullable: false),
                    SundaySalary = table.Column<decimal>(type: "money", nullable: false),
                    LPRate = table.Column<decimal>(nullable: false),
                    IncentiveRate = table.Column<decimal>(nullable: false),
                    IncentiveTarget = table.Column<decimal>(type: "money", nullable: false),
                    WOWBillRate = table.Column<decimal>(nullable: false),
                    WOWBillTarget = table.Column<decimal>(type: "money", nullable: false),
                    IsSundayBillable = table.Column<bool>(nullable: false),
                    EffectiveDate = table.Column<DateTime>(nullable: false),
                    CloseDate = table.Column<DateTime>(nullable: false),
                    IsEffective = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentSalaries", x => x.CurrentSalaryId);
                    table.ForeignKey(
                        name: "FK_CurrentSalaries_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    ExpenseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpDate = table.Column<DateTime>(nullable: false),
                    Particulars = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    PayMode = table.Column<int>(nullable: false),
                    PaymentDetails = table.Column<string>(nullable: true),
                    EmployeeId = table.Column<int>(nullable: false),
                    PaidTo = table.Column<string>(nullable: true),
                    Remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.ExpenseId);
                    table.ForeignKey(
                        name: "FK_Expenses_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PettyCashExpenses",
                columns: table => new
                {
                    PettyCashExpenseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpDate = table.Column<DateTime>(nullable: false),
                    Particulars = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    EmployeeId = table.Column<int>(nullable: false),
                    PaidTo = table.Column<string>(nullable: true),
                    Remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PettyCashExpenses", x => x.PettyCashExpenseId);
                    table.ForeignKey(
                        name: "FK_PettyCashExpenses_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalaryPayments",
                columns: table => new
                {
                    SalaryPaymentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(nullable: false),
                    SalaryMonth = table.Column<string>(nullable: true),
                    SalaryComponet = table.Column<int>(nullable: false),
                    PaymentDate = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    PayMode = table.Column<int>(nullable: false),
                    Details = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryPayments", x => x.SalaryPaymentId);
                    table.ForeignKey(
                        name: "FK_SalaryPayments_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StaffAdvancePayments",
                columns: table => new
                {
                    StaffAdvancePaymentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(nullable: false),
                    PaymentDate = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    PayMode = table.Column<int>(nullable: false),
                    Details = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffAdvancePayments", x => x.StaffAdvancePaymentId);
                    table.ForeignKey(
                        name: "FK_StaffAdvancePayments_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StaffAdvanceReceipts",
                columns: table => new
                {
                    StaffAdvanceReceiptId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(nullable: false),
                    ReceiptDate = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    PayMode = table.Column<int>(nullable: false),
                    Details = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffAdvanceReceipts", x => x.StaffAdvanceReceiptId);
                    table.ForeignKey(
                        name: "FK_StaffAdvanceReceipts_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DailySales",
                columns: table => new
                {
                    DailySaleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SaleDate = table.Column<DateTime>(nullable: false),
                    InvNo = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    PayMode = table.Column<int>(nullable: false),
                    CashAmount = table.Column<decimal>(type: "money", nullable: false),
                    SalesmanId = table.Column<int>(nullable: false),
                    IsDue = table.Column<bool>(nullable: false),
                    IsManualBill = table.Column<bool>(nullable: false),
                    IsTailoringBill = table.Column<bool>(nullable: false),
                    IsSaleReturn = table.Column<bool>(nullable: false),
                    Remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailySales", x => x.DailySaleId);
                    table.ForeignKey(
                        name: "FK_DailySales_Salesmen_SalesmanId",
                        column: x => x.SalesmanId,
                        principalTable: "Salesmen",
                        principalColumn: "SalesmanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TailorAttendances",
                columns: table => new
                {
                    TailorAttendanceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TailoringEmployeeId = table.Column<int>(nullable: false),
                    AttDate = table.Column<DateTime>(nullable: false),
                    EntryTime = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(nullable: true)
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
                    TailoringSalaryPaymentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TailoringEmployeeId = table.Column<int>(nullable: false),
                    SalaryMonth = table.Column<string>(nullable: true),
                    SalaryComponet = table.Column<int>(nullable: false),
                    PaymentDate = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    PayMode = table.Column<int>(nullable: false),
                    Details = table.Column<string>(nullable: true)
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
                    TailoringStaffAdvancePaymentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TailoringEmployeeId = table.Column<int>(nullable: false),
                    PaymentDate = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    PayMode = table.Column<int>(nullable: false),
                    Details = table.Column<string>(nullable: true)
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
                    TailoringStaffAdvanceReceiptId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TailoringEmployeeId = table.Column<int>(nullable: false),
                    ReceiptDate = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    PayMode = table.Column<int>(nullable: false),
                    Details = table.Column<string>(nullable: true)
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

            migrationBuilder.CreateTable(
                name: "TailoringDeliveries",
                columns: table => new
                {
                    TalioringDeliveryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeliveryDate = table.Column<DateTime>(nullable: false),
                    TalioringBookingId = table.Column<int>(nullable: false),
                    InvNo = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    Remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TailoringDeliveries", x => x.TalioringDeliveryId);
                    table.ForeignKey(
                        name: "FK_TailoringDeliveries_TalioringBookings_TalioringBookingId",
                        column: x => x.TalioringBookingId,
                        principalTable: "TalioringBookings",
                        principalColumn: "TalioringBookingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CashPayments",
                columns: table => new
                {
                    CashPaymentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentDate = table.Column<DateTime>(nullable: false),
                    TranscationModeId = table.Column<int>(nullable: false),
                    PaidTo = table.Column<string>(nullable: false),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    SlipNo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashPayments", x => x.CashPaymentId);
                    table.ForeignKey(
                        name: "FK_CashPayments_TranscationModes_TranscationModeId",
                        column: x => x.TranscationModeId,
                        principalTable: "TranscationModes",
                        principalColumn: "TranscationModeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CashReceipts",
                columns: table => new
                {
                    CashReceiptId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InwardDate = table.Column<DateTime>(nullable: false),
                    TranscationModeId = table.Column<int>(nullable: false),
                    ReceiptFrom = table.Column<string>(nullable: false),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    SlipNo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashReceipts", x => x.CashReceiptId);
                    table.ForeignKey(
                        name: "FK_CashReceipts_TranscationModes_TranscationModeId",
                        column: x => x.TranscationModeId,
                        principalTable: "TranscationModes",
                        principalColumn: "TranscationModeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BankDeposits",
                columns: table => new
                {
                    BankDepositId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepoDate = table.Column<DateTime>(nullable: false),
                    AccountNumberId = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    PayMode = table.Column<int>(nullable: false),
                    Details = table.Column<string>(nullable: true),
                    Remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankDeposits", x => x.BankDepositId);
                    table.ForeignKey(
                        name: "FK_BankDeposits_AccountNumbers_AccountNumberId",
                        column: x => x.AccountNumberId,
                        principalTable: "AccountNumbers",
                        principalColumn: "AccountNumberId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BankWithdrawals",
                columns: table => new
                {
                    BankWithdrawalId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepoDate = table.Column<DateTime>(nullable: false),
                    AccountNumberId = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    ChequeNo = table.Column<string>(nullable: true),
                    SignedBy = table.Column<string>(nullable: true),
                    ApprovedBy = table.Column<string>(nullable: true),
                    InNameOf = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankWithdrawals", x => x.BankWithdrawalId);
                    table.ForeignKey(
                        name: "FK_BankWithdrawals_AccountNumbers_AccountNumberId",
                        column: x => x.AccountNumberId,
                        principalTable: "AccountNumbers",
                        principalColumn: "AccountNumberId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaySlips",
                columns: table => new
                {
                    PaySlipId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OnDate = table.Column<DateTime>(nullable: false),
                    Month = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false),
                    CurrentSalaryId = table.Column<int>(nullable: true),
                    BasicSalary = table.Column<decimal>(type: "money", nullable: false),
                    NoOfDaysPresent = table.Column<int>(nullable: false),
                    TotalSale = table.Column<decimal>(type: "money", nullable: false),
                    SaleIncentive = table.Column<decimal>(type: "money", nullable: false),
                    WOWBillAmount = table.Column<decimal>(type: "money", nullable: false),
                    WOWBillIncentive = table.Column<decimal>(type: "money", nullable: false),
                    LastPcsAmount = table.Column<decimal>(type: "money", nullable: false),
                    LastPCsIncentive = table.Column<decimal>(type: "money", nullable: false),
                    OthersIncentive = table.Column<decimal>(type: "money", nullable: false),
                    GrossSalary = table.Column<decimal>(type: "money", nullable: false),
                    StandardDeductions = table.Column<decimal>(type: "money", nullable: false),
                    TDSDeductions = table.Column<decimal>(type: "money", nullable: false),
                    PFDeductions = table.Column<decimal>(type: "money", nullable: false),
                    AdvanceDeducations = table.Column<decimal>(type: "money", nullable: false),
                    OtherDeductions = table.Column<decimal>(type: "money", nullable: false),
                    Remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaySlips", x => x.PaySlipId);
                    table.ForeignKey(
                        name: "FK_PaySlips_CurrentSalaries_CurrentSalaryId",
                        column: x => x.CurrentSalaryId,
                        principalTable: "CurrentSalaries",
                        principalColumn: "CurrentSalaryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaySlips_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DuesLists",
                columns: table => new
                {
                    DuesListId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(nullable: false),
                    IsRecovered = table.Column<bool>(nullable: false),
                    RecoveryDate = table.Column<DateTime>(nullable: true),
                    DailySaleId = table.Column<int>(nullable: false),
                    IsPartialRecovery = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DuesLists", x => x.DuesListId);
                    table.ForeignKey(
                        name: "FK_DuesLists_DailySales_DailySaleId",
                        column: x => x.DailySaleId,
                        principalTable: "DailySales",
                        principalColumn: "DailySaleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DueRecoverds",
                columns: table => new
                {
                    DueRecoverdId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaidDate = table.Column<DateTime>(nullable: false),
                    DuesListId = table.Column<int>(nullable: false),
                    AmountPaid = table.Column<decimal>(type: "money", nullable: false),
                    IsPartialPayment = table.Column<bool>(nullable: false),
                    Modes = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DueRecoverds", x => x.DueRecoverdId);
                    table.ForeignKey(
                        name: "FK_DueRecoverds_DuesLists_DuesListId",
                        column: x => x.DuesListId,
                        principalTable: "DuesLists",
                        principalColumn: "DuesListId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountNumbers_BankId",
                table: "AccountNumbers",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_EmployeeId",
                table: "Attendances",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_BankDeposits_AccountNumberId",
                table: "BankDeposits",
                column: "AccountNumberId");

            migrationBuilder.CreateIndex(
                name: "IX_BankWithdrawals_AccountNumberId",
                table: "BankWithdrawals",
                column: "AccountNumberId");

            migrationBuilder.CreateIndex(
                name: "IX_CashInBanks_CIBDate",
                table: "CashInBanks",
                column: "CIBDate",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CashInHands_CIHDate",
                table: "CashInHands",
                column: "CIHDate",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CashPayments_TranscationModeId",
                table: "CashPayments",
                column: "TranscationModeId");

            migrationBuilder.CreateIndex(
                name: "IX_CashReceipts_TranscationModeId",
                table: "CashReceipts",
                column: "TranscationModeId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrentSalaries_EmployeeId",
                table: "CurrentSalaries",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_DailySales_SalesmanId",
                table: "DailySales",
                column: "SalesmanId");

            migrationBuilder.CreateIndex(
                name: "IX_DueRecoverds_DuesListId",
                table: "DueRecoverds",
                column: "DuesListId");

            migrationBuilder.CreateIndex(
                name: "IX_DuesLists_DailySaleId",
                table: "DuesLists",
                column: "DailySaleId");

            migrationBuilder.CreateIndex(
                name: "IX_EndOfDays_EOD_Date",
                table: "EndOfDays",
                column: "EOD_Date",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_EmployeeId",
                table: "Expenses",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PaySlips_CurrentSalaryId",
                table: "PaySlips",
                column: "CurrentSalaryId");

            migrationBuilder.CreateIndex(
                name: "IX_PaySlips_EmployeeId",
                table: "PaySlips",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PettyCashExpenses_EmployeeId",
                table: "PettyCashExpenses",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_SalaryPayments_EmployeeId",
                table: "SalaryPayments",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffAdvancePayments_EmployeeId",
                table: "StaffAdvancePayments",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffAdvanceReceipts_EmployeeId",
                table: "StaffAdvanceReceipts",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TailorAttendances_TailoringEmployeeId",
                table: "TailorAttendances",
                column: "TailoringEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TailoringDeliveries_TalioringBookingId",
                table: "TailoringDeliveries",
                column: "TalioringBookingId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Attendances");

            migrationBuilder.DropTable(
                name: "BankDeposits");

            migrationBuilder.DropTable(
                name: "BankWithdrawals");

            migrationBuilder.DropTable(
                name: "CashInBanks");

            migrationBuilder.DropTable(
                name: "CashInHands");

            migrationBuilder.DropTable(
                name: "CashPayments");

            migrationBuilder.DropTable(
                name: "CashReceipts");

            migrationBuilder.DropTable(
                name: "ChequesLogs");

            migrationBuilder.DropTable(
                name: "DueRecoverds");

            migrationBuilder.DropTable(
                name: "EndOfDays");

            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "MonthEnds");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "PaySlips");

            migrationBuilder.DropTable(
                name: "PettyCashExpenses");

            migrationBuilder.DropTable(
                name: "Receipts");

            migrationBuilder.DropTable(
                name: "SalaryPayments");

            migrationBuilder.DropTable(
                name: "StaffAdvancePayments");

            migrationBuilder.DropTable(
                name: "StaffAdvanceReceipts");

            migrationBuilder.DropTable(
                name: "Suspenses");

            migrationBuilder.DropTable(
                name: "TailorAttendances");

            migrationBuilder.DropTable(
                name: "TailoringDeliveries");

            migrationBuilder.DropTable(
                name: "TailoringSalaryPayments");

            migrationBuilder.DropTable(
                name: "TailoringStaffAdvancePayments");

            migrationBuilder.DropTable(
                name: "TailoringStaffAdvanceReceipts");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "AccountNumbers");

            migrationBuilder.DropTable(
                name: "TranscationModes");

            migrationBuilder.DropTable(
                name: "DuesLists");

            migrationBuilder.DropTable(
                name: "CurrentSalaries");

            migrationBuilder.DropTable(
                name: "TalioringBookings");

            migrationBuilder.DropTable(
                name: "TailoringEmployees");

            migrationBuilder.DropTable(
                name: "Banks");

            migrationBuilder.DropTable(
                name: "DailySales");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Salesmen");
        }
    }
}
