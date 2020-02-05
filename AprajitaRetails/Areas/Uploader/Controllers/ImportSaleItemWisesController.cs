using Microsoft.AspNetCore.Authorization;    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AprajitaRetails.Areas.Uploader.Models;
using AprajitaRetails.Areas.Voyager.Data;
using Microsoft.AspNetCore.Authorization;

namespace AprajitaRetails.Areas.Uploader.Controllers
{
    [Area("Uploader")]
    public class ImportSaleItemWisesController : Controller
    {
        private readonly VoyagerContext _context;

        public ImportSaleItemWisesController(VoyagerContext context)
        {
            _context = context;
        }

        // GET: Uploader/ImportSaleItemWises
        public async Task<IActionResult> Index()
        {
            return View(await _context.ImportSaleItemWises.ToListAsync());
        }

        // GET: Uploader/ImportSaleItemWises/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var importSaleItemWise = await _context.ImportSaleItemWises
                .FirstOrDefaultAsync(m => m.ImportSaleItemWiseId == id);
            if (importSaleItemWise == null)
            {
                return NotFound();
            }

            return View(importSaleItemWise);
        }

        // GET: Uploader/ImportSaleItemWises/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Uploader/ImportSaleItemWises/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ImportSaleItemWiseId,InvoiceDate,InvoiceNo,InvoiceType,BrandName,ProductName,ItemDesc,HSNCode,Barcode,StyleCode,Quantity,MRP,Discount,BasicRate,Tax,SGST,CGST,LineTotal,RoundOff,BillAmnt,PaymentType,Saleman,IsDataConsumed,ImportTime")] ImportSaleItemWise importSaleItemWise)
        {
            if (ModelState.IsValid)
            {
                _context.Add(importSaleItemWise);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(importSaleItemWise);
        }

        // GET: Uploader/ImportSaleItemWises/Edit/5
         [Authorize(Roles = "Admin,PowerUser")] public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var importSaleItemWise = await _context.ImportSaleItemWises.FindAsync(id);
            if (importSaleItemWise == null)
            {
                return NotFound();
            }
            return View(importSaleItemWise);
        }

        // POST: Uploader/ImportSaleItemWises/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
       [Authorize(Roles = "Admin,PowerUser")]     public async Task<IActionResult> Edit(int id, [Bind("ImportSaleItemWiseId,InvoiceDate,InvoiceNo,InvoiceType,BrandName,ProductName,ItemDesc,HSNCode,Barcode,StyleCode,Quantity,MRP,Discount,BasicRate,Tax,SGST,CGST,LineTotal,RoundOff,BillAmnt,PaymentType,Saleman,IsDataConsumed,ImportTime")] ImportSaleItemWise importSaleItemWise)
        {
            if (id != importSaleItemWise.ImportSaleItemWiseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(importSaleItemWise);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImportSaleItemWiseExists(importSaleItemWise.ImportSaleItemWiseId))
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
            return View(importSaleItemWise);
        }

        // GET: Uploader/ImportSaleItemWises/Delete/5
         [Authorize (Roles = "Admin,PowerUser")]   public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var importSaleItemWise = await _context.ImportSaleItemWises
                .FirstOrDefaultAsync(m => m.ImportSaleItemWiseId == id);
            if (importSaleItemWise == null)
            {
                return NotFound();
            }

            return View(importSaleItemWise);
        }

        // POST: Uploader/ImportSaleItemWises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
         [Authorize (Roles = "Admin,PowerUser")]   public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var importSaleItemWise = await _context.ImportSaleItemWises.FindAsync(id);
            _context.ImportSaleItemWises.Remove(importSaleItemWise);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImportSaleItemWiseExists(int id)
        {
            return _context.ImportSaleItemWises.Any(e => e.ImportSaleItemWiseId == id);
        }
    }
}
