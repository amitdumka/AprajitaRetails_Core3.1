using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AprajitaRetails.Data;
using AprajitaRetails.Ops.Uploader;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AprajitaRetails.Areas.Uploader.Controllers
{
    [Area("Uploader")]
    [Authorize]
    public class ImportBookEntry : Controller
    {

        private readonly AprajitaRetailsContext db;

        public ImportBookEntry(AprajitaRetailsContext con)
        {
            db = con;

        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult UploadData(IFormFile FileUpload)
        {
            ExcelUploaders uploader = new ExcelUploaders();
            UploadReturns response = uploader.UploadBookEntry(db, FileUpload);

            ViewBag.Status = response.ToString();
            if (response == UploadReturns.Success)
            {
                return RedirectToAction("ListUpload");
            }

            return View();
        }

        public IActionResult ListUpload()
        {

            return View(db.ImportBookEntries.Where(c=>c.IsConsumed==false).ToList());
        }
    }
}
