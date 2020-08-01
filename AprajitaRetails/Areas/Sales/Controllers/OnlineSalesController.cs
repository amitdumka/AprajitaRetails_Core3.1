using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AprajitaRetails.Data;
using AprajitaRetails.Models;
using Microsoft.AspNetCore.Authorization;

namespace AprajitaRetails.Areas.Sales.Controllers
{
    [Area("Sales")]
    [Authorize]
    public class OnlineSalesController : Controller
    {
        private readonly AprajitaRetailsContext _context;

        public OnlineSalesController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: Sales/OnlineSales
        public async Task<IActionResult> Index()
        {
            var aprajitaRetailsContext = _context.OnlineSale.Include(o => o.Vendor);
            return View(await aprajitaRetailsContext.ToListAsync());
        }

        // GET: Sales/OnlineSales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var onlineSale = await _context.OnlineSale
                .Include(o => o.Vendor)
                .FirstOrDefaultAsync(m => m.OnlineSaleId == id);
            if (onlineSale == null)
            {
                return NotFound();
            }

            return View(onlineSale);
        }

        // GET: Sales/OnlineSales/Create
        public IActionResult Create()
        {
            ViewData["OnlineVendorId"] = new SelectList(_context.Set<OnlineVendor>(), "OnlineVendorId", "OnlineVendorId");
            return View();
        }

        // POST: Sales/OnlineSales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OnlineSaleId,SaleDate,InvNo,Amount,VoyagerInvoiceNo,VoygerDate,VoyagerAmount,ShippingMode,VendorFee,ProfitValue,Remarks,OnlineVendorId")] OnlineSale onlineSale)
        {
            if (ModelState.IsValid)
            {
                _context.Add(onlineSale);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OnlineVendorId"] = new SelectList(_context.Set<OnlineVendor>(), "OnlineVendorId", "OnlineVendorId", onlineSale.OnlineVendorId);
            return View(onlineSale);
        }

        // GET: Sales/OnlineSales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var onlineSale = await _context.OnlineSale.FindAsync(id);
            if (onlineSale == null)
            {
                return NotFound();
            }
            ViewData["OnlineVendorId"] = new SelectList(_context.Set<OnlineVendor>(), "OnlineVendorId", "OnlineVendorId", onlineSale.OnlineVendorId);
            return View(onlineSale);
        }

        // POST: Sales/OnlineSales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OnlineSaleId,SaleDate,InvNo,Amount,VoyagerInvoiceNo,VoygerDate,VoyagerAmount,ShippingMode,VendorFee,ProfitValue,Remarks,OnlineVendorId")] OnlineSale onlineSale)
        {
            if (id != onlineSale.OnlineSaleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(onlineSale);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OnlineSaleExists(onlineSale.OnlineSaleId))
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
            ViewData["OnlineVendorId"] = new SelectList(_context.Set<OnlineVendor>(), "OnlineVendorId", "OnlineVendorId", onlineSale.OnlineVendorId);
            return View(onlineSale);
        }

        // GET: Sales/OnlineSales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var onlineSale = await _context.OnlineSale
                .Include(o => o.Vendor)
                .FirstOrDefaultAsync(m => m.OnlineSaleId == id);
            if (onlineSale == null)
            {
                return NotFound();
            }

            return View(onlineSale);
        }

        // POST: Sales/OnlineSales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var onlineSale = await _context.OnlineSale.FindAsync(id);
            _context.OnlineSale.Remove(onlineSale);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OnlineSaleExists(int id)
        {
            return _context.OnlineSale.Any(e => e.OnlineSaleId == id);
        }
    }
}
