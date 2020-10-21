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
    public class SaleData
    {
        public decimal Amount { get; set; }
        public DateTime OnDate { get; set; }
    }

    public class SaleDataVM
    {
        public List<int> Amount { get; set; }
        public List<string> Label { get; set; }
    }

    public static class SaleDataVMExt
    {
        public static SaleDataVM CopyObject(this SaleDataVM obj, List<SaleData> data)
        {
            List<int> Amt = new List<int>();
            List<string> label = new List<string>();
            foreach (var item in data)
            {
                Amt.Add((int)item.Amount);
                label.Add(item.OnDate.ToString("dd/MMMM"));
            }
            obj.Amount = Amt; obj.Label = label;
            return new SaleDataVM { Amount = Amt, Label = label };
        }
    }

    [ViewComponent(Name = "weeklysale")]
    public class WeeklySaleViewComponent : ViewComponent
    {
        private readonly AprajitaRetailsContext db;

        public WeeklySaleViewComponent(AprajitaRetailsContext con)
        {
            db = con;
        }

        public List<SaleData> GetWeekSaleData()
        {
            List<SaleData> WeeklySale = new List<SaleData>();

            for (int i = 0; i < 7; i++)
            {
                SaleData data = new SaleData { OnDate = DateTime.Today.AddDays(-i), Amount = 0 };

                data.Amount = db.DailySales.Where(c => c.SaleDate == data.OnDate).Sum(c => c.Amount);

                WeeklySale.Add(data);
            }
            return WeeklySale;
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

            List<SaleData> WeeklySale = GetWeekSaleData();
            SaleDataVM dataVM = new SaleDataVM();
            dataVM = dataVM.CopyObject(WeeklySale);
            dataVM.Label.Reverse();
            dataVM.Amount.Reverse();

            Dataset dataset = new Dataset
            {
                borderWidth = 1,
                label = "Weekly Sale",
                data = dataVM.Amount.ToArray(),
                backgroundColor = new string[]{"rgba(255, 99, 132, 0.2)",
                    "rgba(54, 162, 235, 0.2)",
                    "rgba(255, 206, 86, 0.2)",
                    "rgba(75, 192, 192, 0.2)",
                    "rgba(153, 102, 255, 0.2)",
                    "rgba(54, 162, 235, 0.2)",
                    "rgba(255, 203, 83, 0.2)",
                    "rgba(255, 159, 64, 0.2)" },
                borderColor = new string[] { "rgba(255, 99, 132, 1)",
                        "rgba(54, 162, 235, 1)",
                        "rgba(255, 206, 86, 1)",
                        "rgba(75, 192, 192, 1)",
                        "rgba(153, 102, 255, 1)",
                         "rgba(255, 203, 83, 1)",
                        "rgba(255, 159, 64, 1)"}
            };

            ChartJs chart = new ChartJs
            {
                type = "bar",
                responsive = true,
                options = new Options { scales = scales },
                data = new Models.Data { datasets = new Dataset[] { dataset }, labels = dataVM.Label.ToArray() }
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