using AprajitaRetails.Data;
using AprajitaRetails.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AprajitaRetails.Areas.Banking.Controllers
{
    [Area("Banking")]
    [Authorize(Roles = "Admin,PowerUser")]
    public class BankAccountSecurityInfoesController : Controller
    {
        private readonly AprajitaRetailsContext _context;

        public BankAccountSecurityInfoesController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: Banking/BankAccountSecurityInfoes
        public async Task<IActionResult> Index(int? id, string currentFilter, string searchString, string sortOrder, int? pageNumber)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["BankSortParm"] = sortOrder == "Bank" ? "Bank_desc" : "Bank";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            var aprajitaRetailsContext = _context.AccountSecurityInfos.Include(b => b.BankAccountInfo).ThenInclude(b => b.Bank).OrderBy(c => c.BankAccountInfo.AccountHolder);
            switch (sortOrder)
            {
                case "name_desc": aprajitaRetailsContext = aprajitaRetailsContext.OrderByDescending(c => c.BankAccountInfo.AccountHolder); break;
                case "Bank": aprajitaRetailsContext = aprajitaRetailsContext.OrderBy(c => c.BankAccountInfo.Bank.BankName); break;
                case "Bank_desc":
                    aprajitaRetailsContext = aprajitaRetailsContext.OrderByDescending(c => c.BankAccountInfo.Bank.BankName);
                    break;
                default:
                    aprajitaRetailsContext = aprajitaRetailsContext.OrderBy(c => c.BankAccountInfo.AccountHolder);
                    break;

            }
            // return View(await aprajitaRetailsContext.ToListAsync());
            int pageSize = 10;
            return View(await PaginatedList<BankAccountSecurityInfo>.CreateAsync(aprajitaRetailsContext.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Banking/BankAccountSecurityInfoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankAccountSecurityInfo = await _context.AccountSecurityInfos
                .Include(b => b.BankAccountInfo)
                .FirstOrDefaultAsync(m => m.BankAccountSecurityInfoId == id);
            if (bankAccountSecurityInfo == null)
            {
                return NotFound();
            }

            return View(bankAccountSecurityInfo);
        }

        // GET: Banking/BankAccountSecurityInfoes/Create
        public IActionResult Create()
        {
            ViewData["BankAccountSecurityInfoId"] = new SelectList(_context.BankAccountInfos, "BankAccountInfoId", "AccountNumber");
            return View();
        }

        // POST: Banking/BankAccountSecurityInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BankAccountSecurityInfoId,CustomerId,UserId,Password,TaxPassword,ExtraPassword,ATMCardNumber,ExpiryDate,CVVNo,ATMPin,TPIN")] BankAccountSecurityInfo bankAccountSecurityInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bankAccountSecurityInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BankAccountSecurityInfoId"] = new SelectList(_context.BankAccountInfos, "BankAccountInfoId", "AccountNumber", bankAccountSecurityInfo.BankAccountSecurityInfoId);
            return View(bankAccountSecurityInfo);
        }

        // GET: Banking/BankAccountSecurityInfoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankAccountSecurityInfo = await _context.AccountSecurityInfos.FindAsync(id);
            if (bankAccountSecurityInfo == null)
            {
                return NotFound();
            }
            ViewData["BankAccountSecurityInfoId"] = new SelectList(_context.BankAccountInfos, "BankAccountInfoId", "AccountNumber", bankAccountSecurityInfo.BankAccountSecurityInfoId);
            return View(bankAccountSecurityInfo);
        }

        // POST: Banking/BankAccountSecurityInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BankAccountSecurityInfoId,CustomerId,UserId,Password,TaxPassword,ExtraPassword,ATMCardNumber,ExpiryDate,CVVNo,ATMPin,TPIN")] BankAccountSecurityInfo bankAccountSecurityInfo)
        {
            if (id != bankAccountSecurityInfo.BankAccountSecurityInfoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bankAccountSecurityInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BankAccountSecurityInfoExists(bankAccountSecurityInfo.BankAccountSecurityInfoId))
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
            ViewData["BankAccountSecurityInfoId"] = new SelectList(_context.BankAccountInfos, "BankAccountInfoId", "AccountNumber", bankAccountSecurityInfo.BankAccountSecurityInfoId);
            return View(bankAccountSecurityInfo);
        }

        // GET: Banking/BankAccountSecurityInfoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankAccountSecurityInfo = await _context.AccountSecurityInfos
                .Include(b => b.BankAccountInfo)
                .FirstOrDefaultAsync(m => m.BankAccountSecurityInfoId == id);
            if (bankAccountSecurityInfo == null)
            {
                return NotFound();
            }

            return View(bankAccountSecurityInfo);
        }

        // POST: Banking/BankAccountSecurityInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bankAccountSecurityInfo = await _context.AccountSecurityInfos.FindAsync(id);
            _context.AccountSecurityInfos.Remove(bankAccountSecurityInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BankAccountSecurityInfoExists(int id)
        {
            return _context.AccountSecurityInfos.Any(e => e.BankAccountSecurityInfoId == id);
        }
    }
}
