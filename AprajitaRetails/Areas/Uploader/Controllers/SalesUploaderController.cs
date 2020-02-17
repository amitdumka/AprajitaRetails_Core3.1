using Microsoft.AspNetCore.Authorization;    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AprajitaRetails.Areas.Sales.Models;
using AprajitaRetails.Areas.Voyager.Data;
using AprajitaRetails.Data;
using AprajitaRetails.Ops.TAS;
using AprajitaRetails.Ops.Uploader;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AprajitaRetails.Areas.Uploader.Controllers
{
    [Area("Uploader")]
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
            return View();
        }



        // GET: SalesUploader/SaleList/5
        public IActionResult SaleList(int? id)
        {
            var md = db.ImportSaleItemWises.Where(c => c.IsDataConsumed == false).OrderByDescending(c => c.InvoiceDate);
            return View(md);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProcessSale(string dDate)
        {
            DateTime ddDate = DateTime.Parse (dDate).Date;

            InventoryManger iManage = new InventoryManger();
            int a = iManage.CreateSaleEntry(db,ddDate, aprajitaContext);
            if (a > 0)
            {
                var dm = db.SaleInvoices.Include(c => c.PaymentDetail).Where(c => c.OnDate == ddDate);
                ViewBag.MessageHead = "No. Of Sale Invoice Created  and item processed are " + a;
                return View(dm.ToList());
            }
            else
            {
                ViewBag.MessageHead = "No Sale items added. Some error might has been occured. a=" + a;
                return View(new SaleInvoice());
            }

        }
        [HttpPost]
        public IActionResult UploadSales(string BillType, string InterState, string UploadType, IFormFile FileUpload)
        {
           
            ExcelUploaders uploader = new ExcelUploaders();
            bool IsVat = false;
            bool IsLocal = true;

            if (BillType == "VAT")
            {
                IsVat = true;
            }


            if (InterState == "InterState")
            {
                IsLocal = false;
            }

            UploadTypes uType = UploadTypes.SaleItemWise;
            if (UploadType == "Register")
            {
                uType = UploadTypes.SaleRegister;
            }
            UploadReturns response = uploader.UploadExcel(db,uType, FileUpload, IsVat, IsLocal);

            ViewBag.Status = response.ToString();
            if (response == UploadReturns.Success)
            {
                return RedirectToAction("SaleList");
            }

            return View();

        }



        // GET: SalesUploader/Details/5
        public IActionResult Details(int id)
        {
            return View();
        }

      
    }
}