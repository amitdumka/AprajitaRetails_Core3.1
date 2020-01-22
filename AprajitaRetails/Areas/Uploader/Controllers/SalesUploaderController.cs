using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AprajitaRetails.Areas.Sales.Models;
using AprajitaRetails.Areas.Voyager.Data;
using AprajitaRetails.Ops.TAS;
using AprajitaRetails.Ops.Uploader;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AprajitaRetails.Areas.Uploader.Controllers
{
    public class SalesUploaderController : Controller
    {
        private readonly VoyagerContext db;

        public SalesUploaderController(VoyagerContext context)
        {
            db = context;
        }
        // GET: SalesUploader
        public ActionResult Index()
        {
            return View();
        }



        // GET: SalesUploader/SaleList/5
        public ActionResult SaleList(int? id)
        {
            var md = db.ImportSaleItemWises.Where(c => c.IsDataConsumed == false).OrderByDescending(c => c.InvoiceDate);
            return View(md);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProcessSale(string dDate)
        {
            DateTime ddDate = DateTime.Parse(dDate).Date;

            InventoryManger iManage = new InventoryManger();
            int a = iManage.CreateSaleEntry(db,ddDate);
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
        public ActionResult UploadSales(string BillType, string InterState, string UploadType/*, HttpPostedFileBase FileUpload*/)
        {
            IFormFile FileUpload = null;//TODO: handle This
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
            UploadReturns response = uploader.UploadExcel(db,uType, FileUpload, /*Server.MapPath("~/Doc/")*/"", IsVat, IsLocal);

            ViewBag.Status = response.ToString();
            if (response == UploadReturns.Success)
            {
                return RedirectToAction("SaleList");
            }

            return View();

        }



        // GET: SalesUploader/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //// GET: SalesUploader/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: SalesUploader/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: SalesUploader/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: SalesUploader/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: SalesUploader/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: SalesUploader/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}