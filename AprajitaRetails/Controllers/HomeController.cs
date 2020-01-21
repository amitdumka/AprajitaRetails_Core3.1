using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AprajitaRetails.Models;
using AprajitaRetails.Models.ViewModels;
using AprajitaRetails.Ops.WidgetModel;
using AprajitaRetails.Data;

namespace AprajitaRetails.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AprajitaRetailsContext _context;

        public HomeController(AprajitaRetailsContext context,ILogger<HomeController> logger)
        {
            _logger = logger;
            _context = context;

        }

        public IActionResult Index()
        {
            MasterViewReport reportView = new MasterViewReport
            {
                
                SaleReport = SaleWidgetModel.GetSaleRecord (_context),
                TailoringReport = HomeWidgetModel.GetTailoringReport (_context),
                EmpInfoList = HomeWidgetModel.GetEmpInfo (_context),
                AccountsInfo = HomeWidgetModel.GetAccoutingRecord (_context)
            };
            return View (reportView);
            
        }

        public IActionResult About()
        {
            ViewBag.Message = "Aprajita Retails Daily Record.";
           return View();
        }
        public IActionResult Contact()
        {
            ViewBag.Message = "Contact Us.";
           return View();
        }
        public IActionResult Privacy()
        {
           return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
           return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Chat()
        {
           return View();
        }
    }
}
