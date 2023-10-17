﻿using AdminPageMVC.Data;
using AdminPageMVC.Entities;
using AdminPageMVC.Models.LoginViewModel;
using AdminPageMVC.Models.RegisterViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AdminPageinMVC.Controllers
{
    public class AccountController : Controller
    {


        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _appDbContext;


        public AccountController(UserManager<ApplicationUser> userManager, AppDbContext appDbContext, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _appDbContext = appDbContext;
            _signInManager = signInManager;

        }

        [HttpGet]
        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) return View(loginViewModel);

            var user = await _userManager.FindByEmailAsync(loginViewModel.Email);
            if (user != null)
            {
                //User is found, check password
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
                if (passwordCheck)
                {

                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                //Password is incorrect 
                TempData["Error"] = "Wrong credentials.";
                return View(loginViewModel);
            }

            // User not found
            TempData["Error"] = "Wrong Credentials";
            return View(loginViewModel);


        }

        [HttpGet]
        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View(response);
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(model);
            }

            var newUser = new ApplicationUser()
            {

                Email = model.Email,
                UserName = model.FullName,

            };
            var newUserResponse = await _userManager.CreateAsync(newUser, model.Password);

            if (newUserResponse.Succeeded)
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
            return RedirectToAction("Index", "Home");
        }



        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}