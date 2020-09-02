using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AprajitaRetails.Areas.Sales.Models.Views;
using AprajitaRetails.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AprajitaRetails.Areas.Sales.Controllers
{
    //http://abctutorial.com/Post/35/mvc-5-master-details-using-jquery-ajax
    //http://www.dotnetawesome.com/2016/07/advance-master-details-entry-form-in-mvc.html
    [Area("Sales")]
    public class ManualInvoiceController : Controller
    {
        private readonly AprajitaRetailsContext aprajitaContext;

        public ManualInvoiceController(AprajitaRetailsContext aCtx)
        {
            aprajitaContext = aCtx;
        }
        public IActionResult Index()
        {
            var vm = aprajitaContext.RegularInvoices.Include(c => c.Customer)
                .Where(c => c.IsManualBill).OrderByDescending(c => c.OnDate).ThenBy(c => c.InvoiceNo).ToList();

            return View(vm);
        }

        public IActionResult MainView()
        {

            return View();
        }

        public JsonResult GetBarCode(string barcode)
        {
            try
            {
                var pItem = aprajitaContext.ProductItems.Where(c => c.Barcode == barcode).Select(c => new { c.MRP, c.ProductName, c.TaxRate, c.Units }).First();

                if (pItem == null)
                {
                    pItem = new { MRP = (decimal)0.0, ProductName = "Not Found", TaxRate = (decimal)0, Units = Units.Pcs };
                }

                JsonResult result = new JsonResult(pItem)
                {
                    Value = pItem

                };
                return result;


            }
            catch (Exception)
            {
                var pItem = new { MRP = (decimal)0.0, ProductName = "Not Found!", TaxRate = (decimal)0, Units = Units.Pcs };
                JsonResult result = new JsonResult(pItem)
                {
                    Value = pItem

                };

                return result;
            }
            //return Json ( Data=pItem, JsonRequestBehavior = JsonRequestBehavior.AllowGet );
        }

        public class SaveOrderDTO
        {
            public string Name { get; set; }
            public string Address { get; set; }
            public List<SaleItemList> SaleItems { get; set; }
        }
        [HttpPost]
        public ActionResult SaveOrder([FromBody] SaveOrderDTO dTO) /*string name, [FromBody] String address, [FromBody] SaleItemList[] saleItems)*/
        {
            string result = "Error! Order Is Not Complete!";
            if (dTO.Name != null && dTO.Address != null && dTO.SaleItems != null)
            {
                //        var cutomerId = Guid.NewGuid();
                //        Customer model = new Customer();
                //        model.CustomerId = cutomerId;
                //        model.Name = name;
                //        model.Address = address;
                //        model.OrderDate = DateTime.Now;
                //        db.Customers.Add(model);

                List<RegularSaleItem> itemList = new List<RegularSaleItem>();
                foreach (var item in dTO.SaleItems)
                {
                    //            var orderId = Guid.NewGuid();
                    RegularSaleItem O = new RegularSaleItem();
                    
                    O.BarCode = item.BarCode;
                    O.Qty = item.Quantity;
                    O.MRP = item.Price;
                    O.BasicAmount = item.Amount;
                    itemList.Add(O);
                     
                }
        //        db.SaveChanges();
                result = "Success! Order Is Complete!";
            }
            return Json(result);
        }
    }
}