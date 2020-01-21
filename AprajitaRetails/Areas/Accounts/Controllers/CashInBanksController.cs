using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AprajitaRetails.Data;
using AprajitaRetails.Models;

namespace AprajitaRetails.Areas.Accounts.Controllers
{
    [Area ("Accounts")]
    public class CashInBanksController : Controller
    {
        private readonly AprajitaRetailsContext _context;

        public CashInBanksController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: CashInBanks
        public async Task<IActionResult> Index()
        {
           return View(await _context.CashInBanks.ToListAsync());
        }

        // GET: CashInBanks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cashInBank = await _context.CashInBanks
                .FirstOrDefaultAsync(m => m.CashInBankId == id);
            if (cashInBank == null)
            {
                return NotFound();
            }

           return PartialView(cashInBank);
        }

        // GET: CashInBanks/Create
        public IActionResult Create()
        {
           return PartialView();
        }

        // POST: CashInBanks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CashInBankId,CIBDate,OpenningBalance,ClosingBalance,CashIn,CashOut")] CashInBank cashInBank)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cashInBank);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           return PartialView(cashInBank);
        }

        // GET: CashInBanks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cashInBank = await _context.CashInBanks.FindAsync(id);
            if (cashInBank == null)
            {
                return NotFound();
            }
           return PartialView(cashInBank);
        }

        // POST: CashInBanks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CashInBankId,CIBDate,OpenningBalance,ClosingBalance,CashIn,CashOut")] CashInBank cashInBank)
        {
            if (id != cashInBank.CashInBankId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cashInBank);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CashInBankExists(cashInBank.CashInBankId))
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
           return PartialView(cashInBank);
        }

        // GET: CashInBanks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cashInBank = await _context.CashInBanks
                .FirstOrDefaultAsync(m => m.CashInBankId == id);
            if (cashInBank == null)
            {
                return NotFound();
            }

           return PartialView(cashInBank);
        }

        // POST: CashInBanks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cashInBank = await _context.CashInBanks.FindAsync(id);
            _context.CashInBanks.Remove(cashInBank);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CashInBankExists(int id)
        {
            return _context.CashInBanks.Any(e => e.CashInBankId == id);
        }
    }
}
