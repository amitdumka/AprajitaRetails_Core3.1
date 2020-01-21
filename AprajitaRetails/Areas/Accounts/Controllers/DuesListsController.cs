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
    public class DuesListsController : Controller
    {
        private readonly AprajitaRetailsContext _context;

        public DuesListsController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: DuesLists
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

            var aprajitaRetailsContext = _context.DuesLists.Include(d => d.DailySale);
            return View(await PaginatedList<DuesList>.CreateAsync(aprajitaRetailsContext.AsNoTracking(), pageNumber ?? 1, pageSize));
           // return View(await aprajitaRetailsContext.ToListAsync());
        }

        // GET: DuesLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var duesList = await _context.DuesLists
                .Include(d => d.DailySale)
                .FirstOrDefaultAsync(m => m.DuesListId == id);
            if (duesList == null)
            {
                return NotFound();
            }

           return PartialView(duesList);
        }

        // GET: DuesLists/Create
        public IActionResult Create()
        {
            ViewData["DailySaleId"] = new SelectList(_context.DailySales, "DailySaleId", "InvNo");
           return PartialView();
        }

        // POST: DuesLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DuesListId,Amount,IsRecovered,RecoveryDate,DailySaleId,IsPartialRecovery")] DuesList duesList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(duesList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DailySaleId"] = new SelectList(_context.DailySales, "DailySaleId", "InvNo", duesList.DailySaleId);
           return PartialView(duesList);
        }

        // GET: DuesLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var duesList = await _context.DuesLists.FindAsync(id);
            if (duesList == null)
            {
                return NotFound();
            }
            ViewData["DailySaleId"] = new SelectList(_context.DailySales, "DailySaleId", "InvNo", duesList.DailySaleId);
           return PartialView(duesList);
        }

        // POST: DuesLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DuesListId,Amount,IsRecovered,RecoveryDate,DailySaleId,IsPartialRecovery")] DuesList duesList)
        {
            if (id != duesList.DuesListId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(duesList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DuesListExists(duesList.DuesListId))
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
            ViewData["DailySaleId"] = new SelectList(_context.DailySales, "DailySaleId", "InvNo", duesList.DailySaleId);
           return PartialView(duesList);
        }

        // GET: DuesLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var duesList = await _context.DuesLists
                .Include(d => d.DailySale)
                .FirstOrDefaultAsync(m => m.DuesListId == id);
            if (duesList == null)
            {
                return NotFound();
            }

           return PartialView(duesList);
        }

        // POST: DuesLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var duesList = await _context.DuesLists.FindAsync(id);
            _context.DuesLists.Remove(duesList);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DuesListExists(int id)
        {
            return _context.DuesLists.Any(e => e.DuesListId == id);
        }
    }
}
