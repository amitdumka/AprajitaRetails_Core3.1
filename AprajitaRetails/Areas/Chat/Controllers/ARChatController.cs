using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AprajitaRetails.Areas.Chat.Models;
using AprajitaRetails.Data;
using AprajitaRetails.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AprajitaRetails.Areas.Chat.Controllers
{

    [Area ("Chat")]
    public class ARChatController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly AprajitaRetailsContext aprajitaRetailsContext;
        private readonly UserManager<IdentityUser> _userManager;

        public ARChatController(AprajitaRetailsContext arCon, ApplicationDbContext context, UserManager<IdentityUser> userManger)
        {
            _context = context;
            aprajitaRetailsContext = arCon;
            _userManager = userManger;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync (User);
            ViewBag.CurrentUserName = currentUser.NormalizedUserName;
            var messages = await aprajitaRetailsContext.Messages.ToListAsync();
            return View ();
        }
         [HttpPost]
        public async Task<IActionResult> Create(Message message)
        {
            if ( ModelState.IsValid )
            {
                message.UserName = User.Identity.Name;
                await aprajitaRetailsContext.Messages.AddAsync (message);
                await aprajitaRetailsContext.SaveChangesAsync ();
                return Ok ();
            }
            return  Error();
        }
        [ResponseCache (Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View (new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}