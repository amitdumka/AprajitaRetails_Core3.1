using AprajitaRetails.Areas.Accounts.Models;
using AprajitaRetails.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AprajitaRetails.Areas.Expenses.Controllers
{
    [Area("Expenses")]
    public class RentedLocationsController : Controller
    {
        private readonly AprajitaRetailsContext _context;

        public RentedLocationsController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: Expenses/RentedLocations
        public async Task<IActionResult> Index()
        {
            return View(await _context.RentedLocations.ToListAsync());
        }

        // GET: Expenses/RentedLocations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentedLocation = await _context.RentedLocations
                .FirstOrDefaultAsync(m => m.RentedLocationId == id);
            if (rentedLocation == null)
            {
                return NotFound();
            }

            return View(rentedLocation);
        }

        // GET: Expenses/RentedLocations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Expenses/RentedLocations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RentedLocationId,PlaceName,Address,OnDate,VacatedDate,City,OwnerName,MobileNo,RentAmount,AdvanceAmount,IsRented,RentType")] RentedLocation rentedLocation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rentedLocation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rentedLocation);
        }

        // GET: Expenses/RentedLocations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentedLocation = await _context.RentedLocations.FindAsync(id);
            if (rentedLocation == null)
            {
                return NotFound();
            }
            return View(rentedLocation);
        }

        // POST: Expenses/RentedLocations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RentedLocationId,PlaceName,Address,OnDate,VacatedDate,City,OwnerName,MobileNo,RentAmount,AdvanceAmount,IsRented,RentType")] RentedLocation rentedLocation)
        {
            if (id != rentedLocation.RentedLocationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rentedLocation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RentedLocationExists(rentedLocation.RentedLocationId))
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
            return View(rentedLocation);
        }

        // GET: Expenses/RentedLocations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentedLocation = await _context.RentedLocations
                .FirstOrDefaultAsync(m => m.RentedLocationId == id);
            if (rentedLocation == null)
            {
                return NotFound();
            }

            return View(rentedLocation);
        }

        // POST: Expenses/RentedLocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rentedLocation = await _context.RentedLocations.FindAsync(id);
            _context.RentedLocations.Remove(rentedLocation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RentedLocationExists(int id)
        {
            return _context.RentedLocations.Any(e => e.RentedLocationId == id);
        }
    }
}
