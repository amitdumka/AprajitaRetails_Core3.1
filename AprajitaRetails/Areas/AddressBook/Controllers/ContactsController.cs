using AprajitaRetails.Areas.AddressBook.Models;
using AprajitaRetails.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
// https://www.mikesdotnetting.com/article/256/entity-framework-recipe-alphabetical-paging-in-asp-net-mvc
//Alphabet order pagitnation
namespace AprajitaRetails.Areas.AddressBook.Controllers
{
    [Area("AddressBook")]
    [Authorize]
    public class ContactsController : Controller
    {
        private readonly AprajitaRetailsContext _context;

        public ContactsController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: AddressBook/Contacts
        public async Task<IActionResult> Index(string currentFilter, string searchString, string sortOrder, int? pageNumber)
        {

            ViewData["FirstNameParm"] = String.IsNullOrEmpty(sortOrder) ? "fn_desc" : "";
            ViewData["LastNameParm"] = sortOrder == "ln_asc" ? "fn_desc" : "ln_asc";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }


            ViewData["CurrentFilter"] = searchString;
            var vm = _context.Contact.OrderBy(c => c.FirstName);
            if (!String.IsNullOrEmpty(searchString))
            {
                vm = _context.Contact.Where(c => c.FirstName.Contains(searchString)).OrderBy(c => c.FirstName);


            }
            switch (sortOrder)
            {
                case "fn_desc":
                    vm = vm.OrderByDescending(c => c.FirstName);
                    break;
                case "ln_desc":
                    vm = vm.OrderByDescending(c => c.LastName);
                    break;
                case "ln_asc":
                    vm = vm.OrderBy(c => c.LastName);
                    break;
                default:
                    vm = vm.OrderBy(c => c.FirstName);
                    break;
            }

            int pageSize = 10;
            return View(await PaginatedList<Contact>.CreateAsync(vm.AsNoTracking(), pageNumber ?? 1, pageSize));

            //return View (await vm.ToListAsync ());
        }

        // GET: AddressBook/Contacts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contact
                .FirstOrDefaultAsync(m => m.ContactId == id);
            if (contact == null)
            {
                return NotFound();
            }

            return PartialView(contact);
        }

        // GET: AddressBook/Contacts/Create
        public IActionResult Create()
        {
            return PartialView();
        }

        // POST: AddressBook/Contacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContactId,FirstName,LastName,MobileNo,PhoneNo,EMailAddress,Remarks")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return PartialView(contact);
        }

        // GET: AddressBook/Contacts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contact.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            return PartialView(contact);
        }

        // POST: AddressBook/Contacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContactId,FirstName,LastName,MobileNo,PhoneNo,EMailAddress,Remarks")] Contact contact)
        {
            if (id != contact.ContactId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contact);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactExists(contact.ContactId))
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
            return PartialView(contact);
        }

        // GET: AddressBook/Contacts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contact
                .FirstOrDefaultAsync(m => m.ContactId == id);
            if (contact == null)
            {
                return NotFound();
            }

            return PartialView(contact);
        }

        // POST: AddressBook/Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contact = await _context.Contact.FindAsync(id);
            _context.Contact.Remove(contact);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactExists(int id)
        {
            return _context.Contact.Any(e => e.ContactId == id);
        }
    }
}
