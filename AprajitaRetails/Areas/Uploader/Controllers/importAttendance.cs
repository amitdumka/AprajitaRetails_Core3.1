using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AprajitaRetails.Areas.Voyager.Data;
using AprajitaRetails.Data;
using AprajitaRetails.Ops.Uploader;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AprajitaRetails.Areas.Uploader.Controllers
{
    [Area("Uploader")]
    [Authorize]

    public class ImportAttendanceController : Controller
    {
        private readonly AprajitaRetailsContext db;
        private readonly VoyagerContext voyDb;
        public ImportAttendanceController(AprajitaRetailsContext con, Voyager.Data.VoyagerContext vdb)
        {
            db = con;
            voyDb = vdb;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UploadData(string StoreCode, IFormFile FileUpload)
        {
            ExcelUploaders uploader = new ExcelUploaders();
            UploadReturns response = uploader.UploadAttendance(db, voyDb, StoreCode, FileUpload);

            ViewBag.Status = response.ToString();
            if (response == UploadReturns.Success)
            {
                return RedirectToAction("ListUpload");
            }

            return View();
        }

        public IActionResult ListUpload()
        {

            return View(db.AttendancesImport.ToList());
        }
    }
}
