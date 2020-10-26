using AprajitaRetails.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace AprajitaRetails.Areas.Admin.Controllers
{
    [Area("Admin")]

    [Authorize(Roles = "Admin,PowerUser")]
    public class RoleController : Controller
    {
        readonly RoleManager<IdentityRole> roleManager;
        readonly UserManager<IdentityUser> UserManager;
        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> um)
        {
            this.roleManager = roleManager;
            UserManager = um;
        }
        // GET: Role
        public IActionResult Index()
        {
            var roles = roleManager.Roles.ToList();
            return View(roles);
        }

        // GET: Role/Details/5
        public IActionResult Details(string id)
        {

            var roles = roleManager.FindByIdAsync(id).Result;
            var onRols = UserManager.GetUsersInRoleAsync(roles.Name).Result;
            ViewBag.RoleName = roles.Name;



            return View(onRols);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AssignRole()
        {
            //var roles = roleManager.Roles.ToList ();
            //var userList = UserManager.Users.ToList ();

            ViewData["RoleId"] = new SelectList(roleManager.Roles, "Id", "Name");
            ViewData["UserId"] = new SelectList(UserManager.Users, "Id", "UserName");


            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignRole([Bind("UserId,RoleId")] RoleUserView ruView)
        {
            if (ModelState.IsValid)
            {
                var _user = await UserManager.FindByIdAsync(ruView.UserId);
                var _role = await roleManager.FindByIdAsync(ruView.RoleId);
                if (_user != null && _role != null)
                {
                    var a = await UserManager.AddToRoleAsync(_user, _role.Name);
                    if (a.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewData["RoleId"] = new SelectList(roleManager.Roles, "Id", "Name", ruView.RoleId);
                        ViewData["UserId"] = new SelectList(UserManager.Users, "Id", "UserName", ruView.UserId);
                        ViewBag.ErrorMessage = "Failed to assign, try again";
                        return View(ruView);
                    }

                }
                else
                {
                    ViewData["RoleId"] = new SelectList(roleManager.Roles, "Id", "Name", ruView.RoleId);
                    ViewData["UserId"] = new SelectList(UserManager.Users, "Id", "UserName", ruView.UserId);
                    ViewBag.ErrorMessage = "role or  user not found, try again";
                    return View(ruView);
                }
            }
            ViewData["RoleId"] = new SelectList(roleManager.Roles, "Id", "Name", ruView.RoleId);
            ViewData["UserId"] = new SelectList(UserManager.Users, "Id", "UserName", ruView.UserId);
            return View(ruView);

        }
        // GET: Role/Create
        public ActionResult Create()
        {
            return View(new IdentityRole());
        }
        // POST: Role/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IdentityRole role)
        {
            await roleManager.CreateAsync(role);
            return RedirectToAction("Index");
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
            return View();
        }

        // POST: Role/Edit/5
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

        // GET: Role/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Role/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            try
            {

                var role = await roleManager.FindByIdAsync(id).ConfigureAwait(true);

                var res = await roleManager.DeleteAsync(role);
                if (res.Succeeded)
                    return RedirectToAction(nameof(Index));
                else
                {
                    //TODO: check with other delete options
                    ViewBag.ErrorMessage = "Failed to delete, try again";
                    ViewBag.ID = id;
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult RemoveRole()
        {
            //var roles = roleManager.Roles.ToList ();
            //var userList = UserManager.Users.ToList ();

            ViewData["RoleId"] = new SelectList(roleManager.Roles, "Id", "Name");
            ViewData["UserId"] = new SelectList(UserManager.Users, "Id", "UserName");


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveRole([Bind("UserId,RoleId")] RoleUserView ruView)
        {
            if (ModelState.IsValid)
            {
                var _user = await UserManager.FindByIdAsync(ruView.UserId);
                var _role = await roleManager.FindByIdAsync(ruView.RoleId);
                if (_user != null && _role != null)
                {
                    var a = await UserManager.RemoveFromRoleAsync(_user, _role.Name);
                    if (a.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewData["RoleId"] = new SelectList(roleManager.Roles, "Id", "Name", ruView.RoleId);
                        ViewData["UserId"] = new SelectList(UserManager.Users, "Id", "UserName", ruView.UserId);
                        ViewBag.ErrorMessage = "Failed to assign, try again";
                        return View(ruView);
                    }

                }
                else
                {
                    ViewData["RoleId"] = new SelectList(roleManager.Roles, "Id", "Name", ruView.RoleId);
                    ViewData["UserId"] = new SelectList(UserManager.Users, "Id", "UserName", ruView.UserId);
                    ViewBag.ErrorMessage = "role or  user not found, try again";
                    return View(ruView);
                }
            }
            ViewData["RoleId"] = new SelectList(roleManager.Roles, "Id", "Name", ruView.RoleId);
            ViewData["UserId"] = new SelectList(UserManager.Users, "Id", "UserName", ruView.UserId);
            return View(ruView);

        }

    }
}