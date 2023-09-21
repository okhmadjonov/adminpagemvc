using AdminPageinMVC.Repository;
using AdminPageMVC.Entities;
using AdminPageMVC.OnlyModelViews;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AdminPageMVC.Controllers
{
    public class ResultsController : Controller
    {
        private readonly IResultRepository _resultRepository;
        private readonly IEducationRepository _educationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ResultsController(IResultRepository resultRepository, IEducationRepository educationRepository,
            IMapper mapper,
            IUserRepository userRepository)
        {
            _resultRepository = resultRepository;
            _educationRepository = educationRepository;
            _userRepository = userRepository;
            _mapper = mapper;
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
            //if (!ModelState.IsValid) return View("Index");
            //Result result = new Result();
            //result.Url = addResultDto.Url;
            var result = _mapper.Map<Result>(addResultDto);
            result.User = await _userRepository.GetUserByIdAsync(addResultDto.UserId);
            result.Study = await _educationRepository.GetEducationByIdAsync(addResultDto.EducationId);
            await _resultRepository.AddResultAsync(result);
            var allResultAsync = await _resultRepository.GetAllResultAsync();
            return View("Index", allResultAsync);
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
            if (!ModelState.IsValid) return View("Index");
            var result = await _resultRepository.GetResultByIdAsync(id);
            result.Url = addResultDto.Url;
            result.Study = await _educationRepository.GetEducationByIdAsync(addResultDto.EducationId);
            result.User = await _userRepository.GetUserByIdAsync(addResultDto.UserId);
            await _resultRepository.UpdateResultAsync(result);
            var all = await _resultRepository.GetAllResultAsync();
            return View("Index", all);
        }


        // POST: Results/Delete/id
        public async Task<IActionResult> Delete() => View("Delete");

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid) return View("Index");
            await _resultRepository.DeleteResultAsync(id);
            var all = await _resultRepository.GetAllResultAsync();
            return View("Index", all);
        }

    }
}
