using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AprajitaRetails.Areas.Accountings.Models;
using AprajitaRetails.Data;

namespace AprajitaRetails.Areas.Accountings.Ops
{
    public class AccountOperation
    {
        public static int CreateLedgerMaster(AprajitaRetailsContext db, Party party)
        {
            LedgerMaster master = new LedgerMaster {
            CreatingDate=DateTime.Today,PartyId=party.PartyId, LedgerTypeId  =party.LedgerTypeId
            };
            db.Add (master);
           return db.SaveChanges ();

        }

        public static int UpdateLedgerMaster(AprajitaRetailsContext db, Party party)
        {
            var master = db.LedgerMasters.Where (c => c.PartyId == party.PartyId).FirstOrDefault ();
            if ( master != null )
            {
                master.LedgerTypeId = party.LedgerTypeId;
                db.Update (master);
                return db.SaveChanges ();
            }
            else
                return -1;
        }
    }
}
