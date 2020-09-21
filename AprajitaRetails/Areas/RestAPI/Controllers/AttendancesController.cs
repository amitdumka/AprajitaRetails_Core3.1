using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AprajitaRetails.Data;
using AprajitaRetails.Models;

namespace AprajitaRetails.Areas.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendancesController : ControllerBase
    {
        private readonly AprajitaRetailsContext _context;

        public AttendancesController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: api/Attendances
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Attendance>>> GetAttendances() => await _context.Attendances.ToListAsync();

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Attendance>>> GetAttendances(int EmployeeId, int Year) => await _context.Attendances.Where(c => c.EmployeeId == EmployeeId && c.AttDate.Year == Year).ToListAsync();
        // GET: api/Attendances/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Attendance>> GetAttendance(int id)
        {
            var attendance = await _context.Attendances.FindAsync(id);

            if (attendance == null)
            {
                return NotFound();
            }

            return attendance;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Attendance>>> GetAttendances(int EmployeeId, int Year, string UserName)
        {
            // TODO: Check for UserName. if UserName and EmployeeId is Same then only provide Data. other Wise dont Send Data.
            return await _context.Attendances.Where(c => c.EmployeeId == EmployeeId && c.AttDate.Year == Year).ToListAsync();
        }
        // GET: api/Attendances/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Attendance>> GetAttendance(int id, string UserName)
        {
            // TODO: Check for UserName. if UserName and EmployeeId is Same then only provide Data. other Wise dont Send Data.
            
            var attendance = await _context.Attendances.FindAsync(id);

            if (attendance == null)
            {
                return NotFound();
            }

            return attendance;
        }

        // PUT: api/Attendances/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAttendance(int id, Attendance attendance)
        {
            if (id != attendance.AttendanceId)
            {
                return BadRequest();
            }

            _context.Entry(attendance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttendanceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Attendances
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Attendance>> PostAttendance(Attendance attendance)
        {
            _context.Attendances.Add(attendance);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAttendance", new { id = attendance.AttendanceId }, attendance);
        }

        // DELETE: api/Attendances/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Attendance>> DeleteAttendance(int id)
        {
            var attendance = await _context.Attendances.FindAsync(id);
            if (attendance == null)
            {
                return NotFound();
            }

            _context.Attendances.Remove(attendance);
            await _context.SaveChangesAsync();

            return attendance;
        }

        private bool AttendanceExists(int id) => _context.Attendances.Any(e => e.AttendanceId == id);

        // GET: api/Attendances/5
        [HttpGet("{id}")]
        public bool IsAttendanceExists(int id) => _context.Attendances.Any(e => e.AttendanceId == id);



    }
}
