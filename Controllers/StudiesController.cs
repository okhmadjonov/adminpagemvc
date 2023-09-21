using AdminPageinMVC.Repository;
using AdminPageMVC.Data;
using AdminPageMVC.OnlyModelViews;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminPageMVC.Controllers
{
    public class StudiesController : Controller
    {
        private readonly IEducationRepository _educationRepository;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public StudiesController(IEducationRepository educationRepository, AppDbContext context, IMapper mapper)
        {
            _educationRepository = educationRepository;
            _context = context;
            _mapper = mapper;
        }
        // GET: Studies
        public async Task<IActionResult> Index()
        {
            var education = await _educationRepository.GetAllEducationAsync();
            return View(education);
        }


        // GET: Studies/Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(AddEducationDto educationDto)
        {
            if (!ModelState.IsValid) return View("_EducationPage");
            var edu = new DTO.StudyDTO();
            edu.Title = educationDto.Title;
            edu.Finish = educationDto.End;
            edu.Description = educationDto.Description;
            var findCourse = await _context.Courses.FirstOrDefaultAsync(c => c.Id == educationDto.CourseId);
            if (findCourse != null)
            {
                edu.Course = findCourse;
            }

            await _educationRepository.AddEducationAsync(edu);
            var getEducationList = await _educationRepository.GetAllEducationAsync();
            return View("_EducationPage", getEducationList);
        }


        public async Task<IActionResult> GetByIdEducation(int id)
        {
            var teacherByIdAsync = await _educationRepository.GetEducationByIdAsync(id);
            return View("Edit", teacherByIdAsync);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateEducation(int id, AddEducationDto educationDto)
        {
            if (!ModelState.IsValid) return View("_EducationPage");
            var education = new DTO.StudyDTO();
            education.Title = educationDto.Title;
            education.Finish = educationDto.End;
            education.Description = educationDto.Description;
            var findCourse = await _context.Courses.FirstOrDefaultAsync(c => c.Id == educationDto.CourseId);
            if (findCourse != null)
            {
                education.Course = findCourse;
            }
            await _educationRepository.UpdateEducationAsync(id, education);
            var allListTeachers = await _educationRepository.GetAllEducationAsync();
            return View("Index", allListTeachers);
        }



        public async Task<IActionResult> DeleteCourse(int id)
        {
            await _educationRepository.DeleteEducationAsync(id);
            var allListTeachers = await _educationRepository.GetAllEducationAsync();
            return View("Index", allListTeachers);
        }

    }
}
