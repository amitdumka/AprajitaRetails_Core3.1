﻿using AprajitaRetails.Data;
using AprajitaRetails.Models;
using AprajitaRetails.Ops.Triggers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AprajitaRetails.Areas.PayRoll.Controllers
{
    [Area("PayRoll")]
    [Authorize]
    public class StaffAdvanceReceiptsController : Controller
    {
        private readonly AprajitaRetailsContext _context;
        private readonly int StoreId = 1;

        public StaffAdvanceReceiptsController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: StaffAdvanceReceipts
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
            var aprajitaRetailsContext = _context.StaffAdvanceReceipts.Include(s => s.Employee).Where(c => c.StoreId == StoreId).OrderByDescending(c => c.ReceiptDate);

            return View(await PaginatedList<StaffAdvanceReceipt>.CreateAsync(aprajitaRetailsContext.AsNoTracking(), pageNumber ?? 1, pageSize));


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

            return PartialView(staffAdvanceReceipt);
        }

        // GET: StaffAdvanceReceipts/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees.Where(c => c.IsWorking && c.StoreId == StoreId), "EmployeeId", "StaffName");
            return PartialView();
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

                staffAdvanceReceipt.UserName = User.Identity.Name;
                staffAdvanceReceipt.StoreId = StoreId;
                _context.Add(staffAdvanceReceipt);
                new PayRollManager().OnInsert(_context, staffAdvanceReceipt);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees.Where(c => c.IsWorking && c.StoreId == StoreId), "EmployeeId", "StaffName", staffAdvanceReceipt.EmployeeId);
            return PartialView(staffAdvanceReceipt);
        }

        // GET: StaffAdvanceReceipts/Edit/5
        [Authorize(Roles = "Admin,PowerUser")]
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees.Where(c => c.IsWorking && c.StoreId == StoreId), "EmployeeId", "StaffName", staffAdvanceReceipt.EmployeeId);
            return PartialView(staffAdvanceReceipt);
        }

        // POST: StaffAdvanceReceipts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,PowerUser")]
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
                    staffAdvanceReceipt.UserName = User.Identity.Name;
                    staffAdvanceReceipt.StoreId = StoreId;
                    new PayRollManager().OnUpdate(_context, staffAdvanceReceipt);
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees.Where(c => c.IsWorking && c.StoreId == StoreId), "EmployeeId", "StaffName", staffAdvanceReceipt.EmployeeId);
            return PartialView(staffAdvanceReceipt);
        }

        // GET: StaffAdvanceReceipts/Delete/5
        [Authorize(Roles = "Admin,PowerUser")]
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

            return PartialView(staffAdvanceReceipt);
        }

        // POST: StaffAdvanceReceipts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,PowerUser")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var staffAdvanceReceipt = await _context.StaffAdvanceReceipts.FindAsync(id);
            _context.StaffAdvanceReceipts.Remove(staffAdvanceReceipt);
            new PayRollManager().OnDelete(_context, staffAdvanceReceipt);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StaffAdvanceReceiptExists(int id)
        {
            return _context.StaffAdvanceReceipts.Any(e => e.StaffAdvanceReceiptId == id);
        }
    }
}
