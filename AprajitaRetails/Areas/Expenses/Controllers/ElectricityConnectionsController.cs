using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AprajitaRetails.Areas.Accounts.Models;
using AprajitaRetails.Data;

namespace AprajitaRetails.Areas.Expenses.Controllers
{
    [Area("Expenses")]
    public class ElectricityConnectionsController : Controller
    {
        private readonly AprajitaRetailsContext _context;

        public ElectricityConnectionsController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: Expenses/ElectricityConnections
        public async Task<IActionResult> Index()
        {
            return View(await _context.ElectricityConnections.ToListAsync());
        }

        // GET: Expenses/ElectricityConnections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var electricityConnection = await _context.ElectricityConnections
                .FirstOrDefaultAsync(m => m.ElectricityConnectionId == id);
            if (electricityConnection == null)
            {
                return NotFound();
            }

            return View(electricityConnection);
        }

        // GET: Expenses/ElectricityConnections/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Expenses/ElectricityConnections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ElectricityConnectionId,LocationName,ConnectioName,City,State,PinCode,ConsumerNumber,ConusumerId,Connection,ConnectinDate,DisconnectionDate,KVLoad,OwnedMetter,TotalConnectionCharges,SecurityDeposit,Remarks")] ElectricityConnection electricityConnection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(electricityConnection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(electricityConnection);
        }

        // GET: Expenses/ElectricityConnections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var electricityConnection = await _context.ElectricityConnections.FindAsync(id);
            if (electricityConnection == null)
            {
                return NotFound();
            }
            return View(electricityConnection);
        }

        // POST: Expenses/ElectricityConnections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ElectricityConnectionId,LocationName,ConnectioName,City,State,PinCode,ConsumerNumber,ConusumerId,Connection,ConnectinDate,DisconnectionDate,KVLoad,OwnedMetter,TotalConnectionCharges,SecurityDeposit,Remarks")] ElectricityConnection electricityConnection)
        {
            if (id != electricityConnection.ElectricityConnectionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(electricityConnection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ElectricityConnectionExists(electricityConnection.ElectricityConnectionId))
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
            return View(electricityConnection);
        }

        // GET: Expenses/ElectricityConnections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var electricityConnection = await _context.ElectricityConnections
                .FirstOrDefaultAsync(m => m.ElectricityConnectionId == id);
            if (electricityConnection == null)
            {
                return NotFound();
            }

            return View(electricityConnection);
        }

        // POST: Expenses/ElectricityConnections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var electricityConnection = await _context.ElectricityConnections.FindAsync(id);
            _context.ElectricityConnections.Remove(electricityConnection);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ElectricityConnectionExists(int id)
        {
            return _context.ElectricityConnections.Any(e => e.ElectricityConnectionId == id);
        }
    }
}
