using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;
using TodoList.Web.Models;

namespace TodoList.Web.Controllers
{
    [Area("ToDo")]
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser != null)
            {
                return RedirectToAction(nameof(TodosController.Home), "Todos");
            }
            else
            {
                return RedirectToAction(nameof(AprajitaRetails.Controllers.HomeController.Index), "Home");
            }
            // ViewData["Title"] = "Home";
            // return View();
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
