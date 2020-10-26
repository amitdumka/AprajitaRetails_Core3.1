using AprajitaRetails.Areas.Accountings.Models;
using AprajitaRetails.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AprajitaRetails.Areas.Accountings.Controllers
{
    [Area("Accountings")]
    [Authorize(Roles = "Admin,PowerUser,StoreManager")]
    public class PaymentsController : Controller
    {
        private readonly AprajitaRetailsContext _context;

        public PaymentsController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: Accountings/Payments
        public async Task<IActionResult> Index()
        {
            var aprajitaRetailsContext = _context.PaymentVochers.Include(p => p.FromAccount).Include(p => p.Party).Include(p => p.Store);
            return View(await aprajitaRetailsContext.ToListAsync());
        }

        // GET: Accountings/Payments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.PaymentVochers
                .Include(p => p.FromAccount)
                .Include(p => p.Party)
                .Include(p => p.Store)
                .FirstOrDefaultAsync(m => m.PaymentId == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // GET: Accountings/Payments/Create
        public IActionResult Create()
        {
            ViewData["BankAccountId"] = new SelectList(_context.BankAccounts, "BankAccountId", "BankAccountId");
            ViewData["PartyId"] = new SelectList(_context.Parties, "PartyId", "PartyId");
            ViewData["StoreId"] = new SelectList(_context.Stores, "StoreId", "StoreId");
            return View();
        }

        // POST: Accountings/Payments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaymentId,PartyName,PaymentSlipNo,OnDate,PayMode,BankAccountId,PaymentDetails,Amount,Remarks,PartyId,LedgerEnteryId,IsCash,StoreId,UserName")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(payment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BankAccountId"] = new SelectList(_context.BankAccounts, "BankAccountId", "BankAccountId", payment.BankAccountId);
            ViewData["PartyId"] = new SelectList(_context.Parties, "PartyId", "PartyId", payment.PartyId);
            ViewData["StoreId"] = new SelectList(_context.Stores, "StoreId", "StoreId", payment.StoreId);
            return View(payment);
        }

        // GET: Accountings/Payments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.PaymentVochers.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            ViewData["BankAccountId"] = new SelectList(_context.BankAccounts, "BankAccountId", "BankAccountId", payment.BankAccountId);
            ViewData["PartyId"] = new SelectList(_context.Parties, "PartyId", "PartyId", payment.PartyId);
            ViewData["StoreId"] = new SelectList(_context.Stores, "StoreId", "StoreId", payment.StoreId);
            return View(payment);
        }

        // POST: Accountings/Payments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PaymentId,PartyName,PaymentSlipNo,OnDate,PayMode,BankAccountId,PaymentDetails,Amount,Remarks,PartyId,LedgerEnteryId,IsCash,StoreId,UserName")] Payment payment)
        {
            if (id != payment.PaymentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentExists(payment.PaymentId))
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
            ViewData["BankAccountId"] = new SelectList(_context.BankAccounts, "BankAccountId", "BankAccountId", payment.BankAccountId);
            ViewData["PartyId"] = new SelectList(_context.Parties, "PartyId", "PartyId", payment.PartyId);
            ViewData["StoreId"] = new SelectList(_context.Stores, "StoreId", "StoreId", payment.StoreId);
            return View(payment);
        }

        // GET: Accountings/Payments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.PaymentVochers
                .Include(p => p.FromAccount)
                .Include(p => p.Party)
                .Include(p => p.Store)
                .FirstOrDefaultAsync(m => m.PaymentId == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // POST: Accountings/Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var payment = await _context.PaymentVochers.FindAsync(id);
            _context.PaymentVochers.Remove(payment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentExists(int id)
        {
            return _context.PaymentVochers.Any(e => e.PaymentId == id);
        }
    }
}
