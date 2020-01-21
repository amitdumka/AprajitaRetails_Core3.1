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
    public class PaySlipsController : Controller
    {
        private readonly AprajitaRetailsContext _context;

        public PaySlipsController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: PaySlips
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
            var aprajitaRetailsContext = _context.PaySlips.Include(p => p.CurrentSalary).Include(p => p.Employee);
           return View(await PaginatedList<PaySlip>.CreateAsync(aprajitaRetailsContext.AsNoTracking(), pageNumber ?? 1, pageSize));
            //return View(await aprajitaRetailsContext.ToListAsync());
        }

        // GET: PaySlips/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paySlip = await _context.PaySlips
                .Include(p => p.CurrentSalary)
                .Include(p => p.Employee)
                .FirstOrDefaultAsync(m => m.PaySlipId == id);
            if (paySlip == null)
            {
                return NotFound();
            }

           return PartialView(paySlip);
        }

        // GET: PaySlips/Create
        public IActionResult Create()
        {
            ViewData["CurrentSalaryId"] = new SelectList(_context.CurrentSalaries, "CurrentSalaryId", "CurrentSalaryId");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "StaffName");
           return PartialView();
        }

        // POST: PaySlips/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaySlipId,OnDate,Month,Year,EmployeeId,CurrentSalaryId,BasicSalary,NoOfDaysPresent,TotalSale,SaleIncentive,WOWBillAmount,WOWBillIncentive,LastPcsAmount,LastPCsIncentive,OthersIncentive,GrossSalary,StandardDeductions,TDSDeductions,PFDeductions,AdvanceDeducations,OtherDeductions,Remarks")] PaySlip paySlip)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paySlip);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CurrentSalaryId"] = new SelectList(_context.CurrentSalaries, "CurrentSalaryId", "CurrentSalaryId", paySlip.CurrentSalaryId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "StaffName", paySlip.EmployeeId);
           return PartialView(paySlip);
        }

        // GET: PaySlips/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paySlip = await _context.PaySlips.FindAsync(id);
            if (paySlip == null)
            {
                return NotFound();
            }
            ViewData["CurrentSalaryId"] = new SelectList(_context.CurrentSalaries, "CurrentSalaryId", "CurrentSalaryId", paySlip.CurrentSalaryId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "StaffName", paySlip.EmployeeId);
           return PartialView(paySlip);
        }

        // POST: PaySlips/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PaySlipId,OnDate,Month,Year,EmployeeId,CurrentSalaryId,BasicSalary,NoOfDaysPresent,TotalSale,SaleIncentive,WOWBillAmount,WOWBillIncentive,LastPcsAmount,LastPCsIncentive,OthersIncentive,GrossSalary,StandardDeductions,TDSDeductions,PFDeductions,AdvanceDeducations,OtherDeductions,Remarks")] PaySlip paySlip)
        {
            if (id != paySlip.PaySlipId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paySlip);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaySlipExists(paySlip.PaySlipId))
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
            ViewData["CurrentSalaryId"] = new SelectList(_context.CurrentSalaries, "CurrentSalaryId", "CurrentSalaryId", paySlip.CurrentSalaryId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "StaffName", paySlip.EmployeeId);
           return PartialView(paySlip);
        }

        // GET: PaySlips/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paySlip = await _context.PaySlips
                .Include(p => p.CurrentSalary)
                .Include(p => p.Employee)
                .FirstOrDefaultAsync(m => m.PaySlipId == id);
            if (paySlip == null)
            {
                return NotFound();
            }

           return PartialView(paySlip);
        }

        // POST: PaySlips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paySlip = await _context.PaySlips.FindAsync(id);
            _context.PaySlips.Remove(paySlip);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaySlipExists(int id)
        {
            return _context.PaySlips.Any(e => e.PaySlipId == id);
        }
    }
}
