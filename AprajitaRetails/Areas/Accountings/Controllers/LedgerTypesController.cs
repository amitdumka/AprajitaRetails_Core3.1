using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AprajitaRetails.Areas.Accountings.Models;
using AprajitaRetails.Data;
using Microsoft.AspNetCore.Authorization;

namespace AprajitaRetails.Areas.Accountings.Controllers
{
    [Area("Accountings")]
    [Authorize (Roles = "Admin,PowerUser,StoreManager")]
    public class LedgerTypesController : Controller
    {
        private readonly AprajitaRetailsContext _context;

        public LedgerTypesController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: Accountings/LedgerTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.LedgerTypes.ToListAsync());
        }

        // GET: Accountings/LedgerTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ledgerType = await _context.LedgerTypes
                .FirstOrDefaultAsync(m => m.LedgerTypeId == id);
            if (ledgerType == null)
            {
                return NotFound();
            }

            return View(ledgerType);
        }

        // GET: Accountings/LedgerTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Accountings/LedgerTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LedgerTypeId,LedgerNameType,Category,Remark")] LedgerType ledgerType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ledgerType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ledgerType);
        }

        // GET: Accountings/LedgerTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ledgerType = await _context.LedgerTypes.FindAsync(id);
            if (ledgerType == null)
            {
                return NotFound();
            }
            return View(ledgerType);
        }

        // POST: Accountings/LedgerTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LedgerTypeId,LedgerNameType,Category,Remark")] LedgerType ledgerType)
        {
            if (id != ledgerType.LedgerTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ledgerType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LedgerTypeExists(ledgerType.LedgerTypeId))
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
            return View(ledgerType);
        }

        // GET: Accountings/LedgerTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ledgerType = await _context.LedgerTypes
                .FirstOrDefaultAsync(m => m.LedgerTypeId == id);
            if (ledgerType == null)
            {
                return NotFound();
            }

            return View(ledgerType);
        }

        // POST: Accountings/LedgerTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ledgerType = await _context.LedgerTypes.FindAsync(id);
            _context.LedgerTypes.Remove(ledgerType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LedgerTypeExists(int id)
        {
            return _context.LedgerTypes.Any(e => e.LedgerTypeId == id);
        }
    }
}
