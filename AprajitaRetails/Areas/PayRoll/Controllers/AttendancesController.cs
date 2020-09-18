using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AprajitaRetails.Data;
using AprajitaRetails.Models;
using AprajitaRetails.Ops.Triggers;
using AprajitaRetails.Models.Helpers;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using Castle.Core.Internal;
using System.Runtime.Serialization;
using System.Globalization;

namespace AprajitaRetails.Areas.PayRoll.Controllers
{
    [Area("PayRoll")]
    [Authorize]
    public class AttendancesController : Controller
    {
        private readonly AprajitaRetailsContext _context;
        private readonly int StoreId=1;  // TODO: default storeId

        public AttendancesController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: Attendances
        public async Task<IActionResult> Index(int? id, string currentFilter, string searchString, int? pageNumber, string MonthName)
        {
            // TODO: implement StoreID on this
            //string mName = "Jan";
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            var aprajitaRetailsContext = _context.Attendances.Include(a => a.Employee).Where(c => c.AttDate == DateTime.Today && c.StoreId==StoreId);

            var YearList = _context.Attendances.GroupBy(c => c.AttDate.Year).Select(c => c.Key).ToList();
            YearList.Sort();
            ViewBag.YearList = YearList;
            var MonthList = DateTimeFormatInfo.CurrentInfo.MonthNames.ToList();

            ViewBag.MonthList = MonthList;

            if (id == 101)
            {
                aprajitaRetailsContext = _context.Attendances.Include(a => a.Employee).Where(c => c.StoreId == StoreId).OrderByDescending(c => c.AttDate).ThenBy(c => c.EmployeeId);
                //return View(await aprajitaRetailsContext_all.ToListAsync());
            }
            else if (id == 100)
            {
                aprajitaRetailsContext = _context.Attendances.Include(a => a.Employee).Where(c => c.AttDate.Month == DateTime.Today.Month && c.AttDate.Year == DateTime.Today.Year && c.StoreId == StoreId).OrderByDescending(c => c.AttDate).ThenBy(c => c.EmployeeId);
                //return View(await aprajitaRetailsContext_all.ToListAsync());
            }
            else if (id == 102)
            {
                aprajitaRetailsContext = _context.Attendances.Include(a => a.Employee).Where(c => c.AttDate.Month == DateTime.Today.Month - 1 && c.AttDate.Year == DateTime.Today.Year && c.StoreId == StoreId).OrderByDescending(c => c.AttDate).ThenBy(c => c.EmployeeId);
            }
            else
            {
                if (id != null && YearList.Contains((int)id))
                {
                    ViewBag.YearName = id;
                    if (!MonthName.IsNullOrEmpty())
                    {
                        //  mName = MonthName;
                        int mn = MonthList.IndexOf(MonthName) + 1;
                        aprajitaRetailsContext = _context.Attendances.Include(a => a.Employee).Where(c => c.AttDate.Year == id && c.AttDate.Month == mn && c.StoreId == StoreId).OrderByDescending(c => c.AttDate).ThenBy(c => c.EmployeeId);

                    }
                    else
                        aprajitaRetailsContext = _context.Attendances.Include(a => a.Employee).Where(c => c.AttDate.Year == id && c.StoreId == StoreId).OrderByDescending(c => c.AttDate).ThenBy(c => c.EmployeeId);
                }
                else
                {

                }

            }

            //return View(await aprajitaRetailsContext.ToListAsync());

            int pageSize = 10;
            return View(await PaginatedList<Attendance>.CreateAsync(aprajitaRetailsContext.AsNoTracking(), pageNumber ?? 1, pageSize));
        }


