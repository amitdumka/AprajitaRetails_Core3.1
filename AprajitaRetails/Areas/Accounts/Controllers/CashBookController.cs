using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AprajitaRetails.Areas.Accounts.Controllers
{
    [Area ("Accounts")]
    public class CashBookController : Controller
    {

        //TODO: Implement CashBook from TAS project
        // GET: CashBook
        public ActionResult Index()
        {
            return View();
        }

        // GET: CashBook/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CashBook/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CashBook/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CashBook/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CashBook/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CashBook/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CashBook/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}