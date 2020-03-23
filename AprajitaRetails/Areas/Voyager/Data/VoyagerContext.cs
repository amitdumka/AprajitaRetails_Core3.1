using AprajitaRetails.Areas.Purchase.Models;
using AprajitaRetails.Areas.Sales.Models;
using AprajitaRetails.Areas.Uploader.Models;
using AprajitaRetails.Areas.Voyager.Models;

using Microsoft.EntityFrameworkCore;
using AprajitaRetails.Models;
using AprajitaRetails.Areas.Sales.Models.Views;

namespace AprajitaRetails.Areas.Voyager.Data
{
    public class VoyagerContext : DbContext
    {
        public VoyagerContext(DbContextOptions<VoyagerContext> options) : base (options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating (modelBuilder);
            modelBuilder.Entity<SalesPerson> ().HasData (new SalesPerson { SalesPersonId = 1, SalesmanName = "Sanjeev Mishra" });
            modelBuilder.Entity<SalesPerson> ().HasData (new SalesPerson { SalesPersonId = 2, SalesmanName = "Mukesh Mandal" });
            modelBuilder.Entity<SalesPerson> ().HasData (new SalesPerson { SalesPersonId = 3, SalesmanName = "Manager" });
        }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Customer> Customers { get; set; }

        //Import Table
        public DbSet<ImportInWard> ImportInWards { get; set; }
        public DbSet<ImportPurchase> ImportPurchases { get; set; }
        public DbSet<ImportSaleItemWise> ImportSaleItemWises { get; set; }
        public DbSet<ImportSaleRegister> ImportSaleRegisters { get; set; }
        public DbSet<ProductItem> ProductItems { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        //Purchase Entry System
        public DbSet<ProductPurchase> ProductPurchases { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<PurchaseItem> PurchaseItems { get; set; }
        public DbSet<PurchaseTaxType> PurchaseTaxTypes { get; set; }
        public DbSet<SalesPerson> SalesPerson { get; set; }
        public DbSet<Sales.Models.SaleInvoice> SaleInvoices { get; set; }
        public DbSet<Sales.Models.SaleItem> SaleItems { get; set; }
        public DbSet<SaleTaxType> SaleTaxTypes { get; set; }
        public DbSet<SalePaymentDetail> SalePaymentDetails { get; set; }
        public DbSet<CardPaymentDetail> CardPaymentDetails { get; set; }
        public DbSet<ArvindPayment> ArvindPayments { get; set; }

        // New Invoice System
        public DbSet<RegularInvoice> RegularInvoices { get; set; }
        public DbSet<RegularSaleItem> RegularSaleItems { get; set; }
        public DbSet<RegularPaymentDetail>RegularPaymentDetails { get; set; }
        public DbSet<RegularCardDetail>RegularCardDetails { get; set; }


       

    }


}
