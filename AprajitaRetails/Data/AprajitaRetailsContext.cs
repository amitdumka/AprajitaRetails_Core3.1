using AprajitaRetails.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using AprajitaRetails.Models.ViewModels;
using AprajitaRetails.Areas.AddressBook.Models;
//using AprajitaRetails.Areas.Chat.Models;

namespace AprajitaRetails.Data
{
    public class AprajitaRetailsContext : DbContext
    {
        public AprajitaRetailsContext(DbContextOptions<AprajitaRetailsContext> options) : base(options)
        {
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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

            modelBuilder.Entity<Salesman>().HasData(new Salesman { SalesmanId = 1, SalesmanName = "Sanjeev Mishra" });
            modelBuilder.Entity<Salesman>().HasData(new Salesman { SalesmanId = 2, SalesmanName = "Mukesh Mandal" });
            modelBuilder.Entity<Salesman>().HasData(new Salesman { SalesmanId = 3, SalesmanName = "Manager" });

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


        }
        ////Version 2
        public DbSet<DailySale> DailySales { get; set; }
        public DbSet<CashInHand> CashInHands { get; set; }
        public DbSet<CashInBank> CashInBanks { get; set; }
        public DbSet<Salesman> Salesmen { get; set; }
        public DbSet<DuesList> DuesLists { get; set; }

        public DbSet<TranscationMode> TranscationModes { get; set; }
        public DbSet<CashPayment> CashPayments { get; set; }
        public DbSet<CashReceipt> CashReceipts { get; set; }

        //Payroll
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Employee> Employees { get; set; }

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

        public DbSet<TailorAttendance> TailorAttendances { get; set; }
        public DbSet<TailoringEmployee> TailoringEmployees { get; set; }
        public DbSet<TailoringSalaryPayment> TailoringSalaryPayments { get; set; }
        public DbSet<TailoringStaffAdvancePayment> TailoringStaffAdvancePayments { get; set; }
        public DbSet<TailoringStaffAdvanceReceipt> TailoringStaffAdvanceReceipts { get; set; }

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
    }

}
