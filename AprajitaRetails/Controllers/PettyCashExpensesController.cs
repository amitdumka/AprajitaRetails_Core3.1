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
    public class PettyCashExpensesController : Controller
    {
        private readonly AprajitaRetailsContext _context;

        public PettyCashExpensesController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: PettyCashExpenses
        public async Task<IActionResult> Index()
        {
            var aprajitaRetailsContext = _context.CashExpenses.Include(p => p.PaidBy);
            return View(await aprajitaRetailsContext.ToListAsync());
        }

        // GET: PettyCashExpenses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pettyCashExpense = await _context.CashExpenses
                .Include(p => p.PaidBy)
                .FirstOrDefaultAsync(m => m.PettyCashExpenseId == id);
            if (pettyCashExpense == null)
            {
                return NotFound();
            }

            return View(pettyCashExpense);
        }

        // GET: PettyCashExpenses/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId");
            return View();
        }

        // POST: PettyCashExpenses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PettyCashExpenseId,ExpDate,Particulars,Amount,EmployeeId,PaidTo,Remarks")] PettyCashExpense pettyCashExpense)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pettyCashExpense);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", pettyCashExpense.EmployeeId);
            return View(pettyCashExpense);
        }

        // GET: PettyCashExpenses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pettyCashExpense = await _context.CashExpenses.FindAsync(id);
            if (pettyCashExpense == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", pettyCashExpense.EmployeeId);
            return View(pettyCashExpense);
        }

        // POST: PettyCashExpenses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PettyCashExpenseId,ExpDate,Particulars,Amount,EmployeeId,PaidTo,Remarks")] PettyCashExpense pettyCashExpense)
        {
            if (id != pettyCashExpense.PettyCashExpenseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pettyCashExpense);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PettyCashExpenseExists(pettyCashExpense.PettyCashExpenseId))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", pettyCashExpense.EmployeeId);
            return View(pettyCashExpense);
        }

        // GET: PettyCashExpenses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pettyCashExpense = await _context.CashExpenses
                .Include(p => p.PaidBy)
                .FirstOrDefaultAsync(m => m.PettyCashExpenseId == id);
            if (pettyCashExpense == null)
            {
                return NotFound();
            }

            return View(pettyCashExpense);
        }

        // POST: PettyCashExpenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pettyCashExpense = await _context.CashExpenses.FindAsync(id);
            _context.CashExpenses.Remove(pettyCashExpense);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PettyCashExpenseExists(int id)
        {
            return _context.CashExpenses.Any(e => e.PettyCashExpenseId == id);
        }
    }
}
