using AprajitaRetails.Models;
using Microsoft.EntityFrameworkCore;
using AprajitaRetails.Areas.ToDo.Models;
using System;
using System.Linq;
using AprajitaRetails.Areas.Voyager.Models;
using AprajitaRetails.Areas.Purchase.Models;
using AprajitaRetails.Areas.Sales.Models;
using AprajitaRetails.Areas.Sales.Models.Views;
using AprajitaRetails.Areas.Uploader.Models;
//using ManualSaleItem = AprajitaRetails.Areas.Sales.Models.ManualSaleItem;
//using ManualInvoice = AprajitaRetails.Areas.Sales.Models.ManualInvoice;
//using SaleItem = AprajitaRetails.Areas.Sales.Models.SaleItem;
//using SaleInvoice = AprajitaRetails.Areas.Sales.Models.SaleInvoice;
using AprajitaRetails.Areas.Accounts.Models;
using AprajitaRetails.Areas.Reports.Models;
using AprajitaRetails.Areas.Admin.Models;
//using AprajitaRetails.Areas.Chat.Models;

namespace AprajitaRetails.Data
{
    public class AprajitaRetailsContext : DbContext
    {
        public AprajitaRetailsContext(DbContextOptions<AprajitaRetailsContext> options) : base(options)
        {
            ApplyMigrations(this);
        }

        public DbSet<TodoItem> Todos { get; set; }
        public DbSet<FileInfo> Files { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TodoItem>().ToTable("Todo");
            modelBuilder.Entity<FileInfo>().ToTable("File");
            modelBuilder.Entity<TodoItem>()
                .Property(e => e.Tags)
                .HasConversion(v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));

            modelBuilder.Entity<CashInBank>()
                .HasIndex(b => b.CIBDate)
                .IsUnique();
            modelBuilder.Entity<CashInHand>()
               .HasIndex(b => b.CIHDate)
               .IsUnique();
            modelBuilder.Entity<EndOfDay>()
               .HasIndex(b => b.EOD_Date)
               .IsUnique();
            modelBuilder.Entity<TranscationMode>()
              .HasIndex(b => b.Transcation)
              .IsUnique();

            modelBuilder.Entity<Salesman>().HasData(new Salesman { SalesmanId = 1, SalesmanName = "Sanjeev Mishra", StoreId = 1 });
            modelBuilder.Entity<Salesman>().HasData(new Salesman { SalesmanId = 2, SalesmanName = "Mukesh Mandal", StoreId = 1 });
            modelBuilder.Entity<Salesman>().HasData(new Salesman { SalesmanId = 3, SalesmanName = "Manager", StoreId = 1 });
            modelBuilder.Entity<Salesman>().HasData(new Salesman { SalesmanId = 4, SalesmanName = "Bikash Kumar Sah", StoreId = 1 });


            modelBuilder.Entity<Bank>().HasData(new Bank() { BankId = 1, BankName = "SBI" });
            modelBuilder.Entity<Bank>().HasData(new Bank() { BankId = 2, BankName = "ICICI" });
            modelBuilder.Entity<Bank>().HasData(new Bank() { BankId = 3, BankName = "Bandhan Bank" });
            modelBuilder.Entity<Bank>().HasData(new Bank() { BankId = 4, BankName = "PNB" });
            modelBuilder.Entity<Bank>().HasData(new Bank() { BankId = 5, BankName = "BOB" });
            modelBuilder.Entity<Bank>().HasData(new Bank() { BankId = 6, BankName = "Axis" });
            modelBuilder.Entity<Bank>().HasData(new Bank() { BankId = 7, BankName = "HDFC" });


            modelBuilder.Entity<TranscationMode>().HasData(new TranscationMode { TranscationModeId = 1, Transcation = "Home Expenses" });
            modelBuilder.Entity<TranscationMode>().HasData(new TranscationMode { TranscationModeId = 2, Transcation = "Other Home Expenses" });
            modelBuilder.Entity<TranscationMode>().HasData(new TranscationMode { TranscationModeId = 3, Transcation = "Mukesh(Home Staff)" });
            modelBuilder.Entity<TranscationMode>().HasData(new TranscationMode { TranscationModeId = 4, Transcation = "Amit Kumar" });
            modelBuilder.Entity<TranscationMode>().HasData(new TranscationMode { TranscationModeId = 5, Transcation = "Amit Kumar Expenses" });
            modelBuilder.Entity<TranscationMode>().HasData(new TranscationMode { TranscationModeId = 6, Transcation = "CashIn" });
            modelBuilder.Entity<TranscationMode>().HasData(new TranscationMode { TranscationModeId = 7, Transcation = "CashOut" });
            modelBuilder.Entity<TranscationMode>().HasData(new TranscationMode { TranscationModeId = 8, Transcation = "Regular" });

