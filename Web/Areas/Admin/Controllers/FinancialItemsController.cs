using System;
using System.Threading.Tasks;
using Core.Constants;
using Core.Contracts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.Entities;
using Models.Enums;
using Web.Areas.Admin.Models.FinancialItems;
using Web.Extensions;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Roles.Admin)]
    public class FinancialItemsController : Controller
    {
        private readonly IFinancialItemsService _financialItemsService;

        public FinancialItemsController(IFinancialItemsService financialItemsService)
        {
            _financialItemsService = financialItemsService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int userId)
        {
            var financialItems = await _financialItemsService.GetAllByUserIdAsync(userId);

            var model = new FinancialItemListViewModel()
            {
                UserId = userId,
                FinancialItems = financialItems
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int userId)
        {
            var model = new CreateFinancialItemViewModel();
            model.UserId = userId;
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

                return RedirectToAction(nameof(FinancialItemsController.Index), new { userId = financialItem.UserId });
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
    }
}