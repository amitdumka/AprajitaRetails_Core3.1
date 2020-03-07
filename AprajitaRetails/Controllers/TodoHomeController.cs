using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AprajitaRetails.Areas.ToDo.Interfaces;
using AprajitaRetails.Ops.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AprajitaRetails.Controllers
{
    public class TodoHomeController : Controller
    {
        ITodoItemService _todoItemService;
        SignInManager<IdentityUser> _signInManager;
        UserManager<IdentityUser> _userManager;
        public TodoHomeController(ITodoItemService todoItemService, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _todoItemService = todoItemService;
            _signInManager = signInManager;
            _userManager = userManager;

        }
        public IActionResult TodoList()
        {

            return View(new TodoManager().ListTodoItemAsync(_todoItemService,_signInManager, _userManager));
        }
    }
}