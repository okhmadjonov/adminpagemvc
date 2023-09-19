using AdminPageinMVC.Repository;
using AdminPageMVC.DTO;
using AdminPageMVC.Entities;
using AdminPageMVC.OnlyModelViews;
using Microsoft.AspNetCore.Mvc;

namespace AdminPageMVC.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICourseRepository _courseRepository;

        public CoursesController(ICourseRepository courseRepository) => _courseRepository = courseRepository;


        // GET: Courses
        public async Task<IActionResult> Index()
        {
            var allCourse = await _courseRepository.GetAllCourseAsync();
            return View("Index", allCourse);
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null || _courseRepository.GetAllCourseAsync() == null)
            {
                return NotFound();
            }

            var course = await _courseRepository.GetCourseByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(AddCourseDto addCourseDto)
        {
            if (!ModelState.IsValid) return View();
            var course = new Course
            {
                Price = Convert.ToDouble(addCourseDto.Price),
                Description = addCourseDto.Description,
                ImageUrl = addCourseDto.ImageUrl
            };
            await _courseRepository.AddCourseAsync(course);
            // var allListCourses = await _courseRepository.GetAllCourseAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _courseRepository.GetCourseByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }
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
            if (!ModelState.IsValid) return View();
            var course = new CourseDTO();
            course.Price = addCourseDto.Price;
            course.Description = addCourseDto.Description;
            course.ImageUrl = addCourseDto.ImageUrl;
            await _courseRepository.UpdateCourseAsync(id, course);
            var allCourse = await _courseRepository.GetAllCourseAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _courseRepository.GetCourseByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }


        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_courseRepository.GetAllCourseAsync == null)
            {
                return Problem("Entity set 'AppDbContext.Courses'  is null.");
            }

            if (id != null)
            {
                await _courseRepository.DeleteCourseAsync(id);
            }


            return RedirectToAction(nameof(Index));
        }


    }
}
