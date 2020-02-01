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
    [Area ("Banking")]
    public class BankWithdrawalsController : Controller
    {
        private readonly AprajitaRetailsContext _context;

        public BankWithdrawalsController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: BankWithdrawals
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

            var aprajitaRetailsContext = _context.BankWithdrawals.Include(b => b.Account).OrderByDescending (c => c.DepoDate);
            return View(await PaginatedList<BankWithdrawal>.CreateAsync(aprajitaRetailsContext.AsNoTracking(), pageNumber ?? 1, pageSize));
            //return View(await aprajitaRetailsContext.ToListAsync());
        }

        // GET: BankWithdrawals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankWithdrawal = await _context.BankWithdrawals
                .Include(b => b.Account)
                .FirstOrDefaultAsync(m => m.BankWithdrawalId == id);
            if (bankWithdrawal == null)
            {
                return NotFound();
            }

           return PartialView(bankWithdrawal);
        }

        // GET: BankWithdrawals/Create
        public IActionResult Create()
        {
            ViewData["AccountNumberId"] = new SelectList(_context.AccountNumbers, "AccountNumberId", "Account");
           return PartialView();
        }

        // POST: BankWithdrawals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BankWithdrawalId,DepoDate,AccountNumberId,Amount,ChequeNo,SignedBy,ApprovedBy,InNameOf")] BankWithdrawal bankWithdrawal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bankWithdrawal);
                new BankingManager().OnInsert(_context, bankWithdrawal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountNumberId"] = new SelectList(_context.AccountNumbers, "AccountNumberId", "Account", bankWithdrawal.AccountNumberId);
           return PartialView(bankWithdrawal);
        }

        // GET: BankWithdrawals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankWithdrawal = await _context.BankWithdrawals.FindAsync(id);
            if (bankWithdrawal == null)
            {
                return NotFound();
            }
            ViewData["AccountNumberId"] = new SelectList(_context.AccountNumbers, "AccountNumberId", "Account", bankWithdrawal.AccountNumberId);
           return PartialView(bankWithdrawal);
        }

        // POST: BankWithdrawals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BankWithdrawalId,DepoDate,AccountNumberId,Amount,ChequeNo,SignedBy,ApprovedBy,InNameOf")] BankWithdrawal bankWithdrawal)
        {
            if (id != bankWithdrawal.BankWithdrawalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    new BankingManager().OnUpdate(_context, bankWithdrawal);
                    _context.Update(bankWithdrawal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BankWithdrawalExists(bankWithdrawal.BankWithdrawalId))
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
            ViewData["AccountNumberId"] = new SelectList(_context.AccountNumbers, "AccountNumberId", "Account", bankWithdrawal.AccountNumberId);
           return PartialView(bankWithdrawal);
        }

        // GET: BankWithdrawals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankWithdrawal = await _context.BankWithdrawals
                .Include(b => b.Account)
                .FirstOrDefaultAsync(m => m.BankWithdrawalId == id);
            if (bankWithdrawal == null)
            {
                return NotFound();
            }

           return PartialView(bankWithdrawal);
        }

        // POST: BankWithdrawals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bankWithdrawal = await _context.BankWithdrawals.FindAsync(id);
            new BankingManager().OnDelete(_context, bankWithdrawal);
            _context.BankWithdrawals.Remove(bankWithdrawal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BankWithdrawalExists(int id)
        {
            return _context.BankWithdrawals.Any(e => e.BankWithdrawalId == id);
        }
    }
}
