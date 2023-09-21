using AdminPageinMVC.Repository;
using AdminPageMVC.Data;
using AdminPageMVC.DTO;
using AdminPageMVC.OnlyModelViews;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminPageMVC.Controllers
{
    public class LessonsController : Controller
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public LessonsController(ILessonRepository lessonRepository, AppDbContext context, IMapper mapper)
        {
            _lessonRepository = lessonRepository;
            _context = context;
            _mapper = mapper;
        }

        // GET: Lessons
        public async Task<IActionResult> Index()
        {
            var lessons = await _lessonRepository.GetAllLessonAsync();
            return View("Index", lessons);
        }



        // GET: Lessons/Create
        public async Task<IActionResult> Create(AddLessonDto addLessonDto)
        {
            //if (string.IsNullOrWhiteSpace(addLessonDto.Title) || string.IsNullOrWhiteSpace(addLessonDto.VideoUrl) || string.IsNullOrWhiteSpace(addLessonDto.Information))
            //{
            //    ModelState.AddModelError("", "All fields must be filled");
            //    return View("Index");
            //}
            //var lesson = new LessonDTO();
            //lesson.Title = addLessonDto.Title;
            //lesson.VideoUrl = addLessonDto.VideoUrl;
            //lesson.Information = addLessonDto.Information;
            var lesson = _mapper.Map<LessonDTO>(addLessonDto);
            var findCourse = await _context.Courses.FirstOrDefaultAsync(c => c.Id == addLessonDto.CourseId);
            if (findCourse != null) lesson.Course = findCourse;
            await _lessonRepository.AddLessonAsync(lesson);
            var getLessonList = await _lessonRepository.GetAllLessonAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> GetByIdEducation(int id)
        {
            var teacherByIdAsync = await _lessonRepository.GetLessonByIdAsync(id);
            return View("_ByIdLesson", teacherByIdAsync);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var lesson = await _context.Lessons.FindAsync(id);
            return View(lesson);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, AddLessonDto addLessonDto)
        {
            if (!ModelState.IsValid) return View("Index");
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



        // POST: Lessons/Delete/id
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lesson = await _context.Lessons.FindAsync(id);
            _context.Lessons.Remove(lesson);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
