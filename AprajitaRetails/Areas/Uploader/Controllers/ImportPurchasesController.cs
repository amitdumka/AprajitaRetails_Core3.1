using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AprajitaRetails.Areas.Uploader.Models;
using AprajitaRetails.Data;
using AprajitaRetails.Ops.Utility;
using AprajitaRetails.Ops.TAS;
using AprajitaRetails.Areas.Purchase.Models;
using System;
using System.Collections.Generic;

namespace AprajitaRetails.Areas.Uploader.Controllers
{
    [Area("Uploader")]
    [Authorize]
    public class ImportPurchasesController : Controller
    {
        private readonly AprajitaRetailsContext _context;

        public ImportPurchasesController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> GroupData()
        {
            var vm = await _context.ImportPurchases
                .GroupBy(c => new { c.InvoiceNo, c.GRNNo })
                .Select(c => new
                {
                    c.Key.InvoiceNo,
                    c.Key.GRNNo,
                    TQTY = c.Sum(o => o.Quantity),
                    TCost = c.Sum(o => o.CostValue),
                    TTax = c.Sum(o => o.TaxAmt),
                    TNo = c.Count(),


                })
                .ToListAsync();
            return View(vm);
        }
        // GET: Uploader/ImportPurchases
        public async Task<IActionResult> Index(int? id, string currentFilter, string searchString, int? pageNumber)
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
            var vM = _context.ImportPurchases.OrderByDescending(c => c.GRNDate);
            if (id != null && id == 100)
            {
                vM = (IOrderedQueryable<ImportPurchase>)vM.Where(c => c.IsDataConsumed);
            }
            else if (id != null && id == 101) {
                vM = (IOrderedQueryable<ImportPurchase>)vM.Where(c => !c.IsDataConsumed);
            }

            int pageSize = 15;
            return View(await PaginatedList<ImportPurchase>.CreateAsync(vM.AsNoTracking(), pageNumber ?? 1, pageSize));
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
        [Authorize(Roles = "Admin,PowerUser")]
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
        [Authorize(Roles = "Admin,PowerUser")]
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
        [Authorize(Roles = "Admin,PowerUser")]
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
        [Authorize(Roles = "Admin,PowerUser")]
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
       
        public IActionResult ProcessBill( string? GrnNo)
        {
            HelperUtil.IsSessionSet(HttpContext);
            
            int StoreId = HelperUtil.GetStoreID(HttpContext);
            InventoryManger iManage = new InventoryManger(StoreId);
            int a = -1;
            if (!String.IsNullOrEmpty(GrnNo))
            {
                a = iManage.ProcessPurchaseInward(_context, GrnNo, false);
                if (a > 0)
                    return RedirectToAction("ProcessedPurchase", new { id = a, GRNNo = GrnNo });
            }
 
            ViewBag.MessageHead = "No Product items added. Some error might has been occurred. Item(s)=" + a;
            return View();

        }

        public IActionResult ProcessedPurchase(int id, string GRNNo)
        {
            if (!String.IsNullOrEmpty(GRNNo))
            {
                var dm = _context.ProductPurchases.Include(c => c.Supplier).Include(c => c.PurchaseItems).Where(c => c.InWardNo == GRNNo);
                ViewBag.MessageHead = "Invoices added and No. Of Items Added are " + id;
                return View(dm.ToList());
            }
           
            else
            {
                var dm = _context.ProductPurchases.Include(c => c.Supplier).Include(c => c.PurchaseItems).Where(c => c.InWardNo == GRNNo);
                ViewBag.MessageHead = "Invoices added and No. Of Items Added are " + id;
                return View(dm.ToList());
            }

        }
    }
}
