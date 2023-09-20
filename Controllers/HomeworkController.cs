using AdminPageinMVC.Repository;
using AdminPageMVC.Data;
using AdminPageMVC.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminPageMVC.Controllers
{
    public class HomeworkController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHomeworkRepository _homeworkRepository;
        private readonly IMapper _mapper;

        public HomeworkController(IHomeworkRepository homeworkRepository, AppDbContext context, IMapper mapper)
        {
            _homeworkRepository = homeworkRepository;
            _context = context;
            _mapper = mapper;
        }
        // GET: Homework
        public async Task<IActionResult> Index()
        {
            var homework = await _homeworkRepository.GetAllHomeworkAsync();
            return View("Index", homework);
        }

        // GET: Homework/Create
        public IActionResult Create() { return View(); }


        [HttpPost]
        //[Route("Create")]
        public async Task<IActionResult> Create(HomeworkDTO addHomeworkDto)
        {
            var homework = _mapper.Map<HomeworkDTO>(addHomeworkDto);
            var findTask = await _context.Tasks.FirstOrDefaultAsync(c => c.Id == addHomeworkDto.TaskId);
            if (findTask != null) homework.Task = findTask;
            await _homeworkRepository.AddHomeworkAsync(homework);
            return RedirectToAction(nameof(Index));
        }

        // GET: Homework/Edit/id
        public async Task<IActionResult> Edit(int id)
        {
            var homework = await _context.Homeworks.FindAsync(id);
            return View(homework);
        }

        public async Task<IActionResult> GetByIdHomework(int id)
        {
            var homeworkByIdAsync = await _homeworkRepository.GetHomeworkByIdAsync(id);
            return View("Edit", homeworkByIdAsync);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, HomeworkDTO addHomeworkDto)
        {
            var existHomework = _homeworkRepository.GetHomeworkByIdAsync(id);
            await _mapper.Map(addHomeworkDto, existHomework);
            return RedirectToAction(nameof(Index));
        }



        // : Homework/Delete/id
        public async Task<IActionResult> Delete(int id)
        {
            await _homeworkRepository.DeleteHomeworkAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
