using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdminPageMVC.Data;
using AdminPageMVC.Entities;

namespace AdminPageMVC.Controllers
{
    public class TaskAnswersController : Controller
    {
        private readonly AppDbContext _context;

        public TaskAnswersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: TaskAnswers
        public async Task<IActionResult> Index()
        {
              return _context.TaskAnswers != null ? 
                          View(await _context.TaskAnswers.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.TaskAnswers'  is null.");
        }

        // GET: TaskAnswers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TaskAnswers == null)
            {
                return NotFound();
            }

            var taskAnswer = await _context.TaskAnswers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taskAnswer == null)
            {
                return NotFound();
            }

            return View(taskAnswer);
        }

        // GET: TaskAnswers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TaskAnswers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Answer,FileUrl")] TaskAnswer taskAnswer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(taskAnswer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(taskAnswer);
        }

        // GET: TaskAnswers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TaskAnswers == null)
            {
                return NotFound();
            }

            var taskAnswer = await _context.TaskAnswers.FindAsync(id);
            if (taskAnswer == null)
            {
                return NotFound();
            }
            return View(taskAnswer);
        }

        // POST: TaskAnswers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Answer,FileUrl")] TaskAnswer taskAnswer)
        {
            if (id != taskAnswer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taskAnswer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskAnswerExists(taskAnswer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(taskAnswer);
        }

        // GET: TaskAnswers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TaskAnswers == null)
            {
                return NotFound();
            }

            var taskAnswer = await _context.TaskAnswers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taskAnswer == null)
            {
                return NotFound();
            }

            return View(taskAnswer);
        }

        // POST: TaskAnswers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TaskAnswers == null)
            {
                return Problem("Entity set 'AppDbContext.TaskAnswers'  is null.");
            }
            var taskAnswer = await _context.TaskAnswers.FindAsync(id);
            if (taskAnswer != null)
            {
                _context.TaskAnswers.Remove(taskAnswer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskAnswerExists(int id)
        {
          return (_context.TaskAnswers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
