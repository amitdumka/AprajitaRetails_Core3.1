using Microsoft.AspNetCore.Authorization;    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AprajitaRetails.Data;
using AprajitaRetails.Models;

namespace AprajitaRetails.Areas.Banking.Controllers
{
    [Area ("Banking")]
    [Authorize (Roles = "Admin,PowerUser,StoreManager")]
    public class ChequesLogsController : Controller
    {
        private readonly AprajitaRetailsContext _context;

        public ChequesLogsController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: ChequesLogs
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
            return View(await PaginatedList<ChequesLog>.CreateAsync(_context.ChequesLogs.AsNoTracking(), pageNumber ?? 1, pageSize));
           // return View(await _context.ChequesLogs.ToListAsync());
        }

        // GET: ChequesLogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chequesLog = await _context.ChequesLogs
                .FirstOrDefaultAsync(m => m.ChequesLogId == id);
            if (chequesLog == null)
            {
                return NotFound();
            }

           return PartialView(chequesLog);
        }

        // GET: ChequesLogs/Create
        public IActionResult Create()
        {
           return PartialView();
        }

        // POST: ChequesLogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ChequesLogId,BankName,AccountNumber,ChequesDate,DepositDate,ClearedDate,IssuedBy,IssuedTo,Amount,IsPDC,IsIssuedByAprajitaRetails,IsDepositedOnAprajitaRetails,Remarks")] ChequesLog chequesLog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chequesLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           return PartialView(chequesLog);
        }

        // GET: ChequesLogs/Edit/5
         [Authorize(Roles = "Admin,PowerUser")] public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chequesLog = await _context.ChequesLogs.FindAsync(id);
            if (chequesLog == null)
            {
                return NotFound();
            }
           return PartialView(chequesLog);
        }

        // POST: ChequesLogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
       [Authorize(Roles = "Admin,PowerUser")]     public async Task<IActionResult> Edit(int id, [Bind("ChequesLogId,BankName,AccountNumber,ChequesDate,DepositDate,ClearedDate,IssuedBy,IssuedTo,Amount,IsPDC,IsIssuedByAprajitaRetails,IsDepositedOnAprajitaRetails,Remarks")] ChequesLog chequesLog)
        {
            if (id != chequesLog.ChequesLogId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chequesLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChequesLogExists(chequesLog.ChequesLogId))
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
           return PartialView(chequesLog);
        }

        // GET: ChequesLogs/Delete/5
         [Authorize (Roles = "Admin,PowerUser")]   public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chequesLog = await _context.ChequesLogs
                .FirstOrDefaultAsync(m => m.ChequesLogId == id);
            if (chequesLog == null)
            {
                return NotFound();
            }

           return PartialView(chequesLog);
        }

        // POST: ChequesLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
         [Authorize (Roles = "Admin,PowerUser")]   public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chequesLog = await _context.ChequesLogs.FindAsync(id);
            _context.ChequesLogs.Remove(chequesLog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChequesLogExists(int id)
        {
            return _context.ChequesLogs.Any(e => e.ChequesLogId == id);
        }
    }
}
