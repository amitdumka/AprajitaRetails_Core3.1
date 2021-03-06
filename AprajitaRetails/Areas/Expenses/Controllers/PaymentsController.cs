﻿using AprajitaRetails.Areas.Expenses.Models;
using AprajitaRetails.Data;
using AprajitaRetails.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AprajitaRetails.Areas.Expenses.Controllers
{
    [Area("Expenses")]
    [Authorize]
    public class PaymentsController : Controller
    {
        private readonly AprajitaRetailsContext _context;
        private readonly int StoreId = 1;

        public PaymentsController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: Payments
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
            var aprajitaRetailsContext = _context.Payments.OrderByDescending(c => c.PayDate);
            return View(await PaginatedList<Payment>.CreateAsync(aprajitaRetailsContext.AsNoTracking(), pageNumber ?? 1, pageSize));
            // return PartialView(await _context.Payments.ToListAsync());
        }

        // GET: Payments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments
                .FirstOrDefaultAsync(m => m.PaymentId == id);
            if (payment == null)
            {
                return NotFound();
            }

            return PartialView(payment);
        }

        // GET: Payments/Create
        public IActionResult Create()
        {
            return PartialView();
        }

        // POST: Payments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaymentId,PayDate,PaymentPartry,PayMode,PaymentDetails,Amount,PaymentSlipNo,Remarks")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                payment.UserName = User.Identity.Name;
                payment.StoreId = StoreId;
                _context.Add(payment);
                new ExpenseManager().OnInsert(_context, payment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return PartialView(payment);
        }

        // GET: Payments/Edit/5
        [Authorize(Roles = "Admin,PowerUser")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            return PartialView(payment);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,PowerUser")]
        public async Task<IActionResult> Edit(int id, [Bind("PaymentId,PayDate,PaymentPartry,PayMode,PaymentDetails,Amount,PaymentSlipNo,Remarks")] Payment payment)
        {
            if (id != payment.PaymentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    payment.UserName = User.Identity.Name;
                    payment.StoreId = StoreId;
                    new ExpenseManager().OnUpdate(_context, payment);
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
            return PartialView(payment);
        }

        // GET: Payments/Delete/5
        [Authorize(Roles = "Admin,PowerUser")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments
                .FirstOrDefaultAsync(m => m.PaymentId == id);
            if (payment == null)
            {
                return NotFound();
            }

            return PartialView(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,PowerUser")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            new ExpenseManager().OnDelete(_context, payment);
            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentExists(int id)
        {
            return _context.Payments.Any(e => e.PaymentId == id);
        }
    }
}
