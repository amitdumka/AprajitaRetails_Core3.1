using AprajitaRetails.Areas.StoneWorks.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AprajitaRetails.Areas.StoneWorks.Data
{
    public class StoneWorksContext :DbContext
    {
        public StoneWorksContext(DbContextOptions<StoneWorksContext> options) : base(options)
        {
        }

        //StoneWorks
        public DbSet<DailyLabor> DailyLabor { get; set; }
        public DbSet<Bolder> Bolders { get; set; }
        public DbSet<AprajitaRetails.Areas.StoneWorks.Models.Bolder> Bolder { get; set; }
        public DbSet<AprajitaRetails.Areas.StoneWorks.Models.Fuel> Fuel { get; set; }
        public DbSet<AprajitaRetails.Areas.StoneWorks.Models.FuelConsumtion> FuelConsumtion { get; set; }
        public DbSet<AprajitaRetails.Areas.StoneWorks.Models.ChipSales> ChipSales { get; set; }
        public DbSet<AprajitaRetails.Areas.StoneWorks.Models.Truck> Truck { get; set; }
        public DbSet<AprajitaRetails.Areas.StoneWorks.Models.HiredTruck> HiredTruck { get; set; }
        public DbSet<AprajitaRetails.Areas.StoneWorks.Models.Staff> Staff { get; set; }
        public DbSet<AprajitaRetails.Areas.StoneWorks.Models.StaffSalary> StaffSalary { get; set; }

    }
}
