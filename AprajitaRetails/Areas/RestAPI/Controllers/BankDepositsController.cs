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
    public class BankDepositsController : ControllerBase
    {
        private readonly AprajitaRetailsContext _context;

        public BankDepositsController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: api/BankDeposits
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BankDeposit>>> GetBankDeposits()
        {
            return await _context.BankDeposits.ToListAsync();
        }

        // GET: api/BankDeposits/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BankDeposit>> GetBankDeposit(int id)
        {
            var bankDeposit = await _context.BankDeposits.FindAsync(id);

            if (bankDeposit == null)
            {
                return NotFound();
            }

            return bankDeposit;
        }

        // PUT: api/BankDeposits/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBankDeposit(int id, BankDeposit bankDeposit)
        {
            if (id != bankDeposit.BankDepositId)
            {
                return BadRequest();
            }

            _context.Entry(bankDeposit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BankDepositExists(id))
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

        // POST: api/BankDeposits
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<BankDeposit>> PostBankDeposit(BankDeposit bankDeposit)
        {
            _context.BankDeposits.Add(bankDeposit);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBankDeposit", new { id = bankDeposit.BankDepositId }, bankDeposit);
        }

        // DELETE: api/BankDeposits/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BankDeposit>> DeleteBankDeposit(int id)
        {
            var bankDeposit = await _context.BankDeposits.FindAsync(id);
            if (bankDeposit == null)
            {
                return NotFound();
            }

            _context.BankDeposits.Remove(bankDeposit);
            await _context.SaveChangesAsync();

            return bankDeposit;
        }

        private bool BankDepositExists(int id)
        {
            return _context.BankDeposits.Any(e => e.BankDepositId == id);
        }
    }
}
