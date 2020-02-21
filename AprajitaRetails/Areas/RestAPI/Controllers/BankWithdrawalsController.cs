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
    public class BankWithdrawalsController : ControllerBase
    {
        private readonly AprajitaRetailsContext _context;

        public BankWithdrawalsController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: api/BankWithdrawals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BankWithdrawal>>> GetBankWithdrawals()
        {
            return await _context.BankWithdrawals.ToListAsync();
        }

        // GET: api/BankWithdrawals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BankWithdrawal>> GetBankWithdrawal(int id)
        {
            var bankWithdrawal = await _context.BankWithdrawals.FindAsync(id);

            if (bankWithdrawal == null)
            {
                return NotFound();
            }

            return bankWithdrawal;
        }

        // PUT: api/BankWithdrawals/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBankWithdrawal(int id, BankWithdrawal bankWithdrawal)
        {
            if (id != bankWithdrawal.BankWithdrawalId)
            {
                return BadRequest();
            }

            _context.Entry(bankWithdrawal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BankWithdrawalExists(id))
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

        // POST: api/BankWithdrawals
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<BankWithdrawal>> PostBankWithdrawal(BankWithdrawal bankWithdrawal)
        {
            _context.BankWithdrawals.Add(bankWithdrawal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBankWithdrawal", new { id = bankWithdrawal.BankWithdrawalId }, bankWithdrawal);
        }

        // DELETE: api/BankWithdrawals/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BankWithdrawal>> DeleteBankWithdrawal(int id)
        {
            var bankWithdrawal = await _context.BankWithdrawals.FindAsync(id);
            if (bankWithdrawal == null)
            {
                return NotFound();
            }

            _context.BankWithdrawals.Remove(bankWithdrawal);
            await _context.SaveChangesAsync();

            return bankWithdrawal;
        }

        private bool BankWithdrawalExists(int id)
        {
            return _context.BankWithdrawals.Any(e => e.BankWithdrawalId == id);
        }
    }
}
