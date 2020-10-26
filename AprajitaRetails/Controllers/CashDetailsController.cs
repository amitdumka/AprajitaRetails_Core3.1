using AprajitaRetails.Data;
using AprajitaRetails.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AprajitaRetails.Controllers
{
    public class CashDetailsController : Controller
    {
        private readonly AprajitaRetailsContext _context;

        public CashDetailsController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: CashDetails
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
            var vm = _context.CashDetail.OrderByDescending(c => c.OnDate);
            return View(await PaginatedList<CashDetail>.CreateAsync(vm.AsNoTracking(), pageNumber ?? 1, pageSize));

        }

        // GET: CashDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cashDetail = await _context.CashDetail
                .FirstOrDefaultAsync(m => m.CashDetailId == id);
            if (cashDetail == null)
            {
                return NotFound();
            }

            return PartialView(cashDetail);
        }

        // GET: CashDetails/Create
        public IActionResult Create()
        {
            return PartialView();
        }

        // POST: CashDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CashDetailId,OnDate,TotalAmount,C2000,C1000,C500,C100,C50,C20,C10,C5,Coin10,Coin5,Coin2,Coin1")] CashDetail cashDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cashDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return PartialView(cashDetail);
        }

        // GET: CashDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cashDetail = await _context.CashDetail.FindAsync(id);
            if (cashDetail == null)
            {
                return NotFound();
            }
            return PartialView(cashDetail);
        }

        // POST: CashDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CashDetailId,OnDate,TotalAmount,C2000,C1000,C500,C100,C50,C20,C10,C5,Coin10,Coin5,Coin2,Coin1")] CashDetail cashDetail)
        {
            if (id != cashDetail.CashDetailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cashDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CashDetailExists(cashDetail.CashDetailId))
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
            return PartialView(cashDetail);
        }

        // GET: CashDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cashDetail = await _context.CashDetail
                .FirstOrDefaultAsync(m => m.CashDetailId == id);
            if (cashDetail == null)
            {
                return NotFound();
            }

            return PartialView(cashDetail);
        }

        // POST: CashDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cashDetail = await _context.CashDetail.FindAsync(id);
            _context.CashDetail.Remove(cashDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CashDetailExists(int id)
        {
            return _context.CashDetail.Any(e => e.CashDetailId == id);
        }
    }
}
