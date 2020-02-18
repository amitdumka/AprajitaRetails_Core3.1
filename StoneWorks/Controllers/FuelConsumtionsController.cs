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
    
    public class FuelConsumtionsController : Controller
    {
        private readonly StoneWorksContext _context;

        public FuelConsumtionsController(StoneWorksContext context)
        {
            _context = context;
        }

        // GET: StoneWorks/FuelConsumtions
        public async Task<IActionResult> Index()
        {
            return View(await _context.FuelConsumtion.ToListAsync());
        }

        // GET: StoneWorks/FuelConsumtions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fuelConsumtion = await _context.FuelConsumtion
                .FirstOrDefaultAsync(m => m.FuelConsumtionId == id);
            if (fuelConsumtion == null)
            {
                return NotFound();
            }

            return View(fuelConsumtion);
        }

        // GET: StoneWorks/FuelConsumtions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StoneWorks/FuelConsumtions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FuelConsumtionId,Qty,OnDate,AreaOfUse,Remark")] FuelConsumtion fuelConsumtion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fuelConsumtion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fuelConsumtion);
        }

        // GET: StoneWorks/FuelConsumtions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fuelConsumtion = await _context.FuelConsumtion.FindAsync(id);
            if (fuelConsumtion == null)
            {
                return NotFound();
            }
            return View(fuelConsumtion);
        }

        // POST: StoneWorks/FuelConsumtions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FuelConsumtionId,Qty,OnDate,AreaOfUse,Remark")] FuelConsumtion fuelConsumtion)
        {
            if (id != fuelConsumtion.FuelConsumtionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fuelConsumtion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuelConsumtionExists(fuelConsumtion.FuelConsumtionId))
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
            return View(fuelConsumtion);
        }

        // GET: StoneWorks/FuelConsumtions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fuelConsumtion = await _context.FuelConsumtion
                .FirstOrDefaultAsync(m => m.FuelConsumtionId == id);
            if (fuelConsumtion == null)
            {
                return NotFound();
            }

            return View(fuelConsumtion);
        }

        // POST: StoneWorks/FuelConsumtions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fuelConsumtion = await _context.FuelConsumtion.FindAsync(id);
            _context.FuelConsumtion.Remove(fuelConsumtion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FuelConsumtionExists(int id)
        {
            return _context.FuelConsumtion.Any(e => e.FuelConsumtionId == id);
        }
    }
}
