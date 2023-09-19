using AdminPageinMVC.Repository;
using AdminPageMVC.Data;
using AdminPageMVC.DTO;
using AdminPageMVC.OnlyModelViews;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminPageMVC.Controllers
{
    public class LessonsController : Controller
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly AppDbContext _context;
        public LessonsController(ILessonRepository lessonRepository, AppDbContext context)
        {
            _lessonRepository = lessonRepository;
            _context = context;
        }

        // GET: Lessons
        public async Task<IActionResult> Index()
        {
            var lessons = await _lessonRepository.GetAllLessonAsync();
            return View("Index", lessons);
        }

        // GET: Lessons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Lessons == null)
            {
                return NotFound();
            }

            var lesson = await _context.Lessons
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lesson == null)
            {
                return NotFound();
            }

            return View(lesson);
        }

        // GET: Lessons/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddLessonDto addLessonDto)
        {


            if (string.IsNullOrWhiteSpace(addLessonDto.Title) || string.IsNullOrWhiteSpace(addLessonDto.VideoUrl) || string.IsNullOrWhiteSpace(addLessonDto.Information))
            {
                ModelState.AddModelError("", "All fields must be filled");
                return View("_LessonPage");
            }
            var lesson = new LessonDTO();
            lesson.Title = addLessonDto.Title;
            lesson.VideoUrl = addLessonDto.VideoUrl;
            lesson.Information = addLessonDto.Information;
            var findCourse = await _context.Courses.FirstOrDefaultAsync(c => c.Id == addLessonDto.CourseId);
            if (findCourse != null) lesson.Course = findCourse;
            await _lessonRepository.AddLessonAsync(lesson);
            var getLessonList = await _lessonRepository.GetAllLessonAsync();
            return RedirectToAction(nameof(Index));


        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id == null || _context.Lessons == null)
            {
                return NotFound();
            }

            var lesson = await _context.Lessons.FindAsync(id);
            if (lesson == null)
            {
                return NotFound();
            }
            return View(lesson);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, AddLessonDto addLessonDto)
        {
            if (!ModelState.IsValid) return View("_LessonPage");
            var lesson = new LessonDTO();
            lesson.Title = addLessonDto.Title;
            lesson.VideoUrl = addLessonDto.VideoUrl;
            lesson.Information = addLessonDto.Information;
            var findCourse = await _context.Courses.FirstOrDefaultAsync(c => c.Id == addLessonDto.CourseId);
            if (findCourse != null)
            {
                lesson.Course = findCourse;
            }
            await _lessonRepository.UpdateLessonAsync(id, lesson);
            var allListLesson = await _lessonRepository.GetAllLessonAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Lessons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Lessons == null)
            {
                return NotFound();
            }

            var lesson = await _context.Lessons
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lesson == null)
            {
                return NotFound();
            }

            return View(lesson);
        }

        // POST: Lessons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Lessons == null)
            {
                return Problem("Entity set 'AppDbContext.Lessons'  is null.");
            }
            var lesson = await _context.Lessons.FindAsync(id);
            if (lesson != null)
            {
                _context.Lessons.Remove(lesson);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LessonExists(int id)
        {
            return (_context.Lessons?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
