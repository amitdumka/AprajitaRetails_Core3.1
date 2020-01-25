using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//http://abctutorial.com/Post/35/mvc-5-master-details-using-jquery-ajax
//https://demo.aspnetawesome.com/GridNestingDemo#Master-detail-grid
//https://www.codeproject.com/Articles/531916/Master-Details-using-ASP-NET-MVC
//https://www.codeproject.com/Tips/1057064/MVC-Master-Detail-Example-with-Partial-View-and-Mo
//https://www.youtube.com/watch?v=ir9cMbNQP4w
//https://www.youtube.com/watch?v=wQZRC7vXT08
//http://www.dotnetawesome.com/2016/07/advance-master-details-entry-form-in-mvc.html
namespace AprajitaRetails.Areas.Sales.Controllers
{
    public class ManualInvoiceController : Controller
    {
        public IActionResult Index()
        {
            
            return View();
        }

        public ActionResult SaveOrder(string name, String address, Order[] order)
        {
            string result = "Error! Order Is Not Complete!";
            if (name != null && address != null && order != null)
            {
                var cutomerId = Guid.NewGuid();
                Customer model = new Customer();
                model.CustomerId = cutomerId;
                model.Name = name;
                model.Address = address;
                model.OrderDate = DateTime.Now;
                db.Customers.Add(model);

                foreach (var item in order)
                {
                    var orderId = Guid.NewGuid();
                    Order O = new Order();
                    O.OrderId = orderId;
                    O.ProductName = item.ProductName;
                    O.Quantity = item.Quantity;
                    O.Price = item.Price;
                    O.Amount = item.Amount;
                    O.CustomerId = cutomerId;
                    db.Orders.Add(O);
                }
                db.SaveChanges();
                result = "Success! Order Is Complete!";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}