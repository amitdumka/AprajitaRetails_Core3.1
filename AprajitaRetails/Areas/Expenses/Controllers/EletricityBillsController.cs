using AprajitaRetails.Areas.Accounts.Models;
using AprajitaRetails.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AprajitaRetails.Areas.Expenses.Controllers
{
    [Area("Expenses")]
    public class EletricityBillsController : Controller
    {
        private readonly AprajitaRetailsContext _context;

        public EletricityBillsController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: Expenses/EletricityBills
        public async Task<IActionResult> Index()
        {
            var aprajitaRetailsContext = _context.EletricityBills.Include(e => e.Connection);
            return View(await aprajitaRetailsContext.ToListAsync());
        }

        // GET: Expenses/EletricityBills/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eletricityBill = await _context.EletricityBills
                .Include(e => e.Connection)
                .FirstOrDefaultAsync(m => m.EletricityBillId == id);
            if (eletricityBill == null)
            {
                return NotFound();
            }

            return View(eletricityBill);
        }

        // GET: Expenses/EletricityBills/Create
        public IActionResult Create()
        {
            ViewData["ElectricityConnectionId"] = new SelectList(_context.ElectricityConnections, "ElectricityConnectionId", "ConnectioName");
            return View();
        }

        // POST: Expenses/EletricityBills/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EletricityBillId,ElectricityConnectionId,BillNumber,BillDate,MeterReadingDate,CurrentMeterReading,TotalUnit,CurrentAmount,ArrearAmount,NetDemand")] EletricityBill eletricityBill)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eletricityBill);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ElectricityConnectionId"] = new SelectList(_context.ElectricityConnections, "ElectricityConnectionId", "ConnectioName", eletricityBill.ElectricityConnectionId);
            return View(eletricityBill);
        }

        // GET: Expenses/EletricityBills/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eletricityBill = await _context.EletricityBills.FindAsync(id);
            if (eletricityBill == null)
            {
                return NotFound();
            }
            ViewData["ElectricityConnectionId"] = new SelectList(_context.ElectricityConnections, "ElectricityConnectionId", "ConnectioName", eletricityBill.ElectricityConnectionId);
            return View(eletricityBill);
        }

        // POST: Expenses/EletricityBills/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EletricityBillId,ElectricityConnectionId,BillNumber,BillDate,MeterReadingDate,CurrentMeterReading,TotalUnit,CurrentAmount,ArrearAmount,NetDemand")] EletricityBill eletricityBill)
        {
            if (id != eletricityBill.EletricityBillId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eletricityBill);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EletricityBillExists(eletricityBill.EletricityBillId))
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
            ViewData["ElectricityConnectionId"] = new SelectList(_context.ElectricityConnections, "ElectricityConnectionId", "ConnectioName", eletricityBill.ElectricityConnectionId);
            return View(eletricityBill);
        }

        // GET: Expenses/EletricityBills/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eletricityBill = await _context.EletricityBills
                .Include(e => e.Connection)
                .FirstOrDefaultAsync(m => m.EletricityBillId == id);
            if (eletricityBill == null)
            {
                return NotFound();
            }

            return View(eletricityBill);
        }

        // POST: Expenses/EletricityBills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eletricityBill = await _context.EletricityBills.FindAsync(id);
            _context.EletricityBills.Remove(eletricityBill);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EletricityBillExists(int id)
        {
            return _context.EletricityBills.Any(e => e.EletricityBillId == id);
        }
    }
}
