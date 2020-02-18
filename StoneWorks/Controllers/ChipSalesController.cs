using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using  StoneWorks.Models;
 
using  StoneWorks.Data;

namespace  StoneWorks.Controllers
{
    
    public class ChipSalesController : Controller
    {
        private readonly StoneWorksContext _context;

        public ChipSalesController(StoneWorksContext context)
        {
            _context = context;
        }

        // GET: StoneWorks/ChipSales
        public async Task<IActionResult> Index()
        {
            return View(await _context.ChipSales.ToListAsync());
        }

        // GET: StoneWorks/ChipSales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chipSales = await _context.ChipSales
                .FirstOrDefaultAsync(m => m.ChipSalesId == id);
            if (chipSales == null)
            {
                return NotFound();
            }

            return View(chipSales);
        }

        // GET: StoneWorks/ChipSales/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StoneWorks/ChipSales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ChipSalesId,OnDate,PartyName,TruckNumber,PartyMobileNo,DriverName,DriverMobileNo,Size,Quantity,Rate,Amount,PaidAmount,ClearDate,Remarks,BillMaker,SlipNo")] ChipSales chipSales)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chipSales);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(chipSales);
        }

        // GET: StoneWorks/ChipSales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chipSales = await _context.ChipSales.FindAsync(id);
            if (chipSales == null)
            {
                return NotFound();
            }
            return View(chipSales);
        }

        // POST: StoneWorks/ChipSales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ChipSalesId,OnDate,PartyName,TruckNumber,PartyMobileNo,DriverName,DriverMobileNo,Size,Quantity,Rate,Amount,PaidAmount,ClearDate,Remarks,BillMaker,SlipNo")] ChipSales chipSales)
        {
            if (id != chipSales.ChipSalesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chipSales);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChipSalesExists(chipSales.ChipSalesId))
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
            return View(chipSales);
        }

        // GET: StoneWorks/ChipSales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chipSales = await _context.ChipSales
                .FirstOrDefaultAsync(m => m.ChipSalesId == id);
            if (chipSales == null)
            {
                return NotFound();
            }

            return View(chipSales);
        }

        // POST: StoneWorks/ChipSales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chipSales = await _context.ChipSales.FindAsync(id);
            _context.ChipSales.Remove(chipSales);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChipSalesExists(int id)
        {
            return _context.ChipSales.Any(e => e.ChipSalesId == id);
        }
    }
}
