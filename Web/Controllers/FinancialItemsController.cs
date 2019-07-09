using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Constants;
using Core.Contracts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.Entities;
using Models.Enums;
using Web.Extensions;
using Web.Models.Common;
using Web.Models.FinancialItems;

namespace Web.Controllers
{
    [Authorize(Roles = Roles.User)]
    public class FinancialItemsController : Controller
    {
        private readonly IFinancialItemsService _financialItemsService;

        public FinancialItemsController(IFinancialItemsService financialItemsService)
        {
            _financialItemsService = financialItemsService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.GetCurrentUserId();
            var financialItems = await _financialItemsService.GetAllByUserIdAsync(userId);

            var model = new FinancialItemListViewModel()
            {
                FinancialItems = financialItems
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new CreateFinancialItemViewModel();
            model.Types = new SelectList(Enum.GetNames(typeof(FinancialItemType)));

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateFinancialItemViewModel model)
        {
            var userId = User.GetCurrentUserId();

            if (ModelState.IsValid)
            {
                var financialItem = new FinancialItem()
                {
                    Name = model.Name,
                    Type = model.Type,
                    UserId = userId,
                    IsActive = true
                };

                await _financialItemsService.CreateAsync(financialItem);

                return RedirectToAction(nameof(FinancialItemsController.Index));
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

            var model = new EditFinancialItemViewModel()
            {
                Id = financialItem.Id,
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

                return RedirectToAction(nameof(FinancialItemsController.Index));
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _financialItemsService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

        #region Ajax Methods

        [HttpGet]
        public async Task<JsonResult> GetSimplifiedItems(FinancialItemType type)
        {
            var id = HttpContext.User.GetCurrentUserId();
            IEnumerable<FinancialItem> items = null;

            if (type == FinancialItemType.Income)
            {
                items = await _financialItemsService.GetIncomeByUserIdAsync(id);
            }
            else
            {
                items = await _financialItemsService.GetExpensesByUserIdAsync(id);
            }

            var itemsSimplified = items.Select(fi => new { id = fi.Id, name = fi.Name });

            return Json(itemsSimplified);
        }

        #endregion
    }
}