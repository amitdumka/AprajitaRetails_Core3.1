using Microsoft.AspNetCore.Authorization;    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AprajitaRetails.Data;
using AprajitaRetails.Models;
using AprajitaRetails.Areas.Tailoring.Data;

namespace AprajitaRetails.Areas.Tailoring.Controllers
{
    [Area ("Tailoring")]
    public class TailoringSalaryPaymentsController : Controller
    {
        private readonly AprajitaRetailsContext _context;

        public TailoringSalaryPaymentsController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: TailoringSalaryPayments
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
            var aprajitaRetailsContext = _context.TailoringSalaryPayments.Include(t => t.Employee).OrderByDescending (c => c.PaymentDate);
           return View(await PaginatedList<TailoringSalaryPayment>.CreateAsync(aprajitaRetailsContext.AsNoTracking(), pageNumber ?? 1, pageSize));
            //return View(await aprajitaRetailsContext.ToListAsync());
        }

        // GET: TailoringSalaryPayments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tailoringSalaryPayment = await _context.TailoringSalaryPayments
                .Include(t => t.Employee)
                .FirstOrDefaultAsync(m => m.TailoringSalaryPaymentId == id);
            if (tailoringSalaryPayment == null)
            {
                return NotFound();
            }

           return PartialView(tailoringSalaryPayment);
        }

        // GET: TailoringSalaryPayments/Create
        public IActionResult Create()
        {
             ViewData["TailoringEmployeeId"] = new SelectList(_context.TailoringEmployees, "TailoringEmployeeId", "StaffName");
           return PartialView();
        }

        // POST: TailoringSalaryPayments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TailoringSalaryPaymentId,TailoringEmployeeId,SalaryMonth,SalaryComponet,PaymentDate,Amount,PayMode,Details")] TailoringSalaryPayment tailoringSalaryPayment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tailoringSalaryPayment);
                new TailoringManager().OnInsert(_context, tailoringSalaryPayment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TailoringEmployeeId"] = new SelectList(_context.TailoringEmployees, "TailoringEmployeeId", "StaffName", tailoringSalaryPayment.TailoringEmployeeId);
           return PartialView(tailoringSalaryPayment);
        }

        // GET: TailoringSalaryPayments/Edit/5
         [Authorize(Roles = "Admin,PowerUser")] public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tailoringSalaryPayment = await _context.TailoringSalaryPayments.FindAsync(id);
            if (tailoringSalaryPayment == null)
            {
                return NotFound();
            }
            ViewData["TailoringEmployeeId"] = new SelectList(_context.TailoringEmployees, "TailoringEmployeeId", "StaffName", tailoringSalaryPayment.TailoringEmployeeId);
           return PartialView(tailoringSalaryPayment);
        }

        // POST: TailoringSalaryPayments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
       [Authorize(Roles = "Admin,PowerUser")]     public async Task<IActionResult> Edit(int id, [Bind("TailoringSalaryPaymentId,TailoringEmployeeId,SalaryMonth,SalaryComponet,PaymentDate,Amount,PayMode,Details")] TailoringSalaryPayment tailoringSalaryPayment)
        {
            if (id != tailoringSalaryPayment.TailoringSalaryPaymentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    new TailoringManager().OnUpdate(_context, tailoringSalaryPayment);
                    _context.Update(tailoringSalaryPayment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TailoringSalaryPaymentExists(tailoringSalaryPayment.TailoringSalaryPaymentId))
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
            ViewData["TailoringEmployeeId"] = new SelectList(_context.TailoringEmployees, "TailoringEmployeeId", "StaffName", tailoringSalaryPayment.TailoringEmployeeId);
           return PartialView(tailoringSalaryPayment);
        }

        // GET: TailoringSalaryPayments/Delete/5
         [Authorize (Roles = "Admin,PowerUser")]   public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tailoringSalaryPayment = await _context.TailoringSalaryPayments
                .Include(t => t.Employee)
                .FirstOrDefaultAsync(m => m.TailoringSalaryPaymentId == id);
            if (tailoringSalaryPayment == null)
            {
                return NotFound();
            }

           return PartialView(tailoringSalaryPayment);
        }

        // POST: TailoringSalaryPayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
         [Authorize (Roles = "Admin,PowerUser")]   public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tailoringSalaryPayment = await _context.TailoringSalaryPayments.FindAsync(id);
            new TailoringManager().OnDelete(_context, tailoringSalaryPayment);
            _context.TailoringSalaryPayments.Remove(tailoringSalaryPayment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TailoringSalaryPaymentExists(int id)
        {
            return _context.TailoringSalaryPayments.Any(e => e.TailoringSalaryPaymentId == id);
        }
    }
}
