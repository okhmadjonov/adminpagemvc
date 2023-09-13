using AdminPageMVC.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AdminPageMVC.Controllers
{
    public class UserController : Controller
    {
        public readonly IUserRepsoitory _userRepsoitory;

        public UserController(IUserRepsoitory userRepsoitory)
        {
            _userRepsoitory = userRepsoitory;
        }
        public async Task<IActionResult> GetUserList()
        {
            var allUsersAsync = await _userRepsoitory.GetUserList();
            return View("_UserTable", allUsersAsync);
        }
    }
}
