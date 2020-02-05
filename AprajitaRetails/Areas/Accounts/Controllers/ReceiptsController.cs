using Microsoft.AspNetCore.Authorization;    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AprajitaRetails.Data;
using AprajitaRetails.Models;
using AprajitaRetails.Areas.Accounts.Models;
using Microsoft.AspNetCore.Authorization;

namespace AprajitaRetails.Areas.Accounts.Controllers
{
    [Area ("Accounts")]
    public class ReceiptsController : Controller
    {
        private readonly AprajitaRetailsContext _context;

        public ReceiptsController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: Receipts
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
            var aprajitaretailscontext = _context.Receipts.OrderByDescending (c => c.RecieptDate);
            return View(await PaginatedList<Receipt>.CreateAsync(aprajitaretailscontext.AsNoTracking(), pageNumber ?? 1, pageSize));
                       
        }

        // GET: Receipts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receipt = await _context.Receipts
                .FirstOrDefaultAsync(m => m.ReceiptId == id);
            if (receipt == null)
            {
                return NotFound();
            }

            return PartialView(receipt);
        }

        // GET: Receipts/Create
        public IActionResult Create()
        {
            return PartialView();
        }

        // POST: Receipts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReceiptId,RecieptDate,ReceiptFrom,PayMode,ReceiptDetails,Amount,RecieptSlipNo,Remarks")] Receipt receipt)
        {
            if (ModelState.IsValid)
            {
                _context.Add(receipt);
                new AccountsManager().OnInsert(_context, receipt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return PartialView(receipt);
        }

        // GET: Receipts/Edit/5
         [Authorize(Roles = "Admin,PowerUser")] public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receipt = await _context.Receipts.FindAsync(id);
            if (receipt == null)
            {
                return NotFound();
            }
            return PartialView(receipt);
        }

        // POST: Receipts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
       [Authorize(Roles = "Admin,PowerUser")]     public async Task<IActionResult> Edit(int id, [Bind("ReceiptId,RecieptDate,ReceiptFrom,PayMode,ReceiptDetails,Amount,RecieptSlipNo,Remarks")] Receipt receipt)
        {
            if (id != receipt.ReceiptId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    new AccountsManager().OnUpdate(_context, receipt);
                    _context.Update(receipt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReceiptExists(receipt.ReceiptId))
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
            return PartialView(receipt);
        }

        // GET: Receipts/Delete/5
         [Authorize (Roles = "Admin,PowerUser")]   public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receipt = await _context.Receipts
                .FirstOrDefaultAsync(m => m.ReceiptId == id);
            if (receipt == null)
            {
                return NotFound();
            }

            return PartialView(receipt);
        }

        // POST: Receipts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
         [Authorize (Roles = "Admin,PowerUser")]   public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var receipt = await _context.Receipts.FindAsync(id);
            new AccountsManager().OnDelete(_context, receipt);
            _context.Receipts.Remove(receipt);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReceiptExists(int id)
        {
            return _context.Receipts.Any(e => e.ReceiptId == id);
        }
    }
}
