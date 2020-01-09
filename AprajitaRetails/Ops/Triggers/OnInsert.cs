using AprajitaRetails.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AprajitaRetails.Ops.Triggers
{
    public class OnInsert { }
    public class OnUpdate { }
    public class OnDelete { }

    public class TailoringWork
    {
        public static void UpdateDelivery(AprajitaRetailsContext db)
        {
            var tab = db.Deliveries.Include(c => c.Booking).OrderBy(c => c.TalioringBookingId);
            if (tab != null)
            {
                foreach (var item in tab)
                {
                    item.Booking.IsDelivered = true;
                    db.Entry(item).State = EntityState.Modified;
                }
                db.SaveChanges();
            }
        }
    }
}
