using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AprajitaRetails.Areas.Reports.Ops;
using AprajitaRetails.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AprajitaRetails.Areas.Reports.Controllers
{
    [Area("Reports")]
    [Authorize]
    public class BookingOverDueController : Controller
    {
        private readonly AprajitaRetailsContext _context;
        public BookingOverDueController(AprajitaRetailsContext context)
        {
           
            _context = context;
            

        }
        public IActionResult Index()
        {
            return View(ReportOps.GetTailoringBookingOverDue(_context));
        }
    }
}
