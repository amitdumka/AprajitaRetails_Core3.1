using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AprajitaRetails.Data;
using AprajitaRetails.Models;

namespace AprajitaRetails.Sales.Expenses.Controllers
{
    [Area ("Sales")]
    public class DailySalesController : Controller
    {
        private readonly AprajitaRetailsContext _context;

        public DailySalesController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: DailySales
        public async Task<IActionResult> Index()
        {
            var aprajitaRetailsContext = _context.DailySales.Include(d => d.Salesman);
            return View(await aprajitaRetailsContext.ToListAsync());
        }

        // GET: DailySales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dailySale = await _context.DailySales
                .Include(d => d.Salesman)
                .FirstOrDefaultAsync(m => m.DailySaleId == id);
            if (dailySale == null)
            {
                return NotFound();
            }

            return View(dailySale);
        }

        // GET: DailySales/Create
        public IActionResult Create()
        {
            ViewData["SalesmanId"] = new SelectList(_context.Salesmen, "SalesmanId", "SalesmanId");
            return View();
        }

        // POST: DailySales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DailySaleId,SaleDate,InvNo,Amount,PayMode,CashAmount,SalesmanId,IsDue,IsManualBill,IsTailoringBill,IsSaleReturn,Remarks")] DailySale dailySale)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dailySale);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SalesmanId"] = new SelectList(_context.Salesmen, "SalesmanId", "SalesmanId", dailySale.SalesmanId);
            return View(dailySale);
        }

        // GET: DailySales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dailySale = await _context.DailySales.FindAsync(id);
            if (dailySale == null)
            {
                return NotFound();
            }
            ViewData["SalesmanId"] = new SelectList(_context.Salesmen, "SalesmanId", "SalesmanId", dailySale.SalesmanId);
            return View(dailySale);
        }

        // POST: DailySales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DailySaleId,SaleDate,InvNo,Amount,PayMode,CashAmount,SalesmanId,IsDue,IsManualBill,IsTailoringBill,IsSaleReturn,Remarks")] DailySale dailySale)
        {
            if (id != dailySale.DailySaleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dailySale);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DailySaleExists(dailySale.DailySaleId))
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
            ViewData["SalesmanId"] = new SelectList(_context.Salesmen, "SalesmanId", "SalesmanId", dailySale.SalesmanId);
            return View(dailySale);
        }

        // GET: DailySales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dailySale = await _context.DailySales
                .Include(d => d.Salesman)
                .FirstOrDefaultAsync(m => m.DailySaleId == id);
            if (dailySale == null)
            {
                return NotFound();
            }

            return View(dailySale);
        }

        // POST: DailySales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dailySale = await _context.DailySales.FindAsync(id);
            _context.DailySales.Remove(dailySale);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DailySaleExists(int id)
        {
            return _context.DailySales.Any(e => e.DailySaleId == id);
        }
    }
}
