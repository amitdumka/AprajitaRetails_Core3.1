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
        //[HttpGet("{id}")]
        public async Task<ActionResult<TelegramAuthUser>> GetTelegramAuthUsersById(int id)
        {
            var TelegramAuthUsers = await _context.TelegramAuthUsers.FindAsync(id);

            if (TelegramAuthUsers == null)
            {
                return NotFound();
            }

            return TelegramAuthUsers;
        }
        //[HttpGet ("{mobileNo}")]
        //public async Task<ActionResult<TelegramAuthUser>> GetTelegramAuthUsers(string mobileNo)
        //{
        //    var TelegramAuthUsers = await _context.TelegramAuthUsers.Where(c=>c.MobileNo==mobileNo).FirstOrDefaultAsync();

        //    if ( TelegramAuthUsers == null )
        //    {
        //        return NotFound ();
        //    }

        //    return TelegramAuthUsers;
        //}
        //[HttpGet ("{mobileNo}")]
        public async Task<ActionResult<bool>> GetTelegramAuthUsers(string mobileNo)
        {
            var TelegramAuthUsers =  await _context.TelegramAuthUsers.Where (c => c.MobileNo == mobileNo).CountAsync();

            if ( TelegramAuthUsers == null ||TelegramAuthUsers<=0 )
            {
                return false;
            }

            return true;
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

//public class StudentController : ApiController
//{


//    public IHttpActionResult GetAllStudents(bool includeAddress = false)
//    {
//        IList<StudentViewModel> students = null;

//        using ( var ctx = new SchoolDBEntities () )
//        {
//            students = ctx.Students.Include ("StudentAddress").Select (s => new StudentViewModel ()
//            {
//                Id = s.StudentID,
//                FirstName = s.FirstName,
//                LastName = s.LastName,
//                Address = s.StudentAddress == null || includeAddress == false ? null : new AddressViewModel ()
//                {
//                    StudentId = s.StudentAddress.StudentID,
//                    Address1 = s.StudentAddress.Address1,
//                    Address2 = s.StudentAddress.Address2,
//                    City = s.StudentAddress.City,
//                    State = s.StudentAddress.State
//                }
//            }).ToList<StudentViewModel> ();
//        }

//        if ( students == null )
//        {
//            return NotFound ();
//        }

//        return Ok (students);
//    }

//    public IHttpActionResult GetStudentById(int id)
//    {
//        StudentViewModel student = null;

//        using ( var ctx = new SchoolDBEntities () )
//        {
//            student = ctx.Students.Include ("StudentAddress")
//                .Where (s => s.StudentID == id)
//                .Select (s => new StudentViewModel ()
//                {
//                    Id = s.StudentID,
//                    FirstName = s.FirstName,
//                    LastName = s.LastName
//                }).FirstOrDefault<StudentViewModel> ();
//        }

//        if ( student == null )
//        {
//            return NotFound ();
//        }

//        return Ok (student);
//    }

//    public IHttpActionResult GetAllStudents(string name)
//    {
//        IList<StudentViewModel> students = null;

//        using ( var ctx = new SchoolDBEntities () )
//        {
//            students = ctx.Students.Include ("StudentAddress")
//                .Where (s => s.FirstName.ToLower () == name.ToLower ())
//                .Select (s => new StudentViewModel ()
//                {
//                    Id = s.StudentID,
//                    FirstName = s.FirstName,
//                    LastName = s.LastName,
//                    Address = s.StudentAddress == null ? null : new AddressViewModel ()
//                    {
//                        StudentId = s.StudentAddress.StudentID,
//                        Address1 = s.StudentAddress.Address1,
//                        Address2 = s.StudentAddress.Address2,
//                        City = s.StudentAddress.City,
//                        State = s.StudentAddress.State
//                    }
//                }).ToList<StudentViewModel> ();
//        }

//        if ( students.Count == 0 )
//        {
//            return NotFound ();
//        }

//        return Ok (students);
//    }

//    public IHttpActionResult GetAllStudentsInSameStandard(int standardId)
//    {
//        IList<StudentViewModel> students = null;

//        using ( var ctx = new SchoolDBEntities () )
//        {
//            students = ctx.Students.Include ("StudentAddress").Include ("Standard").Where (s => s.StandardId == standardId)
//                        .Select (s => new StudentViewModel ()
//                        {
//                            Id = s.StudentID,
//                            FirstName = s.FirstName,
//                            LastName = s.LastName,
//                            Address = s.StudentAddress == null ? null : new AddressViewModel ()
//                            {
//                                StudentId = s.StudentAddress.StudentID,
//                                Address1 = s.StudentAddress.Address1,
//                                Address2 = s.StudentAddress.Address2,
//                                City = s.StudentAddress.City,
//                                State = s.StudentAddress.State
//                            },
//                            Standard = new StandardViewModel ()
//                            {
//                                StandardId = s.Standard.StandardId,
//                                Name = s.Standard.StandardName
//                            }
//                        }).ToList<StudentViewModel> ();
//        }

//        if ( students.Count == 0 )
//        {
//            return NotFound ();
//        }

//        return Ok (students);
//    }
//}

//http://localhost:64189/api/student	Returns all students without associated address.
//http://localhost:64189/api/student?includeAddress=false	Returns all students without associated address.
//http://localhost:64189/api/student?includeAddress=true	Returns all students with address.
//http://localhost:64189/api/student?id=123	Returns student with the specified id.
//http://localhost:64189/api/student?name=steve	Returns all students whose name is steve.
//http://localhost:64189/api/student?standardId=5	Returns all students who are in 5th standard.
