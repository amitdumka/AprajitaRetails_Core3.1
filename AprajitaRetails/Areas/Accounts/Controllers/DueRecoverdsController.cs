using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AprajitaRetails.Data;
using AprajitaRetails.Models;

namespace AprajitaRetails.Areas.Accounts.Controllers
{
    [Area ("Accounts")]
    public class DueRecoverdsController : Controller
    {
        private readonly AprajitaRetailsContext _context;

        public DueRecoverdsController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: DueRecoverds
        public async Task<IActionResult> Index()
        {
            var aprajitaRetailsContext = _context.DueRecoverds.Include(d => d.DuesList);
            return View(await aprajitaRetailsContext.ToListAsync());
        }

        // GET: DueRecoverds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dueRecoverd = await _context.DueRecoverds
                .Include(d => d.DuesList)
                .FirstOrDefaultAsync(m => m.DueRecoverdId == id);
            if (dueRecoverd == null)
            {
                return NotFound();
            }

            return View(dueRecoverd);
        }

        // GET: DueRecoverds/Create
        public IActionResult Create()
        {
            ViewData["DuesListId"] = new SelectList(_context.DuesLists, "DuesListId", "DuesListId");
            return View();
        }

        // POST: DueRecoverds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DueRecoverdId,PaidDate,DuesListId,AmountPaid,IsPartialPayment,Modes,Remarks")] DueRecoverd dueRecoverd)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dueRecoverd);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DuesListId"] = new SelectList(_context.DuesLists, "DuesListId", "DuesListId", dueRecoverd.DuesListId);
            return View(dueRecoverd);
        }

        // GET: DueRecoverds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dueRecoverd = await _context.DueRecoverds.FindAsync(id);
            if (dueRecoverd == null)
            {
                return NotFound();
            }
            ViewData["DuesListId"] = new SelectList(_context.DuesLists, "DuesListId", "DuesListId", dueRecoverd.DuesListId);
            return View(dueRecoverd);
        }

        // POST: DueRecoverds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DueRecoverdId,PaidDate,DuesListId,AmountPaid,IsPartialPayment,Modes,Remarks")] DueRecoverd dueRecoverd)
        {
            if (id != dueRecoverd.DueRecoverdId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dueRecoverd);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DueRecoverdExists(dueRecoverd.DueRecoverdId))
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
            ViewData["DuesListId"] = new SelectList(_context.DuesLists, "DuesListId", "DuesListId", dueRecoverd.DuesListId);
            return View(dueRecoverd);
        }

        // GET: DueRecoverds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dueRecoverd = await _context.DueRecoverds
                .Include(d => d.DuesList)
                .FirstOrDefaultAsync(m => m.DueRecoverdId == id);
            if (dueRecoverd == null)
            {
                return NotFound();
            }

            return View(dueRecoverd);
        }

        // POST: DueRecoverds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dueRecoverd = await _context.DueRecoverds.FindAsync(id);
            _context.DueRecoverds.Remove(dueRecoverd);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DueRecoverdExists(int id)
        {
            return _context.DueRecoverds.Any(e => e.DueRecoverdId == id);
        }
    }
}