           // modelBuilder.Entity<SalesPerson>().HasData(new SalesPerson { SalesPersonId = 1, SalesmanName = "Sanjeev Mishra" });
           // modelBuilder.Entity<SalesPerson>().HasData(new SalesPerson { SalesPersonId = 2, SalesmanName = "Mukesh Mandal" });
           // modelBuilder.Entity<SalesPerson>().HasData(new SalesPerson { SalesPersonId = 3, SalesmanName = "Manager" });
           // modelBuilder.Entity<SalesPerson>().HasData(new SalesPerson { SalesPersonId = 4, SalesmanName = "Bikash Kumar Sah" });


            modelBuilder.Entity<SaleTaxType>().HasData(new SaleTaxType { SaleTaxTypeId = 1, CompositeRate = 5, TaxName = "Local Output GST@ 5%  ", TaxType = TaxType.GST });
            modelBuilder.Entity<SaleTaxType>().HasData(new SaleTaxType { SaleTaxTypeId = 2, CompositeRate = 12, TaxName = "Local Output GST@ 12%  ", TaxType = TaxType.GST });

            modelBuilder.Entity<SaleTaxType>().HasData(new SaleTaxType { SaleTaxTypeId = 3, CompositeRate = 5, TaxName = "Output IGST@ 5%  ", TaxType = TaxType.IGST });
            modelBuilder.Entity<SaleTaxType>().HasData(new SaleTaxType { SaleTaxTypeId = 4, CompositeRate = 12, TaxName = "Output IGST@ 12%  ", TaxType = TaxType.IGST });

            modelBuilder.Entity<SaleTaxType>().HasData(new SaleTaxType { SaleTaxTypeId = 5, CompositeRate = 5, TaxName = "Output Vat@ 12%  ", TaxType = TaxType.VAT });
            modelBuilder.Entity<SaleTaxType>().HasData(new SaleTaxType { SaleTaxTypeId = 6, CompositeRate = 12, TaxName = "Output VAT@ 5%  ", TaxType = TaxType.VAT });
            modelBuilder.Entity<SaleTaxType>().HasData(new SaleTaxType { SaleTaxTypeId = 7, CompositeRate = 5, TaxName = "Output Vat Free  ", TaxType = TaxType.VAT });

            modelBuilder.Entity<Customer>().HasData(new Customer
            {
                CustomerId = 1,
                FirstName = "Cash",
                LastName = "Sale",
                Age = 0,
                City = "Dumka",
                CreatedDate = DateTime.Now.Date,
                Gender = Genders.Male,
                NoOfBills = 0,
                TotalAmount = 0,
                MobileNo = "1234567890",
                DateOfBirth = DateTime.Now.Date
            });
            modelBuilder.Entity<Customer>().HasData(new Customer
            {
                CustomerId = 2,
                FirstName = "Card",
                LastName = "Sale",
                Age = 0,
                City = "Dumka",
                CreatedDate = DateTime.Now.Date,
                Gender = Genders.Male,
                NoOfBills = 0,
                TotalAmount = 0,
                MobileNo = "1234567890",
                DateOfBirth = DateTime.Now.Date
            });


        }
        ////Version 2
        public DbSet<DailySale> DailySales { get; set; }   //Version 3.0
        public DbSet<CashInHand> CashInHands { get; set; } //Version 3.0
        public DbSet<CashInBank> CashInBanks { get; set; } //Version 3.0
        public DbSet<Salesman> Salesmen { get; set; }      //Version 3.0
        public DbSet<DuesList> DuesLists { get; set; }     //Version 3.0

        public DbSet<TranscationMode> TranscationModes { get; set; }
        public DbSet<CashPayment> CashPayments { get; set; }
        public DbSet<CashReceipt> CashReceipts { get; set; }

        //Payroll
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public DbSet<EmployeeUser> EmployeeUsers { get; set; }

        public DbSet<SalaryPayment> SalaryPayments { get; set; }
        public DbSet<StaffAdvancePayment> StaffAdvancePayments { get; set; }
        public DbSet<StaffAdvanceReceipt> StaffAdvanceReceipts { get; set; }

        public DbSet<CurrentSalary> CurrentSalaries { get; set; }
        public DbSet<PaySlip> PaySlips { get; set; }

        //Banking
        public DbSet<Bank> Banks { get; set; }
        public DbSet<AccountNumber> AccountNumbers { get; set; }
        public DbSet<BankDeposit> BankDeposits { get; set; }
        public DbSet<BankWithdrawal> BankWithdrawals { get; set; }



