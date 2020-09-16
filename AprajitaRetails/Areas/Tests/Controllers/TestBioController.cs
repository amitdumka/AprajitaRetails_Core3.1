using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using AprajitaRetails.Ops.Helpers.BioMetric;
using Microsoft.AspNetCore.Mvc;

namespace AprajitaRetails.Areas.Tests.Controllers
{
    [Area("Tests")]
    public class TestBioController : Controller
    {
        public IActionResult Index(int? id , string? ipAddress, int? portNo)
        {
            if (!String.IsNullOrEmpty(ipAddress))
            {
                if(portNo!=null && portNo > 0)
                {
                    //if(BioMetricHelper.ConnectDevice(ipAddress,(int)portNo))
                    //{
                    //    ViewBag.Connected = "Connected to Biometric";
                    //}
                    //else
                    //{
                    //    ViewBag.Connected = "Failed to Connect with Biometric";
                    //}
                }
            }



            return View();
        }
    }
}
