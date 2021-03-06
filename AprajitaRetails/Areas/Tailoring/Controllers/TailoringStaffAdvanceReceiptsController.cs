﻿using Microsoft.AspNetCore.Authorization;    using System;
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
    [Authorize]
    public class TailoringStaffAdvanceReceiptsController : Controller
    {
        private readonly AprajitaRetailsContext _context;

        public TailoringStaffAdvanceReceiptsController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: TailoringStaffAdvanceReceipts
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
            var aprajitaRetailsContext = _context.TailoringStaffAdvanceReceipts.Include(t => t.Employee).OrderByDescending (c => c.ReceiptDate);
           return View(await PaginatedList<TailoringStaffAdvanceReceipt>.CreateAsync(aprajitaRetailsContext.AsNoTracking(), pageNumber ?? 1, pageSize));
            //return View(await aprajitaRetailsContext.ToListAsync());
        }

        // GET: TailoringStaffAdvanceReceipts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tailoringStaffAdvanceReceipt = await _context.TailoringStaffAdvanceReceipts
                .Include(t => t.Employee)
                .FirstOrDefaultAsync(m => m.TailoringStaffAdvanceReceiptId == id);
            if (tailoringStaffAdvanceReceipt == null)
            {
                return NotFound();
            }

           return PartialView(tailoringStaffAdvanceReceipt);
        }

        // GET: TailoringStaffAdvanceReceipts/Create
        public IActionResult Create()
        {
             ViewData["TailoringEmployeeId"] = new SelectList(_context.TailoringEmployees, "TailoringEmployeeId", "StaffName");
           return PartialView();
        }

        // POST: TailoringStaffAdvanceReceipts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TailoringStaffAdvanceReceiptId,TailoringEmployeeId,ReceiptDate,Amount,PayMode,Details")] TailoringStaffAdvanceReceipt tailoringStaffAdvanceReceipt)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tailoringStaffAdvanceReceipt);
                new TailoringManager().OnInsert(_context, tailoringStaffAdvanceReceipt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TailoringEmployeeId"] = new SelectList(_context.TailoringEmployees, "TailoringEmployeeId", "StaffName", tailoringStaffAdvanceReceipt.TailoringEmployeeId);
           return PartialView(tailoringStaffAdvanceReceipt);
        }

        // GET: TailoringStaffAdvanceReceipts/Edit/5
         [Authorize(Roles = "Admin,PowerUser")] public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tailoringStaffAdvanceReceipt = await _context.TailoringStaffAdvanceReceipts.FindAsync(id);
            if (tailoringStaffAdvanceReceipt == null)
            {
                return NotFound();
            }
            ViewData["TailoringEmployeeId"] = new SelectList(_context.TailoringEmployees, "TailoringEmployeeId", "StaffName", tailoringStaffAdvanceReceipt.TailoringEmployeeId);
           return PartialView(tailoringStaffAdvanceReceipt);
        }

        // POST: TailoringStaffAdvanceReceipts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
       [Authorize(Roles = "Admin,PowerUser")]     public async Task<IActionResult> Edit(int id, [Bind("TailoringStaffAdvanceReceiptId,TailoringEmployeeId,ReceiptDate,Amount,PayMode,Details")] TailoringStaffAdvanceReceipt tailoringStaffAdvanceReceipt)
        {
            if (id != tailoringStaffAdvanceReceipt.TailoringStaffAdvanceReceiptId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    new TailoringManager().OnUpdate(_context, tailoringStaffAdvanceReceipt);
                    _context.Update(tailoringStaffAdvanceReceipt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TailoringStaffAdvanceReceiptExists(tailoringStaffAdvanceReceipt.TailoringStaffAdvanceReceiptId))
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
            ViewData["TailoringEmployeeId"] = new SelectList(_context.TailoringEmployees, "TailoringEmployeeId", "StaffName", tailoringStaffAdvanceReceipt.TailoringEmployeeId);
           return PartialView(tailoringStaffAdvanceReceipt);
        }

        // GET: TailoringStaffAdvanceReceipts/Delete/5
         [Authorize (Roles = "Admin,PowerUser")]   public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tailoringStaffAdvanceReceipt = await _context.TailoringStaffAdvanceReceipts
                .Include(t => t.Employee)
                .FirstOrDefaultAsync(m => m.TailoringStaffAdvanceReceiptId == id);
            if (tailoringStaffAdvanceReceipt == null)
            {
                return NotFound();
            }

           return PartialView(tailoringStaffAdvanceReceipt);
        }

        // POST: TailoringStaffAdvanceReceipts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
         [Authorize (Roles = "Admin,PowerUser")]   public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tailoringStaffAdvanceReceipt = await _context.TailoringStaffAdvanceReceipts.FindAsync(id);
            new TailoringManager().OnDelete(_context, tailoringStaffAdvanceReceipt);
            _context.TailoringStaffAdvanceReceipts.Remove(tailoringStaffAdvanceReceipt);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TailoringStaffAdvanceReceiptExists(int id)
        {
            return _context.TailoringStaffAdvanceReceipts.Any(e => e.TailoringStaffAdvanceReceiptId == id);
        }
    }
}
