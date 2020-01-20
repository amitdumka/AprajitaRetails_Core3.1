using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AprajitaRetails.Data;
using AprajitaRetails.Ops.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace AprajitaRetails.Controllers
{
    public class ExporterController : Controller
    {
        private readonly AprajitaRetailsContext _context;

        public ExporterController(AprajitaRetailsContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.Message = XMLExporter.WriteToXML(_context);
            return View();
        }
    }
}