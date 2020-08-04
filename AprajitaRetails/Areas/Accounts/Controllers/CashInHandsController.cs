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
    public class CashInHandsController : Controller
    {
        private readonly AprajitaRetailsContext _context;
        private readonly int StoreCode = 1;

        public CashInHandsController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: CashInHands
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
            var aprajitaRetailsContext = _context.CashInHands.Where(c=>c.StoreId==StoreCode).OrderByDescending (c => c.CIHDate).OrderByDescending (c => c.CIHDate);
            return View (await PaginatedList<CashInHand>.CreateAsync (aprajitaRetailsContext.AsNoTracking (), pageNumber ?? 1, pageSize));
            //return View(await _context.CashInHands.ToListAsync());
        }

        // GET: CashInHands/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if ( id == null )
            {
                return NotFound ();
            }

            var cashInHand = await _context.CashInHands
                .FirstOrDefaultAsync (m => m.CashInHandId == id);
            if ( cashInHand == null )
            {
                return NotFound ();
            }

            return PartialView (cashInHand);
        }

        // GET: CashInHands/Create
        public IActionResult Create()
        {
            return PartialView ();
        }

        // POST: CashInHands/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind ("CashInHandId,CIHDate,OpenningBalance,ClosingBalance,CashIn,CashOut")] CashInHand cashInHand)
        {
            if ( ModelState.IsValid )
            {
                cashInHand.StoreId = StoreCode;
                _context.Add (cashInHand);
                await _context.SaveChangesAsync ();
                return RedirectToAction (nameof (Index));
            }
            return PartialView (cashInHand);
        }

        // GET: CashInHands/Edit/5
        [Authorize (Roles = "Admin,PowerUser")]
        public async Task<IActionResult> Edit(int? id)
        {
            if ( id == null )
            {
                return NotFound ();
            }

            var cashInHand = await _context.CashInHands.FindAsync (id);
            if ( cashInHand == null )
            {
                return NotFound ();
            }
            return PartialView (cashInHand);
        }

        // POST: CashInHands/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize (Roles = "Admin,PowerUser")]
        public async Task<IActionResult> Edit(int id, [Bind ("CashInHandId,CIHDate,OpenningBalance,ClosingBalance,CashIn,CashOut")] CashInHand cashInHand)
        {
            if ( id != cashInHand.CashInHandId )
            {
                return NotFound ();
            }

            if ( ModelState.IsValid )
            {
                try
                {
                    _context.Update (cashInHand);
                    await _context.SaveChangesAsync ();
                }
                catch ( DbUpdateConcurrencyException )
                {
                    if ( !CashInHandExists (cashInHand.CashInHandId) )
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
            return PartialView (cashInHand);
        }

        // GET: CashInHands/Delete/5
        [Authorize (Roles = "Admin,PowerUser")]
        public async Task<IActionResult> Delete(int? id)
        {
            if ( id == null )
            {
                return NotFound ();
            }

            var cashInHand = await _context.CashInHands
                .FirstOrDefaultAsync (m => m.CashInHandId == id);
            if ( cashInHand == null )
            {
                return NotFound ();
            }

            return PartialView (cashInHand);
        }

        // POST: CashInHands/Delete/5
        [HttpPost, ActionName ("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize (Roles = "Admin,PowerUser")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cashInHand = await _context.CashInHands.FindAsync (id);
            _context.CashInHands.Remove (cashInHand);
            await _context.SaveChangesAsync ();
            return RedirectToAction (nameof (Index));
        }

        private bool CashInHandExists(int id)
        {
            return _context.CashInHands.Any (e => e.CashInHandId == id);
        }
    }
}