        public async Task<IActionResult> EmpDetails(int? id, string? ondate)
        {
            if (id == null)
            {
                return NotFound();
            }
            //ToString("dd-MM-yyyy")
            var empid = _context.Attendances.Find(id).EmployeeId;

            DateTime ValidDate = DateTime.Today;
            //if (onDate != null) ValidDate = (DateTime)onDate;
            if (!ondate.IsNullOrEmpty())
            {


                if (!DateTime.TryParse(ondate, out ValidDate))
                {
                    ValidDate = DateTime.Today;
                }
            }


            var attList = _context.Attendances.Include(c => c.Employee)
                .Where(c => c.EmployeeId == empid && c.AttDate.Month == ValidDate.Month && c.AttDate.Year == ValidDate.Year)
                .OrderBy(c => c.AttDate);



            var p = attList.Where(c => c.Status == AttUnits.Present).Count();
            var a = attList.Where(c => c.Status == AttUnits.Absent).Count();
            int noofdays = DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month);
            int noofsunday = DateHelper.CountDays(DayOfWeek.Sunday, DateTime.Today);
            int sunPresent = attList.Where(c => c.Status == AttUnits.Sunday).Count();
            int halfDays = attList.Where(c => c.Status == AttUnits.HalfDay).Count();
            int totalAtt = p + sunPresent + (halfDays / 2);

            ViewBag.Present = p;
            ViewBag.Absent = a;
            ViewBag.WorkingDays = noofdays;
            ViewBag.Sundays = noofsunday;
            ViewBag.SundayPresent = sunPresent;
            ViewBag.HalfDays = halfDays;
            ViewBag.Total = totalAtt;


            if (attList == null)
            {
                return NotFound();
            }
            if (attList.Any())
                ViewBag.EmpName = attList.First().Employee.StaffName;
            return PartialView(await attList.ToListAsync());
        }
        // GET: Attendances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendances.Include(a => a.Employee).FirstOrDefaultAsync(m => m.AttendanceId == id);
            if (attendance == null)
            {
                return NotFound();
            }

            return PartialView(attendance);
        }

        // GET: Attendances/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees.Where(c => c.StoreId == StoreId), "EmployeeId", "StaffName");
            return PartialView();
        }

        // POST: Attendances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AttendanceId,EmployeeId,AttDate,EntryTime,Status,Remarks")] Attendance attendance)
        {
            if (ModelState.IsValid)
            {
                var eType = _context.Employees.Find(attendance.EmployeeId).Category;
                attendance.StoreId = StoreId;

                if (eType == EmpType.Tailors || eType == EmpType.TailoringAssistance || eType == EmpType.TailorMaster)
                {
                    attendance.IsTailoring = true;

                }
                else
                {
                    attendance.IsTailoring = false;
                }
                
                attendance.UserName = User.Identity.Name;
                
                _context.Add(attendance);
                
                await _context.SaveChangesAsync();
                new PayRollManager().ONInsertOrUpdate(_context, attendance, false, false);

                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees.Where(c => c.StoreId == StoreId), "EmployeeId", "StaffName", attendance.EmployeeId);
            return PartialView(attendance);
        }

        // GET: Attendances/Edit/5
        [Authorize(Roles = "Admin,PowerUser,StoreManager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendances.FindAsync(id);
            if (attendance == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees.Where(c => c.StoreId == StoreId), "EmployeeId", "StaffName", attendance.EmployeeId);
            return PartialView(attendance);
        }

        // POST: Attendances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,PowerUser, StoreManager")]

        public async Task<IActionResult> Edit(int id, [Bind("AttendanceId,EmployeeId,AttDate,EntryTime,Status,Remarks")] Attendance attendance)
        {
            if (id != attendance.AttendanceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    new PayRollManager().ONInsertOrUpdate(_context, attendance, false, true);
                    attendance.UserName = User.Identity.Name;
                    attendance.StoreId = StoreId;
                    _context.Update(attendance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttendanceExists(attendance.AttendanceId))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees.Where(c=>c.StoreId==StoreId), "EmployeeId", "StaffName", attendance.EmployeeId);
            return PartialView(attendance);
        }

        // GET: Attendances/Delete/5
        [Authorize(Roles = "Admin,PowerUser")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendances
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(m => m.AttendanceId == id);
            if (attendance == null)
            {
                return NotFound();
            }

            return PartialView(attendance);
        }

        // POST: Attendances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,PowerUser")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var attendance = await _context.Attendances.FindAsync(id);
            new PayRollManager().ONInsertOrUpdate(_context, attendance, true, false);
            _context.Attendances.Remove(attendance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AttendanceExists(int id)
        {
            return _context.Attendances.Any(e => e.AttendanceId == id);
        }
    }
}
