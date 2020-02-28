using Microsoft.EntityFrameworkCore;
using StoneWorks.Models;

namespace StoneWorks.Data
{
    public class StoneWorksContext:DbContext
    {
        public StoneWorksContext(DbContextOptions<StoneWorksContext> options1)
            : base(options1)
        {
        }

        //StoneWorks
        public DbSet<DailyLabor> DailyLabor { get; set; }
        public DbSet<Bolder> Bolders { get; set; }
        public DbSet< Bolder> Bolder { get; set; }
        public DbSet< Fuel> Fuel { get; set; }
        public DbSet< FuelConsumtion> FuelConsumtion { get; set; }
        public DbSet< ChipSales> ChipSales { get; set; }
        public DbSet< Truck> Truck { get; set; }
        public DbSet< HiredTruck> HiredTruck { get; set; }
        public DbSet< Staff> Staff { get; set; }
        public DbSet< StaffSalary> StaffSalary { get; set; }
        public DbSet<BasicExpense> BasicExpenses { get; set; }


    }
}
