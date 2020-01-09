using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AprajitaRetails.Data;
using AprajitaRetails.Models;

namespace AprajitaRetails.Controllers
{
    public class AccountNumbersController : Controller
    {
        private readonly AprajitaRetailsContext _context;

        public AccountNumbersController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: AccountNumbers
        public async Task<IActionResult> Index()
        {
            var aprajitaRetailsContext = _context.AccountNumbers.Include(a => a.Bank);
            return View(await aprajitaRetailsContext.ToListAsync());
        }

        // GET: AccountNumbers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountNumber = await _context.AccountNumbers
                .Include(a => a.Bank)
                .FirstOrDefaultAsync(m => m.AccountNumberId == id);
            if (accountNumber == null)
            {
                return NotFound();
            }

            return View(accountNumber);
        }

        // GET: AccountNumbers/Create
        public IActionResult Create()
        {
            ViewData["BankId"] = new SelectList(_context.Banks, "BankId", "BankId");
            return View();
        }

        // POST: AccountNumbers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AccountNumberId,BankId,Account")] AccountNumber accountNumber)
        {
            if (ModelState.IsValid)
            {
                _context.Add(accountNumber);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BankId"] = new SelectList(_context.Banks, "BankId", "BankId", accountNumber.BankId);
            return View(accountNumber);
        }

        // GET: AccountNumbers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountNumber = await _context.AccountNumbers.FindAsync(id);
            if (accountNumber == null)
            {
                return NotFound();
            }
            ViewData["BankId"] = new SelectList(_context.Banks, "BankId", "BankId", accountNumber.BankId);
            return View(accountNumber);
        }

        // POST: AccountNumbers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AccountNumberId,BankId,Account")] AccountNumber accountNumber)
        {
            if (id != accountNumber.AccountNumberId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accountNumber);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountNumberExists(accountNumber.AccountNumberId))
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
            ViewData["BankId"] = new SelectList(_context.Banks, "BankId", "BankId", accountNumber.BankId);
            return View(accountNumber);
        }

        // GET: AccountNumbers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountNumber = await _context.AccountNumbers
                .Include(a => a.Bank)
                .FirstOrDefaultAsync(m => m.AccountNumberId == id);
            if (accountNumber == null)
            {
                return NotFound();
            }

            return View(accountNumber);
        }

        // POST: AccountNumbers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var accountNumber = await _context.AccountNumbers.FindAsync(id);
            _context.AccountNumbers.Remove(accountNumber);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountNumberExists(int id)
        {
            return _context.AccountNumbers.Any(e => e.AccountNumberId == id);
        }
    }
}
