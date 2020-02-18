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
    
    public class StaffSalariesController : Controller
    {
        private readonly StoneWorksContext _context;

        public StaffSalariesController(StoneWorksContext context)
        {
            _context = context;
        }

        // GET: StoneWorks/StaffSalaries
        public async Task<IActionResult> Index()
        {
            var StoneWorksContext = _context.StaffSalary.Include(s => s.Staff);
            return View(await StoneWorksContext.ToListAsync());
        }

        // GET: StoneWorks/StaffSalaries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staffSalary = await _context.StaffSalary
                .Include(s => s.Staff)
                .FirstOrDefaultAsync(m => m.StaffSalaryId == id);
            if (staffSalary == null)
            {
                return NotFound();
            }

            return View(staffSalary);
        }

        // GET: StoneWorks/StaffSalaries/Create
        public IActionResult Create()
        {
            ViewData["StaffId"] = new SelectList(_context.Staff, "StaffId", "StaffId");
            return View();
        }

        // POST: StoneWorks/StaffSalaries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StaffSalaryId,OnDate,StaffId,PaidAmount,CalcualteAmount,Remarks")] StaffSalary staffSalary)
        {
            if (ModelState.IsValid)
            {
                _context.Add(staffSalary);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StaffId"] = new SelectList(_context.Staff, "StaffId", "StaffId", staffSalary.StaffId);
            return View(staffSalary);
        }

        // GET: StoneWorks/StaffSalaries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staffSalary = await _context.StaffSalary.FindAsync(id);
            if (staffSalary == null)
            {
                return NotFound();
            }
            ViewData["StaffId"] = new SelectList(_context.Staff, "StaffId", "StaffId", staffSalary.StaffId);
            return View(staffSalary);
        }

        // POST: StoneWorks/StaffSalaries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StaffSalaryId,OnDate,StaffId,PaidAmount,CalcualteAmount,Remarks")] StaffSalary staffSalary)
        {
            if (id != staffSalary.StaffSalaryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(staffSalary);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StaffSalaryExists(staffSalary.StaffSalaryId))
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
            ViewData["StaffId"] = new SelectList(_context.Staff, "StaffId", "StaffId", staffSalary.StaffId);
            return View(staffSalary);
        }

        // GET: StoneWorks/StaffSalaries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staffSalary = await _context.StaffSalary
                .Include(s => s.Staff)
                .FirstOrDefaultAsync(m => m.StaffSalaryId == id);
            if (staffSalary == null)
            {
                return NotFound();
            }

            return View(staffSalary);
        }

        // POST: StoneWorks/StaffSalaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var staffSalary = await _context.StaffSalary.FindAsync(id);
            _context.StaffSalary.Remove(staffSalary);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StaffSalaryExists(int id)
        {
            return _context.StaffSalary.Any(e => e.StaffSalaryId == id);
        }
    }
}
