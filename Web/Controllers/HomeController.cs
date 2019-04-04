using System;
using System.Diagnostics;
using Core.Constants;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Admin.Controllers;
using Web.Extensions;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (User.IsInRole(Roles.User))
            {
                return RedirectToAction(nameof(FinancialOperationsController.Index), Url.ControllerName(typeof(FinancialOperationsController)));
            }
            else if (User.IsInRole(Roles.Admin))
            {
                return RedirectToAction(nameof(UsersController.Index), Url.ControllerName(typeof(UsersController)), new { area = "admin" });
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
