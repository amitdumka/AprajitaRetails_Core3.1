using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using AprajitaRetails.Areas.Voyager.Models;
using AprajitaRetails.Data;

namespace AprajitaRetails.Areas.Voyager.Controllers
{
    //TODO: move to accounts sections. 

    [Area("Voyager")]
    public class ArvindPaymentsController : Controller
    {
        private readonly AprajitaRetailsContext _context;

        public ArvindPaymentsController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: Voyager/ArvindPayments
        public async Task<IActionResult> Index()
        {
            return  View(await _context.ArvindPayments.ToListAsync());
        }

        // GET: Voyager/ArvindPayments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var arvindPayment = await _context.ArvindPayments
                .FirstOrDefaultAsync(m => m.ArvindPaymentId == id);
            if (arvindPayment == null)
            {
                return NotFound();
            }

            return   PartialView(arvindPayment);
        }

        // GET: Voyager/ArvindPayments/Create
        public IActionResult Create()
        {
            return  PartialView();
        }

        // POST: Voyager/ArvindPayments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ArvindPaymentId,Arvind,OnDate,InvoiceNo,Amount,BankDetails,Remarks")] ArvindPayment arvindPayment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(arvindPayment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return  PartialView(arvindPayment);
        }

        // GET: Voyager/ArvindPayments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var arvindPayment = await _context.ArvindPayments.FindAsync(id);
            if (arvindPayment == null)
            {
                return NotFound();
            }
            return  PartialView(arvindPayment);
        }

        // POST: Voyager/ArvindPayments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ArvindPaymentId,Arvind,OnDate,InvoiceNo,Amount,BankDetails,Remarks")] ArvindPayment arvindPayment)
        {
            if (id != arvindPayment.ArvindPaymentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(arvindPayment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArvindPaymentExists(arvindPayment.ArvindPaymentId))
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
            return  PartialView(arvindPayment);
        }

        // GET: Voyager/ArvindPayments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var arvindPayment = await _context.ArvindPayments
                .FirstOrDefaultAsync(m => m.ArvindPaymentId == id);
            if (arvindPayment == null)
            {
                return NotFound();
            }

            return  PartialView(arvindPayment);
        }

        // POST: Voyager/ArvindPayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var arvindPayment = await _context.ArvindPayments.FindAsync(id);
            _context.ArvindPayments.Remove(arvindPayment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArvindPaymentExists(int id)
        {
            return _context.ArvindPayments.Any(e => e.ArvindPaymentId == id);
        }
    }
}
