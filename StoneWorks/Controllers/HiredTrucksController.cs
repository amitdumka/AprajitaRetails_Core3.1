using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using  StoneWorks.Models;
 
using  StoneWorks.Data;

namespace  StoneWorks.Controllers
{
    
    public class HiredTrucksController : Controller
    {
        private readonly StoneWorksContext _context;

        public HiredTrucksController(StoneWorksContext context)
        {
            _context = context;
        }

        // GET: StoneWorks/HiredTrucks
        public async Task<IActionResult> Index()
        {
            var StoneWorksContext = _context.HiredTruck.Include(h => h.Trucks);
            return View(await StoneWorksContext.ToListAsync());
        }

        // GET: StoneWorks/HiredTrucks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hiredTruck = await _context.HiredTruck
                .Include(h => h.Trucks)
                .FirstOrDefaultAsync(m => m.HiredTruckId == id);
            if (hiredTruck == null)
            {
                return NotFound();
            }

            return View(hiredTruck);
        }

        // GET: StoneWorks/HiredTrucks/Create
        public IActionResult Create()
        {
            ViewData["TruckId"] = new SelectList(_context.Truck, "TruckId", "TruckId");
            return View();
        }

        // POST: StoneWorks/HiredTrucks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HiredTruckId,TruckId,HiredFrom,Rate,HiredDate,SurrenderDate")] HiredTruck hiredTruck)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hiredTruck);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TruckId"] = new SelectList(_context.Truck, "TruckId", "TruckId", hiredTruck.TruckId);
            return View(hiredTruck);
        }

        // GET: StoneWorks/HiredTrucks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hiredTruck = await _context.HiredTruck.FindAsync(id);
            if (hiredTruck == null)
            {
                return NotFound();
            }
            ViewData["TruckId"] = new SelectList(_context.Truck, "TruckId", "TruckId", hiredTruck.TruckId);
            return View(hiredTruck);
        }

        // POST: StoneWorks/HiredTrucks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HiredTruckId,TruckId,HiredFrom,Rate,HiredDate,SurrenderDate")] HiredTruck hiredTruck)
        {
            if (id != hiredTruck.HiredTruckId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hiredTruck);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HiredTruckExists(hiredTruck.HiredTruckId))
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
            ViewData["TruckId"] = new SelectList(_context.Truck, "TruckId", "TruckId", hiredTruck.TruckId);
            return View(hiredTruck);
        }

        // GET: StoneWorks/HiredTrucks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hiredTruck = await _context.HiredTruck
                .Include(h => h.Trucks)
                .FirstOrDefaultAsync(m => m.HiredTruckId == id);
            if (hiredTruck == null)
            {
                return NotFound();
            }

            return View(hiredTruck);
        }

        // POST: StoneWorks/HiredTrucks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hiredTruck = await _context.HiredTruck.FindAsync(id);
            _context.HiredTruck.Remove(hiredTruck);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HiredTruckExists(int id)
        {
            return _context.HiredTruck.Any(e => e.HiredTruckId == id);
        }
    }
}
