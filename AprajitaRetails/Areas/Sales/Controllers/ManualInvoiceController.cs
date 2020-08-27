using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AprajitaRetails.Data;
using Microsoft.AspNetCore.Mvc;

namespace AprajitaRetails.Areas.Sales.Controllers
{
    //http://abctutorial.com/Post/35/mvc-5-master-details-using-jquery-ajax
    //http://www.dotnetawesome.com/2016/07/advance-master-details-entry-form-in-mvc.html
    [Area ("Sales")]
    public class ManualInvoiceController : Controller
    {
        private readonly AprajitaRetailsContext aprajitaContext;

        public ManualInvoiceController(AprajitaRetailsContext aCtx)
        {
            aprajitaContext = aCtx;
        }
        public IActionResult Index()
        {
           

            return View ();
        }

        public IActionResult MainView()
        {

            return View ();
        }
        public JsonResult GetBarCode(string barcode)
        {
            try
            {
                var pItem = aprajitaContext.ProductItems.Where (c => c.Barcode == barcode).Select (c => new { c.MRP, c.ProductName, c.TaxRate }).FirstOrDefault ();
                if ( pItem == null )
                {
                    pItem = new { MRP = (decimal) 0.0, ProductName = "Not Found", TaxRate = (decimal) 0 };
                }
                JsonResult result = new JsonResult (pItem)
                {
                    Value = pItem
                };
                return result;

            }
            catch ( Exception )
            {
                var pItem = new { MRP = (decimal) 0.0, ProductName = "Not Found", TaxRate = (decimal) 0 };
                JsonResult result = new JsonResult (pItem)
                {
                    Value = pItem
                };
                return result;
            }
            //Json { Data=pItem, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        //public ActionResult SaveOrder(string name, String address, Order[] order)
        //{
        //    string result = "Error! Order Is Not Complete!";
        //    if (name != null && address != null && order != null)
        //    {
        //        var cutomerId = Guid.NewGuid();
        //        Customer model = new Customer();
        //        model.CustomerId = cutomerId;
        //        model.Name = name;
        //        model.Address = address;
        //        model.OrderDate = DateTime.Now;
        //        db.Customers.Add(model);

        //        foreach (var item in order)
        //        {
        //            var orderId = Guid.NewGuid();
        //            Order O = new Order();
        //            O.OrderId = orderId;
        //            O.ProductName = item.ProductName;
        //            O.Quantity = item.Quantity;
        //            O.Price = item.Price;
        //            O.Amount = item.Amount;
        //            O.CustomerId = cutomerId;
        //            db.Orders.Add(O);
        //        }
        //        db.SaveChanges();
        //        result = "Success! Order Is Complete!";
        //    }
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}
    }
}