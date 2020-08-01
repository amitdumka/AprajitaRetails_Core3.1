using System.Linq;
using System.Threading.Tasks;
using AprajitaRetails.Data;
using AprajitaRetails.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AprajitaRetails.Areas.Accounts.Controllers
{
    [Area ("Accounts")]
    [Authorize (Roles = "Admin,PowerUser")]
    public class CashInBanksController : Controller
    {
        private readonly AprajitaRetailsContext _context;
        private readonly int StoreCode = 1;

        public CashInBanksController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: CashInBanks
        public async Task<IActionResult> Index(string currentFilter, string searchString, int? pageNumber)
        {
            if ( searchString != null )
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData ["CurrentFilter"] = searchString;
            int pageSize = 10;
            var aprajitaRetailsContext = _context.CashInBanks.Where(c=>c.StoreLocationId==this.StoreCode).OrderByDescending (c => c.CIBDate).OrderByDescending (c => c.CIBDate);
            return View (await PaginatedList<CashInBank>.CreateAsync (aprajitaRetailsContext.AsNoTracking (), pageNumber ?? 1, pageSize));
            //return View(await _context.CashInBanks.ToListAsync());
        }

        // GET: CashInBanks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if ( id == null )
            {
                return NotFound ();
            }

            var cashInBank = await _context.CashInBanks
                .FirstOrDefaultAsync (m => m.CashInBankId == id);
            if ( cashInBank == null )
            {
                return NotFound ();
            }

            return PartialView (cashInBank);
        }

        // GET: CashInBanks/Create
        public IActionResult Create()
        {
            return PartialView ();
        }

        // POST: CashInBanks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind ("CashInBankId,CIBDate,OpenningBalance,ClosingBalance,CashIn,CashOut")] CashInBank cashInBank)
        {
            if ( ModelState.IsValid )
            {
                cashInBank.StoreLocationId =  this.StoreCode;
                _context.Add (cashInBank);
                await _context.SaveChangesAsync ();
                return RedirectToAction (nameof (Index));
            }
            return PartialView (cashInBank);
        }

        // GET: CashInBanks/Edit/5
        [Authorize (Roles = "Admin,PowerUser")]
        public async Task<IActionResult> Edit(int? id)
        {
            if ( id == null )
            {
                return NotFound ();
            }

            var cashInBank = await _context.CashInBanks.FindAsync (id);
            if ( cashInBank == null )
            {
                return NotFound ();
            }
            return PartialView (cashInBank);
        }

        // POST: CashInBanks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize (Roles = "Admin,PowerUser")]
        public async Task<IActionResult> Edit(int id, [Bind ("CashInBankId,CIBDate,OpenningBalance,ClosingBalance,CashIn,CashOut")] CashInBank cashInBank)
        {
            if ( id != cashInBank.CashInBankId )
            {
                return NotFound ();
            }

            if ( ModelState.IsValid )
            {
                try
                {
                    _context.Update (cashInBank);
                    await _context.SaveChangesAsync ();
                }
                catch ( DbUpdateConcurrencyException )
                {
                    if ( !CashInBankExists (cashInBank.CashInBankId) )
                    {
                        return NotFound ();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction (nameof (Index));
            }
            return PartialView (cashInBank);
        }

        // GET: CashInBanks/Delete/5
        [Authorize (Roles = "Admin,PowerUser")]
        public async Task<IActionResult> Delete(int? id)
        {
            if ( id == null )
            {
                return NotFound ();
            }

            var cashInBank = await _context.CashInBanks
                .FirstOrDefaultAsync (m => m.CashInBankId == id);
            if ( cashInBank == null )
            {
                return NotFound ();
            }

            return PartialView (cashInBank);
        }

        // POST: CashInBanks/Delete/5
        [HttpPost, ActionName ("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize (Roles = "Admin,PowerUser")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cashInBank = await _context.CashInBanks.FindAsync (id);
            _context.CashInBanks.Remove (cashInBank);
            await _context.SaveChangesAsync ();
            return RedirectToAction (nameof (Index));
        }

        private bool CashInBankExists(int id)
        {
            return _context.CashInBanks.Any (e => e.CashInBankId == id);
        }
    }
}