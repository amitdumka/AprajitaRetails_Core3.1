using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace AprajitaRetails.Data
{
    public class VoyagerContext : IdentityDbContext
    {
        public VoyagerContext(DbContextOptions<VoyagerContext> options)
            : base(options)
        {
        }
    }

}
