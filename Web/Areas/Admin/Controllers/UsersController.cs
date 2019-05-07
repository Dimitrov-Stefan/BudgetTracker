using System.Linq;
using System.Threading.Tasks;
using Core.Constants;
using Core.Contracts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Areas.Admin.Models.Users;
using Web.Extensions;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Roles.Admin)]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public UsersController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllAsync();

            var model = new UserListViewModel()
            {
                Users = users
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new CreateUserViewModel()
            {
                Roles = _roleService.GetAll().Select(r => new SelectListItem(r.Name, r.Id.ToString())).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.CreateAsync(model.FirstName, model.LastName, model.Email, model.Password, model.RoleId);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(UsersController.Index));
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                // TODO: Add log error here when logging is implemented
            }

            model.Roles = _roleService.GetAll().Select(r => new SelectListItem(r.Name, r.Id.ToString())).ToList();

            return View(model);
        }
    }
}