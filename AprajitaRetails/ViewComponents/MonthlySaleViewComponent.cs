using AprajitaRetails.Data;
using AprajitaRetails.Models;
using AprajitaRetails.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace AprajitaRetails.ViewComponents
{
    public class MonthlySaleData
    {
        public List<string> MonthName { get; set; }
        public List<int> Amount { get; set; }
    }
    [ViewComponent(Name = "monthlysale")]
    public class MonthlySaleViewComponent : ViewComponent
    {
        private readonly AprajitaRetailsContext db;

        public MonthlySaleViewComponent(AprajitaRetailsContext con)
        {
            db = con;
        }

        public MonthlySaleData GetMonthSaleData()
        {

            MonthlySaleData sData = new MonthlySaleData();
            sData.Amount = new List<int>();
            sData.MonthName = new List<string>();

    
            int LastMonth = DateTime.Today.Month;
            for (int i = 1; i <= LastMonth; i++)
            {
                sData.Amount.Add((int?)db.DailySales.Where(c => c.SaleDate.Month == i && c.SaleDate.Year == DateTime.Today.Year).Select(c => c.Amount).Sum() ?? 0);
                sData.MonthName.Add(DateTimeFormatInfo.CurrentInfo.GetMonthName(i));//DateTime.Today.AddMonths(i).ToString("MMM");
            }
            return sData;
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

            MonthlySaleData saleData = GetMonthSaleData();


            Dataset dataset = new Dataset
            {
                borderWidth = 1,
                label = "Monthly Sale",
                data = saleData.Amount.ToArray(),
                backgroundColor = new string[]{
                    "rgba(255, 99, 132, 0.2)",
                    "rgba(54, 162, 235, 0.2)",
                    "rgba(255, 206, 86, 0.2)",
                    "rgba(75, 192, 192, 0.2)",
                    "rgba(153, 102, 255, 0.2)",
                    "rgba(54, 162, 235, 0.2)",
                    "rgba(255, 203, 83, 0.2)",
                    "rgba(255, 159, 64, 0.2)",
                    "rgba(255, 99, 132, 0.2)",
                    "rgba(54, 162, 235, 0.2)",
                    "rgba(255, 206, 86, 0.2)",
                    "rgba(75, 192, 192, 0.2)",

                },
                borderColor = new string[] { 
                    "rgba(255, 99, 132, 1)",
                    "rgba(54, 162, 235, 1)",
                    "rgba(255, 206, 86, 1)",
                    "rgba(75, 192, 192, 1)",
                    "rgba(153, 102, 255, 1)",
                    "rgba(255, 203, 83, 1)",
                    "rgba(255, 159, 64, 1)",
                    "rgba(255, 99, 132, 1)",
                    "rgba(54, 162, 235, 1)",
                    "rgba(255, 206, 86, 1)",
                    "rgba(75, 192, 192, 1)",
                    "rgba(153, 102, 255, 1)",
                }
            };

            ChartJs chart = new ChartJs
            {
                type = "bar",
                responsive = true,
                options = new Options { scales = scales },
                data = new Models.Data { datasets = new Dataset[] { dataset }, labels = saleData.MonthName.ToArray() }
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