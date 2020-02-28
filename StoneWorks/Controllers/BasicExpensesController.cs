using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoneWorks.Data;
using StoneWorks.Models;

namespace StoneWorks.Controllers
{
    public class BasicExpensesController : Controller
    {
        private readonly StoneWorksContext _context;

        public BasicExpensesController(StoneWorksContext context)
        {
            _context = context;
        }

        // GET: BasicExpenses
        public async Task<IActionResult> Index()
        {
            return View(await _context.BasicExpenses.ToListAsync());
        }

        // GET: BasicExpenses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var basicExpense = await _context.BasicExpenses
                .FirstOrDefaultAsync(m => m.BasicExpenseId == id);
            if (basicExpense == null)
            {
                return NotFound();
            }

            return View(basicExpense);
        }

        // GET: BasicExpenses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BasicExpenses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BasicExpenseId,OnDate,Particular,PaidBy,PaidTo,Amount,Remarks")] BasicExpense basicExpense)
        {
            if (ModelState.IsValid)
            {
                _context.Add(basicExpense);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(basicExpense);
        }

        // GET: BasicExpenses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var basicExpense = await _context.BasicExpenses.FindAsync(id);
            if (basicExpense == null)
            {
                return NotFound();
            }
            return View(basicExpense);
        }

        // POST: BasicExpenses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BasicExpenseId,OnDate,Particular,PaidBy,PaidTo,Amount,Remarks")] BasicExpense basicExpense)
        {
            if (id != basicExpense.BasicExpenseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(basicExpense);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BasicExpenseExists(basicExpense.BasicExpenseId))
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
            return View(basicExpense);
        }

        // GET: BasicExpenses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var basicExpense = await _context.BasicExpenses
                .FirstOrDefaultAsync(m => m.BasicExpenseId == id);
            if (basicExpense == null)
            {
                return NotFound();
            }

            return View(basicExpense);
        }

        // POST: BasicExpenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var basicExpense = await _context.BasicExpenses.FindAsync(id);
            _context.BasicExpenses.Remove(basicExpense);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BasicExpenseExists(int id)
        {
            return _context.BasicExpenses.Any(e => e.BasicExpenseId == id);
        }
    }
}
