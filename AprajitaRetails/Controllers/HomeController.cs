using AprajitaRetails.Areas.Reports.Ops;
using AprajitaRetails.Data;
using AprajitaRetails.Models;
using AprajitaRetails.Models.ViewModels;
using AprajitaRetails.Ops.TAS.Mails;
using AprajitaRetails.Ops.WidgetModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace AprajitaRetails.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AprajitaRetailsContext _context;
        //  private static IGiniService _gini;
        public HomeController(AprajitaRetailsContext context, ILogger<HomeController> logger/*, IGiniService gini*/)
        {
            _logger = logger;
            _context = context;
            //  _gini = gini;

        }

        public IActionResult Index()
        {
            MasterViewReport reportView = new MasterViewReport
            {

                SaleReport = SaleWidgetModel.GetSaleRecord(_context),
                TailoringReport = HomeWidgetModel.GetTailoringReport(_context),
                EmpInfoList = HomeWidgetModel.GetEmpInfo(_context),
                AccountsInfo = HomeWidgetModel.GetAccoutingRecord(_context),
                BookingOverDues = ReportOps.GetTailoringBookingOverDue(_context)
            };
            return View(reportView);

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

        public JsonResult ContactUs([FromBody]ContactUsVM data)
        {

            MyMail.SendEmail (data.Subject,$"Email:{data.EmailId}\n Message: {data.Message}","amitnarayansah@.com");

            return Json ("OK");
            
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
        public IActionResult OurTeams()
        {
            return View();
        }
    }
}