        //Expenses
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<PettyCashExpense> PettyCashExpenses { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Receipt> Receipts { get; set; }

        //Suspense
        public DbSet<SuspenseAccount> Suspenses { get; set; }


        //Tailoring

        //public DbSet<TailorAttendance> TailorAttendances { get; set; }
        //public DbSet<TailoringEmployee> TailoringEmployees { get; set; }
       // public DbSet<TailoringSalaryPayment> TailoringSalaryPayments { get; set; }
       // public DbSet<TailoringStaffAdvancePayment> TailoringStaffAdvancePayments { get; set; }
       // public DbSet<TailoringStaffAdvanceReceipt> TailoringStaffAdvanceReceipts { get; set; }

        public DbSet<TalioringBooking> TalioringBookings { get; set; }
        public DbSet<TalioringDelivery> TailoringDeliveries { get; set; }


        //End of Day
        public DbSet<EndOfDay> EndOfDays { get; set; }


        //Others

        public DbSet<DueRecoverd> DueRecoverds { get; set; }
        public DbSet<ChequesLog> ChequesLogs { get; set; }

        public DbSet<MonthEnd> MonthEnds { get; set; }

        public DbSet<AprajitaRetails.Models.ViewModels.IncomeExpensesReport> IncomeExpensesReport { get; set; }

        public DbSet<AprajitaRetails.Areas.AddressBook.Models.Contact> Contact { get; set; }

        // public DbSet<Message> Messages { get; set; }

        public DbSet<TelegramAuthUser> TelegramAuthUsers { get; set; }
        public DbSet<ToDoMessage> ToDoMessages { get; set; }
        public DbSet<AprajitaRetails.Models.CashDetail> CashDetail { get; set; }


        //BankAccount Info

        public DbSet<BankAccountInfo> BankAccountInfos { get; set; }
        public DbSet<BankAccountSecurityInfo> AccountSecurityInfos { get; set; }


        public void ApplyMigrations(AprajitaRetailsContext context)
        {
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
        }

        //Version 3.0
        public DbSet<AprajitaRetails.Models.OnlineSale> OnlineSale { get; set; }
        public DbSet<AprajitaRetails.Models.OnlineVendor> OnlineVendor { get; set; }
        public DbSet<OnlineSaleReturn> OnlineSaleReturns { get; set; }
        public DbSet<AttendanceVM> AttendancesImport { get; set; }



        // Unifiing of Database . 

        public DbSet<Store> Stores { get; set; } // Adding to relevent classes
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ProductItem> ProductItems { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        //Purchase Entry System
        public DbSet<ProductPurchase> ProductPurchases { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<PurchaseItem> PurchaseItems { get; set; }
        public DbSet<PurchaseTaxType> PurchaseTaxTypes { get; set; }
        //public DbSet<SalesPerson> SalesPerson { get; set; }

        public DbSet<Areas.Sales.Models.SaleInvoice> SaleInvoices { get; set; }
        public DbSet<Areas.Sales.Models.SaleItem> SaleItems { get; set; }

        public DbSet<SaleTaxType> SaleTaxTypes { get; set; }
        // public DbSet<SalePaymentDetail> SalePaymentDetails { get; set; }
        // public DbSet<CardPaymentDetail> CardPaymentDetails { get; set; }
        public DbSet<ArvindPayment> ArvindPayments { get; set; }

        // New Invoice System
        public DbSet<RegularInvoice> RegularInvoices { get; set; }
        public DbSet<RegularSaleItem> RegularSaleItems { get; set; }
        //public DbSet<RegularPaymentDetail> RegularPaymentDetails { get; set; }
        //public DbSet<RegularCardDetail> RegularCardDetails { get; set; }

        public DbSet<PaymentDetail> PaymentDetails { get; set; }
        public DbSet<CardDetail> CardDetails { get; set; }

        public DbSet<HSN> HSNList { get; set; }

        // New Invoice System  Manual

        //public DbSet<ManualInvoice> ManualInvoices { get; set; }
        //public DbSet<ManualSaleItem> ManualSaleItems { get; set; }
        //public DbSet<ManualPaymentDetail> ManualPaymentDetails { get; set; }
        //public DbSet<ManualCardDetail> ManualCardDetails { get; set; }

        //Import Table
        public DbSet<ImportInWard> ImportInWards { get; set; }
        public DbSet<ImportPurchase> ImportPurchases { get; set; }
        public DbSet<ImportSaleItemWise> ImportSaleItemWises { get; set; }
        public DbSet<ImportSaleRegister> ImportSaleRegisters { get; set; }

        public DbSet<BookEntry> ImportBookEntries { get; set; }
        public DbSet<BankStatement> BankStatements { get; set; }

        //Accounts
        public DbSet<LedgerMaster> Masters { get; set; }
        public DbSet<Party> Parties { get; set; }
        // public DbSet<LedgerEntry> LedgerEntries { get; set; }
        public DbSet<BasicLedgerEntry> BasicLedgerEntries { get; set; }
        // public DbSet<LedgerEntry> LedgerEntries { get; set; }
        // public DbSet<AprajitaRetails.Areas.Reports.Models.EmpAttReport> EmpAttReport { get; set; }
        // public DbSet<LedgerEntry> LedgerEntries { get; set; }
        //public DbSet<AprajitaRetails.Areas.Reports.Models.EmpFinReport> EmpFinReport { get; set; }


        //Admin
        public DbSet<RegisteredUser> RegisteredUsers { get; set; }


        public DbSet<BankSetting> BankSettings { get; set; }
        public DbSet<AccSetting> AccSettings { get; set; }

    }

}
