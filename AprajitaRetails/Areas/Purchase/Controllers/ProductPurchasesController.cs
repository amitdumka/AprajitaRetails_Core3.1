using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AprajitaRetails.Areas.Purchase.Models;
using AprajitaRetails.Areas.Voyager.Data;

namespace AprajitaRetails.Areas.Purchase.Controllers
{
    [Area("Purchase")]
    public class ProductPurchasesController : Controller
    {
        private readonly VoyagerContext _context;

        public ProductPurchasesController(VoyagerContext context)
        {
            _context = context;
        }

        // GET: Purchase/ProductPurchases
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

            var voyagerContext = _context.ProductPurchases.Include(p => p.Supplier);
           return View(await PaginatedList<ProductPurchase>.CreateAsync(voyagerContext.AsNoTracking(), pageNumber ?? 1, pageSize));
            
            
        }

        // GET: Purchase/ProductPurchases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productPurchase = await _context.ProductPurchases
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.ProductPurchaseId == id);
            if (productPurchase == null)
            {
                return NotFound();
            }

           return PartialView(productPurchase);
        }

        // GET: Purchase/ProductPurchases/Create
        public IActionResult Create()
        {
            ViewData["SupplierID"] = new SelectList(_context.Suppliers, "SupplierID", "SuppilerName");
           return PartialView();
        }

        // POST: Purchase/ProductPurchases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductPurchaseId,InWardNo,InWardDate,PurchaseDate,InvoiceNo,TotalQty,TotalBasicAmount,ShippingCost,TotalTax,TotalAmount,Remarks,SupplierID,IsPaid")] ProductPurchase productPurchase)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productPurchase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SupplierID"] = new SelectList(_context.Suppliers, "SupplierID", "SuppilerName", productPurchase.SupplierID);
           return PartialView(productPurchase);
        }

        // GET: Purchase/ProductPurchases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productPurchase = await _context.ProductPurchases.FindAsync(id);
            if (productPurchase == null)
            {
                return NotFound();
            }
            ViewData["SupplierID"] = new SelectList(_context.Suppliers, "SupplierID", "SuppilerName", productPurchase.SupplierID);
           return PartialView(productPurchase);
        }

        // POST: Purchase/ProductPurchases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductPurchaseId,InWardNo,InWardDate,PurchaseDate,InvoiceNo,TotalQty,TotalBasicAmount,ShippingCost,TotalTax,TotalAmount,Remarks,SupplierID,IsPaid")] ProductPurchase productPurchase)
        {
            if (id != productPurchase.ProductPurchaseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productPurchase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductPurchaseExists(productPurchase.ProductPurchaseId))
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
            ViewData["SupplierID"] = new SelectList(_context.Suppliers, "SupplierID", "SuppilerName", productPurchase.SupplierID);
           return PartialView(productPurchase);
        }

        // GET: Purchase/ProductPurchases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productPurchase = await _context.ProductPurchases
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.ProductPurchaseId == id);
            if (productPurchase == null)
            {
                return NotFound();
            }

           return PartialView(productPurchase);
        }

        // POST: Purchase/ProductPurchases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productPurchase = await _context.ProductPurchases.FindAsync(id);
            _context.ProductPurchases.Remove(productPurchase);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductPurchaseExists(int id)
        {
            return _context.ProductPurchases.Any(e => e.ProductPurchaseId == id);
        }
    }
}
