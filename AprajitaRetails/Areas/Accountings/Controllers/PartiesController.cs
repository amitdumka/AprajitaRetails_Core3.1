using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AprajitaRetails.Areas.Accountings.Models;
using AprajitaRetails.Data;
using Microsoft.AspNetCore.Authorization;
using AprajitaRetails.Areas.Accountings.Ops;

namespace AprajitaRetails.Areas.Accountings.Controllers
{
    [Area("Accountings")]
    [Authorize (Roles = "Admin,PowerUser,StoreManager")]
    public class PartiesController : Controller
    {
        private readonly AprajitaRetailsContext _context;

        public PartiesController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: Accountings/Parties
        public async Task<IActionResult> Index()
        {
            var aprajitaRetailsContext = _context.Parties.Include(p => p.LedgerType);
            return View(await aprajitaRetailsContext.ToListAsync());
        }
        public async Task<IActionResult> LedgerEntries()
        {
            var aprajitaRetailsContext = _context.LedgerEntries.Include (p => p.Party).ThenInclude(c=>c.LedgerType);
            return View (await aprajitaRetailsContext.ToListAsync ());
        }

        // GET: Accountings/Parties/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var party = await _context.Parties
                .Include(p => p.LedgerType)
                .FirstOrDefaultAsync(m => m.PartyId == id);
            if (party == null)
            {
                return NotFound();
            }

            return View(party);
        }

        // GET: Accountings/Parties/Create
        public IActionResult Create()
        {
            ViewData["LedgerTypeId"] = new SelectList(_context.LedgerTypes, "LedgerTypeId", "LedgerNameType");
            return View();
        }

        // POST: Accountings/Parties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PartyId,PartyName,OpenningDate,OpenningBalance,Address,PANNo,GSTNo,LedgerTypeId")] Party party)
        {
            if (ModelState.IsValid)
            {
                _context.Add(party);
                await _context.SaveChangesAsync();
                AccountOperation.CreateLedgerMaster (_context, party);
                return RedirectToAction(nameof(Index));
            }
            ViewData["LedgerTypeId"] = new SelectList(_context.LedgerTypes, "LedgerTypeId", "LedgerNameType", party.LedgerTypeId);
            return View(party);
        }

        // GET: Accountings/Parties/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var party = await _context.Parties.FindAsync(id);
            if (party == null)
            {
                return NotFound();
            }
            ViewData["LedgerTypeId"] = new SelectList(_context.LedgerTypes, "LedgerTypeId", "LedgerNameType", party.LedgerTypeId);
            return View(party);
        }

        // POST: Accountings/Parties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PartyId,PartyName,OpenningDate,OpenningBalance,Address,PANNo,GSTNo,LedgerTypeId")] Party party)
        {
            if (id != party.PartyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(party);
                    await _context.SaveChangesAsync();
                    AccountOperation.UpdateLedgerMaster (_context, party);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PartyExists(party.PartyId))
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
            ViewData["LedgerTypeId"] = new SelectList(_context.LedgerTypes, "LedgerTypeId", "LedgerNameType", party.LedgerTypeId);
            return View(party);
        }

        // GET: Accountings/Parties/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var party = await _context.Parties
                .Include(p => p.LedgerType)
                .FirstOrDefaultAsync(m => m.PartyId == id);
            if (party == null)
            {
                return NotFound();
            }

            return View(party);
        }

        // POST: Accountings/Parties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var party = await _context.Parties.FindAsync(id);
            _context.Parties.Remove(party);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PartyExists(int id)
        {
            return _context.Parties.Any(e => e.PartyId == id);
        }
    }
}
