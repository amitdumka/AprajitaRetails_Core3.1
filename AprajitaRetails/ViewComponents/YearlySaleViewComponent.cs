using AprajitaRetails.Data;
using AprajitaRetails.Models;
using AprajitaRetails.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AprajitaRetails.ViewComponents
{
    public class YearlySaleData
    {
        public List<int> Year { get; set; }
        public List<int> Amount { get; set; }
    }

    [ViewComponent(Name = "yearlysale")]
    public class YearlySaleViewComponent : ViewComponent
    {
        private readonly AprajitaRetailsContext db;

        public YearlySaleViewComponent(AprajitaRetailsContext con)
        {
            db = con;
        }

        public YearlySaleData GetYearlySaleData()
        {
            YearlySaleData saleData = new YearlySaleData();
            saleData.Year = db.DailySales.GroupBy(c => c.SaleDate.Year).Select(c => c.Key).ToList();
            saleData.Year.Sort();
            saleData.Amount = new List<int>();
            foreach (var year in saleData.Year)
            {
                saleData.Amount.Add( (int)db.DailySales.Where(c => c.SaleDate.Year == year).Sum(c => c.Amount));

            }

            
            return saleData;
        }

        public IViewComponentResult Invoke()
        {
            //  var chart = JsonConvert.DeserializeObject<ChartJs> (chartData);

            Ticks ticks = new Ticks { beginAtZero = true };
            Yax yax = new Yax { ticks = ticks };
            Yax[] y = new Yax[1];
            y[0] = yax;
            Scales scales = new Scales();
            scales.yAxes = y;

            AprajitaRetails.Models.Data data = new AprajitaRetails.Models.Data();

            YearlySaleData saleData = GetYearlySaleData();

            Dataset dataset = new Dataset
            {
                borderWidth = 1,
                label = "Yearly Sale",
                data = saleData.Amount.ToArray(),
                backgroundColor = new string[]{
                    "rgba(255, 99, 132, 0.2)",
                    "rgba(54, 162, 235, 0.2)",
                    "rgba(255, 206, 86, 0.2)",
                    "rgba(75, 192, 192, 0.2)",
                    "rgba(153, 102, 255, 0.2)",
                    "rgba(54, 162, 235, 0.2)",
                    "rgba(255, 203, 83, 0.2)",
                    "rgba(255, 159, 64, 0.2)" },
                borderColor = new string[] { 
                    "rgba(255, 99, 132, 1)",
                    "rgba(54, 162, 235, 1)",
                    "rgba(255, 206, 86, 1)",
                    "rgba(75, 192, 192, 1)",
                    "rgba(153, 102, 255, 1)",
                    "rgba(255, 203, 83, 1)",
                    "rgba(153, 102, 255, 1)",
                    "rgba(255, 203, 83, 1)",
                    "rgba(255, 159, 64, 1)"}
            };

            ChartJs chart = new ChartJs
            {
                type = "bar",
                responsive = true,
                options = new Options { scales = scales },
                data = new Models.Data { datasets = new Dataset[] { dataset }, labels = saleData.Year.ConvertAll<string>(delegate (int i) { return i.ToString(); }).ToArray() }
            };

            var chartModel = new ChartJsViewModel
            {
                Chart = chart,
                ChartJson = JsonConvert.SerializeObject(chart, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                })
            };

            return View(chartModel);
        }
    }
}