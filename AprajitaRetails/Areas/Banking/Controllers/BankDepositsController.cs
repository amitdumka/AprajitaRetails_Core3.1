using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AprajitaRetails.Data;
using AprajitaRetails.Models;
using AprajitaRetails.Areas.Banking.Models;

namespace AprajitaRetails.Areas.Banking.Controllers
{
    [Area("Banking")]
    public class BankDepositsController : Controller
    {
        private readonly AprajitaRetailsContext _context;

        public BankDepositsController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: BankDeposits
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

            var aprajitaRetailsContext = _context.BankDeposits.Include(b => b.Account);
            // return View(await aprajitaRetailsContext.ToListAsync());
            return View(await PaginatedList<BankDeposit>.CreateAsync(aprajitaRetailsContext.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: BankDeposits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankDeposit = await _context.BankDeposits
                .Include(b => b.Account)
                .FirstOrDefaultAsync(m => m.BankDepositId == id);
            if (bankDeposit == null)
            {
                return NotFound();
            }

            return PartialView(bankDeposit);
        }

        // GET: BankDeposits/Create
        public IActionResult Create()
        {
            ViewData["AccountNumberId"] = new SelectList(_context.AccountNumbers, "AccountNumberId", "Account");
            return PartialView();
        }

        // POST: BankDeposits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BankDepositId,DepoDate,AccountNumberId,Amount,PayMode,Details,Remarks")] BankDeposit bankDeposit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bankDeposit);
                new BankingManager().OnInsert(_context, bankDeposit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountNumberId"] = new SelectList(_context.AccountNumbers, "AccountNumberId", "Account", bankDeposit.AccountNumberId);
            return PartialView(bankDeposit);
        }

        // GET: BankDeposits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankDeposit = await _context.BankDeposits.FindAsync(id);
            if (bankDeposit == null)
            {
                return NotFound();
            }
            ViewData["AccountNumberId"] = new SelectList(_context.AccountNumbers, "AccountNumberId", "Account", bankDeposit.AccountNumberId);
            return PartialView(bankDeposit);
        }

        // POST: BankDeposits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BankDepositId,DepoDate,AccountNumberId,Amount,PayMode,Details,Remarks")] BankDeposit bankDeposit)
        {
            if (id != bankDeposit.BankDepositId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    new BankingManager().OnUpdate(_context, bankDeposit);
                    _context.Update(bankDeposit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BankDepositExists(bankDeposit.BankDepositId))
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
            ViewData["AccountNumberId"] = new SelectList(_context.AccountNumbers, "AccountNumberId", "Account", bankDeposit.AccountNumberId);
            return PartialView(bankDeposit);
        }

        // GET: BankDeposits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankDeposit = await _context.BankDeposits
                .Include(b => b.Account)
                .FirstOrDefaultAsync(m => m.BankDepositId == id);
            if (bankDeposit == null)
            {
                return NotFound();
            }

            return PartialView(bankDeposit);
        }

        // POST: BankDeposits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bankDeposit = await _context.BankDeposits.FindAsync(id);
            new BankingManager().OnDelete(_context, bankDeposit);
            _context.BankDeposits.Remove(bankDeposit);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BankDepositExists(int id)
        {
            return _context.BankDeposits.Any(e => e.BankDepositId == id);
        }
    }
}
