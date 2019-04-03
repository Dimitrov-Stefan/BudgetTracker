using System;
using System.Diagnostics;
using Core.Constants;
using Microsoft.AspNetCore.Mvc;
using Web.Extensions;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //TODO: Remove this when users and roles are all done. Used for testing now.
            return RedirectToAction(nameof(FinancialOperationsController.Index), Url.ControllerName(typeof(FinancialOperationsController)));

            //if (User.IsInRole(Roles.User))
            //{
            //    return RedirectToAction(nameof(FinancialOperationsController.Index), Url.ControllerName(typeof(FinancialOperationsController)));
            //}

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
