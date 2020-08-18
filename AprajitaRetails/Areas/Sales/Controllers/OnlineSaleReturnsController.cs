using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AprajitaRetails.Data;
using AprajitaRetails.Models;

namespace AprajitaRetails.Areas.Sales.Controllers
{
    [Area("Sales")]
    public class OnlineSaleReturnsController : Controller
    {
        private readonly AprajitaRetailsContext _context;

        public OnlineSaleReturnsController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: Sales/OnlineSaleReturns
        public async Task<IActionResult> Index()
        {
            var aprajitaRetailsContext = _context.OnlineSaleReturns.Include(o => o.OnlineSale);
            return View(await aprajitaRetailsContext.ToListAsync());
        }

        // GET: Sales/OnlineSaleReturns/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var onlineSaleReturn = await _context.OnlineSaleReturns
                .Include(o => o.OnlineSale)
               
                .FirstOrDefaultAsync(m => m.OnlineSaleReturnId == id);
            if (onlineSaleReturn == null)
            {
                return NotFound();
            }

            return View(onlineSaleReturn);
        }

        // GET: Sales/OnlineSaleReturns/Create
        public IActionResult Create()
        {
            ViewData["OnlineSaleId"] = new SelectList(_context.OnlineSale, "OnlineSaleId", "InvNo");
           // ViewData["OnlineVendorId"] = new SelectList(_context.OnlineVendor, "OnlineVendorId", "OnlineVendorId");
            return View();
        }

        // POST: Sales/OnlineSaleReturns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OnlineSaleReturnId,OnlineSaleId,ReturnDate,InvNo,Amount,VoyagerInvoiceNo,VoygerDate,VoyagerAmount,Remarks,IsRecived,RecivedDate")] OnlineSaleReturn onlineSaleReturn)
        {
            if (ModelState.IsValid)
            {
                _context.Add(onlineSaleReturn);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OnlineSaleId"] = new SelectList(_context.OnlineSale, "OnlineSaleId", "InvNo", onlineSaleReturn.OnlineSaleId);
           // ViewData["OnlineVendorId"] = new SelectList(_context.OnlineVendor, "OnlineVendorId", "OnlineVendorId", onlineSaleReturn.OnlineVendorId);
            return View(onlineSaleReturn);
        }

        // GET: Sales/OnlineSaleReturns/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var onlineSaleReturn = await _context.OnlineSaleReturns.FindAsync(id);
            if (onlineSaleReturn == null)
            {
                return NotFound();
            }
            ViewData["OnlineSaleId"] = new SelectList(_context.OnlineSale, "OnlineSaleId", "InvNo", onlineSaleReturn.OnlineSaleId);
           // ViewData["OnlineVendorId"] = new SelectList(_context.OnlineVendor, "OnlineVendorId", "OnlineVendorId", onlineSaleReturn.OnlineVendorId);
            return View(onlineSaleReturn);
        }

        // POST: Sales/OnlineSaleReturns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OnlineSaleReturnId,OnlineSaleId,ReturnDate,InvNo,Amount,VoyagerInvoiceNo,VoygerDate,VoyagerAmount,Remarks,IsRecived,RecivedDate")] OnlineSaleReturn onlineSaleReturn)
        {
            if (id != onlineSaleReturn.OnlineSaleReturnId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(onlineSaleReturn);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OnlineSaleReturnExists(onlineSaleReturn.OnlineSaleReturnId))
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
            ViewData["OnlineSaleId"] = new SelectList(_context.OnlineSale, "OnlineSaleId", "InvNo", onlineSaleReturn.OnlineSaleId);
          //  ViewData["OnlineVendorId"] = new SelectList(_context.OnlineVendor, "OnlineVendorId", "OnlineVendorId", onlineSaleReturn.OnlineVendorId);
            return View(onlineSaleReturn);
        }

        // GET: Sales/OnlineSaleReturns/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var onlineSaleReturn = await _context.OnlineSaleReturns
                .Include(o => o.OnlineSale)
                
                .FirstOrDefaultAsync(m => m.OnlineSaleReturnId == id);
            if (onlineSaleReturn == null)
            {
                return NotFound();
            }

            return View(onlineSaleReturn);
        }

        // POST: Sales/OnlineSaleReturns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var onlineSaleReturn = await _context.OnlineSaleReturns.FindAsync(id);
            _context.OnlineSaleReturns.Remove(onlineSaleReturn);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OnlineSaleReturnExists(int id)
        {
            return _context.OnlineSaleReturns.Any(e => e.OnlineSaleReturnId == id);
        }
    }
}
