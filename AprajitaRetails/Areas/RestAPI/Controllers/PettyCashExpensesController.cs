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
    public class PettyCashExpensesController : ControllerBase
    {
        private readonly AprajitaRetailsContext _context;

        public PettyCashExpensesController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: api/PettyCashExpenses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PettyCashExpense>>> GetPettyCashExpenses()
        {
            return await _context.PettyCashExpenses.ToListAsync();
        }

        // GET: api/PettyCashExpenses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PettyCashExpense>> GetPettyCashExpense(int id)
        {
            var pettyCashExpense = await _context.PettyCashExpenses.FindAsync(id);

            if (pettyCashExpense == null)
            {
                return NotFound();
            }

            return pettyCashExpense;
        }

        // PUT: api/PettyCashExpenses/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPettyCashExpense(int id, PettyCashExpense pettyCashExpense)
        {
            if (id != pettyCashExpense.PettyCashExpenseId)
            {
                return BadRequest();
            }

            _context.Entry(pettyCashExpense).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PettyCashExpenseExists(id))
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

        // POST: api/PettyCashExpenses
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<PettyCashExpense>> PostPettyCashExpense(PettyCashExpense pettyCashExpense)
        {
            _context.PettyCashExpenses.Add(pettyCashExpense);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPettyCashExpense", new { id = pettyCashExpense.PettyCashExpenseId }, pettyCashExpense);
        }

        // DELETE: api/PettyCashExpenses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PettyCashExpense>> DeletePettyCashExpense(int id)
        {
            var pettyCashExpense = await _context.PettyCashExpenses.FindAsync(id);
            if (pettyCashExpense == null)
            {
                return NotFound();
            }

            _context.PettyCashExpenses.Remove(pettyCashExpense);
            await _context.SaveChangesAsync();

            return pettyCashExpense;
        }

        private bool PettyCashExpenseExists(int id)
        {
            return _context.PettyCashExpenses.Any(e => e.PettyCashExpenseId == id);
        }
    }
}
