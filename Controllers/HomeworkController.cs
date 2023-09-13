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
    public class HomeworkController : Controller
    {
        private readonly AppDbContext _context;

        public HomeworkController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Homework
        public async Task<IActionResult> Index()
        {
              return _context.Homeworks != null ? 
                          View(await _context.Homeworks.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Homeworks'  is null.");
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

        // POST: Homework/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ImageUrl,Description")] Homework homework)
        {
            if (ModelState.IsValid)
            {
                _context.Add(homework);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(homework);
        }

        // GET: Homework/Edit/5
        public async Task<IActionResult> Edit(int? id)
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

        // POST: Homework/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ImageUrl,Description")] Homework homework)
        {
            if (id != homework.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(homework);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HomeworkExists(homework.Id))
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
            return View(homework);
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
