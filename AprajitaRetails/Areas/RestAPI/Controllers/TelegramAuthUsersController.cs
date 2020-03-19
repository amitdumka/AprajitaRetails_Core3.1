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
    [Route ("api/[controller]")]
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
            return await _context.TelegramAuthUsers.ToListAsync ();
        }

        // GET: api/TelegramAuthUsers/5
        [HttpGet ("{id:int}")]
        public async Task<ActionResult<TelegramAuthUser>> GetTelegramAuthUser(int id)
        {
            var telegramAuthUser = await _context.TelegramAuthUsers.FindAsync (id);

            if ( telegramAuthUser == null )
            {
                return NotFound ();
            }

            return telegramAuthUser;
        }
        // GET: api/TelegramAuthUsers/7779997556
        [HttpGet ("{mobileNo}")]
        public async Task<ActionResult<TelegramAuthUser>> GetTelegramAuthUser(string mobileNo)
        {
            var telegramAuthUser = await _context.TelegramAuthUsers.Where (c => c.MobileNo == mobileNo).FirstOrDefaultAsync ();

            if ( telegramAuthUser == null )
            {
                return NotFound ();
            }

            return telegramAuthUser;
        }

        // PUT: api/TelegramAuthUsers/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut ("{id}")]
        public async Task<IActionResult> PutTelegramAuthUser(int id, TelegramAuthUser telegramAuthUser)
        {
            if ( id != telegramAuthUser.TelegramAuthUserId )
            {
                return BadRequest ();
            }

            _context.Entry (telegramAuthUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync ();
            }
            catch ( DbUpdateConcurrencyException )
            {
                if ( !TelegramAuthUserExists (id) )
                {
                    return NotFound ();
                }
                else
                {
                    throw;
                }
            }

            return NoContent ();
        }

        // POST: api/TelegramAuthUsers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<TelegramAuthUser>> PostTelegramAuthUser(TelegramAuthUser telegramAuthUser)
        {
            telegramAuthUser = ProcessUser (telegramAuthUser);
            if(telegramAuthUser.TelegramUserName=="Error" || telegramAuthUser.TelegramUserName== "UserExist" )
            {
                return NotFound ();
            }
            _context.TelegramAuthUsers.Add (telegramAuthUser);
            await _context.SaveChangesAsync ();

            return CreatedAtAction ("GetTelegramAuthUser", new { id = telegramAuthUser.TelegramAuthUserId }, telegramAuthUser);
        }

        // DELETE: api/TelegramAuthUsers/5
        [HttpDelete ("{id}")]
        public async Task<ActionResult<TelegramAuthUser>> DeleteTelegramAuthUser(int id)
        {
            var telegramAuthUser = await _context.TelegramAuthUsers.FindAsync (id);
            if ( telegramAuthUser == null )
            {
                return NotFound ();
            }

            _context.TelegramAuthUsers.Remove (telegramAuthUser);
            await _context.SaveChangesAsync ();

            return telegramAuthUser;
        }

        private bool TelegramAuthUserExists(int id)
        {
            return _context.TelegramAuthUsers.Any (e => e.TelegramAuthUserId == id);
        }


        private TelegramAuthUser ProcessUser(TelegramAuthUser user)
        {
            if ( user.TelegramUserName == "AddInfo" )
            {
                var ctr = _context.TelegramAuthUsers.Where (c => c.MobileNo == user.MobileNo).Count ();
                if (  ctr <= 0 )
                {
                    var emp = _context.Employees.Where (c => c.MobileNo == user.MobileNo).Select (c => new { c.EmployeeId, c.StaffName, c.Category }).FirstOrDefault ();
                    if ( emp != null )
                    {
                        user.EmployeeId = emp.EmployeeId;
                        user.EmpType = emp.Category;
                        user.TelegramUserName = emp.StaffName;
                        return user;
                    }
                    else
                    {
                        user.TelegramUserName = "Error";
                        return user;
                    }

                }
                else
                {
                    user.TelegramUserName = "UserExist";
                    return user;
                }


            }


            return user;


        }
    }
}
