using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AprajitaRetails.Areas.Uploader.Controllers
{
    public class BankStatementUploadController : Controller
    {
        // GET: BankStatementUploadController
        public ActionResult Index()
        {
            return View();
        }

        // GET: BankStatementUploadController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

       

        // POST: BankStatementUploadController/Uploader
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Uploader(IFormFile FileUpload)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        
    }
}
