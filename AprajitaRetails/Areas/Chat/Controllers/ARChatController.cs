using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AprajitaRetails.Areas.Chat.Models;
using AprajitaRetails.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AprajitaRetails.Areas.Chat.Controllers
{
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

        public async Task<IActionResult> Create(Message message)
        {
            if ( ModelState.IsValid )
            {
                message.UserName = User.Identity.Name;
                await aprajitaRetailsContext.Messages.AddAsync (message);
                await aprajitaRetailsContext.SaveChangesAsync ();
                return Ok ();
            }
            return NotFound();// Error();
        }

    }
}