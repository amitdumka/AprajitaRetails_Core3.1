using AprajitaRetails.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using AprajitaRetails.Models.ViewModels;

namespace AprajitaRetails.Data
{
    public class AprajitaRetailsContext : IdentityDbContext
    {
        public AprajitaRetailsContext(DbContextOptions<AprajitaRetailsContext> options) : base (options)
        {
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating (modelBuilder);

            modelBuilder.Entity<CashInBank> ()
                .HasIndex (b => b.CIBDate)
                .IsUnique ();
            modelBuilder.Entity<CashInHand> ()
               .HasIndex (b => b.CIHDate)
               .IsUnique ();
            modelBuilder.Entity<EndOfDay> ()
               .HasIndex (b => b.EOD_Date)
               .IsUnique ();
            modelBuilder.Entity<TranscationMode>()
              .HasIndex(b => b.Transcation)
              .IsUnique();
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

      

    }

}
