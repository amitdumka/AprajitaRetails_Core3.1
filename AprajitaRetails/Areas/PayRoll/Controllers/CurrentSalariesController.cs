using AprajitaRetails.Data;
using AprajitaRetails.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AprajitaRetails.Areas.PayRoll.Controllers
{
    [Area("PayRoll")]
    [Authorize]
    public class CurrentSalariesController : Controller
    {
        private readonly AprajitaRetailsContext _context;

        public CurrentSalariesController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: CurrentSalaries
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
            var aprajitaRetailsContext = _context.CurrentSalaries.Include(c => c.Employee).OrderBy(c => c.IsEffective);

            return View(await PaginatedList<CurrentSalary>.CreateAsync(aprajitaRetailsContext.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: CurrentSalaries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var currentSalary = await _context.CurrentSalaries
                .Include(c => c.Employee)
                .FirstOrDefaultAsync(m => m.CurrentSalaryId == id);
            if (currentSalary == null)
            {
                return NotFound();
            }
            return PartialView(currentSalary);
        }

        // GET: CurrentSalaries/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "StaffName");
            return PartialView();
        }

        // POST: CurrentSalaries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CurrentSalaryId,EmployeeId,BasicSalary,SundaySalary,LPRate,IncentiveRate,IncentiveTarget,WOWBillRate,WOWBillTarget,IsSundayBillable,EffectiveDate,CloseDate,IsEffective")] CurrentSalary currentSalary)
        {
            if (ModelState.IsValid)
            {
                //if ( currentSalary.IsSundayBillable )
                //{
                //    var sunsal = currentSalary.BasicSalary / 30;
                //    if ( currentSalary.SundaySalary != sunsal )
                //        currentSalary.SundaySalary = sunsal;
                //}
                _context.Add(currentSalary);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "StaffName", currentSalary.EmployeeId);
            return PartialView(currentSalary);
        }

        // GET: CurrentSalaries/Edit/5
        [Authorize(Roles = "Admin,PowerUser")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currentSalary = await _context.CurrentSalaries.FindAsync(id);
            if (currentSalary == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "StaffName", currentSalary.EmployeeId);
            return PartialView(currentSalary);
        }

        // POST: CurrentSalaries/Edit/5
        // To protect from over posting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,PowerUser")]
        public async Task<IActionResult> Edit(int id, [Bind("CurrentSalaryId,EmployeeId,BasicSalary,SundaySalary,LPRate,IncentiveRate,IncentiveTarget,WOWBillRate,WOWBillTarget,IsSundayBillable,EffectiveDate,CloseDate,IsEffective")] CurrentSalary currentSalary)
        {
            if (id != currentSalary.CurrentSalaryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //if ( currentSalary.IsSundayBillable )
                    //{
                    //    var sunsal = currentSalary.BasicSalary / 30;
                    //    if ( currentSalary.SundaySalary != sunsal )
                    //        currentSalary.SundaySalary = sunsal;
                    //}
                    _context.Update(currentSalary);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CurrentSalaryExists(currentSalary.CurrentSalaryId))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "StaffName", currentSalary.EmployeeId);
            return PartialView(currentSalary);
        }

        // GET: CurrentSalaries/Delete/5
        [Authorize(Roles = "Admin,PowerUser")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currentSalary = await _context.CurrentSalaries
                .Include(c => c.Employee)
                .FirstOrDefaultAsync(m => m.CurrentSalaryId == id);
            if (currentSalary == null)
            {
                return NotFound();
            }

            return PartialView(currentSalary);
        }

        // POST: CurrentSalaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,PowerUser")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var currentSalary = await _context.CurrentSalaries.FindAsync(id);
            _context.CurrentSalaries.Remove(currentSalary);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CurrentSalaryExists(int id)
        {
            return _context.CurrentSalaries.Any(e => e.CurrentSalaryId == id);
        }
    }
}
