using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using AprajitaRetails.Ops.Helpers.BioMetric;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using AprajitaRetails.Models.Helpers;

namespace AprajitaRetails.Areas.Tests.Controllers
{
    [Area("Tests")]
    public class TestBioController : Controller
    {
        public IActionResult Index(string TIMEZONE)
        {
            var timeZones = TimeZoneInfo.GetSystemTimeZones();
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var timeZone in timeZones)
            {
                items.Add(new SelectListItem() { Text = timeZone.Id + " # "+timeZone.DisplayName, Value=timeZone.Id });
                
            }
            ViewBag.TimeZones = items;

            if (!String.IsNullOrEmpty(TIMEZONE))
            {
                DateTime dt = DateTime.Now;
                DateTime dt2 = dt.ToIST();



                ViewBag.TIMEID = TIMEZONE;
                ViewBag.TIMEZONENAME = timeZones.Where(c => c.Id == TIMEZONE).Select(c => c.DisplayName).FirstOrDefault();
                ViewBag.TIMEVALUE = "IPDate:" + dt.ToString()+"/ OPDate: "+dt2.ToString();

            }
            else
            {
                ViewBag.TIMEID = "Not Selected";
                ViewBag.TIMEZONENAME = "";
                ViewBag.TIMEVALUE = "LocalTime: " +DateTime.Now.ToString();

            }





            return View();



        }
    }
}
