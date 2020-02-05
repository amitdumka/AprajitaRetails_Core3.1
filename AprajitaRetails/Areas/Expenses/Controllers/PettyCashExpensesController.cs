using Microsoft.AspNetCore.Authorization;    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AprajitaRetails.Data;
using AprajitaRetails.Models;
using AprajitaRetails.Areas.Expenses.Models;

namespace AprajitaRetails.Areas.Expenses.Controllers
{
    [Area ("Expenses")]
    public class PettyCashExpensesController : Controller
    {
        private readonly AprajitaRetailsContext _context;

        public PettyCashExpensesController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: PettyCashExpenses
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
            var aprajitaRetailsContext = _context.PettyCashExpenses.Include(p => p.PaidBy).OrderByDescending(c=>c.ExpDate);

            int pageSize = 10;
           return View(await PaginatedList<PettyCashExpense>.CreateAsync(aprajitaRetailsContext.AsNoTracking(), pageNumber ?? 1, pageSize));

            //return View(await aprajitaRetailsContext.ToListAsync());
        }

        // GET: PettyCashExpenses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pettyCashExpense = await _context.PettyCashExpenses
                .Include(p => p.PaidBy)
                .FirstOrDefaultAsync(m => m.PettyCashExpenseId == id);
            if (pettyCashExpense == null)
            {
                return NotFound();
            }

            return PartialView(pettyCashExpense);
        }

        // GET: PettyCashExpenses/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "StaffName");
            return PartialView();
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
                new ExpenseManager().OnInsert(_context, pettyCashExpense);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "StaffName", pettyCashExpense.EmployeeId);
           return PartialView(pettyCashExpense);
        }

        // GET: PettyCashExpenses/Edit/5
         [Authorize(Roles = "Admin,PowerUser")] public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pettyCashExpense = await _context.PettyCashExpenses.FindAsync(id);
            if (pettyCashExpense == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "StaffName", pettyCashExpense.EmployeeId);
           return PartialView(pettyCashExpense);
        }

        // POST: PettyCashExpenses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
       [Authorize(Roles = "Admin,PowerUser")]     public async Task<IActionResult> Edit(int id, [Bind("PettyCashExpenseId,ExpDate,Particulars,Amount,EmployeeId,PaidTo,Remarks")] PettyCashExpense pettyCashExpense)
        {
            if (id != pettyCashExpense.PettyCashExpenseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    new ExpenseManager().OnUpdate(_context, pettyCashExpense);
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "StaffName", pettyCashExpense.EmployeeId);
           return PartialView(pettyCashExpense);
        }

        // GET: PettyCashExpenses/Delete/5
         [Authorize (Roles = "Admin,PowerUser")]   public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pettyCashExpense = await _context.PettyCashExpenses
                .Include(p => p.PaidBy)
                .FirstOrDefaultAsync(m => m.PettyCashExpenseId == id);
            if (pettyCashExpense == null)
            {
                return NotFound();
            }

           return PartialView(pettyCashExpense);
        }

        // POST: PettyCashExpenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
         [Authorize (Roles = "Admin,PowerUser")]   public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pettyCashExpense = await _context.PettyCashExpenses.FindAsync(id);
            new ExpenseManager().OnDelete(_context, pettyCashExpense);
            _context.PettyCashExpenses.Remove(pettyCashExpense);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PettyCashExpenseExists(int id)
        {
            return _context.PettyCashExpenses.Any(e => e.PettyCashExpenseId == id);
        }
    }
}
