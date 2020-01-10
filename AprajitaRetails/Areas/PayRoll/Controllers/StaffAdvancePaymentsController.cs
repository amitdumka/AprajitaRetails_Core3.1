using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AprajitaRetails.Data;
using AprajitaRetails.Models;

namespace AprajitaRetails.Areas.PayRoll.Controllers
{
    [Area("PayRoll")]
    public class StaffAdvancePaymentsController : Controller
    {
        private readonly AprajitaRetailsContext _context;

        public StaffAdvancePaymentsController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: StaffAdvancePayments
        public async Task<IActionResult> Index()
        {
            var aprajitaRetailsContext = _context.StaffAdvancePayments.Include(s => s.Employee);
            return View(await aprajitaRetailsContext.ToListAsync());
        }

        // GET: StaffAdvancePayments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staffAdvancePayment = await _context.StaffAdvancePayments
                .Include(s => s.Employee)
                .FirstOrDefaultAsync(m => m.StaffAdvancePaymentId == id);
            if (staffAdvancePayment == null)
            {
                return NotFound();
            }

            return View(staffAdvancePayment);
        }

        // GET: StaffAdvancePayments/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId");
            return View();
        }

        // POST: StaffAdvancePayments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StaffAdvancePaymentId,EmployeeId,PaymentDate,Amount,PayMode,Details")] StaffAdvancePayment staffAdvancePayment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(staffAdvancePayment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", staffAdvancePayment.EmployeeId);
            return View(staffAdvancePayment);
        }

        // GET: StaffAdvancePayments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staffAdvancePayment = await _context.StaffAdvancePayments.FindAsync(id);
            if (staffAdvancePayment == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", staffAdvancePayment.EmployeeId);
            return View(staffAdvancePayment);
        }

        // POST: StaffAdvancePayments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StaffAdvancePaymentId,EmployeeId,PaymentDate,Amount,PayMode,Details")] StaffAdvancePayment staffAdvancePayment)
        {
            if (id != staffAdvancePayment.StaffAdvancePaymentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(staffAdvancePayment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StaffAdvancePaymentExists(staffAdvancePayment.StaffAdvancePaymentId))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", staffAdvancePayment.EmployeeId);
            return View(staffAdvancePayment);
        }

        // GET: StaffAdvancePayments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staffAdvancePayment = await _context.StaffAdvancePayments
                .Include(s => s.Employee)
                .FirstOrDefaultAsync(m => m.StaffAdvancePaymentId == id);
            if (staffAdvancePayment == null)
            {
                return NotFound();
            }

            return View(staffAdvancePayment);
        }

        // POST: StaffAdvancePayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var staffAdvancePayment = await _context.StaffAdvancePayments.FindAsync(id);
            _context.StaffAdvancePayments.Remove(staffAdvancePayment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StaffAdvancePaymentExists(int id)
        {
            return _context.StaffAdvancePayments.Any(e => e.StaffAdvancePaymentId == id);
        }
    }
}
