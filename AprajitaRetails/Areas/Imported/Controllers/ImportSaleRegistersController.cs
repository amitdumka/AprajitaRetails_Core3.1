using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AprajitaRetails.Areas.Uploader.Models;
using AprajitaRetails.Data;

namespace AprajitaRetails.Areas.Imported.Controllers
{
    [Area("Imported")]
    [Authorize]
    public class ImportSaleRegistersController : Controller
    {
        private readonly AprajitaRetailsContext _context;

        public ImportSaleRegistersController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: Uploader/ImportSaleRegisters
        public async Task<IActionResult> Index()
        {
            return View(await _context.ImportSaleRegisters.ToListAsync());
        }

        // GET: Uploader/ImportSaleRegisters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var importSaleRegister = await _context.ImportSaleRegisters
                .FirstOrDefaultAsync(m => m.ImportSaleRegisterId == id);
            if (importSaleRegister == null)
            {
                return NotFound();
            }

            return View(importSaleRegister);
        }

        // GET: Uploader/ImportSaleRegisters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Uploader/ImportSaleRegisters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ImportSaleRegisterId,InvoiceNo,InvoiceType,InvoiceDate,Quantity,MRP,Discount,BasicRate,Tax,RoundOff,BillAmnt,PaymentType,IsConsumed,ImportTime")] ImportSaleRegister importSaleRegister)
        {
            if (ModelState.IsValid)
            {
                _context.Add(importSaleRegister);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(importSaleRegister);
        }

        // GET: Uploader/ImportSaleRegisters/Edit/5
         [Authorize(Roles = "Admin,PowerUser")] public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var importSaleRegister = await _context.ImportSaleRegisters.FindAsync(id);
            if (importSaleRegister == null)
            {
                return NotFound();
            }
            return View(importSaleRegister);
        }

        // POST: Uploader/ImportSaleRegisters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
       [Authorize(Roles = "Admin,PowerUser")]     public async Task<IActionResult> Edit(int id, [Bind("ImportSaleRegisterId,InvoiceNo,InvoiceType,InvoiceDate,Quantity,MRP,Discount,BasicRate,Tax,RoundOff,BillAmnt,PaymentType,IsConsumed,ImportTime")] ImportSaleRegister importSaleRegister)
        {
            if (id != importSaleRegister.ImportSaleRegisterId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(importSaleRegister);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImportSaleRegisterExists(importSaleRegister.ImportSaleRegisterId))
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
            return View(importSaleRegister);
        }

        // GET: Uploader/ImportSaleRegisters/Delete/5
         [Authorize (Roles = "Admin,PowerUser")]   public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var importSaleRegister = await _context.ImportSaleRegisters
                .FirstOrDefaultAsync(m => m.ImportSaleRegisterId == id);
            if (importSaleRegister == null)
            {
                return NotFound();
            }

            return View(importSaleRegister);
        }

        // POST: Uploader/ImportSaleRegisters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
         [Authorize (Roles = "Admin,PowerUser")]   public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var importSaleRegister = await _context.ImportSaleRegisters.FindAsync(id);
            _context.ImportSaleRegisters.Remove(importSaleRegister);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImportSaleRegisterExists(int id)
        {
            return _context.ImportSaleRegisters.Any(e => e.ImportSaleRegisterId == id);
        }
    }
}
