using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AprajitaRetails.Data;
using AprajitaRetails.Models;
using AprajitaRetails.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AprajitaRetails.ViewComponents
{
    [ViewComponent (Name = "weeklysale")]
    public class WeeklySaleViewComponent : ViewComponent
    {
        private readonly AprajitaRetailsContext db;

        public WeeklySaleViewComponent(AprajitaRetailsContext con)
        {
            db = con;
        }

        public IViewComponentResult Invoke()
        {
          //  var chart = JsonConvert.DeserializeObject<ChartJs> (chartData);
            ChartJs chart = new ChartJs { };

            var chartModel = new ChartJsViewModel
            {
                Chart = chart,
                ChartJson = JsonConvert.SerializeObject (chart, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                })
            };

            return View (chartModel);
        }


        public void GetWeekSaleData()
        {
            DateTime date = DateTime.Today.Date;

            List<decimal> WeeklySale = new List<decimal> ();
            List<DateTime> DateList = new List<DateTime> ();

            for(int i=0 ;i<7 ;i++ )
            {
                date = DateTime.Today.AddDays (-i);
                var amt = db.DailySales.Where (c => c.SaleDate == date).Sum (c => c.Amount);
                WeeklySale.Add (amt);
                DateList.Add (date);
            }
          
        }
    }
}
