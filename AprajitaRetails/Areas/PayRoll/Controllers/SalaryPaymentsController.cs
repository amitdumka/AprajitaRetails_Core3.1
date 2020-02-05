using Microsoft.AspNetCore.Authorization;    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AprajitaRetails.Data;
using AprajitaRetails.Models;
using AprajitaRetails.Ops.Triggers;

namespace AprajitaRetails.Areas.PayRoll.Controllers
{
    [Area("PayRoll")]
    [Authorize]
    public class SalaryPaymentsController : Controller
    {
        private readonly AprajitaRetailsContext _context;

        public SalaryPaymentsController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: SalaryPayments
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

            var aprajitaRetailsContext = _context.SalaryPayments.Include(s => s.Employee).OrderByDescending (c => c.PaymentDate);

            int pageSize = 10;
           return View(await PaginatedList<SalaryPayment>.CreateAsync(aprajitaRetailsContext.AsNoTracking(), pageNumber ?? 1, pageSize));
            //return View(await aprajitaRetailsContext.ToListAsync());
        }

        // GET: SalaryPayments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salaryPayment = await _context.SalaryPayments
                .Include(s => s.Employee)
                .FirstOrDefaultAsync(m => m.SalaryPaymentId == id);
            if (salaryPayment == null)
            {
                return NotFound();
            }

           return PartialView(salaryPayment);
        }

        // GET: SalaryPayments/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "StaffName");
           return PartialView();
        }

        // POST: SalaryPayments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SalaryPaymentId,EmployeeId,SalaryMonth,SalaryComponet,PaymentDate,Amount,PayMode,Details")] SalaryPayment salaryPayment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salaryPayment);
                new PayRollManager().OnInsert(_context, salaryPayment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "StaffName", salaryPayment.EmployeeId);
           return PartialView(salaryPayment);
        }

        // GET: SalaryPayments/Edit/5
         [Authorize(Roles = "Admin,PowerUser")] public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salaryPayment = await _context.SalaryPayments.FindAsync(id);
            if (salaryPayment == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "StaffName", salaryPayment.EmployeeId);
           return PartialView(salaryPayment);
        }

        // POST: SalaryPayments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
       [Authorize(Roles = "Admin,PowerUser")]     public async Task<IActionResult> Edit(int id, [Bind("SalaryPaymentId,EmployeeId,SalaryMonth,SalaryComponet,PaymentDate,Amount,PayMode,Details")] SalaryPayment salaryPayment)
        {
            if (id != salaryPayment.SalaryPaymentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    new PayRollManager().OnInsert(_context, salaryPayment);
                    _context.Update(salaryPayment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalaryPaymentExists(salaryPayment.SalaryPaymentId))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "StaffName", salaryPayment.EmployeeId);
           return PartialView(salaryPayment);
        }

        // GET: SalaryPayments/Delete/5
         [Authorize (Roles = "Admin,PowerUser")]   public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salaryPayment = await _context.SalaryPayments
                .Include(s => s.Employee)
                .FirstOrDefaultAsync(m => m.SalaryPaymentId == id);
            if (salaryPayment == null)
            {
                return NotFound();
            }

           return PartialView(salaryPayment);
        }

        // POST: SalaryPayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
         [Authorize (Roles = "Admin,PowerUser")]   public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salaryPayment = await _context.SalaryPayments.FindAsync(id);
            new PayRollManager().OnDelete(_context, salaryPayment);
            _context.SalaryPayments.Remove(salaryPayment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalaryPaymentExists(int id)
        {
            return _context.SalaryPayments.Any(e => e.SalaryPaymentId == id);
        }
    }
}
