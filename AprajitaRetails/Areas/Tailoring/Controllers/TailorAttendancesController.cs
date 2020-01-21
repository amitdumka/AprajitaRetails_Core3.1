using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AprajitaRetails.Data;
using AprajitaRetails.Models;

namespace AprajitaRetails.Areas.Tailoring.Controllers
{
    [Area ("Tailoring")]
    public class TailorAttendancesController : Controller
    {
        private readonly AprajitaRetailsContext _context;

        public TailorAttendancesController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: TailorAttendances
        public async Task<IActionResult> Index(string currentFilter, string searchString, int? pageNumber)
        {
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }


            ViewData["CurrentFilter"] = searchString;
            int pageSize = 10;
            var aprajitaRetailsContext = _context.TailorAttendances.Include(t => t.Employee);
           return View(await PaginatedList<TailorAttendance>.CreateAsync(aprajitaRetailsContext.AsNoTracking(), pageNumber ?? 1, pageSize));
           //return PartialView(await aprajitaRetailsContext.ToListAsync());
        }

        // GET: TailorAttendances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tailorAttendance = await _context.TailorAttendances
                .Include(t => t.Employee)
                .FirstOrDefaultAsync(m => m.TailorAttendanceId == id);
            if (tailorAttendance == null)
            {
                return NotFound();
            }

           return PartialView(tailorAttendance);
        }

        // GET: TailorAttendances/Create
        public IActionResult Create()
        {
            ViewData["TailoringEmployeeId"] = new SelectList(_context.TailoringEmployees, "TailoringEmployeeId", "TailoringEmployeeId");
           return PartialView();
        }

        // POST: TailorAttendances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TailorAttendanceId,TailoringEmployeeId,AttDate,EntryTime,Status,Remarks")] TailorAttendance tailorAttendance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tailorAttendance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TailoringEmployeeId"] = new SelectList(_context.TailoringEmployees, "TailoringEmployeeId", "TailoringEmployeeId", tailorAttendance.TailoringEmployeeId);
           return PartialView(tailorAttendance);
        }

        // GET: TailorAttendances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tailorAttendance = await _context.TailorAttendances.FindAsync(id);
            if (tailorAttendance == null)
            {
                return NotFound();
            }
            ViewData["TailoringEmployeeId"] = new SelectList(_context.TailoringEmployees, "TailoringEmployeeId", "TailoringEmployeeId", tailorAttendance.TailoringEmployeeId);
           return PartialView(tailorAttendance);
        }

        // POST: TailorAttendances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TailorAttendanceId,TailoringEmployeeId,AttDate,EntryTime,Status,Remarks")] TailorAttendance tailorAttendance)
        {
            if (id != tailorAttendance.TailorAttendanceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tailorAttendance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TailorAttendanceExists(tailorAttendance.TailorAttendanceId))
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
            ViewData["TailoringEmployeeId"] = new SelectList(_context.TailoringEmployees, "TailoringEmployeeId", "TailoringEmployeeId", tailorAttendance.TailoringEmployeeId);
           return PartialView(tailorAttendance);
        }

        // GET: TailorAttendances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tailorAttendance = await _context.TailorAttendances
                .Include(t => t.Employee)
                .FirstOrDefaultAsync(m => m.TailorAttendanceId == id);
            if (tailorAttendance == null)
            {
                return NotFound();
            }

           return PartialView(tailorAttendance);
        }

        // POST: TailorAttendances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tailorAttendance = await _context.TailorAttendances.FindAsync(id);
            _context.TailorAttendances.Remove(tailorAttendance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TailorAttendanceExists(int id)
        {
            return _context.TailorAttendances.Any(e => e.TailorAttendanceId == id);
        }
    }
}
