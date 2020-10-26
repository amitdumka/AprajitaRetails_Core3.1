using AprajitaRetails.Data;
using AprajitaRetails.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AprajitaRetails.Areas.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DueRecoverdsController : ControllerBase
    {
        private readonly AprajitaRetailsContext _context;

        public DueRecoverdsController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: api/DueRecoverds
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DueRecoverd>>> GetDueRecoverds()
        {
            return await _context.DueRecoverds.ToListAsync();
        }

        // GET: api/DueRecoverds/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DueRecoverd>> GetDueRecoverd(int id)
        {
            var dueRecoverd = await _context.DueRecoverds.FindAsync(id);

            if (dueRecoverd == null)
            {
                return NotFound();
            }

            return dueRecoverd;
        }

        // PUT: api/DueRecoverds/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDueRecoverd(int id, DueRecoverd dueRecoverd)
        {
            if (id != dueRecoverd.DueRecoverdId)
            {
                return BadRequest();
            }

            _context.Entry(dueRecoverd).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DueRecoverdExists(id))
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

        // POST: api/DueRecoverds
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<DueRecoverd>> PostDueRecoverd(DueRecoverd dueRecoverd)
        {
            _context.DueRecoverds.Add(dueRecoverd);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDueRecoverd", new { id = dueRecoverd.DueRecoverdId }, dueRecoverd);
        }

        // DELETE: api/DueRecoverds/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DueRecoverd>> DeleteDueRecoverd(int id)
        {
            var dueRecoverd = await _context.DueRecoverds.FindAsync(id);
            if (dueRecoverd == null)
            {
                return NotFound();
            }

            _context.DueRecoverds.Remove(dueRecoverd);
            await _context.SaveChangesAsync();

            return dueRecoverd;
        }

        private bool DueRecoverdExists(int id)
        {
            return _context.DueRecoverds.Any(e => e.DueRecoverdId == id);
        }
    }
}
