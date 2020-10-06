using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AprajitaRetails.Data;
using AprajitaRetails.Ops.Uploader;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AprajitaRetails.Areas.Uploader.Controllers
{
    [Area("Uploader")]
    [Authorize]

    public class ImportAttendanceController : Controller
    {
        private readonly AprajitaRetailsContext db;
      
        public ImportAttendanceController(AprajitaRetailsContext con)
        {
            db = con;
            
        }
        public IActionResult Index()
        {
            ViewData["EmployeeId"] = new SelectList(db.Employees, "EmployeeId", "StaffName");
            return View();
        }

        public IActionResult UploadData(string StoreCode, IFormFile FileUpload)
        {
            ExcelUploaders uploader = new ExcelUploaders();
            UploadReturn response = uploader.UploadAttendance(db, StoreCode, FileUpload);
            ViewBag.Status = response.ToString();
            if (response == UploadReturn.Success)
            {
                return RedirectToAction("ListUpload");
            }
            return View();
        }

        public IActionResult ListUpload()
        {
            return View(db.AttendancesImport.ToList());
        }

        public IActionResult UploadEmpAttendance()
        {
            ViewData["EmployeeId"] = new SelectList(db.Employees, "EmployeeId", "StaffName");

            return View();
        }

        public IActionResult UploadDataForEmp(IFormFile FileUpload, int EmployeeId)
        {
            ExcelUploaders uploader = new ExcelUploaders();
                     
            UploadReturn response = uploader.UploadAttendanceForEmp(db,FileUpload, EmployeeId);
            ViewBag.Status = response.ToString();
            if (response == UploadReturn.Success)
            {
                return RedirectToAction("ListUploadEmpWise", new { empId = EmployeeId } );
            }
            return View();
        }
        public IActionResult ListUploadEmpWise(int empId)
        {
            // Try to get for emp id based. 
            return View(db.Attendances.Include(a=>a.Employee).Include(a=>a.Store).Where(c=>c.EmployeeId==empId).ToList());
        }

    }
}
