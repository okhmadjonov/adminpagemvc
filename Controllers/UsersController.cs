using AdminPageinMVC.Repository;
using AdminPageMVC.DTO;
using AdminPageMVC.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AdminPageMVC.Controllers
{
    public class UsersController : Controller
    {

        public readonly IUserRepository UserRepository;
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IResultRepository _resultRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IMapper mapper, IFeedbackRepository feedbackRepository, IResultRepository resultRepository)
        {
            UserRepository = userRepository;
            _feedbackRepository = feedbackRepository;
            _resultRepository = resultRepository;
            _mapper = mapper;
        }




        // GET: Users
        public async Task<IActionResult> Index()
        {
            var allUsers = await UserRepository.GetAllUsersAsync();
            return View(allUsers);
        }
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(string FullName, string Email, string Password)
        {
            if (ModelState.IsValid)
            {
                User newUser = new User();

                newUser.FullName = FullName;
                newUser.Email = Email;
                newUser.Password = Password;

                await UserRepository.AddUserAsync(newUser);

                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var user = await UserRepository.GetUserByIdAsync(id);
            return View(user);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, string newFullName, string newEmail, string newPassword)
        {

            UserDTO userDTO = new UserDTO();
            userDTO.FullName = newFullName;
            userDTO.Email = newEmail;
            userDTO.Password = newPassword;

            await UserRepository.UpdateUserAsync(id, userDTO);

            var allUsers = UserRepository.GetAllUsersAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid) return View("Index");
            await UserRepository.DeleteUserAsync(id);
            var allUsersAsync = await UserRepository.GetAllUsersAsync();
            return View("Index");
        }
    }
}
