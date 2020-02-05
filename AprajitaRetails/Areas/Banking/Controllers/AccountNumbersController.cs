using Microsoft.AspNetCore.Authorization;    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AprajitaRetails.Data;
using AprajitaRetails.Models;
using Microsoft.AspNetCore.Authorization;

namespace AprajitaRetails.Areas.Banking.Controllers
{
    [Area("Banking")]
    [Authorize(Roles = "Admin,PowerUser")]
    public class AccountNumbersController : Controller
    {
        private readonly AprajitaRetailsContext _context;

        public AccountNumbersController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: AccountNumbers
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

            var aprajitaRetailsContext = _context.AccountNumbers.Include(a => a.Bank);
            // return View(await aprajitaRetailsContext.ToListAsync());
            return View(await PaginatedList<AccountNumber>.CreateAsync(aprajitaRetailsContext.AsNoTracking(), pageNumber ?? 1, pageSize));
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

            return PartialView(accountNumber);
        }

        // GET: AccountNumbers/Create
        public IActionResult Create()
        {
            ViewData["BankId"] = new SelectList(_context.Banks, "BankId", "BankName");
            return PartialView();
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
            ViewData["BankId"] = new SelectList(_context.Banks, "BankId", "BankName", accountNumber.BankId);
            return PartialView(accountNumber);
        }

        // GET: AccountNumbers/Edit/5
         [Authorize(Roles = "Admin,PowerUser")] public async Task<IActionResult> Edit(int? id)
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
            ViewData["BankId"] = new SelectList(_context.Banks, "BankId", "BankName", accountNumber.BankId);
            return PartialView(accountNumber);
        }

        // POST: AccountNumbers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
       [Authorize(Roles = "Admin,PowerUser")]     public async Task<IActionResult> Edit(int id, [Bind("AccountNumberId,BankId,Account")] AccountNumber accountNumber)
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
            ViewData["BankId"] = new SelectList(_context.Banks, "BankId", "BankName", accountNumber.BankId);
            return PartialView(accountNumber);
        }

        // GET: AccountNumbers/Delete/5
         [Authorize (Roles = "Admin,PowerUser")]   public async Task<IActionResult> Delete(int? id)
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

            return PartialView(accountNumber);
        }

        // POST: AccountNumbers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
         [Authorize (Roles = "Admin,PowerUser")]   public async Task<IActionResult> DeleteConfirmed(int id)
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
