using System;
using System.Linq;
using AprajitaRetails.Areas.Sales.Models;
using AprajitaRetails.Areas.Voyager.Data;
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
    public class SalesUploaderController : Controller
    {
        private readonly VoyagerContext db;
        private readonly AprajitaRetailsContext aprajitaContext;

        public SalesUploaderController(VoyagerContext context, AprajitaRetailsContext arContext)
        {
            db = context;
            aprajitaContext = arContext;
        }

        // GET: SalesUploader
        public IActionResult Index()
        {
            return View ();
        }

        // GET: SalesUploader/SaleList/5
        public IActionResult SaleList(int? id)
        {
            var md = db.ImportSaleItemWises.Where (c => c.IsDataConsumed == false).OrderByDescending (c => c.InvoiceDate);
            return View (md);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProcessSale(string dDate)
        {
            DateTime ddDate = DateTime.Parse (dDate).Date;
            //Store Based Addition
            int StoreId = HelperUtil.GetStoreID (HttpContext);
            InventoryManger iManage = new InventoryManger (StoreId);

            int a = iManage.CreateSaleEntry (db, ddDate, aprajitaContext);
            if ( a > 0 )
            {
                return RedirectToAction ("ProcessedSale", new { id = a, onDate = ddDate });

                //var dm = db.SaleInvoices.Include (c => c.PaymentDetail).Where (c => c.OnDate == ddDate);
                //ViewBag.MessageHead = "No. Of Sale Invoice Created  and item processed are " + a;
                //return View (dm.ToList ());
            }
            else
            {
                ViewBag.MessageHead = "No Sale items added. Some error might has been occurred. a=" + a;
                return View (new SaleInvoice ());
            }
        }
        public IActionResult ProcessedSale(int id, DateTime onDate)
        {
            var dm = db.SaleInvoices.Include (c => c.PaymentDetail).Where (c => c.OnDate == onDate.Date);
            ViewBag.MessageHead = "No. Of Sale Invoice Created  and item processed are " + id;
            return View (dm.ToList ());
        }
        [HttpPost]
        public IActionResult UploadSales(string BillType, string InterState, string UploadType, string StoreCode, IFormFile FileUpload)
        {
            ExcelUploaders uploader = new ExcelUploaders ();
            bool IsVat = false;
            bool IsLocal = true;

            if ( BillType == "VAT" )
            {
                IsVat = true;
            }

            if ( InterState == "InterState" )
            {
                IsLocal = false;
            }

            UploadTypes uType = UploadTypes.SaleItemWise;
            if ( UploadType == "Register" )
            {
                uType = UploadTypes.SaleRegister;
            }
            UploadReturns response = uploader.UploadExcel (db, uType, FileUpload, StoreCode, IsVat, IsLocal);

            ViewBag.Status = response.ToString ();
            if ( response == UploadReturns.Success )
            {
                return RedirectToAction ("SaleList");
            }

            return View ();
        }

        // GET: SalesUploader/Details/5
        public IActionResult Details(int id)
        {
            return View ();
        }
    }
}