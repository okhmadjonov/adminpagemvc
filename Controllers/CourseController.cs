using AdminPageMVC.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AdminPageMVC.Controllers
{
    public class CourseController : Controller
    {
        public readonly ICourseRepository _courseRepository;

        public CourseController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        public async Task<IActionResult> GetCourseList()
        {
            var allCoursesAsync = await _courseRepository.GetCourseList();
            return View("_CourseTable", allCoursesAsync);
        }
    }
}
