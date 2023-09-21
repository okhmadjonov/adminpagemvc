using AdminPageinMVC.Repository;
using AdminPageMVC.Entities;
using AdminPageMVC.OnlyModelViews;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AdminPageMVC.Controllers
{
    public class TestsController : Controller
    {
        private readonly ITestRepository _testRepository;
        private readonly IMapper _mapper;

        public TestsController(ITestRepository testRepository, IMapper mapper)
        {
            _testRepository = testRepository;
            _mapper = mapper;
        }


        // GET: Tests
        public async Task<IActionResult> Index()
        {
            var allTests = await _testRepository.GetAll();
            return View("Index", allTests);
        }


        // GET: Tests/Create
        public async Task<IActionResult> Create() { return View(); }

        [HttpPost]
        public async Task<IActionResult> Create(AddTestDto addTestDto)
        {
            if (!ModelState.IsValid) return View();

            Test test = _mapper.Map<Test>(addTestDto);
            await _testRepository.AddTestAsync(test);
            return RedirectToAction(nameof(Index));


        }

        public async Task<IActionResult> Edit(int id)
        {
            var test = await _testRepository.GetTestById(id);
            return View(test);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, AddTestDto addTestDto)
        {

            Test test = _mapper.Map<Test>(addTestDto);
            await _testRepository.UpdateTest(test);
            await _testRepository.AddTestAsync(test);
            var alltest = _testRepository.GetAll();
            return RedirectToAction(nameof(Index));

        }


        public async Task<IActionResult> DeleteTest(int id)
        {
            await _testRepository.DeleteTestAsync(id);
            var allListTest = await _testRepository.GetAll();
            return View("Index", allListTest);
        }
    }
}
