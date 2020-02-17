using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AprajitaRetails.Areas.Accounts.Controllers
{
    [Area ("Accounts")]
    [Authorize]
    public class CashBookController : Controller
    {
        //TODO: Implement CashBook from TAS project
        // GET: CashBook
        public ActionResult Index()
        {
            return View ();
        }

        // GET: CashBook/Details/5
        public ActionResult Details(int id)
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