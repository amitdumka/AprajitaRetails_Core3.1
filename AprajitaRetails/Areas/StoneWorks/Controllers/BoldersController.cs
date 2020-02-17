using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AprajitaRetails.Areas.StoneWorks.Models;
using AprajitaRetails.Areas.Voyager.Data;
using AprajitaRetails.Areas.StoneWorks.Data;

namespace AprajitaRetails.Areas.StoneWorks.Controllers
{
    [Area("StoneWorks")]
    public class BoldersController : Controller
    {
        private readonly StoneWorksContext _context;

        public BoldersController(StoneWorksContext context)
        {
            _context = context;
        }

        // GET: StoneWorks/Bolders
        public async Task<IActionResult> Index()
        {
            return View(await _context.Bolder.ToListAsync());
        }

        // GET: StoneWorks/Bolders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bolder = await _context.Bolder
                .FirstOrDefaultAsync(m => m.BolderId == id);
            if (bolder == null)
            {
                return NotFound();
            }

            return View(bolder);
        }

        // GET: StoneWorks/Bolders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StoneWorks/Bolders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BolderId,OnDate,VendorName,Qty,Rate,Payment,PaymentDate,Remarks,TruckNo,IsOwnTruck")] Bolder bolder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bolder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bolder);
        }

        // GET: StoneWorks/Bolders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bolder = await _context.Bolder.FindAsync(id);
            if (bolder == null)
            {
                return NotFound();
            }
            return View(bolder);
        }

        // POST: StoneWorks/Bolders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BolderId,OnDate,VendorName,Qty,Rate,Payment,PaymentDate,Remarks,TruckNo,IsOwnTruck")] Bolder bolder)
        {
            if (id != bolder.BolderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bolder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BolderExists(bolder.BolderId))
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
            return View(bolder);
        }

        // GET: StoneWorks/Bolders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bolder = await _context.Bolder
                .FirstOrDefaultAsync(m => m.BolderId == id);
            if (bolder == null)
            {
                return NotFound();
            }

            return View(bolder);
        }

        // POST: StoneWorks/Bolders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bolder = await _context.Bolder.FindAsync(id);
            _context.Bolder.Remove(bolder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BolderExists(int id)
        {
            return _context.Bolder.Any(e => e.BolderId == id);
        }
    }
}
