using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AprajitaRetails.Data;
using AprajitaRetails.Models;

namespace AprajitaRetails.Controllers
{
    public class ChequesLogsController : Controller
    {
        private readonly AprajitaRetailsContext _context;

        public ChequesLogsController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: ChequesLogs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cheques.ToListAsync());
        }

        // GET: ChequesLogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chequesLog = await _context.Cheques
                .FirstOrDefaultAsync(m => m.ChequesLogId == id);
            if (chequesLog == null)
            {
                return NotFound();
            }

            return View(chequesLog);
        }

        // GET: ChequesLogs/Create
        public IActionResult Create()
        {
            return View();
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
            return View(chequesLog);
        }

        // GET: ChequesLogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chequesLog = await _context.Cheques.FindAsync(id);
            if (chequesLog == null)
            {
                return NotFound();
            }
            return View(chequesLog);
        }

        // POST: ChequesLogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ChequesLogId,BankName,AccountNumber,ChequesDate,DepositDate,ClearedDate,IssuedBy,IssuedTo,Amount,IsPDC,IsIssuedByAprajitaRetails,IsDepositedOnAprajitaRetails,Remarks")] ChequesLog chequesLog)
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
            return View(chequesLog);
        }

        // GET: ChequesLogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chequesLog = await _context.Cheques
                .FirstOrDefaultAsync(m => m.ChequesLogId == id);
            if (chequesLog == null)
            {
                return NotFound();
            }

            return View(chequesLog);
        }

        // POST: ChequesLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chequesLog = await _context.Cheques.FindAsync(id);
            _context.Cheques.Remove(chequesLog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChequesLogExists(int id)
        {
            return _context.Cheques.Any(e => e.ChequesLogId == id);
        }
    }
}
