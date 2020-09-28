using AprajitaRetails.Data;
using AprajitaRetails.Models;
using AprajitaRetails.Ops.Triggers;
using AprajitaRetails.Ops.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AprajitaRetails.Sales.Expenses.Controllers
{
    [Area("Sales")]
    [Authorize]
    public class DailySalesController : Controller
    {
        //Version 3.0
        private int StoreCodeId = 1;   //TODO:Default Value. For Now.

        private readonly AprajitaRetailsContext db;
        private SortedList<string, string> SessionData;
        private readonly CultureInfo c = CultureInfo.GetCultureInfo("In");
        private readonly ILogger<DailySalesController> logger;

        public DailySalesController(AprajitaRetailsContext context, ILogger<DailySalesController> logger)
        {
            this.logger = logger;
            db = context;
        }

        // GET: DailySales
        public async Task<IActionResult> Index(int? id, string salesmanId, string currentFilter, string searchString, DateTime? SaleDate, string sortOrder, int? pageNumber)
        {
            if (SessionCookies.IsSessionSet(HttpContext))
            {
                SessionData = SessionCookies.GetLoginSessionInfo(HttpContext);
                StoreCodeId = Int32.Parse(SessionData[Constants.STOREID]);
            }
            else
            {
                //TODO: Redirect to login Page
            }
            ViewData["InvoiceSortParm"] = String.IsNullOrEmpty(sortOrder) ? "inv_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["ManualSortParm"] = sortOrder == "Manual" ? "notManual_desc" : "Manual";
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;
            //For Current Day
            var dailySales = db.DailySales.Include(d => d.Salesman).Where(c => c.SaleDate == DateTime.Today && c.StoreId == this.StoreCodeId);

            if (id != null && id == 101)
            {
                //All
                dailySales = db.DailySales.Include(d => d.Salesman).Where(c => c.StoreId == this.StoreCodeId).OrderByDescending(c => c.SaleDate).ThenByDescending(c => c.DailySaleId);
            }
            else if (id != null && id == 102)
            {
                //Current Month
                dailySales = db.DailySales.Include(d => d.Salesman).Where(c => c.SaleDate.Month == DateTime.Today.Month && c.SaleDate.Year == DateTime.Today.Year && c.StoreId == this.StoreCodeId).OrderByDescending(c => c.SaleDate).ThenByDescending(c => c.DailySaleId);
            }
            else if (id != null && id == 103)
            {
                //Last Month
                dailySales = db.DailySales.Include(d => d.Salesman).Where(c => c.SaleDate.Month == DateTime.Today.Month - 1 && c.SaleDate.Year == DateTime.Today.Year && c.StoreId == this.StoreCodeId).OrderByDescending(c => c.SaleDate).ThenByDescending(c => c.DailySaleId);
            }
            else
            {
                dailySales = dailySales.OrderByDescending(c => c.SaleDate).ThenByDescending(c => c.DailySaleId);
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                dailySales = db.DailySales.Include(d => d.Salesman).Where(c => c.InvNo == searchString && c.StoreId == this.StoreCodeId);
                //return View(await dls.ToListAsync());
            }
            else if (!String.IsNullOrEmpty(salesmanId) || SaleDate != null)
            {
                //IEnumerable<DailySale> DailySales;

                if (SaleDate != null)
                {
                    dailySales = db.DailySales.Include(d => d.Salesman).Where(c => c.SaleDate == SaleDate && c.StoreId == this.StoreCodeId).OrderByDescending(c => c.DailySaleId);
                }
                else
                {
                    dailySales = db.DailySales.Include(d => d.Salesman).Where(c => c.SaleDate.Month == DateTime.Today.Month && c.SaleDate.Year == DateTime.Today.Year && c.StoreId == this.StoreCodeId).OrderByDescending(c => c.SaleDate).ThenByDescending(c => c.DailySaleId);
                }

                if (!String.IsNullOrEmpty(salesmanId))
                {
                    dailySales = dailySales.Where(c => c.Salesman.SalesmanName == salesmanId);
                }
            }

            // For All Invoice
            //TODO: Make a Static class and function to fetch details

            #region FixedUI

            //Fixed Query
            var totalSale = db.DailySales.Where(c => c.IsManualBill == false && c.SaleDate.Date == DateTime.Today.Date && c.StoreId == this.StoreCodeId).Sum(c => (decimal?)c.Amount) ?? 0;
            var totalManualSale = db.DailySales.Where(c => c.IsManualBill == true && c.SaleDate.Date == DateTime.Today.Date && c.StoreId == this.StoreCodeId).Sum(c => (decimal?)c.Amount) ?? 0;
            var totalMonthlySale = db.DailySales.Where(c => c.SaleDate.Year == DateTime.Today.Year && c.SaleDate.Month == DateTime.Today.Month && c.StoreId == this.StoreCodeId).Sum(c => (decimal?)c.Amount) ?? 0;
            var totalLastMonthlySale = db.DailySales.Where(c => c.SaleDate.Year == DateTime.Today.Year && c.SaleDate.Month == DateTime.Today.Month - 1 && c.StoreId == this.StoreCodeId).Sum(c => (decimal?)c.Amount) ?? 0;
            var duesamt = db.DuesLists.Where(c => c.IsRecovered == false && c.StoreId == this.StoreCodeId).Sum(c => (decimal?)c.Amount) ?? 0;
            var cashinhand = (decimal)0.00;
            try
            {
                cashinhand = db.CashInHands.Where(c => c.CIHDate == DateTime.Today && c.StoreId == this.StoreCodeId).FirstOrDefault().InHand;
            }
            catch (Exception)
            {
                // Utility.ProcessOpenningClosingBalance(db, DateTime.Today, false, true);
                new CashWork().ProcessOpenningBalance(db, DateTime.Today, StoreCodeId, true);
                cashinhand = (decimal)0.00;
                //Log.Error("Cash In Hand is null");
            }
            // Fixed UI
            ViewBag.TodaySale = totalSale;
            ViewBag.ManualSale = totalManualSale;
            ViewBag.MonthlySale = totalMonthlySale;
            ViewBag.DuesAmount = duesamt;
            ViewBag.CashInHand = cashinhand;
            ViewBag.LastMonthSale = totalLastMonthlySale;

            #endregion FixedUI

            #region bySalesman

            // By Salesman
            var salesmanList = new List<string>();
            var smQry = from d in db.Salesmen
                        orderby d.SalesmanName
                        select d.SalesmanName;
            salesmanList.AddRange(smQry.Distinct());
            ViewBag.salesmanId = new SelectList(salesmanList);

            #endregion bySalesman

            #region ByDate

            //var dateList = new List<DateTime>();
            //var opdQry = from d in db.DailySales
            //             orderby d.SaleDate
            //             select d.SaleDate;
            //dateList.AddRange(opdQry.Distinct());
            //ViewBag.dateID = new SelectList(dateList);

            #endregion ByDate

            //By Invoice No Search

            switch (sortOrder)
            {
                case "Manual":
                    dailySales = dailySales.OrderBy(c => c.IsManualBill);
                    break;

                case "notManual_desc":
                    dailySales = dailySales.OrderByDescending(c => c.IsManualBill);
                    break;

                case "inv_desc":
                    dailySales = dailySales.OrderByDescending(s => s.InvNo);
                    break;

                case "Date":
                    dailySales = dailySales.OrderBy(s => s.SaleDate);
                    break;

                case "date_desc":
                    dailySales = dailySales.OrderByDescending(s => s.SaleDate);
                    break;

                default:
                    dailySales = dailySales.OrderBy(s => s.InvNo);
                    break;
            }
            //For Day or All Questry
            //return View(await dailySales.ToListAsync());

            int pageSize = 10;
            return View(await PaginatedList<DailySale>.CreateAsync(dailySales.AsNoTracking(), pageNumber ?? 1, pageSize));

            //OrignalCode
            // var aprajitaRetailsContext = db.DailySales.Include(d => d.Salesman);
            //return View(await aprajitaRetailsContext.ToListAsync());
        }

        // GET: DailySales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                logger.LogWarning("DailySale:Details/ID is null");
                return NotFound();
            }

            var dailySale = await db.DailySales
                .Include(d => d.Salesman)
                .FirstOrDefaultAsync(m => m.DailySaleId == id);
            if (dailySale == null)
            {
                logger.LogError("DailySale:Details  Not Found");
                return NotFound();
            }

            return PartialView(dailySale);
        }

        // GET: DailySales/Create
        public IActionResult Create()
        {
            ViewData["SalesmanId"] = new SelectList(db.Salesmen, "SalesmanId", "SalesmanName");
            return PartialView();
        }

        public async Task<IActionResult> _AddEditPaymentDetailsAsync( string invNumber="add")
        {
            ViewData["EDCId"] = new SelectList(db.CardMachine, "EDCId", "EDCName");
            if (invNumber == "add")
            {
                
                return View(new EDCTranscation { OnDate=DateTime.Today.Date});
            }
            else
            {
                var paydetails = await db.CardTranscations.Where(c => c.InvoiceNumber == invNumber).FirstOrDefaultAsync();

                if (paydetails == null)
                {
                    return NotFound();
                }
                return View(paydetails);

                //return PartialView();
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> _AddEditPaymentDetailsAsync(int id, [Bind("TransactionId,AccountNumber,BeneficiaryName,BankName,SWIFTCode,Amount,Date")] EDCTranscation eDC)
        {
            if (ModelState.IsValid)
            {
                //Insert
                if (id == 0)
                {
                    
                    db.Add(eDC);
                    await db.SaveChangesAsync();

                }
                //Update
                else
                {
                    try
                    {
                        db.Update(eDC);
                        await db.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!CardTranscationExists(eDC.EDCTranscationId))
                        { return NotFound(); }
                        else
                        { throw; }
                    }
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", db.CardTranscations.ToList()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", eDC) });
        }

        // POST: DailySales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DailySaleId,SaleDate,InvNo,Amount,PayMode,CashAmount,SalesmanId,IsDue,IsManualBill,IsTailoringBill,IsSaleReturn,Remarks")] DailySale dailySale)
        {
            if (ModelState.IsValid)
            {
                //version 3.0  StoreCode
                dailySale.StoreId = this.StoreCodeId;
                dailySale.UserName = User.Identity.Name;

                db.Add(dailySale);
                new SalesManager().OnInsert(db, dailySale);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            logger.LogWarning("DailySale:Create[Post] ModelState is not Valid!");
            ViewData["SalesmanId"] = new SelectList(db.Salesmen, "SalesmanId", "SalesmanName", dailySale.SalesmanId);
            return PartialView(dailySale);
        }

        // GET: DailySales/Edit/5
        [Authorize(Roles = "Admin,PowerUser,StoreManager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                logger.LogWarning("DailySale:Edit[Get] ID is null!");
                return NotFound();
            }

            var dailySale = await db.DailySales.FindAsync(id);
            if (dailySale == null)
            {
                logger.LogWarning("DailySale:Edit[Get] DailSale  not found!");
                return NotFound();
            }
            logger.LogWarning("DailySale:Edit[Get] ModelState is not Valid!");
            ViewData["SalesmanId"] = new SelectList(db.Salesmen, "SalesmanId", "SalesmanName", dailySale.SalesmanId);
            return PartialView(dailySale);
        }

        // POST: DailySales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,PowerUser,StoreManager")]
        public async Task<IActionResult> Edit(int id, [Bind("DailySaleId,SaleDate,InvNo,Amount,PayMode,CashAmount,SalesmanId,IsDue,IsManualBill,IsTailoringBill,IsSaleReturn,Remarks")] DailySale dailySale)
        {
            if (id != dailySale.DailySaleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    dailySale.StoreId = this.StoreCodeId;
                    dailySale.UserName = User.Identity.Name;

                    db.Update(dailySale);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DailySaleExists(dailySale.DailySaleId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["SalesmanId"] = new SelectList(db.Salesmen, "SalesmanId", "SalesmanName", dailySale.SalesmanId);
            return PartialView(dailySale);
        }

        // GET: DailySales/Delete/5
        [Authorize(Roles = "Admin,PowerUser")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dailySale = await db.DailySales
                .Include(d => d.Salesman)
                .FirstOrDefaultAsync(m => m.DailySaleId == id);
            if (dailySale == null)
            {
                return NotFound();
            }

            return PartialView(dailySale);
        }

        // POST: DailySales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,PowerUser")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dailySale = await db.DailySales.FindAsync(id);
            new SalesManager().OnDelete(db, dailySale);

            db.DailySales.Remove(dailySale);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedPayment(int id)
        {
            var transactionModel = await db.CardTranscations.FindAsync(id);
            db.CardTranscations.Remove(transactionModel);
            await db.SaveChangesAsync();
            return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAll", db.CardTranscations.ToList()) });
        }

        private bool DailySaleExists(int id)
        {
            return db.DailySales.Any(e => e.DailySaleId == id);
        }
        private bool CardTranscationExists(int id)
        {
            return db.CardTranscations.Any(e => e.EDCTranscationId == id);
        }
    }

    public class Helper
    {

        public static string RenderRazorViewToString(Controller controller, string viewName, object model = null)
        {
            controller.ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                IViewEngine viewEngine = controller.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
                ViewEngineResult viewResult = viewEngine.FindView(controller.ControllerContext, viewName, false);

                ViewContext viewContext = new ViewContext(
                    controller.ControllerContext,
                    viewResult.View,
                    controller.ViewData,
                    controller.TempData,
                    sw,
                    new HtmlHelperOptions()
                );
                viewResult.View.RenderAsync(viewContext);
                return sw.GetStringBuilder().ToString();
            }
        }

    }
}

//https://www.codaffection.com/asp-net-core-article/how-to-use-jquery-ajax-in-asp-net-core-mvc-for-crud-operations-with-modal-popup/#Let%E2%80%99s_Start_Designing_the_App

//https://morioh.com/p/cac7badbf881