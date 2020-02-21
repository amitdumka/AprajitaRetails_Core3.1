using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AprajitaRetails.Areas.Voyager.Data;
using AprajitaRetails.Models;
using AprajitaRetails.Data;

namespace AprajitaRetails.Areas.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelegramAuthUsersController : ControllerBase
    {
        private readonly AprajitaRetailsContext _context;

        public TelegramAuthUsersController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: api/TelegramAuthUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TelegramAuthUser>>> GetTelegramAuthUsers()
        {
            return await _context.TelegramAuthUsers.ToListAsync();
        }

        // GET: api/TelegramAuthUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TelegramAuthUser>> GetTelegramAuthUsers(int id)
        {
            var TelegramAuthUsers = await _context.TelegramAuthUsers.FindAsync(id);

            if (TelegramAuthUsers == null)
            {
                return NotFound();
            }

            return TelegramAuthUsers;
        }

        // PUT: api/TelegramAuthUsers/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTelegramAuthUsers(int id, TelegramAuthUser TelegramAuthUsers)
        {
            if (id != TelegramAuthUsers.TelegramAuthUserId)
            {
                return BadRequest();
            }

            _context.Entry(TelegramAuthUsers).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TelegramAuthUsersExists(id))
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

        // POST: api/TelegramAuthUsers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<TelegramAuthUser>> PostTelegramAuthUsers(TelegramAuthUser TelegramAuthUsers)
        {
            _context.TelegramAuthUsers.Add(TelegramAuthUsers);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTelegramAuthUsers", new { id = TelegramAuthUsers.TelegramAuthUserId }, TelegramAuthUsers);
        }

        // DELETE: api/TelegramAuthUsers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TelegramAuthUser>> DeleteTelegramAuthUsers(int id)
        {
            var TelegramAuthUsers = await _context.TelegramAuthUsers.FindAsync(id);
            if (TelegramAuthUsers == null)
            {
                return NotFound();
            }

            _context.TelegramAuthUsers.Remove(TelegramAuthUsers);
            await _context.SaveChangesAsync();

            return TelegramAuthUsers;
        }

        private bool TelegramAuthUsersExists(int id)
        {
            return _context.TelegramAuthUsers.Any(e => e.TelegramAuthUserId == id);
        }
    }
}
