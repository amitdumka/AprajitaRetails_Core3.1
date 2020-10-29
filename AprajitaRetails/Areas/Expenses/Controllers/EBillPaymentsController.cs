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
    public class EBillPaymentsController : Controller
    {
        private readonly AprajitaRetailsContext _context;

        public EBillPaymentsController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: Expenses/EBillPayments
        public async Task<IActionResult> Index()
        {
            var aprajitaRetailsContext = _context.BillPayments.Include(e => e.Bill);
            return View(await aprajitaRetailsContext.ToListAsync());
        }

        // GET: Expenses/EBillPayments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eBillPayment = await _context.BillPayments
                .Include(e => e.Bill)
                .FirstOrDefaultAsync(m => m.EBillPaymentId == id);
            if (eBillPayment == null)
            {
                return NotFound();
            }

            return View(eBillPayment);
        }

        // GET: Expenses/EBillPayments/Create
        public IActionResult Create()
        {
            ViewData["EletricityBillId"] = new SelectList(_context.EletricityBills, "EletricityBillId", "BillNumber");
            return View();
        }

        // POST: Expenses/EBillPayments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EBillPaymentId,EletricityBillId,PaymentDate,Amount,Mode,PaymentDetails,Remarks,IsPartialPayment,IsBillCleared")] EBillPayment eBillPayment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eBillPayment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EletricityBillId"] = new SelectList(_context.EletricityBills, "EletricityBillId", "BillNumber", eBillPayment.EletricityBillId);
            return View(eBillPayment);
        }

        // GET: Expenses/EBillPayments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eBillPayment = await _context.BillPayments.FindAsync(id);
            if (eBillPayment == null)
            {
                return NotFound();
            }
            ViewData["EletricityBillId"] = new SelectList(_context.EletricityBills, "EletricityBillId", "BillNumber", eBillPayment.EletricityBillId);
            return View(eBillPayment);
        }

        // POST: Expenses/EBillPayments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EBillPaymentId,EletricityBillId,PaymentDate,Amount,Mode,PaymentDetails,Remarks,IsPartialPayment,IsBillCleared")] EBillPayment eBillPayment)
        {
            if (id != eBillPayment.EBillPaymentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eBillPayment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EBillPaymentExists(eBillPayment.EBillPaymentId))
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
            ViewData["EletricityBillId"] = new SelectList(_context.EletricityBills, "EletricityBillId", "BillNumber", eBillPayment.EletricityBillId);
            return View(eBillPayment);
        }

        // GET: Expenses/EBillPayments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eBillPayment = await _context.BillPayments
                .Include(e => e.Bill)
                .FirstOrDefaultAsync(m => m.EBillPaymentId == id);
            if (eBillPayment == null)
            {
                return NotFound();
            }

            return View(eBillPayment);
        }

        // POST: Expenses/EBillPayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eBillPayment = await _context.BillPayments.FindAsync(id);
            _context.BillPayments.Remove(eBillPayment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EBillPaymentExists(int id)
        {
            return _context.BillPayments.Any(e => e.EBillPaymentId == id);
        }
    }
}
