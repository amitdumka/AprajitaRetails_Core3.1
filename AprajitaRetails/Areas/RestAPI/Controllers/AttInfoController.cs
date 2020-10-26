using AprajitaRetails.Areas.RestAPI.Models;
using AprajitaRetails.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AprajitaRetails.Areas.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttInfoController : ControllerBase
    {
        AprajitaRetailsContext db;
        public AttInfoController(AprajitaRetailsContext con)
        {
            db = con;

        }
        // GET: api/AttInfo
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/AttInfo/5
        [HttpGet("{id:long}", Name = "GetAtt")]
        public string GetAtt(long id)
        {
            var usr = DBHelper.GetChatUser(db, id);

            if (usr == null) return "You are authorize to use this service!.";

            if (usr.EmpType == EmpType.Owner || usr.EmpType == EmpType.StoreManager)
            {
                var empList = db.Employees.Where(c => c.IsWorking && c.Category != EmpType.Owner).Select(c => new { c.EmployeeId, c.IsWorking, c.StaffName }).ToList();
                if (empList != null)
                {
                    string msg = "Attendances Info:\n";
                    foreach (var emp in empList)
                    {
                        if (emp != null && emp.IsWorking)
                        {
                            var present = db.Attendances.Where(c => (c.Status == AttUnit.Present || c.Status == AttUnit.Sunday) && c.EmployeeId == emp.EmployeeId && c.AttDate.Month == DateTime.Today.Month).Count();
                            var absent = db.Attendances.Where(c => c.Status == AttUnit.Absent && c.EmployeeId == emp.EmployeeId && c.AttDate.Month == DateTime.Today.Month).Count();
                            var halfDay = db.Attendances.Where(c => c.Status == AttUnit.HalfDay && c.EmployeeId == emp.EmployeeId && c.AttDate.Month == DateTime.Today.Month).Count();
                            float tp = present + (halfDay / 2);
                            msg += $"StaffName: {emp.StaffName} \t  Present: {tp}\t Absent: {absent}\n";
                        }
                    }
                    return msg;
                }
                else return "Failed to get Employee details";

            }
            else
            {
                var emp = db.Employees.Where(c => c.EmployeeId == usr.EmployeeId).Select(c => new { c.EmployeeId, c.IsWorking, c.StaffName }).FirstOrDefault();
                if (emp != null && emp.IsWorking)
                {
                    var present = db.Attendances.Where(c => c.Status == AttUnit.Present || c.Status == AttUnit.Sunday && c.EmployeeId == emp.EmployeeId && c.AttDate.Month == DateTime.Today.Month).Count();
                    var absent = db.Attendances.Where(c => c.Status == AttUnit.Absent && c.EmployeeId == emp.EmployeeId && c.AttDate.Month == DateTime.Today.Month).Count();
                    var halfDay = db.Attendances.Where(c => c.Status == AttUnit.HalfDay && c.EmployeeId == emp.EmployeeId && c.AttDate.Month == DateTime.Today.Month).Count();

                    float tp = present + (halfDay / 2);
                    string msg = $"StaffName: {emp.StaffName} \t  Presnt: {tp}\t Absent: {absent}  ";
                    return msg;
                }
                else
                {
                    return "Your Information is not avilable, Kindly Contact Store Manager or Admin!";
                }
            }
        }

        // POST: api/AttInfo
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/AttInfo/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
