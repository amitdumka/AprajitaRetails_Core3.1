using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AprajitaRetails.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AprajitaRetails.Areas.RestAPI.Controllers
{
    [Route ("api/[controller]")]
    [ApiController]
    public class SaleDataController : ControllerBase
    {
        private readonly AprajitaRetailsContext db;

        public SaleDataController(AprajitaRetailsContext context)
        {
            db = context;
        }
        // GET: api/SaleData
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string [] { "value1", "value2" };
        }

        // GET: api/SaleData/5
        [HttpGet ("{id}", Name = "Get")]
        public SortedList<string, decimal> Get(long id)
        {
            SortedList<string, decimal> empData = new SortedList<string, decimal> ();

            var staff = db.TelegramAuthUsers.Where (c => c.TelegramChatId == id).FirstOrDefault ();
            
            if ( staff != null )
            {
                if(staff.EmpType==EmpType.StoreManager || staff.EmpType == EmpType.Owner )
                {
                    var emps = db.Salesmen.Select (c => new { c.SalesmanId, c.SalesmanName }).ToList();
                    foreach ( var emp in emps )
                    {
                        var amount = db.DailySales.Include (c => c.Salesman).Where (c => c.Salesman.SalesmanName == emp.SalesmanName && c.SaleDate.Month==DateTime.Today.Month).Sum (c => c.Amount);
                        empData.Add (emp.SalesmanName, amount);

                    }
                }
                else
                {
                    var empName = db.Employees.Find (staff.EmployeeId).StaffName;
                    var amount = db.DailySales.Include (c => c.Salesman).Where (c => c.Salesman.SalesmanName == empName && c.SaleDate.Month == DateTime.Today.Month).Sum (c => c.Amount);
                    empData.Add (empName, amount);
                }
               
            }
            return empData;
        }
        [HttpGet ("{id}/{today}", Name = "GetSaleData")]
        public SortedList<string, decimal> Get(long id, int today)
        {
            SortedList<string, decimal> empData = new SortedList<string, decimal> ();

            var staff = db.TelegramAuthUsers.Where (c => c.TelegramChatId == id).FirstOrDefault ();

            if ( staff != null )
            {
                if ( staff.EmpType == EmpType.StoreManager || staff.EmpType == EmpType.Owner )
                {
                    var emps = db.Salesmen.Select (c => new { c.SalesmanId, c.SalesmanName }).ToList();
                    foreach ( var emp in emps )
                    {
                        var amount = db.DailySales.Include (c => c.Salesman).Where (c => c.Salesman.SalesmanName == emp.SalesmanName && c.SaleDate.Date == DateTime.Today.Date).Sum (c => c.Amount);
                        empData.Add (emp.SalesmanName, amount);

                    }
                }
                else
                {
                    var empName = db.Employees.Find (staff.EmployeeId).StaffName;
                    var amount = db.DailySales.Include (c => c.Salesman).Where (c => c.Salesman.SalesmanName == empName && c.SaleDate.Date == DateTime.Today.Date).Sum (c => c.Amount);
                    empData.Add (empName, amount);
                }

            }
            return empData;
        }

        // POST: api/SaleData
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/SaleData/5
        [HttpPut ("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete ("{id}")]
        public void Delete(int id)
        {
        }
    }
}
