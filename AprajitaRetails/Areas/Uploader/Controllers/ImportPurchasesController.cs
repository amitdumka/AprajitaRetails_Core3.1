using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AprajitaRetails.Areas.Uploader.Models;
using AprajitaRetails.Areas.Voyager.Data;

namespace AprajitaRetails.Areas.Uploader.Controllers
{
    [Area("Uploader")]
    public class ImportPurchasesController : Controller
    {
        private readonly VoyagerContext _context;

        public ImportPurchasesController(VoyagerContext context)
        {
            _context = context;
        }

        // GET: Uploader/ImportPurchases
        public async Task<IActionResult> Index()
        {
            return View(await _context.ImportPurchases.ToListAsync());
        }

        // GET: Uploader/ImportPurchases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var importPurchase = await _context.ImportPurchases
                .FirstOrDefaultAsync(m => m.ImportPurchaseId == id);
            if (importPurchase == null)
            {
                return NotFound();
            }

            return View(importPurchase);
        }

        // GET: Uploader/ImportPurchases/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Uploader/ImportPurchases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ImportPurchaseId,GRNNo,GRNDate,InvoiceNo,InvoiceDate,SupplierName,Barcode,ProductName,StyleCode,ItemDesc,Quantity,MRP,MRPValue,Cost,CostValue,TaxAmt,IsVatBill,IsLocal,IsDataConsumed,ImportTime")] ImportPurchase importPurchase)
        {
            if (ModelState.IsValid)
            {
                _context.Add(importPurchase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(importPurchase);
        }

        // GET: Uploader/ImportPurchases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var importPurchase = await _context.ImportPurchases.FindAsync(id);
            if (importPurchase == null)
            {
                return NotFound();
            }
            return View(importPurchase);
        }

        // POST: Uploader/ImportPurchases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ImportPurchaseId,GRNNo,GRNDate,InvoiceNo,InvoiceDate,SupplierName,Barcode,ProductName,StyleCode,ItemDesc,Quantity,MRP,MRPValue,Cost,CostValue,TaxAmt,IsVatBill,IsLocal,IsDataConsumed,ImportTime")] ImportPurchase importPurchase)
        {
            if (id != importPurchase.ImportPurchaseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(importPurchase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImportPurchaseExists(importPurchase.ImportPurchaseId))
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
            return View(importPurchase);
        }

        // GET: Uploader/ImportPurchases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var importPurchase = await _context.ImportPurchases
                .FirstOrDefaultAsync(m => m.ImportPurchaseId == id);
            if (importPurchase == null)
            {
                return NotFound();
            }

            return View(importPurchase);
        }

        // POST: Uploader/ImportPurchases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var importPurchase = await _context.ImportPurchases.FindAsync(id);
            _context.ImportPurchases.Remove(importPurchase);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImportPurchaseExists(int id)
        {
            return _context.ImportPurchases.Any(e => e.ImportPurchaseId == id);
        }
    }
}
