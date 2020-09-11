using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AprajitaRetails.Areas.Sales.Data;
using AprajitaRetails.Areas.Sales.Models.Views;
using AprajitaRetails.Data;
using AprajitaRetails.Ops.Triggers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

//TODO: Negative or zero stock waring. 
//      Only admin can zero or neg stock 
//      Manual Stock Ajustment record . 
//      Implement Delete Invoice and Edit. or Marked Deleted /Canceled Invoice
//      must have option to provide Manul Invice no entry of Phsycaial Invoice. 
//      http://abctutorial.com/Post/35/mvc-5-master-details-using-jquery-ajax
//      http://www.dotnetawesome.com/2016/07/advance-master-details-entry-form-in-mvc.html


namespace AprajitaRetails.Areas.Sales.Controllers
{
  
    [Area("Sales")]
    [Authorize]
    public class ManualInvoiceController : Controller
    {
        private readonly AprajitaRetailsContext aprajitaContext;
        private readonly int StoreId = 1; //TODO: Fixed now
        private readonly UserManager<IdentityUser> UserManager;
        public ManualInvoiceController(AprajitaRetailsContext aCtx, UserManager<IdentityUser> userManager)
        {
            aprajitaContext = aCtx; UserManager = userManager;

        }
        public IActionResult Index()
        {
            var vm = aprajitaContext.RegularInvoices.Include(c => c.Customer).Include(c => c.SaleItems).Include(c => c.PaymentDetail)
                .Where(c => c.IsManualBill).OrderByDescending(c => c.OnDate).ThenByDescending(c => c.InvoiceNo).ToList();

            return View(vm);
        }

        public IActionResult MainView()
        {

            return View();
        }

        public IActionResult ReprintInvoice(int? id, int? Download)
        {

            var vm = aprajitaContext.RegularInvoices.Include(c => c.Customer).Include(c => c.SaleItems).Include(c => c.PaymentDetail).
                ThenInclude(c => c.CardDetail).Where(c => c.RegularInvoiceId == id).FirstOrDefault();

            string fileName = new RegularSaleManager().RePrintManaulInvoice(aprajitaContext, vm, StoreId);

            if (Download != null && Download == 101)
            {
                return File(fileName, "application/pdf", Path.GetFileName(fileName));
            }

            return File(fileName, "application/pdf");

        }

        [HttpGet]
        public JsonResult GetInvoiceDetails(int? id)
        {
            string errMsg = "Error!";
            InvoiceDetails retunDetails;
            if (id == null)
            {
                errMsg = "Kindly send Invoice No!";
                return Json(new { Msg = errMsg, Error = "true" });
            }
            retunDetails = SaleHelper.GetInvoiceData(aprajitaContext, (int)id);
            if (retunDetails == null)
            {
                errMsg = "Invoice Number Not found!";
                return Json(new { Msg = errMsg, Error = "true" });
            }
            retunDetails.Msg = "Data is loaded successfuly";
            retunDetails.Error = "OK";
            return Json(retunDetails);          
        }


        public JsonResult GetBarCode(string barcode)
        {
            try
            {
                var pItem = aprajitaContext.ProductItems.Where(c => c.Barcode == barcode).Select(c => new { c.MRP, c.ProductName, c.TaxRate, Units = "" + c.Units }).First();

                if (pItem == null)
                {
                    pItem = new
                    {
                        MRP = (decimal)0.0,
                        ProductName = "Not Found",
                        TaxRate = (decimal)0,
                        Units = Enum.GetName(typeof(Units), Units.Pcs)
                    };
                }
                else
                {
                    var pItem2 = new { MRP = pItem.MRP, ProductName = pItem.ProductName, TaxRate = pItem.TaxRate, Units = Enum.GetName(typeof(Units), Int32.Parse(pItem.Units)) };
                    // pItem.Units = Enum.GetName(typeof(Units), Int32.Parse( pItem.Units));
                    return new JsonResult(pItem2);
                }

                JsonResult result = new JsonResult(pItem)
                {
                    Value = pItem

                };
                return result;


            }
            catch (Exception ex)
            {

                var pItem = new { MRP = (decimal)0.0, ProductName = "Not Found!", TaxRate = (decimal)0, Units = Enum.GetName(typeof(Units), Units.Pcs) };
                JsonResult result = new JsonResult(pItem)
                {
                    Value = pItem

                };

                return result;
            }
            //return Json ( Data=pItem, JsonRequestBehavior = JsonRequestBehavior.AllowGet );
        }
        [HttpGet]
        public JsonResult GetSalesmanList()
        {
            var list = aprajitaContext.Salesmen.Where(c => c.StoreId == StoreId).OrderBy(c => c.SalesmanId).Select(c => new { c.SalesmanId, c.SalesmanName }).ToList();
            return new JsonResult(list);
        }

