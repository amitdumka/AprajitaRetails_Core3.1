using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AprajitaRetails.Data;
using AprajitaRetails.Models.Helpers;
using AprajitaRetails.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AprajitaRetails.Controllers
{
    public class AccountsDetailsController : Controller
    {
        private readonly AprajitaRetailsContext db;
        public AccountsDetailsController(AprajitaRetailsContext context)
        {
            db = context;
        }
        public IActionResult Index(DateTime? OnDate)
        {
            if ( OnDate != null )
            {
                DetailIEVM vm1 = new AccountingDetails ().GetAccountDetails (db, OnDate.Value.Date);

                return View (vm1);
            }

            DetailIEVM vm = new AccountingDetails ().GetAccountDetails (db, DateTime.Now.Date);
             
            return View(vm);
        }
    }
}