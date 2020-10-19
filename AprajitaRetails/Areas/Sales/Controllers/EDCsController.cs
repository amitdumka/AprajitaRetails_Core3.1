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
    public class EDCsController : Controller
    {
        private readonly AprajitaRetailsContext _context;

        public EDCsController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: Sales/EDCs
        public async Task<IActionResult> Index()
        {
            var aprajitaRetailsContext = _context.CardMachine.Include(e => e.Store);
            return View(await aprajitaRetailsContext.ToListAsync());
        }

        // GET: Sales/EDCs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eDC = await _context.CardMachine
                .Include(e => e.Store)
                .FirstOrDefaultAsync(m => m.EDCId == id);
            if (eDC == null)
            {
                return NotFound();
            }

            return View(eDC);
        }

        // GET: Sales/EDCs/Create
        public IActionResult Create()
        {
            ViewData["StoreId"] = new SelectList(_context.Stores, "StoreId", "StoreId");
            return View();
        }

        // POST: Sales/EDCs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EDCId,TID,EDCName,AccountNumberId,StartDate,EndDate,IsWorking,MID,Remark,StoreId")] EDC eDC)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eDC);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StoreId"] = new SelectList(_context.Stores, "StoreId", "StoreId", eDC.StoreId);
            return View(eDC);
        }

        // GET: Sales/EDCs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eDC = await _context.CardMachine.FindAsync(id);
            if (eDC == null)
            {
                return NotFound();
            }
            ViewData["StoreId"] = new SelectList(_context.Stores, "StoreId", "StoreId", eDC.StoreId);
            return View(eDC);
        }

        // POST: Sales/EDCs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EDCId,TID,EDCName,AccountNumberId,StartDate,EndDate,IsWorking,MID,Remark,StoreId")] EDC eDC)
        {
            if (id != eDC.EDCId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eDC);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EDCExists(eDC.EDCId))
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
            ViewData["StoreId"] = new SelectList(_context.Stores, "StoreId", "StoreId", eDC.StoreId);
            return View(eDC);
        }

        // GET: Sales/EDCs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eDC = await _context.CardMachine
                .Include(e => e.Store)
                .FirstOrDefaultAsync(m => m.EDCId == id);
            if (eDC == null)
            {
                return NotFound();
            }

            return View(eDC);
        }

        // POST: Sales/EDCs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eDC = await _context.CardMachine.FindAsync(id);
            _context.CardMachine.Remove(eDC);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EDCExists(int id)
        {
            return _context.CardMachine.Any(e => e.EDCId == id);
        }
    }
}
