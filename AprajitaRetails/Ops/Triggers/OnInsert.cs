﻿using AprajitaRetails.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AprajitaRetails.Ops.Triggers
{
    public class OnInsert { }
    public class OnUpdate { }
    public class OnDelete { }

    public static class TailoringWork
    {
        public static void UpdateDelivery(AprajitaRetailsContext db)
        {
            var tab = db.TailoringDeliveries.Include(c => c.Booking).OrderBy(c => c.TalioringBookingId);
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
