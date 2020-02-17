using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AprajitaRetails.Data;
using AprajitaRetails.Ops.Uploader;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AprajitaRetails.Areas.Uploader.Controllers
{
    [Area ("Uploader")]
    [Authorize]
    public class ImportAddressBookController : Controller
    {
        private readonly AprajitaRetailsContext db;
        public ImportAddressBookController(AprajitaRetailsContext con)
        {
            db = con;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UploadData(IFormFile FileUpload)
        {
            ExcelUploaders uploader = new ExcelUploaders ();
            UploadReturns response = uploader.UploadAddressBook (db,  FileUpload);
            ViewBag.Status = response.ToString ();
            if ( response == UploadReturns.Success )
            {
                return RedirectToAction ("ListUpload");
            }

            return View ();
        }

        public IActionResult ListUpload()
        {

            return View (db.Contact.ToList());
        }
    }


}