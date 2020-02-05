using Microsoft.AspNetCore.Authorization;    using System;
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
    [Authorize]
    public class TailoringEmployeesController : Controller
    {
        private readonly AprajitaRetailsContext _context;

        public TailoringEmployeesController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: TailoringEmployees
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
           return View(await PaginatedList<TailoringEmployee>.CreateAsync(_context.TailoringEmployees.AsNoTracking(), pageNumber ?? 1, pageSize));
            //return View(await _context.TailoringEmployees.ToListAsync());
        }

        // GET: TailoringEmployees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tailoringEmployee = await _context.TailoringEmployees
                .FirstOrDefaultAsync(m => m.TailoringEmployeeId == id);
            if (tailoringEmployee == null)
            {
                return NotFound();
            }

           return PartialView(tailoringEmployee);
        }

        // GET: TailoringEmployees/Create
        public IActionResult Create()
        {
           return PartialView();
        }

        // POST: TailoringEmployees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TailoringEmployeeId,StaffName,MobileNo,JoiningDate,LeavingDate,IsWorking")] TailoringEmployee tailoringEmployee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tailoringEmployee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           return PartialView(tailoringEmployee);
        }

        // GET: TailoringEmployees/Edit/5
         [Authorize(Roles = "Admin,PowerUser")] public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tailoringEmployee = await _context.TailoringEmployees.FindAsync(id);
            if (tailoringEmployee == null)
            {
                return NotFound();
            }
           return PartialView(tailoringEmployee);
        }

        // POST: TailoringEmployees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
       [Authorize(Roles = "Admin,PowerUser")]     public async Task<IActionResult> Edit(int id, [Bind("TailoringEmployeeId,StaffName,MobileNo,JoiningDate,LeavingDate,IsWorking")] TailoringEmployee tailoringEmployee)
        {
            if (id != tailoringEmployee.TailoringEmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tailoringEmployee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TailoringEmployeeExists(tailoringEmployee.TailoringEmployeeId))
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
           return PartialView(tailoringEmployee);
        }

        // GET: TailoringEmployees/Delete/5
         [Authorize (Roles = "Admin,PowerUser")]   public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tailoringEmployee = await _context.TailoringEmployees
                .FirstOrDefaultAsync(m => m.TailoringEmployeeId == id);
            if (tailoringEmployee == null)
            {
                return NotFound();
            }

           return PartialView(tailoringEmployee);
        }

        // POST: TailoringEmployees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
         [Authorize (Roles = "Admin,PowerUser")]   public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tailoringEmployee = await _context.TailoringEmployees.FindAsync(id);
            _context.TailoringEmployees.Remove(tailoringEmployee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TailoringEmployeeExists(int id)
        {
            return _context.TailoringEmployees.Any(e => e.TailoringEmployeeId == id);
        }
    }
}
