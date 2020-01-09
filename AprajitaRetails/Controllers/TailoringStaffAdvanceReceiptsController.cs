using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AprajitaRetails.Data;
using AprajitaRetails.Models;

namespace AprajitaRetails.Controllers
{
    public class TailoringStaffAdvanceReceiptsController : Controller
    {
        private readonly AprajitaRetailsContext _context;

        public TailoringStaffAdvanceReceiptsController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: TailoringStaffAdvanceReceipts
        public async Task<IActionResult> Index()
        {
            var aprajitaRetailsContext = _context.TailoringStaffAdvanceReceipts.Include(t => t.Employee);
            return View(await aprajitaRetailsContext.ToListAsync());
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

            return View(tailoringStaffAdvanceReceipt);
        }

        // GET: TailoringStaffAdvanceReceipts/Create
        public IActionResult Create()
        {
            ViewData["TailoringEmployeeId"] = new SelectList(_context.TailoringEmployees, "TailoringEmployeeId", "TailoringEmployeeId");
            return View();
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
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TailoringEmployeeId"] = new SelectList(_context.TailoringEmployees, "TailoringEmployeeId", "TailoringEmployeeId", tailoringStaffAdvanceReceipt.TailoringEmployeeId);
            return View(tailoringStaffAdvanceReceipt);
        }

        // GET: TailoringStaffAdvanceReceipts/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
            ViewData["TailoringEmployeeId"] = new SelectList(_context.TailoringEmployees, "TailoringEmployeeId", "TailoringEmployeeId", tailoringStaffAdvanceReceipt.TailoringEmployeeId);
            return View(tailoringStaffAdvanceReceipt);
        }

        // POST: TailoringStaffAdvanceReceipts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TailoringStaffAdvanceReceiptId,TailoringEmployeeId,ReceiptDate,Amount,PayMode,Details")] TailoringStaffAdvanceReceipt tailoringStaffAdvanceReceipt)
        {
            if (id != tailoringStaffAdvanceReceipt.TailoringStaffAdvanceReceiptId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
            ViewData["TailoringEmployeeId"] = new SelectList(_context.TailoringEmployees, "TailoringEmployeeId", "TailoringEmployeeId", tailoringStaffAdvanceReceipt.TailoringEmployeeId);
            return View(tailoringStaffAdvanceReceipt);
        }

        // GET: TailoringStaffAdvanceReceipts/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

            return View(tailoringStaffAdvanceReceipt);
        }

        // POST: TailoringStaffAdvanceReceipts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tailoringStaffAdvanceReceipt = await _context.TailoringStaffAdvanceReceipts.FindAsync(id);
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
