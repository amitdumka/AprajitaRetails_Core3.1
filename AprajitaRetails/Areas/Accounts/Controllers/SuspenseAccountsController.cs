using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AprajitaRetails.Data;
using AprajitaRetails.Models;

namespace AprajitaRetails.Areas.Accounts.Controllers
{
    [Area ("Accounts")]
    [Authorize(Roles ="Admin, PowerUser")]
    public class SuspenseAccountsController : Controller
    {
        private readonly AprajitaRetailsContext _context;

        public SuspenseAccountsController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: SuspenseAccounts
        public async Task<IActionResult> Index(string currentFilter, string searchString, int? pageNumber)
        {
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }


            ViewData["CurrentFilter"] = searchString;
            int pageSize = 10;
            var aprajitaretailscontext = _context.Suspenses.OrderByDescending (c => c.EntryDate);
            return View(await PaginatedList<SuspenseAccount>.CreateAsync(aprajitaretailscontext.AsNoTracking(), pageNumber ?? 1, pageSize));
            //return View(await _context.Suspenses.ToListAsync());
        }

        // GET: SuspenseAccounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suspenseAccount = await _context.Suspenses
                .FirstOrDefaultAsync(m => m.SuspenseAccountId == id);
            if (suspenseAccount == null)
            {
                return NotFound();
            }

           return PartialView(suspenseAccount);
        }

        // GET: SuspenseAccounts/Create
        public IActionResult Create()
        {
           return PartialView();
        }

        // POST: SuspenseAccounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SuspenseAccountId,EntryDate,ReferanceDetails,InAmount,OutAmount,IsCleared,ClearedDetails,ReviewBy")] SuspenseAccount suspenseAccount)
        {
            if (ModelState.IsValid)
            {
                _context.Add(suspenseAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           return PartialView(suspenseAccount);
        }

        // GET: SuspenseAccounts/Edit/5
         [Authorize(Roles = "Admin,PowerUser")] public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suspenseAccount = await _context.Suspenses.FindAsync(id);
            if (suspenseAccount == null)
            {
                return NotFound();
            }
           return PartialView(suspenseAccount);
        }

        // POST: SuspenseAccounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
       [Authorize(Roles = "Admin,PowerUser")]     public async Task<IActionResult> Edit(int id, [Bind("SuspenseAccountId,EntryDate,ReferanceDetails,InAmount,OutAmount,IsCleared,ClearedDetails,ReviewBy")] SuspenseAccount suspenseAccount)
        {
            if (id != suspenseAccount.SuspenseAccountId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(suspenseAccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SuspenseAccountExists(suspenseAccount.SuspenseAccountId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
           return PartialView(suspenseAccount);
        }

        // GET: SuspenseAccounts/Delete/5
         [Authorize (Roles = "Admin,PowerUser")]   public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suspenseAccount = await _context.Suspenses
                .FirstOrDefaultAsync(m => m.SuspenseAccountId == id);
            if (suspenseAccount == null)
            {
                return NotFound();
            }

           return PartialView(suspenseAccount);
        }

        // POST: SuspenseAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
         [Authorize (Roles = "Admin,PowerUser")]   public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var suspenseAccount = await _context.Suspenses.FindAsync(id);
            _context.Suspenses.Remove(suspenseAccount);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SuspenseAccountExists(int id)
        {
            return _context.Suspenses.Any(e => e.SuspenseAccountId == id);
        }
    }
}
