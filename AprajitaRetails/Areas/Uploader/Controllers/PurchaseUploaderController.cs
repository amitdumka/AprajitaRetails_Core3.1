using System;
using System.Collections.Generic;
using System.Linq;
using AprajitaRetails.Areas.Purchase.Models;
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
    [Area ("Uploader")]
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
            return View ();
        }

        [HttpPost]
        public IActionResult UploadPurchase(string BillType, string InterState, string StoreCode, IFormFile FileUpload)
        {
            // IFormFile FileUpload = null ;
            ExcelUploaders uploader = new ExcelUploaders ();
            bool IsVat = false;
            bool IsLocal = false;

            if ( BillType == "VAT" )
            {
                IsVat = true;
            }

            if ( InterState == "WithInState" )
            {
                IsLocal = true;
            }

            UploadReturns response = uploader.UploadExcel (db, UploadTypes.Purchase, FileUpload, StoreCode, IsVat, IsLocal);

            ViewBag.Status = response.ToString ();
            if ( response == UploadReturns.Success )
            {
                return RedirectToAction ("ListUpload");
            }

            return View ();
        }

        public IActionResult ListUpload(int? id)
        {
            if ( id == 100 )
            {
                var md2 = db.ImportPurchases.Where (c => c.IsDataConsumed == true).OrderByDescending (c => c.GRNDate);
                return View (md2);
            }
            else if ( id == 101 )
            {
                var md1 = db.ImportPurchases.OrderByDescending (c => c.GRNDate).ThenBy (c => c.IsDataConsumed);
                return View (md1);
            }
            var md = db.ImportPurchases.Where (c => c.IsDataConsumed == false).OrderByDescending (c => c.GRNDate);
            return View (md);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProcessPurchase(string dDate)
        {
            HelperUtil.IsSessionSet (HttpContext);

            DateTime ddDate = DateTime.Parse (dDate).Date;

            int StoreId = HelperUtil.GetStoreID (HttpContext);
            InventoryManger iManage = new InventoryManger (StoreId);

            int a = iManage.ProcessPurchaseInward (db, ddDate, false);

            if ( a > 0 )
            {
                return RedirectToAction ("ProcessedPurchase", new { id = a, onDate = ddDate });
            }
            else
            {
                //TODO: In view Check for Model is null or not
                ViewBag.MessageHead = "No Product items added. Some error might has been occurred. a=" + a;
                return View (new List<ProductPurchase> ());
            }
        }

        public IActionResult ProcessedPurchase(int id, DateTime onDate)
        {
            var dm = db.ProductPurchases.Include (c => c.PurchaseItems).Where (c => c.InWardDate.Date == ( onDate.Date ));
            ViewBag.MessageHead = "Invoices added and No. Of Items Added are " + id;
            return View (dm.ToList ());
        }

        // GET: PurchaseUploader/Details/5
        public IActionResult Details(int? id)
        {
            if ( id == null )
            {
                return NotFound ();
            }
            if ( id > 0 )
            {
                var productPurchases1 = db.ProductPurchases.Include (p => p.Supplier).Include (c => c.PurchaseItems).Where (c => c.ProductPurchaseId == id);
                if ( productPurchases1 == null )
                {
                    return NotFound ();
                }
                ViewBag.Details = "Invoice No: " + productPurchases1.FirstOrDefault ().InvoiceNo;
                return View (productPurchases1.ToList ());
            }
            ViewBag.Details = "";
            var productPurchases = db.ProductPurchases.Include (p => p.Supplier).Include (c => c.PurchaseItems);
            if ( productPurchases == null )
            {
                return NotFound ();
            }
            return View (productPurchases.ToList ());
        }
    }
}