using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AprajitaRetails.Extension.Database
{
    public class AprajitaRetailsContext : DbContext
    {
        public AprajitaRetailsContext(DbContextOptions<AprajitaRetailsContext> options) : base(options)
        {
            ApplyMigrations(this);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public void ApplyMigrations(AprajitaRetailsContext context)
        {
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
        }
    }
}