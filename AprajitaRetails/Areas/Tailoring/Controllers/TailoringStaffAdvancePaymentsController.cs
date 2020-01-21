using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AprajitaRetails.Models;
using AprajitaRetails.Data;

namespace AprajitaRetails.Areas.Tailoring.Controllers
{
    [Area ("Tailoring")]
    public class TailoringStaffAdvancePaymentsController : Controller
    {
        private readonly AprajitaRetailsContext _context;

        public TailoringStaffAdvancePaymentsController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: TailoringStaffAdvancePayments
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
            int pageSize = 10;
            var aprajitaRetailsContext = _context.TailoringStaffAdvancePayments.Include(t => t.Employee);
           return View(await PaginatedList<TailoringStaffAdvancePayment>.CreateAsync(aprajitaRetailsContext.AsNoTracking(), pageNumber ?? 1, pageSize));
            //return View(await aprajitaRetailsContext.ToListAsync());
        }

        // GET: TailoringStaffAdvancePayments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tailoringStaffAdvancePayment = await _context.TailoringStaffAdvancePayments
                .Include(t => t.Employee)
                .FirstOrDefaultAsync(m => m.TailoringStaffAdvancePaymentId == id);
            if (tailoringStaffAdvancePayment == null)
            {
                return NotFound();
            }

           return PartialView(tailoringStaffAdvancePayment);
        }

        // GET: TailoringStaffAdvancePayments/Create
        public IActionResult Create()
        {
            ViewData["TailoringEmployeeId"] = new SelectList(_context.TailoringEmployees, "TailoringEmployeeId", "TailoringEmployeeId");
           return PartialView();
        }

        // POST: TailoringStaffAdvancePayments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TailoringStaffAdvancePaymentId,TailoringEmployeeId,PaymentDate,Amount,PayMode,Details")] TailoringStaffAdvancePayment tailoringStaffAdvancePayment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tailoringStaffAdvancePayment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TailoringEmployeeId"] = new SelectList(_context.TailoringEmployees, "TailoringEmployeeId", "TailoringEmployeeId", tailoringStaffAdvancePayment.TailoringEmployeeId);
           return PartialView(tailoringStaffAdvancePayment);
        }

        // GET: TailoringStaffAdvancePayments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tailoringStaffAdvancePayment = await _context.TailoringStaffAdvancePayments.FindAsync(id);
            if (tailoringStaffAdvancePayment == null)
            {
                return NotFound();
            }
            ViewData["TailoringEmployeeId"] = new SelectList(_context.TailoringEmployees, "TailoringEmployeeId", "TailoringEmployeeId", tailoringStaffAdvancePayment.TailoringEmployeeId);
           return PartialView(tailoringStaffAdvancePayment);
        }

        // POST: TailoringStaffAdvancePayments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TailoringStaffAdvancePaymentId,TailoringEmployeeId,PaymentDate,Amount,PayMode,Details")] TailoringStaffAdvancePayment tailoringStaffAdvancePayment)
        {
            if (id != tailoringStaffAdvancePayment.TailoringStaffAdvancePaymentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tailoringStaffAdvancePayment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TailoringStaffAdvancePaymentExists(tailoringStaffAdvancePayment.TailoringStaffAdvancePaymentId))
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
            ViewData["TailoringEmployeeId"] = new SelectList(_context.TailoringEmployees, "TailoringEmployeeId", "TailoringEmployeeId", tailoringStaffAdvancePayment.TailoringEmployeeId);
           return PartialView(tailoringStaffAdvancePayment);
        }

        // GET: TailoringStaffAdvancePayments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tailoringStaffAdvancePayment = await _context.TailoringStaffAdvancePayments
                .Include(t => t.Employee)
                .FirstOrDefaultAsync(m => m.TailoringStaffAdvancePaymentId == id);
            if (tailoringStaffAdvancePayment == null)
            {
                return NotFound();
            }

           return PartialView(tailoringStaffAdvancePayment);
        }

        // POST: TailoringStaffAdvancePayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tailoringStaffAdvancePayment = await _context.TailoringStaffAdvancePayments.FindAsync(id);
            _context.TailoringStaffAdvancePayments.Remove(tailoringStaffAdvancePayment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TailoringStaffAdvancePaymentExists(int id)
        {
            return _context.TailoringStaffAdvancePayments.Any(e => e.TailoringStaffAdvancePaymentId == id);
        }
    }
}
