using AdminPageinMVC.Repository;
using AdminPageMVC.Data;
using AdminPageMVC.DTO;
using AdminPageMVC.Entities.enums;

using AdminPageMVC.OnlyModelViews;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminPageMVC.Controllers
{
    public class TasksController : Controller
    {
        private readonly ITaskRepository _taskRepository;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public TasksController(ITaskRepository taskRepository, AppDbContext context, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _context = context;
            _mapper = mapper;
        }
        // GET: Tasks
        public async Task<IActionResult> Index()
        {
            var allListTask = await _taskRepository.GetAllTaskAsync();
            return View("Index", allListTask);
        }

        // GET: Tasks/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddTaskDto lessonDto)
        {
            if (!ModelState.IsValid) return View("Index");

            var task = new TaskDTO
            {
                Process = EProcess.PROGRESS,
                DateTime = DateTime.Now,
                Title = lessonDto.Title,
                Description = lessonDto.Description,
            };

            var findLesson = await _context.Lessons.FirstOrDefaultAsync(c => c.Id == lessonDto.LessonId);
            if (findLesson != null) task.Lesson = findLesson;

            await _taskRepository.AddTaskAsync(task);

            var allListTask = await _taskRepository.GetAllTaskAsync();
            return View("Index", allListTask);
        }

        public async Task<IActionResult> GetByIdTask(int id)
        {
            if (!ModelState.IsValid) return View("Index");
            var taskByIdAsync = await _taskRepository.GetTaskByIdAsync(id);
            return View("Edit", taskByIdAsync);
        }

        // GET: Tasks/Edit/id
        public async Task<IActionResult> Edit(int? id)
        {

            var task = await _context.Tasks.FindAsync(id);

            return View(task);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTask(int id, AddTaskDto lessonDto)
        {
            if (!ModelState.IsValid) return View("Index");
            var task = new TaskDTO();
            task.Process = EProcess.PROGRESS;
            task.DateTime = DateTime.Now;
            task.Title = lessonDto.Title;
            task.Description = lessonDto.Description;
            var findLesson = await _context.Lessons.FirstOrDefaultAsync(c => c.Id == lessonDto.LessonId);
            if (findLesson != null) task.Lesson = findLesson;
            await _taskRepository.UpdateTaskAsync(id, task);
            var allListTask = await _taskRepository.GetAllTaskAsync();
            return View("Index", allListTask);
        }



        public async Task<IActionResult> DeleteTask(int id)
        {
            await _taskRepository.DeleteTaskAsync(id);
            var allListTask = await _taskRepository.GetAllTaskAsync();
            return View("Index", allListTask);
        }


    }
}
