using Microsoft.AspNetCore.Authorization;    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AprajitaRetails.Areas.Uploader.Models;
using AprajitaRetails.Data;

namespace AprajitaRetails.Areas.Imported.Controllers
{
    [Area("Imported")]
    [Authorize]
    public class ImportInWardsController : Controller
    {
        private readonly AprajitaRetailsContext _context;

        public ImportInWardsController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: Uploader/ImportInWards
        public async Task<IActionResult> Index()
        {
            return View(await _context.ImportInWards.ToListAsync());
        }

        // GET: Uploader/ImportInWards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var importInWard = await _context.ImportInWards
                .FirstOrDefaultAsync(m => m.ImportInWardId == id);
            if (importInWard == null)
            {
                return NotFound();
            }

            return View(importInWard);
        }

        // GET: Uploader/ImportInWards/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Uploader/ImportInWards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ImportInWardId,InWardNo,InWardDate,InvoiceNo,InvoiceDate,PartyName,TotalQty,TotalMRPValue,TotalCost,ImportDate")] ImportInWard importInWard)
        {
            if (ModelState.IsValid)
            {
                _context.Add(importInWard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(importInWard);
        }

        // GET: Uploader/ImportInWards/Edit/5
         [Authorize(Roles = "Admin,PowerUser")] public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var importInWard = await _context.ImportInWards.FindAsync(id);
            if (importInWard == null)
            {
                return NotFound();
            }
            return View(importInWard);
        }

        // POST: Uploader/ImportInWards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
       [Authorize(Roles = "Admin,PowerUser")]     public async Task<IActionResult> Edit(int id, [Bind("ImportInWardId,InWardNo,InWardDate,InvoiceNo,InvoiceDate,PartyName,TotalQty,TotalMRPValue,TotalCost,ImportDate")] ImportInWard importInWard)
        {
            if (id != importInWard.ImportInWardId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(importInWard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImportInWardExists(importInWard.ImportInWardId))
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
            return View(importInWard);
        }

        // GET: Uploader/ImportInWards/Delete/5
         [Authorize (Roles = "Admin,PowerUser")]   public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var importInWard = await _context.ImportInWards
                .FirstOrDefaultAsync(m => m.ImportInWardId == id);
            if (importInWard == null)
            {
                return NotFound();
            }

            return View(importInWard);
        }

        // POST: Uploader/ImportInWards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
         [Authorize (Roles = "Admin,PowerUser")]   public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var importInWard = await _context.ImportInWards.FindAsync(id);
            _context.ImportInWards.Remove(importInWard);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImportInWardExists(int id)
        {
            return _context.ImportInWards.Any(e => e.ImportInWardId == id);
        }
    }
}
