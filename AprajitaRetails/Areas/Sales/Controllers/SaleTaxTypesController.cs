﻿using AprajitaRetails.Areas.Sales.Models;
using AprajitaRetails.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AprajitaRetails.Areas.Sales.Controllers
{
    [Area("Sales")]
    [Authorize]
    public class SaleTaxTypesController : Controller
    {
        private readonly AprajitaRetailsContext _context;

        public SaleTaxTypesController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: Sales/SaleTaxTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.SaleTaxTypes.ToListAsync());
        }

        // GET: Sales/SaleTaxTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleTaxType = await _context.SaleTaxTypes
                .FirstOrDefaultAsync(m => m.SaleTaxTypeId == id);
            if (saleTaxType == null)
            {
                return NotFound();
            }

            return View(saleTaxType);
        }

        // GET: Sales/SaleTaxTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sales/SaleTaxTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SaleTaxTypeId,TaxName,TaxType,CompositeRate")] SaleTaxType saleTaxType)
        {
            if (ModelState.IsValid)
            {

                _context.Add(saleTaxType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(saleTaxType);
        }

        // GET: Sales/SaleTaxTypes/Edit/5
        [Authorize(Roles = "Admin,PowerUser")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleTaxType = await _context.SaleTaxTypes.FindAsync(id);
            if (saleTaxType == null)
            {
                return NotFound();
            }
            return View(saleTaxType);
        }

        // POST: Sales/SaleTaxTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,PowerUser")]
        public async Task<IActionResult> Edit(int id, [Bind("SaleTaxTypeId,TaxName,TaxType,CompositeRate")] SaleTaxType saleTaxType)
        {
            if (id != saleTaxType.SaleTaxTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(saleTaxType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SaleTaxTypeExists(saleTaxType.SaleTaxTypeId))
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
            return View(saleTaxType);
        }

        // GET: Sales/SaleTaxTypes/Delete/5
        [Authorize(Roles = "Admin,PowerUser")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleTaxType = await _context.SaleTaxTypes
                .FirstOrDefaultAsync(m => m.SaleTaxTypeId == id);
            if (saleTaxType == null)
            {
                return NotFound();
            }

            return View(saleTaxType);
        }

        // POST: Sales/SaleTaxTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,PowerUser")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var saleTaxType = await _context.SaleTaxTypes.FindAsync(id);
            _context.SaleTaxTypes.Remove(saleTaxType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SaleTaxTypeExists(int id)
        {
            return _context.SaleTaxTypes.Any(e => e.SaleTaxTypeId == id);
        }
    }
}
