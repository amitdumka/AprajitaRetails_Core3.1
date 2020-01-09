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
    public class CashPaymentsController : Controller
    {
        private readonly AprajitaRetailsContext _context;

        public CashPaymentsController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: CashPayments
        public async Task<IActionResult> Index()
        {
            var aprajitaRetailsContext = _context.CashPayments.Include(c => c.Mode);
            return View(await aprajitaRetailsContext.ToListAsync());
        }

        // GET: CashPayments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cashPayment = await _context.CashPayments
                .Include(c => c.Mode)
                .FirstOrDefaultAsync(m => m.CashPaymentId == id);
            if (cashPayment == null)
            {
                return NotFound();
            }

            return View(cashPayment);
        }

        // GET: CashPayments/Create
        public IActionResult Create()
        {
            ViewData["TranscationModeId"] = new SelectList(_context.TranscationModes, "TranscationModeId", "TranscationModeId");
            return View();
        }

        // POST: CashPayments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CashPaymentId,PaymentDate,TranscationModeId,PaidTo,Amount,SlipNo")] CashPayment cashPayment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cashPayment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TranscationModeId"] = new SelectList(_context.TranscationModes, "TranscationModeId", "TranscationModeId", cashPayment.TranscationModeId);
            return View(cashPayment);
        }

        // GET: CashPayments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cashPayment = await _context.CashPayments.FindAsync(id);
            if (cashPayment == null)
            {
                return NotFound();
            }
            ViewData["TranscationModeId"] = new SelectList(_context.TranscationModes, "TranscationModeId", "TranscationModeId", cashPayment.TranscationModeId);
            return View(cashPayment);
        }

        // POST: CashPayments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CashPaymentId,PaymentDate,TranscationModeId,PaidTo,Amount,SlipNo")] CashPayment cashPayment)
        {
            if (id != cashPayment.CashPaymentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cashPayment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CashPaymentExists(cashPayment.CashPaymentId))
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
            ViewData["TranscationModeId"] = new SelectList(_context.TranscationModes, "TranscationModeId", "TranscationModeId", cashPayment.TranscationModeId);
            return View(cashPayment);
        }

        // GET: CashPayments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cashPayment = await _context.CashPayments
                .Include(c => c.Mode)
                .FirstOrDefaultAsync(m => m.CashPaymentId == id);
            if (cashPayment == null)
            {
                return NotFound();
            }

            return View(cashPayment);
        }

        // POST: CashPayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cashPayment = await _context.CashPayments.FindAsync(id);
            _context.CashPayments.Remove(cashPayment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CashPaymentExists(int id)
        {
            return _context.CashPayments.Any(e => e.CashPaymentId == id);
        }
    }
}
