using Microsoft.AspNetCore.Authorization;    using System;
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
    [Authorize]
    public class StocksController : Controller
    {
        private readonly VoyagerContext _context;

        public StocksController(VoyagerContext context)
        {
            _context = context;
        }

        // GET: Purchase/Stocks
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
            var voyagerContext = _context.Stocks.Include(s => s.ProductItem);

            return View(await PaginatedList<Stock>.CreateAsync(voyagerContext.AsNoTracking(), pageNumber ?? 1, pageSize));
            
            
        }

        // GET: Purchase/Stocks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stock = await _context.Stocks
                .Include(s => s.ProductItem)
                .FirstOrDefaultAsync(m => m.StockID == id);
            if (stock == null)
            {
                return NotFound();
            }
            
            return PartialView(stock);
        }

        // GET: Purchase/Stocks/Create
        public IActionResult Create()
        {
            ViewData["ProductItemId"] = new SelectList(_context.ProductItems, "ProductItemId", "ProductName");
            return PartialView();
        }

        // POST: Purchase/Stocks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StockID,ProductItemId,Quantity,SaleQty,PurchaseQty,Units")] Stock stock)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stock);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductItemId"] = new SelectList(_context.ProductItems, "ProductItemId", "ProductName", stock.ProductItemId);
            return PartialView(stock);
        }

        // GET: Purchase/Stocks/Edit/5
         [Authorize(Roles = "Admin,PowerUser")] public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            ViewData["ProductItemId"] = new SelectList(_context.ProductItems, "ProductItemId", "ProductName", stock.ProductItemId);
            return PartialView(stock);
        }

        // POST: Purchase/Stocks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
       [Authorize(Roles = "Admin,PowerUser")]     public async Task<IActionResult> Edit(int id, [Bind("StockID,ProductItemId,Quantity,SaleQty,PurchaseQty,Units")] Stock stock)
        {
            if (id != stock.StockID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stock);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StockExists(stock.StockID))
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
            ViewData["ProductItemId"] = new SelectList(_context.ProductItems, "ProductItemId", "ProductName", stock.ProductItemId);
            return PartialView(stock);
        }

        // GET: Purchase/Stocks/Delete/5
         [Authorize (Roles = "Admin,PowerUser")]   public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stock = await _context.Stocks
                .Include(s => s.ProductItem)
                .FirstOrDefaultAsync(m => m.StockID == id);
            if (stock == null)
            {
                return NotFound();
            }

            return PartialView(stock);
        }

        // POST: Purchase/Stocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
         [Authorize (Roles = "Admin,PowerUser")]   public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stock = await _context.Stocks.FindAsync(id);
            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StockExists(int id)
        {
            return _context.Stocks.Any(e => e.StockID == id);
        }
    }
}
