using AdminPageinMVC.Repository;
using AdminPageMVC.Data;
using AdminPageMVC.DTO;
using AdminPageMVC.OnlyModelViews;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminPageMVC.Controllers
{
    public class HomeworkController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHomeworkRepository _homeworkRepository;

        public HomeworkController(IHomeworkRepository homeworkRepository, AppDbContext context)
        {
            _homeworkRepository = homeworkRepository;
            _context = context;
        }
        // GET: Homework
        public async Task<IActionResult> Index()
        {
            var homework = await _homeworkRepository.GetAllHomeworkAsync();
            return View("Index", homework);
        }

        // GET: Homework/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Homeworks == null)
            {
                return NotFound();
            }

            var homework = await _context.Homeworks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (homework == null)
            {
                return NotFound();
            }

            return View(homework);
        }

        // GET: Homework/Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(AddHomeworkDto addHomeworkDto)
        {
            if (!ModelState.IsValid) return View();
            var homework = new HomeworkDTO();
            homework.ImageUrl = addHomeworkDto.ImageUrl;
            homework.Description = addHomeworkDto.Description;
            var findTask = await _context.Tasks.FirstOrDefaultAsync(c => c.Id == addHomeworkDto.TaskId);
            if (findTask != null) homework.Task = findTask;
            await _homeworkRepository.AddHomeworkAsync(homework);
            // var allListHomework = await _homeworkRepository.GetAllHomeworkAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Homework/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null || _context.Homeworks == null)
            {
                return NotFound();
            }

            var homework = await _context.Homeworks.FindAsync(id);
            if (homework == null)
            {
                return NotFound();
            }
            return View(homework);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AddHomeworkDto addHomeworkDto)
        {
            //if (id != homework.Id)
            //{
            //    return NotFound();
            //}

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        _context.Update(homework);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!HomeworkExists(homework.Id))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            //}
            //return View(homework);

            if (!ModelState.IsValid) return View("_HomeworkPage");
            var homeworkDto = new HomeworkDTO();
            homeworkDto.ImageUrl = addHomeworkDto.ImageUrl;
            homeworkDto.Description = addHomeworkDto.Description;
            var findTask = await _context.Tasks.FirstOrDefaultAsync(c => c.Id == addHomeworkDto.TaskId);
            if (findTask != null) homeworkDto.Task = findTask;
            await _homeworkRepository.UpdateHomeworkAsync(id, homeworkDto);
            var allListHomework = await _homeworkRepository.GetAllHomeworkAsync();
            return RedirectToAction(nameof(Index));







        }

        // GET: Homework/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Homeworks == null)
            {
                return NotFound();
            }

            var homework = await _context.Homeworks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (homework == null)
            {
                return NotFound();
            }

            return View(homework);
        }

        // POST: Homework/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Homeworks == null)
            {
                return Problem("Entity set 'AppDbContext.Homeworks'  is null.");
            }
            var homework = await _context.Homeworks.FindAsync(id);
            if (homework != null)
            {
                _context.Homeworks.Remove(homework);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HomeworkExists(int id)
        {
            return (_context.Homeworks?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
