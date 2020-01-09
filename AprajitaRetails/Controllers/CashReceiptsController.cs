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
    public class CashReceiptsController : Controller
    {
        private readonly AprajitaRetailsContext _context;

        public CashReceiptsController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: CashReceipts
        public async Task<IActionResult> Index()
        {
            var aprajitaRetailsContext = _context.CashReceipts.Include(c => c.Mode);
            return View(await aprajitaRetailsContext.ToListAsync());
        }

        // GET: CashReceipts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cashReceipt = await _context.CashReceipts
                .Include(c => c.Mode)
                .FirstOrDefaultAsync(m => m.CashReceiptId == id);
            if (cashReceipt == null)
            {
                return NotFound();
            }

            return View(cashReceipt);
        }

        // GET: CashReceipts/Create
        public IActionResult Create()
        {
            ViewData["TranscationModeId"] = new SelectList(_context.TranscationModes, "TranscationModeId", "TranscationModeId");
            return View();
        }

        // POST: CashReceipts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CashReceiptId,InwardDate,TranscationModeId,ReceiptFrom,Amount,SlipNo")] CashReceipt cashReceipt)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cashReceipt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TranscationModeId"] = new SelectList(_context.TranscationModes, "TranscationModeId", "TranscationModeId", cashReceipt.TranscationModeId);
            return View(cashReceipt);
        }

        // GET: CashReceipts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cashReceipt = await _context.CashReceipts.FindAsync(id);
            if (cashReceipt == null)
            {
                return NotFound();
            }
            ViewData["TranscationModeId"] = new SelectList(_context.TranscationModes, "TranscationModeId", "TranscationModeId", cashReceipt.TranscationModeId);
            return View(cashReceipt);
        }

        // POST: CashReceipts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CashReceiptId,InwardDate,TranscationModeId,ReceiptFrom,Amount,SlipNo")] CashReceipt cashReceipt)
        {
            if (id != cashReceipt.CashReceiptId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cashReceipt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CashReceiptExists(cashReceipt.CashReceiptId))
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
            ViewData["TranscationModeId"] = new SelectList(_context.TranscationModes, "TranscationModeId", "TranscationModeId", cashReceipt.TranscationModeId);
            return View(cashReceipt);
        }

        // GET: CashReceipts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cashReceipt = await _context.CashReceipts
                .Include(c => c.Mode)
                .FirstOrDefaultAsync(m => m.CashReceiptId == id);
            if (cashReceipt == null)
            {
                return NotFound();
            }

            return View(cashReceipt);
        }

        // POST: CashReceipts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cashReceipt = await _context.CashReceipts.FindAsync(id);
            _context.CashReceipts.Remove(cashReceipt);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CashReceiptExists(int id)
        {
            return _context.CashReceipts.Any(e => e.CashReceiptId == id);
        }
    }
}
