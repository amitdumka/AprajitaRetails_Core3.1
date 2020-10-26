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
    public class CashPaymentsController : ControllerBase
    {
        private readonly AprajitaRetailsContext _context;

        public CashPaymentsController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: api/CashPayments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CashPayment>>> GetCashPayments()
        {
            return await _context.CashPayments.ToListAsync();
        }

        // GET: api/CashPayments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CashPayment>> GetCashPayment(int id)
        {
            var cashPayment = await _context.CashPayments.FindAsync(id);

            if (cashPayment == null)
            {
                return NotFound();
            }

            return cashPayment;
        }

        // PUT: api/CashPayments/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCashPayment(int id, CashPayment cashPayment)
        {
            if (id != cashPayment.CashPaymentId)
            {
                return BadRequest();
            }

            _context.Entry(cashPayment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CashPaymentExists(id))
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

        // POST: api/CashPayments
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<CashPayment>> PostCashPayment(CashPayment cashPayment)
        {
            _context.CashPayments.Add(cashPayment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCashPayment", new { id = cashPayment.CashPaymentId }, cashPayment);
        }

        // DELETE: api/CashPayments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CashPayment>> DeleteCashPayment(int id)
        {
            var cashPayment = await _context.CashPayments.FindAsync(id);
            if (cashPayment == null)
            {
                return NotFound();
            }

            _context.CashPayments.Remove(cashPayment);
            await _context.SaveChangesAsync();

            return cashPayment;
        }

        private bool CashPaymentExists(int id)
        {
            return _context.CashPayments.Any(e => e.CashPaymentId == id);
        }
    }
}
