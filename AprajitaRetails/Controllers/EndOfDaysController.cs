using Microsoft.AspNetCore.Authorization;    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AprajitaRetails.Data;
using AprajitaRetails.Models;
using Microsoft.AspNetCore.Authorization;

namespace AprajitaRetails.Controllers
{
    public class EndOfDaysController : Controller
    {
        private readonly AprajitaRetailsContext _context;

        public EndOfDaysController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: EndOfDays
        public async Task<IActionResult> Index(string currentFilter, string searchString, int? pageNumber)
        {
            if ( searchString != null )
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }


            ViewData ["CurrentFilter"] = searchString;
            int pageSize = 10;
            return View (await PaginatedList<EndOfDay>.CreateAsync (_context.EndOfDays.AsNoTracking (), pageNumber ?? 1, pageSize));
            // return View(await _context.EndOfDays.ToListAsync());
        }

        // GET: EndOfDays/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if ( id == null )
            {
                return NotFound ();
            }

            var endOfDay = await _context.EndOfDays
                .FirstOrDefaultAsync (m => m.EndOfDayId == id);
            if ( endOfDay == null )
            {
                return NotFound ();
            }

            return View (endOfDay);
        }

        // GET: EndOfDays/Create
        public IActionResult Create()
        {
            return View ();
        }

        // POST: EndOfDays/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind ("EndOfDayId,EOD_Date,Shirting,Suiting,USPA,FM_Arrow,RWT,Access,CashInHand")] EndOfDay endOfDay)
        {
            if ( ModelState.IsValid )
            {
                _context.Add (endOfDay);
                await _context.SaveChangesAsync ();
                return RedirectToAction (nameof (Index));
            }
            return View (endOfDay);
        }

        // GET: EndOfDays/Edit/5
        [Authorize (Roles = "Admin,PowerUser")]
        public async Task<IActionResult> Edit(int? id)
        {
            if ( id == null )
            {
                return NotFound ();
            }

            var endOfDay = await _context.EndOfDays.FindAsync (id);
            if ( endOfDay == null )
            {
                return NotFound ();
            }
            return View (endOfDay);
        }

        // POST: EndOfDays/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
         [Authorize (Roles = "Admin,PowerUser")]   [Authorize(Roles = "Admin,PowerUser")]     public async Task<IActionResult> Edit(int id, [Bind("EndOfDayId,EOD_Date,Shirting,Suiting,USPA,FM_Arrow,RWT,Access,CashInHand")] EndOfDay endOfDay)
        {
            if ( id != endOfDay.EndOfDayId )
            {
                return NotFound ();
            }

            if ( ModelState.IsValid )
            {
                try
                {
                    _context.Update (endOfDay);
                    await _context.SaveChangesAsync ();
                }
                catch ( DbUpdateConcurrencyException )
                {
                    if ( !EndOfDayExists (endOfDay.EndOfDayId) )
                    {
                        return NotFound ();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction (nameof (Index));
            }
            return View (endOfDay);
        }

        // GET: EndOfDays/Delete/5
         [Authorize (Roles = "Admin,PowerUser")]   public async Task<IActionResult> Delete(int? id)
        {
            if ( id == null )
            {
                return NotFound ();
            }

            var endOfDay = await _context.EndOfDays
                .FirstOrDefaultAsync (m => m.EndOfDayId == id);
            if ( endOfDay == null )
            {
                return NotFound ();
            }

            return View (endOfDay);
        }

        // POST: EndOfDays/Delete/5
        [HttpPost, ActionName ("Delete")]
        [ValidateAntiForgeryToken]
         [Authorize (Roles = "Admin,PowerUser")]   public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var endOfDay = await _context.EndOfDays.FindAsync (id);
            _context.EndOfDays.Remove (endOfDay);
            await _context.SaveChangesAsync ();
            return RedirectToAction (nameof (Index));
        }

        private bool EndOfDayExists(int id)
        {
            return _context.EndOfDays.Any (e => e.EndOfDayId == id);
        }
    }
}
