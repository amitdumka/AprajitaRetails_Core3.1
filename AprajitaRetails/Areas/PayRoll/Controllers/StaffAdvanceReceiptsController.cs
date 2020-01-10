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
    public class StaffAdvanceReceiptsController : Controller
    {
        private readonly AprajitaRetailsContext _context;

        public StaffAdvanceReceiptsController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: StaffAdvanceReceipts
        public async Task<IActionResult> Index()
        {
            var aprajitaRetailsContext = _context.StaffAdvanceReceipts.Include(s => s.Employee);
            return View(await aprajitaRetailsContext.ToListAsync());
        }

        // GET: StaffAdvanceReceipts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staffAdvanceReceipt = await _context.StaffAdvanceReceipts
                .Include(s => s.Employee)
                .FirstOrDefaultAsync(m => m.StaffAdvanceReceiptId == id);
            if (staffAdvanceReceipt == null)
            {
                return NotFound();
            }

            return View(staffAdvanceReceipt);
        }

        // GET: StaffAdvanceReceipts/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId");
            return View();
        }

        // POST: StaffAdvanceReceipts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StaffAdvanceReceiptId,EmployeeId,ReceiptDate,Amount,PayMode,Details")] StaffAdvanceReceipt staffAdvanceReceipt)
        {
            if (ModelState.IsValid)
            {
                _context.Add(staffAdvanceReceipt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", staffAdvanceReceipt.EmployeeId);
            return View(staffAdvanceReceipt);
        }

        // GET: StaffAdvanceReceipts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staffAdvanceReceipt = await _context.StaffAdvanceReceipts.FindAsync(id);
            if (staffAdvanceReceipt == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", staffAdvanceReceipt.EmployeeId);
            return View(staffAdvanceReceipt);
        }

        // POST: StaffAdvanceReceipts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StaffAdvanceReceiptId,EmployeeId,ReceiptDate,Amount,PayMode,Details")] StaffAdvanceReceipt staffAdvanceReceipt)
        {
            if (id != staffAdvanceReceipt.StaffAdvanceReceiptId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(staffAdvanceReceipt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StaffAdvanceReceiptExists(staffAdvanceReceipt.StaffAdvanceReceiptId))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", staffAdvanceReceipt.EmployeeId);
            return View(staffAdvanceReceipt);
        }

        // GET: StaffAdvanceReceipts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staffAdvanceReceipt = await _context.StaffAdvanceReceipts
                .Include(s => s.Employee)
                .FirstOrDefaultAsync(m => m.StaffAdvanceReceiptId == id);
            if (staffAdvanceReceipt == null)
            {
                return NotFound();
            }

            return View(staffAdvanceReceipt);
        }

        // POST: StaffAdvanceReceipts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var staffAdvanceReceipt = await _context.StaffAdvanceReceipts.FindAsync(id);
            _context.StaffAdvanceReceipts.Remove(staffAdvanceReceipt);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StaffAdvanceReceiptExists(int id)
        {
            return _context.StaffAdvanceReceipts.Any(e => e.StaffAdvanceReceiptId == id);
        }
    }
}
