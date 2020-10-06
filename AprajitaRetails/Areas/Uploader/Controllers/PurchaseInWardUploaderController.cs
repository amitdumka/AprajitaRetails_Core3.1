using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AprajitaRetails.Areas.Uploader.Models;
using AprajitaRetails.Data;
using AprajitaRetails.Ops.Uploader;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AprajitaRetails.Areas.Uploader.Controllers
{
    public class PurchaseInWardUploaderController : Controller
    {
        public AprajitaRetailsContext db;
        public PurchaseInWardUploaderController(AprajitaRetailsContext _db)
        {
            db = _db;
         
        }

        public IActionResult Index()
        {
         
            return View();
        }

        [HttpPost]
        public IActionResult UploadPurchase(string StoreCode, IFormFile FileUpload)
        {
            ExcelUploaders uploader = new ExcelUploaders();
            UploadReturn response = uploader.UploadExcel(db, UploadType.InWard, FileUpload, StoreCode, false, false);
            ViewBag.Status = response.ToString();
            if (response == UploadReturn.Success)
                return RedirectToAction("ListUpload");
            return View();
        }
        public async Task<IActionResult> ListUpload(int? id, string currentFilter, string searchString, int? pageNumber)
        {
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;
            int pageSize = 30;

            var YearList = db.ImportInWards.GroupBy(c => c.InWardDate.Year).Select(c => c.Key).ToList();
            YearList.Sort();
            ViewBag.YearList = YearList;

            if (id == 100)
            {
                var md2 = db.ImportInWards.Where(c => c.IsDataConsumed == true).OrderByDescending(c => c.InWardDate);
                return View(await PaginatedList<Models.ImportInWard>.CreateAsync(md2.AsNoTracking(), pageNumber ?? 1, pageSize));
            }
            //else if (id == 101)
            //{
            //    var md1 = db.ImportPurchases.OrderByDescending(c => c.GRNDate).ThenBy(c => c.IsDataConsumed);
            //    return View(await PaginatedList<ImportPurchase>.CreateAsync(md1.AsNoTracking(), pageNumber ?? 1, pageSize));
            //}
            var md = db.ImportInWards.Where(c => c.IsDataConsumed == false).OrderByDescending(c => c.InWardDate);

            return View(await PaginatedList<ImportInWard>.CreateAsync(md.AsNoTracking(), pageNumber ?? 1, pageSize));
            //return View(md);
        }
    }
}
