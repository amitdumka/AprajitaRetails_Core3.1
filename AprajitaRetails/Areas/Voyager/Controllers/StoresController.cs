using Microsoft.AspNetCore.Authorization;    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AprajitaRetails.Areas.Voyager.Data;
using AprajitaRetails.Areas.Voyager.Models;
using Microsoft.AspNetCore.Authorization;


namespace AprajitaRetails.Areas.Voyager.Controllers
{
    [Area("Voyager")]
    public class StoresController : Controller
    {
        private readonly VoyagerContext _context;

        public StoresController(VoyagerContext context)
        {
            _context = context;
        }

        // GET: Voyager/Stores
        public async Task<IActionResult> Index()
        {
            return View(await _context.Stores.ToListAsync());
        }

        // GET: Voyager/Stores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var store = await _context.Stores
                .FirstOrDefaultAsync(m => m.StoreId == id);
            if (store == null)
            {
                return NotFound();
            }

            return PartialView(store);
        }

        // GET: Voyager/Stores/Create
        public IActionResult Create()
        {
            return PartialView();
        }

        // POST: Voyager/Stores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StoreId,StoreCode,StoreName,Address,City,PinCode,PhoneNo,StoreManagerName,StoreManagerPhoneNo,PanNo,GSTNO,NoOfEmployees,OpeningDate,ClosingDate,Status")] Store store)
        {
            if (ModelState.IsValid)
            {
                _context.Add(store);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return PartialView(store);
        }

        // GET: Voyager/Stores/Edit/5
         [Authorize(Roles = "Admin,PowerUser")] public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var store = await _context.Stores.FindAsync(id);
            if (store == null)
            {
                return NotFound();
            }
            return PartialView(store);
        }

        // POST: Voyager/Stores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
       [Authorize(Roles = "Admin,PowerUser")]     public async Task<IActionResult> Edit(int id, [Bind("StoreId,StoreCode,StoreName,Address,City,PinCode,PhoneNo,StoreManagerName,StoreManagerPhoneNo,PanNo,GSTNO,NoOfEmployees,OpeningDate,ClosingDate,Status")] Store store)
        {
            if (id != store.StoreId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(store);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StoreExists(store.StoreId))
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
            return PartialView(store);
        }

        // GET: Voyager/Stores/Delete/5
         [Authorize (Roles = "Admin,PowerUser")]   public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var store = await _context.Stores
                .FirstOrDefaultAsync(m => m.StoreId == id);
            if (store == null)
            {
                return NotFound();
            }

            return PartialView(store);
        }

        // POST: Voyager/Stores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
         [Authorize (Roles = "Admin,PowerUser")]   public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var store = await _context.Stores.FindAsync(id);
            _context.Stores.Remove(store);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StoreExists(int id)
        {
            return _context.Stores.Any(e => e.StoreId == id);
        }
    }
}
