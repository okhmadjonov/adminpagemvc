using AdminPageinMVC.Repository;
using AdminPageMVC.Entities;
using AdminPageMVC.OnlyModelViews;
using Microsoft.AspNetCore.Mvc;

namespace AdminPageMVC.Controllers
{
    public class TestsController : Controller
    {
        private readonly ITestRepository _testRepository;

        public TestsController(ITestRepository testRepository) => _testRepository = testRepository;


        // GET: Tests
        public async Task<IActionResult> Index()
        {
            List<Test> allTests = await _testRepository.GetAll();
            return View(allTests);
        }

        // GET: Tests/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null || _testRepository.GetAll() == null)
            {
                return NotFound();
            }

            var test = await _testRepository.GetTestById(id);
            if (test == null)
            {
                return NotFound();
            }

            return View(test);
        }

        // GET: Tests/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddTestDto addTestDto)
        {
            if (!ModelState.IsValid) return View();

            Test test = new Test();
            test.Question = addTestDto.Question;
            test.Variants = addTestDto.Options;
            test.RightVariant = addTestDto.RightOption;

            await _testRepository.AddTestAsync(test);
            return RedirectToAction(nameof(Index));


        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id == null || _testRepository.GetAll() == null)
            {
                return NotFound();
            }

            var test = await _testRepository.GetTestById(id);
            if (test == null)
            {
                return NotFound();
            }
            return View(test);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, AddTestDto addTestDto)
        {

            Test test = new Test();
            test.Question = addTestDto.Question;
            test.Variants = addTestDto.Options;
            test.RightVariant = addTestDto.RightOption;
            await _testRepository.UpdateTest(test);

            await _testRepository.AddTestAsync(test);
            var alltest = _testRepository.GetAll();
            return RedirectToAction(nameof(Index));

        }


        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var test = await _testRepository.GetTestById(id);
            if (test == null)
            {
                return NotFound();
            }
            return View(test);
        }

        // POST: Tests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_testRepository.GetAll == null)
            {
                return Problem("Entity set 'AppDbContext.Tests'  is null.");
            }
            if (id != null)
            {
                await _testRepository.DeleteTestAsync(id);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
