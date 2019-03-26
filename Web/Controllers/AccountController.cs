using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Extensions;
using Web.Models.Account;

namespace Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            var model = new LoginViewModel()
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(HomeController.Index), Url.ControllerName(typeof(HomeController)));
            }
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Logout()
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(HomeController.Index), Url.ControllerName(typeof(HomeController)));
            }
            return View();
        }
    }
}