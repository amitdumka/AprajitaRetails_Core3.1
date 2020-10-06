using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AprajitaRetails.Areas.Purchase.Models;
using AprajitaRetails.Data;

namespace AprajitaRetails.Areas.Purchase.Controllers
{
    [Area("Purchase")]
    [Authorize]
    public class ProductItemsController : Controller
    {
        private readonly AprajitaRetailsContext _context;

        public ProductItemsController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: Purchase/ProductItems
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
            var AprajitaRetailsContext = _context.ProductItems.Include(p => p.BrandName);
            return View(await PaginatedList<ProductItem>.CreateAsync(AprajitaRetailsContext.AsNoTracking(), pageNumber ?? 1, pageSize));
           
            //return PartialView(await AprajitaRetailsContext.ToListAsync());
        }

        // GET: Purchase/ProductItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productItem = await _context.ProductItems
                .Include(p => p.BrandName)
                .FirstOrDefaultAsync(m => m.ProductItemId == id);
            if (productItem == null)
            {
                return NotFound();
            }

            return PartialView(productItem);
        }

        // GET: Purchase/ProductItems/Create
        public IActionResult Create()
        {
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandName");
            return PartialView();
        }

        // POST: Purchase/ProductItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductItemId,Barcode,BrandId,StyleCode,ProductName,ItemDesc,Categorys,MRP,TaxRate,Cost,Size,Unit")] ProductItem productItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandName", productItem.BrandId);
            return PartialView(productItem);
        }

        // GET: Purchase/ProductItems/Edit/5
         [Authorize(Roles = "Admin,PowerUser")] public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productItem = await _context.ProductItems.FindAsync(id);
            if (productItem == null)
            {
                return NotFound();
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandName", productItem.BrandId);
            return PartialView(productItem);
        }

        // POST: Purchase/ProductItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
       [Authorize(Roles = "Admin,PowerUser")]     public async Task<IActionResult> Edit(int id, [Bind("ProductItemId,Barcode,BrandId,StyleCode,ProductName,ItemDesc,Categorys,MRP,TaxRate,Cost,Size,Unit")] ProductItem productItem)
        {
            if (id != productItem.ProductItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductItemExists(productItem.ProductItemId))
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
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandName", productItem.BrandId);
            return PartialView(productItem);
        }

        // GET: Purchase/ProductItems/Delete/5
         [Authorize (Roles = "Admin,PowerUser")]   public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productItem = await _context.ProductItems
                .Include(p => p.BrandName)
                .FirstOrDefaultAsync(m => m.ProductItemId == id);
            if (productItem == null)
            {
                return NotFound();
            }

            return PartialView(productItem);
        }

        // POST: Purchase/ProductItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
         [Authorize (Roles = "Admin,PowerUser")]   public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productItem = await _context.ProductItems.FindAsync(id);
            _context.ProductItems.Remove(productItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductItemExists(int id)
        {
            return _context.ProductItems.Any(e => e.ProductItemId == id);
        }
    }
}
