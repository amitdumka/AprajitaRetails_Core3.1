using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AprajitaRetails.Areas.Reports.Data;
using AprajitaRetails.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AprajitaRetails.Areas.Reports.Controllers
{

    [Area("Reports")]
    public class ReportExpoterController : Controller
    {
        private readonly List<string> TableList = new List<string> {"Payment", "Reciept","Cash Expenses", "Cash Payment", "Booking","Delivery","DailySale", "Expenses", "Salary", "Adv Reciept", "Dues Report", "ManualSale"};
        private readonly AprajitaRetailsContext db;
        public ReportExpoterController(AprajitaRetailsContext c)
        {
            db = c;
        }
        public IActionResult Index()
        {        
            ViewData["TableList"] = new SelectList(TableList);
            return View();
        }

        public ActionResult ReportViewer(string OutPutType, DateTime toDate, DateTime fromDate, FunctionName tableLists)
        {
            ViewBag.DateFrom = fromDate;
            ViewBag.DateTo = toDate;
            ViewBag.OutPut = OutPutType;
            ViewBag.TableType = tableLists;




            return View();
        }
    }
}
