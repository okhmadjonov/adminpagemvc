using AdminPageinMVC.Repository;
using AdminPageMVC.Entities;
using AdminPageMVC.OnlyModelViews;
using Microsoft.AspNetCore.Mvc;

namespace AdminPageMVC.Controllers
{
    public class ResultsController : Controller
    {
        private readonly IResultRepository _resultRepository;
        private readonly IEducationRepository _educationRepository;
        private readonly IUserRepository _userRepository;

        public ResultsController(IResultRepository resultRepository, IEducationRepository educationRepository, IUserRepository userRepository)
        {
            _resultRepository = resultRepository;
            _educationRepository = educationRepository;
            _userRepository = userRepository;
        }

        // GET: Results
        public async Task<IActionResult> Index()
        {
            var allResult = await _resultRepository.GetAllResultAsync();

            return View("Index", allResult);
        }



        // GET: Results/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.EducationList = await _educationRepository.GetAllEducationAsync();
            ViewBag.UserList = await _userRepository.GetAllUsersAsync();
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(AddResultDto addResultDto)
        {
            if (!ModelState.IsValid) return View("Index");
            Result result = new Result();
            result.Url = addResultDto.Url;
            result.User = await _userRepository.GetUserByIdAsync(addResultDto.UserId);
            result.Study = await _educationRepository.GetEducationByIdAsync(addResultDto.EducationId);
            await _resultRepository.AddResultAsync(result);
            var allResultAsync = await _resultRepository.GetAllResultAsync();
            return View("Index", allResultAsync);
        }

        // GET: Results/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _resultRepository.GetResultByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetResultById(int id)
        {
            if (!ModelState.IsValid) return View("ResultTable");
            var resultByIdAsync = await _resultRepository.GetResultByIdAsync(id);
            ViewBag.EducationList = await _educationRepository.GetAllEducationAsync();
            ViewBag.UserList = await _userRepository.GetAllUsersAsync();
            return View("Edit", resultByIdAsync);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AddResultDto addResultDto)
        {
            if (!ModelState.IsValid) return View("ResultTable");
            var result = await _resultRepository.GetResultByIdAsync(id);
            result.Url = addResultDto.Url;
            result.Study = await _educationRepository.GetEducationByIdAsync(addResultDto.EducationId);
            result.User = await _userRepository.GetUserByIdAsync(addResultDto.UserId);
            await _resultRepository.UpdateResultAsync(result);
            var all = await _resultRepository.GetAllResultAsync();
            return View("Index", all);
        }

        // GET: Results/Delete/5
        public async Task<IActionResult> Delete(int id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var course = await _resultRepository.GetResultByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Results/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!ModelState.IsValid) return View("Index");
            await _resultRepository.DeleteResultAsync(id);
            var all = await _resultRepository.GetAllResultAsync();
            return View("Index", all);
        }


    }
}
