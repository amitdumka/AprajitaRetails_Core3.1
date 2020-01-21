using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AprajitaRetails.Data;
using AprajitaRetails.Models;
using AprajitaRetails.Ops.Triggers;
using System.Globalization;
using AprajitaRetails;

namespace AprajitaRetails.Sales.Expenses.Controllers
{
    [Area("Sales")]
    public class DailySalesController : Controller
    {
        private readonly AprajitaRetailsContext db;
        readonly CultureInfo c = CultureInfo.GetCultureInfo("In");


        public DailySalesController(AprajitaRetailsContext context)
        {
            db = context;
        }

        // GET: DailySales
        public async Task<IActionResult> Index(int? id, string salesmanId, string currentFilter, string searchString, DateTime? SaleDate, string sortOrder, int? pageNumber)
        {
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
            var dailySales = db.DailySales.Include(d => d.Salesman).Where(c => c.SaleDate == DateTime.Today);
            
            if (id != null && id == 101)
            {
                dailySales = db.DailySales.Include(d => d.Salesman).OrderByDescending(c => c.SaleDate).ThenByDescending(c => c.DailySaleId);
            }
            else if (id != null && id == 102)
            {
                dailySales = db.DailySales.Include(d => d.Salesman).Where(c => c.SaleDate.Month == DateTime.Today.Month).OrderByDescending(c => c.SaleDate).ThenByDescending(c => c.DailySaleId);

            }
            else
            {
                dailySales = dailySales.OrderByDescending(c => c.SaleDate).ThenByDescending(c => c.DailySaleId);
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                dailySales = db.DailySales.Include(d => d.Salesman).Where(c => c.InvNo == searchString);
                //return View(await dls.ToListAsync());

            }
            else if (!String.IsNullOrEmpty(salesmanId) || SaleDate != null)
            {
                //IEnumerable<DailySale> DailySales;

                if (SaleDate != null)
                {
                    dailySales = db.DailySales.Include(d => d.Salesman).Where(c => (c.SaleDate) == (SaleDate)).OrderByDescending(c => c.DailySaleId);
                }
                else
                {
                    dailySales = db.DailySales.Include(d => d.Salesman).Where(c => (c.SaleDate.Month) == (DateTime.Today.Month)).OrderByDescending(c => c.SaleDate).ThenByDescending(c => c.DailySaleId);
                }

                if (!String.IsNullOrEmpty(salesmanId))
                {
                    dailySales = dailySales.Where(c => c.Salesman.SalesmanName == salesmanId);
                }

                //return View(DailySales);

            }

            // For All Invoice
           
            #region FixedUI 
            //Fixed Query
            var totalSale = db.DailySales.Where(c => c.IsManualBill == false && c.SaleDate.Date == DateTime.Today.Date).Sum(c => (decimal?)c.Amount) ?? 0;
            var totalManualSale = db.DailySales.Where(c => c.IsManualBill == true && c.SaleDate.Date == DateTime.Today.Date).Sum(c => (decimal?)c.Amount) ?? 0;
            var totalMonthlySale = db.DailySales.Where(c => c.SaleDate.Month == DateTime.Today.Month).Sum(c => (decimal?)c.Amount) ?? 0;
            var duesamt = db.DuesLists.Where(c => c.IsRecovered == false).Sum(c => (decimal?)c.Amount) ?? 0;
            var cashinhand = (decimal)0.00;
            try
            {
                cashinhand = db.CashInHands.Where(c => c.CIHDate == DateTime.Today).FirstOrDefault().InHand;
            }
            catch (Exception)
            {
                // Utils.ProcessOpenningClosingBalance(db, DateTime.Today, false, true);
                new CashWork().Process_OpenningBalance(db, DateTime.Today, true);
                cashinhand = (decimal)0.00;
                //Log.Error("Cash In Hand is null");
            }
            // Fixed UI
            ViewBag.TodaySale = totalSale;
            ViewBag.ManualSale = totalManualSale;
            ViewBag.MonthlySale = totalMonthlySale;
            ViewBag.DuesAmount = duesamt;
            ViewBag.CashInHand = cashinhand;

            #endregion
           




            #region bySalesman
            // By Salesman
            var salesmanList = new List<string>();
            var smQry = from d in db.Salesmen
                        orderby d.SalesmanName
                        select d.SalesmanName;
            salesmanList.AddRange(smQry.Distinct());
            ViewBag.salesmanId = new SelectList(salesmanList);

            #endregion
            #region ByDate
            //var dateList = new List<DateTime>();
            //var opdQry = from d in db.DailySales
            //             orderby d.SaleDate
            //             select d.SaleDate;
            //dateList.AddRange(opdQry.Distinct());
            //ViewBag.dateID = new SelectList(dateList);

            #endregion
            
            
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

        // GET: DailySales/Create
        public IActionResult Create()
        {
            ViewData["SalesmanId"] = new SelectList(db.Salesmen, "SalesmanId", "SalesmanName");
            return PartialView();
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
                db.Add(dailySale);
                new SalesManager().OnInsert(db, dailySale);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["SalesmanId"] = new SelectList(db.Salesmen, "SalesmanId", "SalesmanName", dailySale.SalesmanId);
            return PartialView(dailySale);
        }

        // GET: DailySales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dailySale = await db.DailySales.FindAsync(id);
            if (dailySale == null)
            {
                return NotFound();
            }
            ViewData["SalesmanId"] = new SelectList(db.Salesmen, "SalesmanId", "SalesmanName", dailySale.SalesmanId);
            return PartialView(dailySale);
        }

        // POST: DailySales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dailySale = await db.DailySales.FindAsync(id);
            new SalesManager().OnDelete(db, dailySale);

            db.DailySales.Remove(dailySale);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DailySaleExists(int id)
        {
            return db.DailySales.Any(e => e.DailySaleId == id);
        }
    }
}
