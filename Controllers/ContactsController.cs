using AdminPageinMVC.Repository;
using AdminPageMVC.Entities;
using AdminPageMVC.OnlyModelViews;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AdminPageMVC.Controllers
{
    public class ContactsController : Controller
    {
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;
        public ContactsController(IContactRepository contactRepository, IMapper mapper)
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
        }

        // GET: Contacts
        public async Task<IActionResult> Index()
        {
            var allContacts = await _contactRepository.GetAll();
            return View(allContacts);
        }
        // GET: Contacts/Create
        public IActionResult Create() { return View(); }

        [HttpPost]
        public async Task<IActionResult> Create(ContactDto contactDto)
        {
            var contact = _mapper.Map<Contact>(contactDto);
            await _contactRepository.AddContact(contact);
            return RedirectToAction(nameof(Index));
        }

        // GET: Contacts/Edit/id
        public async Task<IActionResult> Edit(int id)
        {
            var contact = await _contactRepository.GetAll();
            return View(contact);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,PhoneNumber,DateTime")] Contact contact)
        {
            await _contactRepository.AddContact(contact);
            return View("Index", contact);
        }


        public async Task<IActionResult> Delete(int id)
        {
            await _contactRepository.Delete(id);
            var contacts = await _contactRepository.GetAll();
            return View("Index", contacts);
        }
    }
}
