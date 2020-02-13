using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AprajitaRetails.Areas.StoneWorks.Models;
using AprajitaRetails.Areas.Voyager.Data;

namespace AprajitaRetails.Areas.StoneWorks.Controllers
{
    [Area("StoneWorks")]
    public class FuelsController : Controller
    {
        private readonly VoyagerContext _context;

        public FuelsController(VoyagerContext context)
        {
            _context = context;
        }

        // GET: StoneWorks/Fuels
        public async Task<IActionResult> Index()
        {
            return View(await _context.Fuel.ToListAsync());
        }

        // GET: StoneWorks/Fuels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fuel = await _context.Fuel
                .FirstOrDefaultAsync(m => m.FuelId == id);
            if (fuel == null)
            {
                return NotFound();
            }

            return View(fuel);
        }

        // GET: StoneWorks/Fuels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StoneWorks/Fuels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FuelId,OnDate,PartyName,Qty,Amount,PaymentDate,Remarks,IsOnVechile")] Fuel fuel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fuel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fuel);
        }

        // GET: StoneWorks/Fuels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fuel = await _context.Fuel.FindAsync(id);
            if (fuel == null)
            {
                return NotFound();
            }
            return View(fuel);
        }

        // POST: StoneWorks/Fuels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FuelId,OnDate,PartyName,Qty,Amount,PaymentDate,Remarks,IsOnVechile")] Fuel fuel)
        {
            if (id != fuel.FuelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fuel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuelExists(fuel.FuelId))
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
            return View(fuel);
        }

        // GET: StoneWorks/Fuels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fuel = await _context.Fuel
                .FirstOrDefaultAsync(m => m.FuelId == id);
            if (fuel == null)
            {
                return NotFound();
            }

            return View(fuel);
        }

        // POST: StoneWorks/Fuels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fuel = await _context.Fuel.FindAsync(id);
            _context.Fuel.Remove(fuel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FuelExists(int id)
        {
            return _context.Fuel.Any(e => e.FuelId == id);
        }
    }
}
