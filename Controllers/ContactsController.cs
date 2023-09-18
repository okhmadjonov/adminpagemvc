using AdminPageinMVC.Repository;
using AdminPageMVC.Entities;
using AdminPageMVC.OnlyModelViews;
using Microsoft.AspNetCore.Mvc;

namespace AdminPageMVC.Controllers
{
    public class ContactsController : Controller
    {
        private readonly IContactRepository _contactRepository;

        public ContactsController(IContactRepository contactRepository) => _contactRepository = contactRepository;

        // GET: Contacts
        public async Task<IActionResult> Index()
        {
            var allContacts = await _contactRepository.GetAll();
            return View(allContacts);
        }



        // GET: Contacts/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(ContactDto contactDto)
        {
            if (!ModelState.IsValid) return View();
            var addContact = new Contact()
            {
                Name = contactDto.name,
                PhoneNumber = contactDto.phoneNumber,
                DateTime = DateTime.UtcNow,
            };

            await _contactRepository.AddContact(addContact);
            return RedirectToAction(nameof(Index));
        }

        // GET: Contacts/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var contact = await _contactRepository.GetAll();
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,PhoneNumber,DateTime")] Contact contact)
        {
            if (id != contact.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                await _contactRepository.AddContact(contact);


                return RedirectToAction(nameof(Index));
            }
            return View(contact);
        }

        // GET: Contacts/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _contactRepository.GetAll();
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_contactRepository.GetAll == null)
            {
                return Problem("Entity set 'AppDbContext.Contacts'  is null.");
            }
            // var contact = await _context.Contacts.FindAsync(id);
            if (id != null)
            {
                await _contactRepository.Delete(id);
            }

            return RedirectToAction(nameof(Index));
        }


    }
}
