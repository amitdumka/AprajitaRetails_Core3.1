using AprajitaRetails.Areas.Tailoring.Data;
using AprajitaRetails.Data;
using AprajitaRetails.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AprajitaRetails.Areas.Tailoring.Controllers
{
    [Area("Tailoring")]
    [Authorize]
    public class TalioringDeliveriesController : Controller
    {
        private readonly AprajitaRetailsContext _context;

        public TalioringDeliveriesController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: TalioringDeliveries
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
            var aprajitaRetailsContext = _context.TailoringDeliveries.Include(t => t.Booking).OrderByDescending(c => c.DeliveryDate);
            return View(await PaginatedList<TalioringDelivery>.CreateAsync(aprajitaRetailsContext.AsNoTracking(), pageNumber ?? 1, pageSize));
            //return View(await aprajitaRetailsContext.ToListAsync());
        }

        // GET: TalioringDeliveries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var talioringDelivery = await _context.TailoringDeliveries
                .Include(t => t.Booking)
                .FirstOrDefaultAsync(m => m.TalioringDeliveryId == id);
            if (talioringDelivery == null)
            {
                return NotFound();
            }

            return PartialView(talioringDelivery);
        }

        // GET: TalioringDeliveries/Create
        public IActionResult Create()
        {
            ViewData["TalioringBookingId"] = new SelectList(_context.TalioringBookings.Where(c => !c.IsDelivered), "TalioringBookingId", "BookingSlipNo");
            return PartialView();
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
                talioringDelivery.UserName = User.Identity.Name;
                _context.Add(talioringDelivery);
                new TailoringManager().OnUpdateData(_context, talioringDelivery, false, false);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TalioringBookingId"] = new SelectList(_context.TalioringBookings.Where(c => !c.IsDelivered), "TalioringBookingId", "BookingSlipNo", talioringDelivery.TalioringBookingId);
            return PartialView(talioringDelivery);
        }

        // GET: TalioringDeliveries/Edit/5
        [Authorize(Roles = "Admin,PowerUser")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var talioringDelivery = await _context.TailoringDeliveries.FindAsync(id);
            if (talioringDelivery == null)
            {
                return NotFound();
            }
            ViewData["TalioringBookingId"] = new SelectList(_context.TalioringBookings, "TalioringBookingId", "BookingSlipNo", talioringDelivery.TalioringBookingId);
            return PartialView(talioringDelivery);
        }

        // POST: TalioringDeliveries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,PowerUser")]
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
                    new TailoringManager().OnUpdateData(_context, talioringDelivery, true, false);
                    talioringDelivery.UserName = User.Identity.Name;
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
            ViewData["TalioringBookingId"] = new SelectList(_context.TalioringBookings, "TalioringBookingId", "BookingSlipNo", talioringDelivery.TalioringBookingId);
            return PartialView(talioringDelivery);
        }

        // GET: TalioringDeliveries/Delete/5
        [Authorize(Roles = "Admin,PowerUser")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var talioringDelivery = await _context.TailoringDeliveries
                .Include(t => t.Booking)
                .FirstOrDefaultAsync(m => m.TalioringDeliveryId == id);
            if (talioringDelivery == null)
            {
                return NotFound();
            }

            return PartialView(talioringDelivery);
        }

        // POST: TalioringDeliveries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,PowerUser")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var talioringDelivery = await _context.TailoringDeliveries.FindAsync(id);
            new TailoringManager().OnUpdateData(_context, talioringDelivery, false, true);
            _context.TailoringDeliveries.Remove(talioringDelivery);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TalioringDeliveryExists(int id)
        {
            return _context.TailoringDeliveries.Any(e => e.TalioringDeliveryId == id);
        }
    }
}
