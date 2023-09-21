using AdminPageinMVC.Repository;
using AdminPageMVC.DTO;
using AdminPageMVC.Entities;
using AdminPageMVC.OnlyModelViews;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AdminPageMVC.Controllers
{
    public class TeachersController : Controller
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IMapper _mapper;

        public TeachersController(ITeacherRepository teacherRepository, IMapper mapper)
        {
            _teacherRepository = teacherRepository;
            _mapper = mapper;

        }

        public async Task<IActionResult> Index()
        {
            var allTeacherAsync = await _teacherRepository.GetAllTeacherAsync();

            return View("Index", allTeacherAsync);
        }



        // GET: Teachers/Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(AddTeacherDto addTeacherDto)
        {
            if (!ModelState.IsValid) return View("Index");
            var teacher = _mapper.Map<Teacher>(addTeacherDto);
            await _teacherRepository.AddTeacherAsync(teacher);
            var allListTeachers = await _teacherRepository.GetAllTeacherAsync();
            return View("Index", allListTeachers);
        }

        // GET: Teachers/Edit/id
        public async Task<IActionResult> Edit(int id)
        {
            var teacher = await _teacherRepository.GetTeacherByIdAsync(id);
            return View(teacher);

        }


        public async Task<IActionResult> GetById(int id)
        {
            if (!ModelState.IsValid) return View("Index");
            var teacherByIdAsync = await _teacherRepository.GetTeacherByIdAsync(id);
            return View("Edit", teacherByIdAsync);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AddTeacherDto addTeacherDto)
        {

            if (!ModelState.IsValid) return View("Index");
            var teacher = _mapper.Map<TeacherDTO>(addTeacherDto);
            await _teacherRepository.UpdateTeacherAsync(id, teacher);
            var allListTeachers = await _teacherRepository.GetAllTeacherAsync();
            return View("Index", allListTeachers);

        }


        public async Task<IActionResult> Delete() => View("Delete");
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid) return View("Index");
            await _teacherRepository.DeleteTeacherAsync(id);
            var allListTeachers = await _teacherRepository.GetAllTeacherAsync();
            return View("Index", allListTeachers);
        }

    }
}
