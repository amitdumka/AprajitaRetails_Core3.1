using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AprajitaRetails.Data;
using AprajitaRetails.Models;
using TAS_AprajiataRetails.Models.Helpers;
using AprajitaRetails.Ops.TAS.Mails;
using AprajitaRetails.Ops.Triggers;
using Microsoft.AspNetCore.Authorization;

namespace AprajitaRetails.Areas.PayRoll.Controllers
{
    [Area ("PayRoll")]
    [Authorize]
    public class AttendancesController : Controller
    {
        private readonly AprajitaRetailsContext _context;

        public AttendancesController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: Attendances
        public async Task<IActionResult> Index(int? id, string currentFilter, string searchString, int? pageNumber)
        {
            if ( searchString != null )
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData ["CurrentFilter"] = searchString;
            var aprajitaRetailsContext = _context.Attendances.Include (a => a.Employee).Where (c => c.AttDate == DateTime.Today);

            if ( id == 101 )
            {
                aprajitaRetailsContext = _context.Attendances.Include (a => a.Employee).OrderByDescending (c => c.AttDate).ThenBy (c => c.EmployeeId);
                //return View(await aprajitaRetailsContext_all.ToListAsync());
            }
            else if ( id == 100 )
            {
                aprajitaRetailsContext = _context.Attendances.Include (a => a.Employee).Where (c => c.AttDate.Month == DateTime.Today.Month).OrderByDescending (c => c.AttDate).ThenBy (c => c.EmployeeId);
                //return View(await aprajitaRetailsContext_all.ToListAsync());
            }

            //return View(await aprajitaRetailsContext.ToListAsync());

            int pageSize = 10;
            return View (await PaginatedList<Attendance>.CreateAsync (aprajitaRetailsContext.AsNoTracking (), pageNumber ?? 1, pageSize));
        }


        public async Task<IActionResult> EmpDetails(int? id)
        {
            if ( id == null )
            {
                return NotFound ();
            }
            //Attendance attendance = db.Attendances.Include (c => c.Employee).Where (c => c.AttendanceId == id).FirstOrDefault ();

            var empid = _context.Attendances.Find (id).EmployeeId;
            var attList = _context.Attendances.Include (c => c.Employee)
                .Where (c => c.EmployeeId == empid && c.AttDate.Month == DateTime.Today.Month)
                .OrderBy (c => c.AttDate);

            var p = attList.Where (c => c.Status == AttUnits.Present).Count ();
            var a = attList.Where (c => c.Status == AttUnits.Absent).Count ();
            int noofdays = DateTime.DaysInMonth (DateTime.Today.Year, DateTime.Today.Month);
            int noofsunday = DateHelper.CountDays (DayOfWeek.Sunday, DateTime.Today);
            int sunPresent = attList.Where (c => c.Status == AttUnits.Sunday).Count ();
            int halfDays = attList.Where (c => c.Status == AttUnits.HalfDay).Count ();
            int totalAtt = p + sunPresent + ( halfDays / 2 );

            ViewBag.Present = p;
            ViewBag.Absent = a;
            ViewBag.WorkingDays = noofdays;
            ViewBag.Sundays = noofsunday;
            ViewBag.SundayPresent = sunPresent;
            ViewBag.HalfDays = halfDays;
            ViewBag.Total = totalAtt;


            if ( attList == null )
            {
                return NotFound ();
            }
            if ( attList.Any () )
                ViewBag.EmpName = attList.First ().Employee.StaffName;
            return PartialView (await attList.ToListAsync ());
        }
        // GET: Attendances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if ( id == null )
            {
                return NotFound ();
            }

            var attendance = await _context.Attendances.Include (a => a.Employee).FirstOrDefaultAsync (m => m.AttendanceId == id);
            if ( attendance == null )
            {
                return NotFound ();
            }

            return PartialView (attendance);
        }

        // GET: Attendances/Create
        public IActionResult Create()
        {
            ViewData ["EmployeeId"] = new SelectList (_context.Employees, "EmployeeId", "StaffName");
            return PartialView ();
        }

        // POST: Attendances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind ("AttendanceId,EmployeeId,AttDate,EntryTime,Status,Remarks")] Attendance attendance)
        {
            if ( ModelState.IsValid )
            {
                _context.Add (attendance);
                await _context.SaveChangesAsync ();
                new PayRollManager ().ONInsertOrUpdate (_context, attendance, false, false);

                return RedirectToAction (nameof (Index));
            }
            ViewData ["EmployeeId"] = new SelectList (_context.Employees, "EmployeeId", "StaffName", attendance.EmployeeId);
            return PartialView (attendance);
        }

        // GET: Attendances/Edit/5
        [Authorize (Roles = "Admin,PowerUser")]
        public async Task<IActionResult> Edit(int? id)
        {
            if ( id == null )
            {
                return NotFound ();
            }

            var attendance = await _context.Attendances.FindAsync (id);
            if ( attendance == null )
            {
                return NotFound ();
            }
            ViewData ["EmployeeId"] = new SelectList (_context.Employees, "EmployeeId", "StaffName", attendance.EmployeeId);
            return PartialView (attendance);
        }

        // POST: Attendances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
         [Authorize (Roles = "Admin,PowerUser")]   [Authorize(Roles = "Admin,PowerUser")]     public async Task<IActionResult> Edit(int id, [Bind("AttendanceId,EmployeeId,AttDate,EntryTime,Status,Remarks")] Attendance attendance)
        {
            if ( id != attendance.AttendanceId )
            {
                return NotFound ();
            }

            if ( ModelState.IsValid )
            {
                try
                {
                    new PayRollManager ().ONInsertOrUpdate (_context, attendance, false, true);
                    _context.Update (attendance);
                    await _context.SaveChangesAsync ();
                }
                catch ( DbUpdateConcurrencyException )
                {
                    if ( !AttendanceExists (attendance.AttendanceId) )
                    {
                        return NotFound ();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction (nameof (Index));
            }
            ViewData ["EmployeeId"] = new SelectList (_context.Employees, "EmployeeId", "StaffName", attendance.EmployeeId);
            return PartialView (attendance);
        }

        // GET: Attendances/Delete/5
         [Authorize (Roles = "Admin,PowerUser")]   public async Task<IActionResult> Delete(int? id)
        {
            if ( id == null )
            {
                return NotFound ();
            }

            var attendance = await _context.Attendances
                .Include (a => a.Employee)
                .FirstOrDefaultAsync (m => m.AttendanceId == id);
            if ( attendance == null )
            {
                return NotFound ();
            }

            return PartialView (attendance);
        }

        // POST: Attendances/Delete/5
        [HttpPost, ActionName ("Delete")]
        [ValidateAntiForgeryToken]
         [Authorize (Roles = "Admin,PowerUser")]   public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var attendance = await _context.Attendances.FindAsync (id);
            new PayRollManager ().ONInsertOrUpdate (_context, attendance, true, false);
            _context.Attendances.Remove (attendance);
            await _context.SaveChangesAsync ();
            return RedirectToAction (nameof (Index));
        }

        private bool AttendanceExists(int id)
        {
            return _context.Attendances.Any (e => e.AttendanceId == id);
        }
    }
}
