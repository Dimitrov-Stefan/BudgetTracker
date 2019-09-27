using System.Threading.Tasks;
using Core.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Entities.Identity;
using NToastNotify;
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
        private readonly IToastNotification _toastNotification;

        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager, IAccountService accountService, IToastNotification toastNotification)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _accountService = accountService;
            _toastNotification = toastNotification;
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

        [HttpGet]
        public async Task<IActionResult> Manage()
        {
            var user = await _userManager.GetUserAsync(User);

            var manageViewModel = new ManageViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };

            return View(manageViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Manage(ManageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound("User not found. Please try again.");
            }

            if (!string.IsNullOrWhiteSpace(model.NewPassword))
            {
                if (model.CurrentPassword == null)
                {
                    model.CurrentPassword = string.Empty;
                }

                var currentPasswordCorrect = await _userManager.CheckPasswordAsync(user, model.CurrentPassword);

                if (currentPasswordCorrect)
                {
                    var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

                    if (!changePasswordResult.Succeeded)
                    {
                        foreach (var error in changePasswordResult.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }

                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Your current password is incorrect.");

                    return View(model);
                }
            }
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.UserName = model.Email;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                _toastNotification.AddSuccessToastMessage("Save successful!", new NotyOptions() { Timeout = 5000, Layout = "bottomRight" });
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }
    }
}