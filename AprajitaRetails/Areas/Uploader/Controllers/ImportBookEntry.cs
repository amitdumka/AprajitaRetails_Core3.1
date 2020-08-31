using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AprajitaRetails.Areas.Uploader.Models;
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

        public async Task<IActionResult> ListUpload(int? id, string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["VoucherTypeParm"] = String.IsNullOrEmpty(sortOrder) ? "vouc_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["LedgerToParm"] = sortOrder == "LedgerTo" ? "LedgerToDesc" : "LedgerTo";
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }


            ViewData["CurrentFilter"] = searchString;

            int pageSize = 20;
            bool isConsumed = false;
            if (id == 100) isConsumed = true;
            var vModel = db.ImportBookEntries.Where(c => c.IsConsumed == isConsumed);
            if (id == 101) vModel = db.ImportBookEntries;

            switch (sortOrder)
            {
                case "Date":
                    vModel = vModel.OrderBy(c => c.OnDate);
                    break;
                case "date_desc":
                    vModel = vModel.OrderByDescending(c => c.OnDate);
                    break;
                case "LedgerTo":
                    vModel = vModel.OrderBy(c => c.LedgerTo);
                    break;

                case "LedgerToDesc":
                    vModel = vModel.OrderByDescending(c => c.LedgerTo);
                    break;
                case "vouc_desc":
                    vModel = vModel.OrderByDescending(c => c.VoucherType);
                    break;
                default:
                    vModel = vModel.OrderBy(c => c.VoucherType);
                    break;

            }


            return View(await PaginatedList<BookEntry>.CreateAsync(vModel.AsNoTracking(), pageNumber ?? 1, pageSize));

            // return View(db.ImportBookEntries.Where(c=>c.IsConsumed==false).ToList());
        }
    }
}