        [HttpPost]
        public ActionResult SaveOrder([FromBody] SaveOrderDTO dTO) /*string name, [FromBody] String address, [FromBody] SaleItemList[] saleItems)*/
        {
            string result = "Error! Order Is Not Complete!";
            if (dTO.Name != null && dTO.Address != null && dTO.SaleItems != null)
            {
                InvoiceSaveReturn x = new RegularSaleManager().OnInsert(aprajitaContext, dTO, User.Identity.Name, StoreId );
                if (x.NoOfRecord <= 0)
                    result = "Error while saving bill, Kindly try again!";
                else
                {
                    result = "Invoice is Generated! Kindly print if required";
                    return Json(new { x.FileName, result });
                }
            }
            return Json(new { FileName = new String("Error"), result });
        }

        public ActionResult SaveEditedInvoice([FromBody] SaveOrderDTO dTO)
        {

            string result = "Error! Order Is Not Complete!";

            return Json(new { FileName = new String("Error"), result });
        }

        /// <summary>
        /// Delete Bill No from Regular Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DeleteBillNo(int? id)
        {

            string errMsg = "Error!";
            int ret = 0;
            if (id == null)
            {
                errMsg = "Kindly send Invoice No!";
                return Json(new { Count = ret, Msg = errMsg });
            }

            var inv = aprajitaContext.RegularInvoices.Include(c => c.PaymentDetail).Include(c => c.SaleItems).Include(c => c.PaymentDetail.CardDetail).Where(c => c.RegularInvoiceId == id).FirstOrDefault();
            if (inv == null) errMsg = "Invoice Number Not found!";
            else
            {
                aprajitaContext.RegularInvoices.Remove(inv);
                ret = aprajitaContext.SaveChanges();
                if (ret > 0)
                {
                    errMsg = "Invoice is Deleted!";
                }
                else
                    errMsg = "It fails to delete Invoice!";

            }


            return Json(new { Count = ret, Msg = errMsg });
        }

        /// <summary>
        ///  Delete Invoice from InvoiceNo
        /// </summary>
        /// <param name="InvoiceNo"></param>
        /// <returns></returns>
        //[HttpGet]
        //public ActionResult DeleteBillNo(string InvoiceNo)
        //{

        //    string errMsg = "Error!";
        //    int ret = 0;
        //    if (String.IsNullOrEmpty(InvoiceNo))
        //    {
        //        errMsg = "Kindly send Invoice No!";
        //        return Json(new { Count = ret, Msg = errMsg });
        //    }

        //    var inv = aprajitaContext.RegularInvoices.Include(c => c.PaymentDetail).Include(c => c.SaleItems).Include(c => c.PaymentDetail.CardDetail).Where(c => c.InvoiceNo == InvoiceNo).FirstOrDefault();
        //    if (inv == null) errMsg = "Invoice Number Not found!";
        //    else
        //    {
        //        aprajitaContext.RegularInvoices.Remove(inv);
        //        ret = aprajitaContext.SaveChanges();
        //        if (ret > 0)
        //        {
        //            errMsg = "Invoice is Deleted!";
        //        }
        //        else
        //            errMsg = "It fails to delete Invoice!";

        //    }


        //    return Json(new { Count = ret, Msg = errMsg });
        //}

        [HttpPost]
        public ActionResult SaveBillNo([FromBody] SaveOrderDTO dTO)
        {
            string errMsg = "Error!";
            int ret = 0;
            if (dTO == null)
            {
                errMsg = "Kindly send Invoice Data!";
                return Json(new { Count = ret, Msg = errMsg });
            }

            return Json(new { Count = ret, Msg = errMsg });
        }
    }
}

/*
 * customer Name
Sn, BarCode Qty Des MRP Dis% DisAmt Addl Dis Value Scheme

Bin, SM BarCode Dept Qty Des	 MRP Disc% Disc Amt	Value

T Qty
T MRP 
T Dis
T Tax
===========
NEt Bill Amt
Round Off
Reciving Amt
Refund 

Payment 
Cash Paid    Card Amt
Card Type 	Auth Code
Card Bank	Vocuher Am
Card Code  Other Col	
Card Number

 */
