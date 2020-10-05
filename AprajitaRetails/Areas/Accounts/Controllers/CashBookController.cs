using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AprajitaRetails.Ops.Utility;
using AprajitaRetails.Data;
using System;
using AprajitaRetails.Models.ViewModels;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using AprajitaRetails.Ops.Printers.Reports;
using AprajitaRetails.Ops.Exporter;

namespace AprajitaRetails.Areas.Accounts.Controllers
{
    [Area("Accounts")]
    [Authorize(Roles = "Admin,PowerUser,StoreManager")]
    public class CashBookController : Controller
    {
        private readonly AprajitaRetailsContext db;
        private readonly int StoreId = 1; //TODO: default for testing
        public CashBookController(AprajitaRetailsContext context)
        {
            db = context;
        }
        // GET: CashBook
        public IActionResult Index(int? id, DateTime? EDate, string ModeType, string OpsType, string OutputType)
        {

            string fileName = /*"ExcelFiles\\" +*/ "Export_CashBook_" + DateTime.Now.ToFileTimeUtc().ToString() + ".xlsx";
            string path = Path.Combine("wwwroot", fileName);
            FileInfo file = new FileInfo(path);
            if (!file.Directory.Exists)
                file.Directory.Create();
            ViewBag.FileName = "";

            CashBookManagerExporter managerexporter = new CashBookManagerExporter(StoreId);
            CashBookManager manager = new CashBookManager(StoreId);
            List<CashBook> cashList;
            if (!String.IsNullOrEmpty(OpsType))
            {
                if (OpsType == "Correct")
                {
                    //TODO: Implement Correct cash in hand
                    ViewBag.Message = "Cash Book Correction  ";

                    if (ModeType == "MonthWise")
                        cashList = managerexporter.CorrectCashInHands(db, EDate.Value.Date, path, StoreId,false);
                    else
                        cashList = managerexporter.CorrectCashInHands(db, EDate.Value.Date, path, StoreId, true);

                    ViewBag.FileName = "/" + fileName;
                }
                else
                {
                    ViewBag.Message = "";
                }
            }
            if (EDate != null)
            {
                if (ModeType == "MonthWise")
                    cashList = manager.GetMontlyCashBook(db, EDate.Value.Date,StoreId);
                else
                    cashList = manager.GetDailyCashBook(db, EDate.Value.Date, StoreId);
            }
            else if (id == 101)
            {
                cashList = manager.GetDailyCashBook(db, DateTime.Now, StoreId);
            }
            else
            {
                cashList = manager.GetMontlyCashBook(db, DateTime.Now, StoreId);
            }
            if (cashList != null)
            {
                if (!String.IsNullOrEmpty(OutputType) && OutputType == "PDF")
                {
                    string fName = ReportPrinter.PrintCashBook(cashList);
                    return File(fName, "application/pdf");
                }
                else if (!String.IsNullOrEmpty(OutputType) && OutputType == "XLS")
                {
                    if (OpsType == "List")
                        ExcelExporter.CashBookExporter(path, cashList, "CashBook", StoreId);

                    return File(fileName, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                }
                return View(cashList);
            }
            else
                return NotFound();
        }
     
    }
}