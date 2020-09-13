using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AprajitaRetails.Ops.Utility;
using AprajitaRetails.Data;
using System;
using AprajitaRetails.Models.ViewModels;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using AprajitaRetails.Ops.Printers.Reports;

namespace AprajitaRetails.Areas.Accounts.Controllers
{
    [Area("Accounts")]
    [Authorize(Roles = "Admin,PowerUser,StoreManager")]
    public class CashBookController : Controller
    {
        //[Obsolete]
        //private readonly IHostingEnvironment _hostingEnvironment;
        private readonly AprajitaRetailsContext db;

        //[Obsolete]
        public CashBookController(AprajitaRetailsContext context/*, IHostingEnvironment hostingEnvironment*/)
        {
            db = context;
            //  _hostingEnvironment = hostingEnvironment;
        }


        // GET: CashBook
        public IActionResult Index(int? id, DateTime? EDate, string ModeType, string OpsType, string OutputType)
        {
            // string sWebRootFolder = _hostingEnvironment.WebRootPath;
            string fileName = "ExcelFiles\\"+ "Export_CashBook_" + DateTime.Now.ToFileTimeUtc().ToString() + ".xlsx";

            string path = Path.Combine("wwwroot",fileName );

            FileInfo file = new FileInfo(path);
            if (!file.Directory.Exists)
            {
                //   var dir = file.Directory.CreateSubdirectory(fileName);
                file.Directory.Create();

            }

            ViewBag.FileName = "";
            
            CashBookManagerExporter managerexporter = new CashBookManagerExporter();

            CashBookManager manager = new CashBookManager();
            List<CashBook> cashList;

            if (!String.IsNullOrEmpty(OpsType))
            {
                if (OpsType == "Correct")
                {
                    //TODO: Expoerter is helping or not . check and verify
                    //TODO: Implement Correct cash in hand
                    ViewBag.Message = "Cash Book Correction  ";

                    if (ModeType == "MonthWise")
                        managerexporter.CorrectCashInHands(db, EDate.Value.Date, path, false);
                    else
                        managerexporter.CorrectCashInHands(db, EDate.Value.Date, path, true);
                    
                    ViewBag.FileName = "/" + fileName;
                }
                else
                {
                    ViewBag.Message = ""; 
                }
            }


            if (EDate != null)
            {
                if (ModeType == "MonthWise")
                    cashList = manager.GetMontlyCashBook(db, EDate.Value.Date);
                else
                    cashList = manager.GetDailyCashBook(db, EDate.Value.Date);

            }
            else if (id == 101)
            {
                cashList = manager.GetDailyCashBook(db, DateTime.Now);
            }
            else
            {
                cashList = manager.GetMontlyCashBook(db, DateTime.Now);
            }

            if (cashList != null)
            {
                if (!String.IsNullOrEmpty(OutputType) && OutputType == "PDF")
                {
                    string fName = ReportPrinter.PrintCashBook(cashList);                   
                    return File(fName, "application/pdf");

                }
                else
                {
                    //ViewBag.FileName = "/"+fileName;
                }
               

                return View(cashList);
            }
            else
                return NotFound();
        }

        //// GET: CashBook/Details/5
        //public IActionResult Details(int id)
        //{
        //    return PartialView ();
        //}

        //// GET: CashBook/Create
        //public ActionResult Create()
        //{
        //    return PartialView ();
        //}

        // POST: CashBook/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction (nameof (Index));
        //    }
        //    catch
        //    {
        //        return PartialView ();
        //    }
        //}

        //// GET: CashBook/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return PartialView ();
        //}

        ////// POST: CashBook/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction (nameof (Index));
        //    }
        //    catch
        //    {
        //        return PartialView ();
        //    }
        //}

        //// GET: CashBook/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return PartialView ();
        //}

        //// POST: CashBook/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction (nameof (Index));
        //    }
        //    catch
        //    {
        //        return PartialView ();
        //    }
        //}
    }
}