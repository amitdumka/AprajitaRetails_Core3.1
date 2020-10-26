using AprajitaRetails.Data;
using AprajitaRetails.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace AprajitaRetails.Areas.Reports.Controllers
{
    [Area("Reports")]
    [Authorize]
    public class EmpAttReportController : Controller
    {
        private readonly AprajitaRetailsContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public EmpAttReportController(AprajitaRetailsContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }
        // GET: EmpAttReportController
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
            return View(await PaginatedList<Employee>.CreateAsync(_context.Employees.AsNoTracking(), pageNumber ?? 1, pageSize));

        }


        // GET: EmpAttReportController/Details/5
        public ActionResult AttdDetails(int id, DateTime? JoinningDate, DateTime? LeavingDate)
        {
            DateTime sDate, eDate;
            if (JoinningDate != null && LeavingDate != null)
            {
                sDate = JoinningDate.Value.Date; eDate = LeavingDate.Value.Date;
            }
            else
            {
                sDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                eDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month));
            }
            var modelData = Reports.Ops.ReportOps.GetEmployeeAttendanceReport(_context, id, sDate, eDate);
            return View(modelData);
        }
        // GET: EmpAttReportController/Details/5
        public ActionResult FinDetails(int id, DateTime? JoinningDate, DateTime? LeavingDate)
        {
            DateTime sDate, eDate;
            if (JoinningDate != null && LeavingDate != null)
            {
                sDate = JoinningDate.Value.Date;
                eDate = LeavingDate.Value.Date;
            }
            else
            {
                sDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                eDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month));
            }
            var modelData = Reports.Ops.ReportOps.GetEmployeeFinReport(_context, id, sDate, eDate);
            return View(modelData);
        }
        // GET: EmpAttReportController/Create
        public ActionResult Create()
        {
            return View();
        }
    }
}
