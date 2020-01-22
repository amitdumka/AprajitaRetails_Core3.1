using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AprajitaRetails.Areas.Sales.Models;
using AprajitaRetails.Areas.Voyager.Data;

namespace AprajitaRetails.Areas.Sales.Controllers
{
    [Area("Sales")]
    public class SaleInvoicesController : Controller
    {
        private readonly VoyagerContext _context;

        public SaleInvoicesController(VoyagerContext context)
        {
            _context = context;
        }

        // GET: Sales/SaleInvoices
        public async Task<IActionResult> Index()
        {
            return View(await _context.SaleInvoices.ToListAsync());
        }

        // GET: Sales/SaleInvoices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleInvoice = await _context.SaleInvoices
                .FirstOrDefaultAsync(m => m.SaleInvoiceId == id);
            if (saleInvoice == null)
            {
                return NotFound();
            }

            return View(saleInvoice);
        }

        // GET: Sales/SaleInvoices/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sales/SaleInvoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SaleInvoiceId,CustomerId,OnDate,InvoiceNo,TotalItems,TotalQty,TotalBillAmount,TotalDiscountAmount,RoundOffAmount,TotalTaxAmount")] SaleInvoice saleInvoice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(saleInvoice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(saleInvoice);
        }

        // GET: Sales/SaleInvoices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleInvoice = await _context.SaleInvoices.FindAsync(id);
            if (saleInvoice == null)
            {
                return NotFound();
            }
            return View(saleInvoice);
        }

        // POST: Sales/SaleInvoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SaleInvoiceId,CustomerId,OnDate,InvoiceNo,TotalItems,TotalQty,TotalBillAmount,TotalDiscountAmount,RoundOffAmount,TotalTaxAmount")] SaleInvoice saleInvoice)
        {
            if (id != saleInvoice.SaleInvoiceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(saleInvoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SaleInvoiceExists(saleInvoice.SaleInvoiceId))
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
            return View(saleInvoice);
        }

        // GET: Sales/SaleInvoices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleInvoice = await _context.SaleInvoices
                .FirstOrDefaultAsync(m => m.SaleInvoiceId == id);
            if (saleInvoice == null)
            {
                return NotFound();
            }

            return View(saleInvoice);
        }

        // POST: Sales/SaleInvoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var saleInvoice = await _context.SaleInvoices.FindAsync(id);
            _context.SaleInvoices.Remove(saleInvoice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SaleInvoiceExists(int id)
        {
            return _context.SaleInvoices.Any(e => e.SaleInvoiceId == id);
        }
    }
}
