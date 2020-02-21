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
    public class DuesListsController : ControllerBase
    {
        private readonly AprajitaRetailsContext _context;

        public DuesListsController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: api/DuesLists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DuesList>>> GetDuesLists()
        {
            return await _context.DuesLists.ToListAsync();
        }

        // GET: api/DuesLists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DuesList>> GetDuesList(int id)
        {
            var duesList = await _context.DuesLists.FindAsync(id);

            if (duesList == null)
            {
                return NotFound();
            }

            return duesList;
        }

        // PUT: api/DuesLists/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDuesList(int id, DuesList duesList)
        {
            if (id != duesList.DuesListId)
            {
                return BadRequest();
            }

            _context.Entry(duesList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DuesListExists(id))
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

        // POST: api/DuesLists
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<DuesList>> PostDuesList(DuesList duesList)
        {
            _context.DuesLists.Add(duesList);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDuesList", new { id = duesList.DuesListId }, duesList);
        }

        // DELETE: api/DuesLists/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DuesList>> DeleteDuesList(int id)
        {
            var duesList = await _context.DuesLists.FindAsync(id);
            if (duesList == null)
            {
                return NotFound();
            }

            _context.DuesLists.Remove(duesList);
            await _context.SaveChangesAsync();

            return duesList;
        }

        private bool DuesListExists(int id)
        {
            return _context.DuesLists.Any(e => e.DuesListId == id);
        }
    }
}
