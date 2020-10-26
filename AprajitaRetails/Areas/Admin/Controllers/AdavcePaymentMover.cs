using AprajitaRetails.Data;
using AprajitaRetails.Models;
using AprajitaRetails.Ops.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AprajitaRetails.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdavcePaymentMoverController : Controller
    {
        private readonly AprajitaRetailsContext context;
        private readonly int StoreId = 1;
        public AdavcePaymentMoverController(AprajitaRetailsContext db)
        {
            context = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult MoveData()
        {
            List<SalaryPayment> salaries = DataMover.MoveAdvancePaymentToSalary(context, StoreId);
            return View(salaries);

        }
    }
}
