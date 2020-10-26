using AprajitaRetails.Areas.Expenses.Models;
using AprajitaRetails.Data;
using AprajitaRetails.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AprajitaRetails.Areas.Expenses.Controllers
{
    [Area("Expenses")]
    [Authorize]
    public class CashPaymentsController : Controller
    {
        private readonly AprajitaRetailsContext _context;
        private readonly int StoreId = 1;

        public CashPaymentsController(AprajitaRetailsContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> HomeExpenses(string currentFilter, string searchString, int? pageNumber)
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

            var vd = _context.CashPayments.Include(c => c.Mode).Where(c => c.Mode.Transcation == "Home Expenses" && c.PaymentDate.Month == DateTime.Today.Month).OrderByDescending(c => c.PaymentDate);
            int pageSize = 10;

            if (vd != null)
            {
                var amt = vd.Sum(c => c.Amount);
                ViewBag.TotalAmount = amt;
                //return PartialView(await vd.ToListAsync());

                return PartialView(await PaginatedList<CashPayment>.CreateAsync(vd.AsNoTracking(), pageNumber ?? 1, pageSize));
            }

            return PartialView(await PaginatedList<CashPayment>.CreateAsync(vd.AsNoTracking(), pageNumber ?? 1, pageSize));

            //TODO: Implement for if vd/Data return is null.
            //else
            // return PartialView(await PaginatedList<CashPayment>.CreateAsync(new CashPayment(), pageNumber ?? 1, pageSize));
            //return PartialView(new CashPayment());
        }

        // GET: CashPayments
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
            var aprajitaRetailsContext = _context.CashPayments.Include(c => c.Mode).OrderByDescending(c => c.PaymentDate);

            int pageSize = 10;
            return View(await PaginatedList<CashPayment>.CreateAsync(aprajitaRetailsContext.AsNoTracking(), pageNumber ?? 1, pageSize));


            //return View(await aprajitaRetailsContext.ToListAsync());
        }

        // GET: CashPayments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cashPayment = await _context.CashPayments
                .Include(c => c.Mode)
                .FirstOrDefaultAsync(m => m.CashPaymentId == id);
            if (cashPayment == null)
            {
                return NotFound();
            }

            return PartialView(cashPayment);
        }

        // GET: CashPayments/Create
        public IActionResult Create()
        {
            ViewData["TranscationModeId"] = new SelectList(_context.TranscationModes, "TranscationModeId", "Transcation");
            return PartialView();
        }

        // POST: CashPayments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CashPaymentId,PaymentDate,TranscationModeId,PaidTo,Amount,SlipNo")] CashPayment cashPayment)
        {
            if (ModelState.IsValid)
            {
                cashPayment.UserName = User.Identity.Name;
                cashPayment.StoreId = StoreId;
                _context.Add(cashPayment);
                new ExpenseManager().OnInsert(_context, cashPayment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TranscationModeId"] = new SelectList(_context.TranscationModes, "TranscationModeId", "Transcation", cashPayment.TranscationModeId);
            return PartialView(cashPayment);
        }

        // GET: CashPayments/Edit/5
        [Authorize(Roles = "Admin,PowerUser")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cashPayment = await _context.CashPayments.FindAsync(id);
            if (cashPayment == null)
            {
                return NotFound();
            }
            ViewData["TranscationModeId"] = new SelectList(_context.TranscationModes, "TranscationModeId", "Transcation", cashPayment.TranscationModeId);
            return PartialView(cashPayment);
        }

        // POST: CashPayments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,PowerUser")]
        public async Task<IActionResult> Edit(int id, [Bind("CashPaymentId,PaymentDate,TranscationModeId,PaidTo,Amount,SlipNo")] CashPayment cashPayment)
        {
            if (id != cashPayment.CashPaymentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    cashPayment.UserName = User.Identity.Name;
                    cashPayment.StoreId = StoreId;
                    new ExpenseManager().OnUpdate(_context, cashPayment);
                    _context.Update(cashPayment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CashPaymentExists(cashPayment.CashPaymentId))
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
            ViewData["TranscationModeId"] = new SelectList(_context.TranscationModes, "TranscationModeId", "Transcation", cashPayment.TranscationModeId);
            return PartialView(cashPayment);
        }

        // GET: CashPayments/Delete/5
        [Authorize(Roles = "Admin,PowerUser")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cashPayment = await _context.CashPayments
                .Include(c => c.Mode)
                .FirstOrDefaultAsync(m => m.CashPaymentId == id);
            if (cashPayment == null)
            {
                return NotFound();
            }

            return PartialView(cashPayment);
        }

        // POST: CashPayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,PowerUser")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cashPayment = await _context.CashPayments.FindAsync(id);
            _context.CashPayments.Remove(cashPayment);
            new ExpenseManager().OnDelete(_context, cashPayment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CashPaymentExists(int id)
        {
            return _context.CashPayments.Any(e => e.CashPaymentId == id);
        }
    }
}
