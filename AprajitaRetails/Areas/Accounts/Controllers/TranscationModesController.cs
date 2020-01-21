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
    public class TranscationModesController : Controller
    {
        private readonly AprajitaRetailsContext _context;

        public TranscationModesController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: TranscationModes
        public async Task<IActionResult> Index()
        {
           return View(await _context.TranscationModes.ToListAsync());
        }

        // GET: TranscationModes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transcationMode = await _context.TranscationModes
                .FirstOrDefaultAsync(m => m.TranscationModeId == id);
            if (transcationMode == null)
            {
                return NotFound();
            }

           return PartialView(transcationMode);
        }

        // GET: TranscationModes/Create
        public IActionResult Create()
        {
           return PartialView();
        }

        // POST: TranscationModes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TranscationModeId,Transcation")] TranscationMode transcationMode)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transcationMode);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           return PartialView(transcationMode);
        }

        // GET: TranscationModes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transcationMode = await _context.TranscationModes.FindAsync(id);
            if (transcationMode == null)
            {
                return NotFound();
            }
           return PartialView(transcationMode);
        }

        // POST: TranscationModes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TranscationModeId,Transcation")] TranscationMode transcationMode)
        {
            if (id != transcationMode.TranscationModeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transcationMode);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TranscationModeExists(transcationMode.TranscationModeId))
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
           return PartialView(transcationMode);
        }

        // GET: TranscationModes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transcationMode = await _context.TranscationModes
                .FirstOrDefaultAsync(m => m.TranscationModeId == id);
            if (transcationMode == null)
            {
                return NotFound();
            }

           return PartialView(transcationMode);
        }

        // POST: TranscationModes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transcationMode = await _context.TranscationModes.FindAsync(id);
            _context.TranscationModes.Remove(transcationMode);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TranscationModeExists(int id)
        {
            return _context.TranscationModes.Any(e => e.TranscationModeId == id);
        }
    }
}
