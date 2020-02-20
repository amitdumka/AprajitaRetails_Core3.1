using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AprajitaRetails.Ops.Utility;
using AprajitaRetails.Data;
using System;
using AprajitaRetails.Models.ViewModels;
using System.Collections.Generic;

namespace AprajitaRetails.Areas.Accounts.Controllers
{
    [Area ("Accounts")]
    [Authorize (Roles = "Admin,PowerUser,StoreManager")]
    public class CashBookController : Controller
    {
        private readonly AprajitaRetailsContext db;
        public CashBookController(AprajitaRetailsContext context)
        {
            db = context;
        }

        //TODO: Implement CashBook from TAS project
        // GET: CashBook
        public IActionResult Index(int? id, DateTime? EDate, string ModeType)
        {
            CashBookManager manager = new CashBookManager ();
            List<CashBook> cashList;
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