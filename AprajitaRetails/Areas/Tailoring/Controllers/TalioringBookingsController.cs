using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AprajitaRetails.Data;
using AprajitaRetails.Models;

namespace AprajitaRetails.Areas.Tailoring.Controllers
{
    [Area ("Tailoring")]
    public class TalioringBookingsController : Controller
    {
        private readonly AprajitaRetailsContext _context;

        public TalioringBookingsController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: TalioringBookings
        public async Task<IActionResult> Index(string currentFilter, string searchString, int? pageNumber)
        {
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }


            ViewData["CurrentFilter"] = searchString;
            int pageSize = 10;
           return View(await PaginatedList<TalioringBooking>.CreateAsync(_context.TalioringBookings.AsNoTracking(), pageNumber ?? 1, pageSize));
            //return View(await _context.TalioringBookings.ToListAsync());
        }

        // GET: TalioringBookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var talioringBooking = await _context.TalioringBookings
                .FirstOrDefaultAsync(m => m.TalioringBookingId == id);
            if (talioringBooking == null)
            {
                return NotFound();
            }

           return PartialView(talioringBooking);
        }

        // GET: TalioringBookings/Create
        public IActionResult Create()
        {
           return PartialView();
        }

        // POST: TalioringBookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TalioringBookingId,BookingDate,CustName,DeliveryDate,TryDate,BookingSlipNo,TotalAmount,TotalQty,ShirtQty,ShirtPrice,PantQty,PantPrice,CoatQty,CoatPrice,KurtaQty,KurtaPrice,BundiQty,BundiPrice,Others,OthersPrice,IsDelivered")] TalioringBooking talioringBooking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(talioringBooking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           return PartialView(talioringBooking);
        }

        // GET: TalioringBookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var talioringBooking = await _context.TalioringBookings.FindAsync(id);
            if (talioringBooking == null)
            {
                return NotFound();
            }
           return PartialView(talioringBooking);
        }

        // POST: TalioringBookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TalioringBookingId,BookingDate,CustName,DeliveryDate,TryDate,BookingSlipNo,TotalAmount,TotalQty,ShirtQty,ShirtPrice,PantQty,PantPrice,CoatQty,CoatPrice,KurtaQty,KurtaPrice,BundiQty,BundiPrice,Others,OthersPrice,IsDelivered")] TalioringBooking talioringBooking)
        {
            if (id != talioringBooking.TalioringBookingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(talioringBooking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TalioringBookingExists(talioringBooking.TalioringBookingId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
           return PartialView(talioringBooking);
        }

        // GET: TalioringBookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var talioringBooking = await _context.TalioringBookings
                .FirstOrDefaultAsync(m => m.TalioringBookingId == id);
            if (talioringBooking == null)
            {
                return NotFound();
            }

           return PartialView(talioringBooking);
        }

        // POST: TalioringBookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var talioringBooking = await _context.TalioringBookings.FindAsync(id);
            _context.TalioringBookings.Remove(talioringBooking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TalioringBookingExists(int id)
        {
            return _context.TalioringBookings.Any(e => e.TalioringBookingId == id);
        }
    }
}
