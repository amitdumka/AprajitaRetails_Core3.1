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
    
    public class DailyLaborsController : Controller
    {
        private readonly StoneWorksContext _context;

        public DailyLaborsController(StoneWorksContext context)
        {
            _context = context;
        }
        // GET: StoneWorks/DailyLabors
        public async Task<IActionResult> Index()
        {
            return View(await _context.DailyLabor.ToListAsync());
        }

        // GET: StoneWorks/DailyLabors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dailyLabor = await _context.DailyLabor
                .FirstOrDefaultAsync(m => m.DailyLaborId == id);
            if (dailyLabor == null)
            {
                return NotFound();
            }

            return View(dailyLabor);
        }

        // GET: StoneWorks/DailyLabors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StoneWorks/DailyLabors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DailyLaborId,Name,OnDate,IsPresent,IsDailyBillable,Amount,ExtraAmount,Remarks")] DailyLabor dailyLabor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dailyLabor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dailyLabor);
        }

        // GET: StoneWorks/DailyLabors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dailyLabor = await _context.DailyLabor.FindAsync(id);
            if (dailyLabor == null)
            {
                return NotFound();
            }
            return View(dailyLabor);
        }

        // POST: StoneWorks/DailyLabors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DailyLaborId,Name,OnDate,IsPresent,IsDailyBillable,Amount,ExtraAmount,Remarks")] DailyLabor dailyLabor)
        {
            if (id != dailyLabor.DailyLaborId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dailyLabor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DailyLaborExists(dailyLabor.DailyLaborId))
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
            return View(dailyLabor);
        }

        // GET: StoneWorks/DailyLabors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dailyLabor = await _context.DailyLabor
                .FirstOrDefaultAsync(m => m.DailyLaborId == id);
            if (dailyLabor == null)
            {
                return NotFound();
            }

            return View(dailyLabor);
        }

        // POST: StoneWorks/DailyLabors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dailyLabor = await _context.DailyLabor.FindAsync(id);
            _context.DailyLabor.Remove(dailyLabor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DailyLaborExists(int id)
        {
            return _context.DailyLabor.Any(e => e.DailyLaborId == id);
        }
    }
}
