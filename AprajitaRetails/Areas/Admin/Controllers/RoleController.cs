using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AprajitaRetails.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController : Controller
    {
        RoleManager<IdentityRole> roleManager;
        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        // GET: Role
        public ActionResult Index()
        {
            var roles = roleManager.Roles.ToList ();
            return View (roles);
        }

        // GET: Role/Details/5
        public ActionResult Details(int id)
        {
            return View ();
        }

        // GET: Role/Create
        public ActionResult Create()
        {
            return View (new IdentityRole ());
        }
        // POST: Role/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IdentityRole role)
        {
            await roleManager.CreateAsync (role);
            return RedirectToAction ("Index");
        }

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
        //        return View ();
        //    }
        //}

        // GET: Role/Edit/5
        public ActionResult Edit(int id)
        {
            return View ();
        }

        // POST: Role/Edit/5
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
                return View ();
            }
        }

        // GET: Role/Delete/5
        public ActionResult Delete(int id)
        {
            return View ();
        }

        // POST: Role/Delete/5
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
                return View ();
            }
        }
    }
}