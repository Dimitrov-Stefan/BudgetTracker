using System.Linq;
using System.Threading.Tasks;
using Core.Constants;
using Core.Contracts.Services;
using Core.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.DatatableModels;
using Newtonsoft.Json;
using Web.Areas.Admin.Models.Users;

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
        public IActionResult Index()
        {
            return View();
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
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                // TODO: Add log error here when logging is implemented
            }

            model.Roles = _roleService.GetAll().Select(r => new SelectListItem(r.Name, r.Id.ToString())).ToList();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userService.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound(user);
            }

            var model = new EditUserViewModel()
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                IsActive = user.IsActive
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            var user = await _userService.GetByIdAsync(model.Id);

            if (user == null)
            {
                return NotFound(user);
            }

            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.IsActive = model.IsActive;


            var result = await _userService.EditAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(UsersController.Index));
            }

            var userModel = new EditUserViewModel()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Details(int userId)
        {
            var model = new UserDetailsViewModel();
            model.UserId = userId;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int userId)
        {
            await _userService.DeleteAsync(userId, User.GetCurrentUserId());

            // TODO: Show user-friendly errors if delete fails.

            return RedirectToAction(nameof(UsersController.Index));
        }

        #region Ajax Methods

        [HttpPost]
        public async Task<IActionResult> LoadTable([FromBody]DTParameters dtParameters)
        {
            var result = await _userService.GetFilteredUsersAsync(dtParameters);

            // now just get the count of items (without the skip and take) - eg how many could be returned with filtering
            var filteredResultsCount = result.Count();
            var totalResultsCount = await _userService.GetCountAsync();

            return Json(new
            {
                draw = dtParameters.Draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = filteredResultsCount,
                data = result
                    .Skip(dtParameters.Start)
                    .Take(dtParameters.Length)
                    .ToList()
            }, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        }

        #endregion
    }
}