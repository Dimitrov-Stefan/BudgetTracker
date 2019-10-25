using Core.Constants;
using Core.Contracts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.DatatableModels;
using Models.Entities;
using Models.Enums;
using System;
using System.Linq;
using System.Threading.Tasks;
using Web.Areas.Admin.Models.FinancialItems;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Roles.Admin)]
    public class FinancialItemsController : Controller
    {
        private readonly IFinancialItemsService _financialItemsService;
        private readonly IUserService _userService;

        public FinancialItemsController(IFinancialItemsService financialItemsService, IUserService userService)
        {
            _financialItemsService = financialItemsService;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Index([FromRoute]int id)
        {
            var model = new FinancialItemListViewModel()
            {
                UserId = id
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int userId)
        {
            var user = await _userService.GetByIdAsync(userId);

            if (user == null)
            {
                return NotFound(user);
            }

            var model = new CreateFinancialItemViewModel();
            model.UserId = userId;
            model.UserName = user.UserName;
            model.Types = new SelectList(Enum.GetNames(typeof(FinancialItemType)));

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateFinancialItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                var financialItem = new FinancialItem()
                {

                    Name = model.Name,
                    Type = model.Type,
                    UserId = model.UserId,
                    IsActive = true
                };

                await _financialItemsService.CreateAsync(financialItem);

                return RedirectToAction(nameof(FinancialItemsController.Index), new { id = financialItem.UserId });
            }

            model.Types = new SelectList(Enum.GetNames(typeof(FinancialItemType)));

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var financialItem = await _financialItemsService.GetByIdAsync(id);

            if (financialItem == null)
            {
                return NotFound(financialItem);
            }

            var user = await _userService.GetByIdAsync(financialItem.UserId);

            if (user == null)
            {
                return NotFound(user);
            }

            var model = new EditFinancialItemViewModel()
            {
                Id = financialItem.Id,
                UserId = financialItem.UserId,
                UserName = user.UserName,
                Name = financialItem.Name,
                Type = financialItem.Type,
                IsActive = financialItem.IsActive,
                Types = new SelectList(Enum.GetNames(typeof(FinancialItemType)))
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditFinancialItemViewModel model)
        {
            var financialItem = await _financialItemsService.GetByIdAsync(model.Id);

            if (financialItem == null)
            {
                return NotFound(financialItem);
            }

            if (ModelState.IsValid)
            {
                financialItem.Name = model.Name;
                financialItem.Type = model.Type;
                financialItem.IsActive = model.IsActive;

                await _financialItemsService.UpdateAsync(financialItem);

                return RedirectToAction(nameof(FinancialItemsController.Index), new { id = financialItem.UserId });
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var financialItem = await _financialItemsService.GetByIdAsync(id);

            if (financialItem == null)
            {
                return NotFound(financialItem);
            }

            await _financialItemsService.DeleteAsync(id);

            return RedirectToAction(nameof(FinancialItemsController.Index), new { id = financialItem.UserId });
        }

        #region Ajax Methods

        [HttpPost]
        public async Task<IActionResult> LoadItemsTable([FromBody]DTParameters dtParameters)
        {
            var userIdString = dtParameters.AdditionalValues.FirstOrDefault();

            if (string.IsNullOrWhiteSpace(userIdString) || !int.TryParse(userIdString, out int userId))
            {
                return NotFound(userIdString);
            }

            var result = await _financialItemsService.GetFilteredItemsByUserIdAsync(userId, dtParameters);

            // now just get the count of items (without the skip and take) - eg how many could be returned with filtering
            var filteredResultsCount = result.Count();
            var totalResultsCount = await _financialItemsService.GetCountByUserIdAsync(userId);

            return Json(new
            {
                draw = dtParameters.Draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = filteredResultsCount,
                data = result
                    .Skip(dtParameters.Start)
                    .Take(dtParameters.Length)
                    .ToList()
            });
        }

        #endregion
    }
}