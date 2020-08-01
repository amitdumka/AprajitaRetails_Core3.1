using Microsoft.AspNetCore.Authorization;    using System;
using System.Collections.Generic;
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
    public class PurchaseTaxTypesController : Controller
    {
        private readonly AprajitaRetailsContext _context;

        public PurchaseTaxTypesController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: Purchase/PurchaseTaxTypes
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

            return View(await PaginatedList<PurchaseTaxType>.CreateAsync(_context.PurchaseTaxTypes.AsNoTracking(), pageNumber ?? 1, pageSize));
            
        }

        // GET: Purchase/PurchaseTaxTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseTaxType = await _context.PurchaseTaxTypes
                .FirstOrDefaultAsync(m => m.PurchaseTaxTypeId == id);
            if (purchaseTaxType == null)
            {
                return NotFound();
            }

            return PartialView(purchaseTaxType);
        }

        // GET: Purchase/PurchaseTaxTypes/Create
        public IActionResult Create()
        {
            return PartialView();
        }

        // POST: Purchase/PurchaseTaxTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PurchaseTaxTypeId,TaxName,TaxType,CompositeRate")] PurchaseTaxType purchaseTaxType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(purchaseTaxType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return PartialView(purchaseTaxType);
        }

        // GET: Purchase/PurchaseTaxTypes/Edit/5
         [Authorize(Roles = "Admin,PowerUser")] public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseTaxType = await _context.PurchaseTaxTypes.FindAsync(id);
            if (purchaseTaxType == null)
            {
                return NotFound();
            }
            return PartialView(purchaseTaxType);
        }

        // POST: Purchase/PurchaseTaxTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
       [Authorize(Roles = "Admin,PowerUser")]     public async Task<IActionResult> Edit(int id, [Bind("PurchaseTaxTypeId,TaxName,TaxType,CompositeRate")] PurchaseTaxType purchaseTaxType)
        {
            if (id != purchaseTaxType.PurchaseTaxTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchaseTaxType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseTaxTypeExists(purchaseTaxType.PurchaseTaxTypeId))
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
            return PartialView(purchaseTaxType);
        }

        // GET: Purchase/PurchaseTaxTypes/Delete/5
         [Authorize (Roles = "Admin,PowerUser")]   public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseTaxType = await _context.PurchaseTaxTypes
                .FirstOrDefaultAsync(m => m.PurchaseTaxTypeId == id);
            if (purchaseTaxType == null)
            {
                return NotFound();
            }

            return PartialView(purchaseTaxType);
        }

        // POST: Purchase/PurchaseTaxTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
         [Authorize (Roles = "Admin,PowerUser")]   public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var purchaseTaxType = await _context.PurchaseTaxTypes.FindAsync(id);
            _context.PurchaseTaxTypes.Remove(purchaseTaxType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseTaxTypeExists(int id)
        {
            return _context.PurchaseTaxTypes.Any(e => e.PurchaseTaxTypeId == id);
        }
    }
}
