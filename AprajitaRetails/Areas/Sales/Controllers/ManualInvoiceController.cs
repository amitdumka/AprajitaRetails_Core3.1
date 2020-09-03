using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AprajitaRetails.Areas.Sales.Models.Views;
using AprajitaRetails.Data;
using AprajitaRetails.Ops.Triggers;
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
        private readonly int StoreId = 1; //TODO: Fixed now
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
        [HttpGet]
        public JsonResult GetSalesmanList() {
            var list = aprajitaContext.Salesmen.Where(c=>c.StoreId == StoreId).OrderBy(c => c.SalesmanName ).Select(c=> new { c.SalesmanId,c.SalesmanName}).ToList();
            return  new JsonResult(list);
        }

        [HttpPost]
        public ActionResult SaveOrder([FromBody] SaveOrderDTO dTO) /*string name, [FromBody] String address, [FromBody] SaleItemList[] saleItems)*/
        {
            string result = "Error! Order Is Not Complete!";
            if (dTO.Name != null && dTO.Address != null && dTO.SaleItems != null)
            {
               /*bool x=*/ new RegularSaleManager().OnInsert(aprajitaContext, dTO);
                //if (!x) result = "Error while saving bill, Kindly try again!";
                //else
                result = "Success! Order Is Complete!";
            }
            return Json(result);
        }
    }
}