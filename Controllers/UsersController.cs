using AdminPageinMVC.Repository;
using AdminPageMVC.DTO;
using AdminPageMVC.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AdminPageMVC.Controllers
{
    public class UsersController : Controller
    {

        public readonly IUserRepository UserRepository;
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IResultRepository _resultRepository;

        public UsersController(IUserRepository userRepository, IFeedbackRepository feedbackRepository, IResultRepository resultRepository)
        {
            UserRepository = userRepository;
            _feedbackRepository = feedbackRepository;
            _resultRepository = resultRepository;
        }




        // GET: Users
        public async Task<IActionResult> Index()
        {
            var allUsers = await UserRepository.GetAllUsersAsync();
            return View(allUsers);
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null || UserRepository.GetAllUsersAsync() == null)
            {
                return NotFound();
            }

            var user = await UserRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
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

            if (id == null || UserRepository.GetAllUsersAsync == null)
            {
                return NotFound();
            }

            var user = await UserRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string newFullName, string newEmail, string newPassword)
        {

            UserDTO userDTO = new UserDTO();
            userDTO.FullName = newFullName;
            userDTO.Email = newEmail;
            userDTO.Password = newPassword;

            await UserRepository.UpdateUserAsync(id, userDTO);
            var allUsers = UserRepository.GetAllUsersAsync();
            return RedirectToAction("Index");
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await UserRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (UserRepository.GetAllUsersAsync == null)
            {
                return Problem("Entity set 'AppDbContext.Users'  is null.");
            }
            //var user = await UserRepository.GetUserByIdAsync(id);
            if (id != null)
            {
                await UserRepository.DeleteUserAsync(id);
            }


            return RedirectToAction(nameof(Index));
        }


    }
}
