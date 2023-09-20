using AdminPageinMVC.Repository;
using AdminPageMVC.Entities;
using AdminPageMVC.OnlyModelViews;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AdminPageMVC.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public CoursesController(ICourseRepository courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            var allCourse = await _courseRepository.GetAllCourseAsync();
            return View("Index", allCourse);
        }



        // GET: Courses/Create
        public IActionResult Create() { return View(); }


        [HttpPost]
        public async Task<IActionResult> Create(AddCourseDto addCourseDto)
        {
            var course = _mapper.Map<Course>(addCourseDto);
            await _courseRepository.AddCourseAsync(course);
            return RedirectToAction(nameof(Index));
        }

        // GET: Courses/Edit/id
        public async Task<IActionResult> Edit(int id)
        {
            var course = await _courseRepository.GetCourseByIdAsync(id);
            return View(course);
        }

        public async Task<IActionResult> GetByIdCourse(int id)
        {
            var courseByid = await _courseRepository.GetCourseByIdAsync(id);
            return View(courseByid);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, AddCourseDto addCourseDto)
        {
            var existCourse = _courseRepository.GetCourseByIdAsync(id);
            await _mapper.Map(addCourseDto, existCourse);
            return RedirectToAction(nameof(Index));
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _courseRepository.GetCourseByIdAsync(id);
            return View(course);
        }


        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _courseRepository.DeleteCourseAsync(id);
            return RedirectToAction(nameof(Index));
        }


    }
}
