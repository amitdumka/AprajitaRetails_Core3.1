using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AprajitaRetails.Areas.Purchase.Models;
using AprajitaRetails.Areas.Uploader.Models;
using AprajitaRetails.Data;
using AprajitaRetails.Ops.TAS;
using AprajitaRetails.Ops.Uploader;
using AprajitaRetails.Ops.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AprajitaRetails.Areas.Uploader.Controllers
{
    [Area("Uploader")]
    [Authorize]
    public class PurchaseUploaderController : Controller
    {
        private readonly AprajitaRetailsContext db;

        public PurchaseUploaderController(AprajitaRetailsContext context)
        {
            db = context;
        }

        // GET: PurchaseUploader
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult ViewList()
        {
            var list = db.ImportSearches.ToList ();
            return View (list);
        }

        [HttpPost]
        public IActionResult UploadPurchase(string BillType, string InterState, string StoreCode, IFormFile FileUpload)
        {
            // IFormFile FileUpload = null ;
            ExcelUploaders uploader = new ExcelUploaders();
            bool IsVat = false;
            bool IsLocal = false;

            if(StoreCode== "JH00100" )
            {
                UploadReturn response1 = uploader.UploadExcel (db, UploadType.Search, FileUpload, StoreCode, IsVat, IsLocal);
                if ( response1 == UploadReturn.OKGen )
                {
                    return RedirectToAction ("ViewList");
                }
            }
            if (BillType == "VAT")
            {
                IsVat = true;
            }

            if (InterState == "WithInState")
            {
                IsLocal = true;
            }

            UploadReturn response = uploader.UploadExcel(db, UploadType.Purchase, FileUpload, StoreCode, IsVat, IsLocal);

            ViewBag.Status = response.ToString();
            if (response == UploadReturn.Success)
            {
                return RedirectToAction("ListUpload");
            }

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

            var YearList = db.ImportPurchases.GroupBy(c => c.GRNDate.Year).Select(c => c.Key).ToList();
            YearList.Sort();
            ViewBag.YearList = YearList;

            if (id == 100)
            {
                var md2 = db.ImportPurchases.Where(c => c.IsDataConsumed == true).OrderByDescending(c => c.GRNDate);
                return View(await PaginatedList<ImportPurchase>.CreateAsync(md2.AsNoTracking(), pageNumber ?? 1, pageSize));
            }
            else if (id == 101)
            {
                var md1 = db.ImportPurchases.OrderByDescending(c => c.GRNDate).ThenBy(c => c.IsDataConsumed);
                return View(await PaginatedList<ImportPurchase>.CreateAsync(md1.AsNoTracking(), pageNumber ?? 1, pageSize));
            }
            var md = db.ImportPurchases.Where(c => c.IsDataConsumed == false).OrderByDescending(c => c.GRNDate);

            return View(await PaginatedList<ImportPurchase>.CreateAsync(md.AsNoTracking(), pageNumber ?? 1, pageSize));
            //return View(md);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //TODO: make these function secure and post 
        public IActionResult ProcessPurchase(int? id, int? year, string? dDate, string? GrnNo)
        {
            HelperUtil.IsSessionSet(HttpContext);
            DateTime ddDate;
            int StoreId = HelperUtil.GetStoreID(HttpContext);
            InventoryManger iManage = new InventoryManger(StoreId);
            int a = -1;
           
            if(id!=null && id== 888 && year!=null)
            {
                a = iManage.ProcessPurchaseInwardByYear(db, (int)year);
                if (a > 0)
                    return RedirectToAction("ProcessedPurchase", new { id = a, Year=year});
            }
            
            
            if (!String.IsNullOrEmpty(GrnNo))
            {
                a = iManage.ProcessPurchaseInward(db, GrnNo);
                if (a > 0)
                    return RedirectToAction("ProcessedPurchase", new { id = a, GRNNo = GrnNo });
            }
            else if (dDate != null)
            {
                ddDate = DateTime.Parse(dDate).Date;
                a = iManage.ProcessPurchaseInward(db, ddDate);
                if (a > 0)
                    return RedirectToAction("ProcessedPurchase", new { id = a, onDate = ddDate });
            }

            ViewBag.MessageHead = "No Product items added. Some error might has been occurred. Item(s)=" + a;
            return View(new List<ProductPurchase>());

        }

        public IActionResult ProcessedPurchase(int id, int? year,  DateTime? onDate, string GRNNo)
        {
            if (!String.IsNullOrEmpty(GRNNo))
            {
                var dm = db.ProductPurchases.Include(c=>c.Supplier).Include(c => c.PurchaseItems).Where(c => c.InWardNo == GRNNo);
                ViewBag.MessageHead = "Invoices added and No. Of Items Added are " + id;
                return View(dm.ToList());
            }
            else if (onDate != null)
            {
                var dm = db.ProductPurchases.Include(c => c.Supplier).Include(c => c.PurchaseItems).Where(c => c.InWardDate.Date == onDate.Value.Date);
                ViewBag.MessageHead = "Invoices added and No. Of Items Added are " + id;
                return View(dm.ToList());
            }
            else if (year != null)
            {
                var dm = db.ProductPurchases.Include(c => c.Supplier).Include(c => c.PurchaseItems).Where(c => c.InWardDate.Year == year);
                ViewBag.MessageHead = "Invoices added and No. Of Items Added are " + id;
                return View(dm.ToList());
            }
            else
            {
                var dm = db.ProductPurchases.Include(c => c.Supplier).Include(c => c.PurchaseItems).OrderByDescending(c=>c.ProductPurchaseId).ThenBy(c=>c.InWardDate);
                ViewBag.MessageHead = "Invoices added and No. Of Items Added are " + id;
                return View(dm.ToList());
            }

        }

        // GET: PurchaseUploader/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (id > 0)
            {
                var productPurchases1 = db.ProductPurchases.Include(p => p.Supplier).Include(c => c.PurchaseItems).Where(c => c.ProductPurchaseId == id);
                if (productPurchases1 == null)
                {
                    return NotFound();
                }
                ViewBag.Details = "Invoice No: " + productPurchases1.FirstOrDefault().InvoiceNo;
                return View(productPurchases1.ToList());
            }
            ViewBag.Details = "";
            var productPurchases = db.ProductPurchases.Include(p => p.Supplier).Include(c => c.PurchaseItems);
            if (productPurchases == null)
            {
                return NotFound();
            }
            return View(productPurchases.ToList());
        }
    }
}