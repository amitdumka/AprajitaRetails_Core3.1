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

    }
}
