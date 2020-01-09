using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AprajitaRetails.Data;
using AprajitaRetails.Models;

namespace AprajitaRetails.Controllers
{
    public class TalioringDeliveriesController : Controller
    {
        private readonly AprajitaRetailsContext _context;

        public TalioringDeliveriesController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: TalioringDeliveries
        public async Task<IActionResult> Index()
        {
            var aprajitaRetailsContext = _context.Deliveries.Include(t => t.Booking);
            return View(await aprajitaRetailsContext.ToListAsync());
        }

        // GET: TalioringDeliveries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var talioringDelivery = await _context.Deliveries
                .Include(t => t.Booking)
                .FirstOrDefaultAsync(m => m.TalioringDeliveryId == id);
            if (talioringDelivery == null)
            {
                return NotFound();
            }

            return View(talioringDelivery);
        }

        // GET: TalioringDeliveries/Create
        public IActionResult Create()
        {
            ViewData["TalioringBookingId"] = new SelectList(_context.Bookings, "TalioringBookingId", "TalioringBookingId");
            return View();
        }

        // POST: TalioringDeliveries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TalioringDeliveryId,DeliveryDate,TalioringBookingId,InvNo,Amount,Remarks")] TalioringDelivery talioringDelivery)
        {
            if (ModelState.IsValid)
            {
                _context.Add(talioringDelivery);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TalioringBookingId"] = new SelectList(_context.Bookings, "TalioringBookingId", "TalioringBookingId", talioringDelivery.TalioringBookingId);
            return View(talioringDelivery);
        }

        // GET: TalioringDeliveries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var talioringDelivery = await _context.Deliveries.FindAsync(id);
            if (talioringDelivery == null)
            {
                return NotFound();
            }
            ViewData["TalioringBookingId"] = new SelectList(_context.Bookings, "TalioringBookingId", "TalioringBookingId", talioringDelivery.TalioringBookingId);
            return View(talioringDelivery);
        }

        // POST: TalioringDeliveries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TalioringDeliveryId,DeliveryDate,TalioringBookingId,InvNo,Amount,Remarks")] TalioringDelivery talioringDelivery)
        {
            if (id != talioringDelivery.TalioringDeliveryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(talioringDelivery);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TalioringDeliveryExists(talioringDelivery.TalioringDeliveryId))
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
            ViewData["TalioringBookingId"] = new SelectList(_context.Bookings, "TalioringBookingId", "TalioringBookingId", talioringDelivery.TalioringBookingId);
            return View(talioringDelivery);
        }

        // GET: TalioringDeliveries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var talioringDelivery = await _context.Deliveries
                .Include(t => t.Booking)
                .FirstOrDefaultAsync(m => m.TalioringDeliveryId == id);
            if (talioringDelivery == null)
            {
                return NotFound();
            }

            return View(talioringDelivery);
        }

        // POST: TalioringDeliveries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var talioringDelivery = await _context.Deliveries.FindAsync(id);
            _context.Deliveries.Remove(talioringDelivery);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TalioringDeliveryExists(int id)
        {
            return _context.Deliveries.Any(e => e.TalioringDeliveryId == id);
        }
    }
}
