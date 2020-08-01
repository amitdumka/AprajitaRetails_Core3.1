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

namespace AprajitaRetails.Areas.Accounts.Controllers
{
    [Area ("Accounts")]
    [Authorize (Roles = "Admin,PowerUser,StoreManager")]
    public class CashBookController : Controller
    {
        [Obsolete]
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly AprajitaRetailsContext db;

        [Obsolete]
        public CashBookController(AprajitaRetailsContext context, IHostingEnvironment hostingEnvironment)
        {
            db = context;
            _hostingEnvironment = hostingEnvironment;
        }


        // GET: CashBook
        public IActionResult Index(int? id, DateTime? EDate, string ModeType, string OpsType)
        {
            string sWebRootFolder = _hostingEnvironment.WebRootPath;
            string fileName = Path.Combine (sWebRootFolder, "Export_" + DateTime.Now.ToFileTimeUtc().ToString () + ".xlsx");

            CashBookManagerExporter managerexporter = new CashBookManagerExporter ();
            CashBookManager manager = new CashBookManager ();
            List<CashBook> cashList;
            if ( !String.IsNullOrEmpty (OpsType) )
            {
                if ( OpsType == "Correct" )
                {
                    //TODO: Implement Correct cash in hand
                    ViewBag.Message = "Cash Book Correction: ";
                    if ( ModeType == "MonthWise" )
                        managerexporter.CorrectCashInHands (db, EDate.Value.Date, fileName, false);
                    else
                        managerexporter.CorrectCashInHands (db, EDate.Value.Date, fileName, true);
                }
                else
                { ViewBag.Message = ""; }
            }


            if ( EDate != null )
            {
                if ( ModeType == "MonthWise" )
                    cashList = manager.GetMontlyCashBook (db, EDate.Value.Date);
                else
                    cashList = manager.GetDailyCashBook (db, EDate.Value.Date);

            }
            else if ( id == 101 )
            {
                cashList = manager.GetDailyCashBook (db, DateTime.Now);
            }
            else
            {
                cashList = manager.GetMontlyCashBook (db, DateTime.Now);
            }

            if ( cashList != null )
                return View (cashList);
            else
                return NotFound ();
        }

        // GET: CashBook/Details/5
        public IActionResult Details(int id)
        {
            return PartialView ();
        }

        // GET: CashBook/Create
        public ActionResult Create()
        {
            return PartialView ();
        }

        // POST: CashBook/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction (nameof (Index));
            }
            catch
            {
                return PartialView ();
            }
        }

        // GET: CashBook/Edit/5
        public ActionResult Edit(int id)
        {
            return PartialView ();
        }

        // POST: CashBook/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction (nameof (Index));
            }
            catch
            {
                return PartialView ();
            }
        }

        // GET: CashBook/Delete/5
        public ActionResult Delete(int id)
        {
            return PartialView ();
        }

        // POST: CashBook/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction (nameof (Index));
            }
            catch
            {
                return PartialView ();
            }
        }
    }
}