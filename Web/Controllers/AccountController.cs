﻿using System.Threading.Tasks;
using Core.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Entities.Identity;
using Web.Extensions;
using Web.Models.Account;
using Web.Services;

namespace Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IAccountService _accountService;

        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager, IAccountService accountService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _accountService = accountService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login([FromQuery]string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            var model = new LoginViewModel()
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromForm]LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

                if (result.Succeeded)
                {
                    return LocalRedirect(model.ReturnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An error occured during login");
                }
            }
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(HomeController.Index), Url.ControllerName(typeof(HomeController)));
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Register()
        {
            var model = new RegisterViewModel();


            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromForm] RegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.CreateAccountAsync(
                    viewModel.FirstName,
                    viewModel.LastName,
                    viewModel.Email,
                    viewModel.Password,
                    Roles.User);

                if (result.Succeeded)
                {

                    var userId = await _userManager.GetUserIdAsync(result.User);

                    await _signInManager.SignInAsync(result.User, isPersistent: false);

                    return RedirectToAction(nameof(HomeController.Index), Url.ControllerName(typeof(HomeController)));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(viewModel);
        }
    }
}