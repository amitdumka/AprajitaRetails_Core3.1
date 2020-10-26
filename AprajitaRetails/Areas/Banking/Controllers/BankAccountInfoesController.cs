using AprajitaRetails.Data;
using AprajitaRetails.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AprajitaRetails.Areas.Banking.Controllers
{
    [Area("Banking")]
    [Authorize(Roles = "Admin,PowerUser")]
    public class BankAccountInfoesController : Controller
    {
        private readonly AprajitaRetailsContext _context;

        public BankAccountInfoesController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: Banking/BankAccountInfoes
        public async Task<IActionResult> Index()
        {
            var aprajitaRetailsContext = _context.BankAccountInfos.Include(b => b.Bank);
            return View(await aprajitaRetailsContext.ToListAsync());
        }

        // GET: Banking/BankAccountInfoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankAccountInfo = await _context.BankAccountInfos
                .Include(b => b.Bank)
                .FirstOrDefaultAsync(m => m.BankAccountInfoId == id);
            if (bankAccountInfo == null)
            {
                return NotFound();
            }

            return View(bankAccountInfo);
        }

        // GET: Banking/BankAccountInfoes/Create
        public IActionResult Create()
        {
            ViewData["BankId"] = new SelectList(_context.Banks, "BankId", "BankName");
            return View();
        }

        // POST: Banking/BankAccountInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BankAccountInfoId,AccountHolder,AccountNumber,BankId,BranchName,IFSCCode,AccountType,IsClientAccount")] BankAccountInfo bankAccountInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bankAccountInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BankId"] = new SelectList(_context.Banks, "BankId", "BankName", bankAccountInfo.BankId);
            return View(bankAccountInfo);
        }

        // GET: Banking/BankAccountInfoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankAccountInfo = await _context.BankAccountInfos.FindAsync(id);
            if (bankAccountInfo == null)
            {
                return NotFound();
            }
            ViewData["BankId"] = new SelectList(_context.Banks, "BankId", "BankName", bankAccountInfo.BankId);
            return View(bankAccountInfo);
        }

        // POST: Banking/BankAccountInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BankAccountInfoId,AccountHolder,AccountNumber,BankId,BranchName,IFSCCode,AccountType,IsClientAccount")] BankAccountInfo bankAccountInfo)
        {
            if (id != bankAccountInfo.BankAccountInfoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bankAccountInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BankAccountInfoExists(bankAccountInfo.BankAccountInfoId))
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
            ViewData["BankId"] = new SelectList(_context.Banks, "BankId", "BankName", bankAccountInfo.BankId);
            return View(bankAccountInfo);
        }

        // GET: Banking/BankAccountInfoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankAccountInfo = await _context.BankAccountInfos
                .Include(b => b.Bank)
                .FirstOrDefaultAsync(m => m.BankAccountInfoId == id);
            if (bankAccountInfo == null)
            {
                return NotFound();
            }

            return View(bankAccountInfo);
        }

        // POST: Banking/BankAccountInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bankAccountInfo = await _context.BankAccountInfos.FindAsync(id);
            _context.BankAccountInfos.Remove(bankAccountInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BankAccountInfoExists(int id)
        {
            return _context.BankAccountInfos.Any(e => e.BankAccountInfoId == id);
        }
    }
}
