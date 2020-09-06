using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AprajitaRetails.Ops.Printers;
using Microsoft.AspNetCore.Mvc;

namespace AprajitaRetails.Controllers
{
    public class TestPrintController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TestPrint()
        {
            InvoicePrinter.TestPrint();
            return RedirectToAction(nameof(Index));
        }
    }
}
