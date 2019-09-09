using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Constants;
using Core.Contracts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.Entities;
using Web.Areas.Admin.Models.FinancialOperations;
using Web.Extensions;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Roles.Admin)]
    public class FinancialOperationsController : Controller
    {
        private readonly IFinancialOperationsService _financialOperationsService;
        private readonly IFinancialItemsService _financialItemsService;
        private readonly IUserService _userService;

        public FinancialOperationsController(IFinancialOperationsService financialOperationsService,
            IFinancialItemsService financialItemsService,
            IUserService userService)
        {
            _financialOperationsService = financialOperationsService;
            _financialItemsService = financialItemsService;
            _userService = userService;
        }

        public async Task<IActionResult> Index(int id)
        {
            var financialOperations = await _financialOperationsService.GetAllByUserIdAsync(id);
            var user = await _userService.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound(user);
            }

            var model = new FinancialOperationListViewModel()
            {
                UserId = id,
                UserName = user.UserName,
                FinancialOperations = financialOperations
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int userId)
        {
            var model = new CreateFinancialOperationViewModel()
            {
                Timestamp = DateTimeOffset.Now
            };

            var user = await _userService.GetByIdAsync(userId);

            if (user == null)
            {
                return NotFound(user);
            }

            var financialItems = await _financialItemsService.GetAllActiveByUserIdAsync(userId);
            model.FinancialItems = financialItems.Select(fi => new SelectListItem(fi.Name, fi.Id.ToString())).ToList();
            model.UserId = userId;
            model.UserName = user.UserName;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateFinancialOperationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var financialOperation = new FinancialOperation()
                {
                    Amount = model.Amount,
                    Timestamp = model.Timestamp,
                    FinancialItemId = model.FinancialItemId,
                    Description = model.Description
                };

                if (financialOperation.FinancialItemId != 0)
                {
                    await _financialOperationsService.CreateAsync(financialOperation);

                    return RedirectToAction(nameof(FinancialOperationsController.Index), new { id = model.UserId });
                }
                else
                {
                    // Hack: Find a way to remove this hack
                    ModelState.AddModelError(String.Empty, "Please select a financial item.");
                }
            }

            var financialItems = await _financialItemsService.GetAllActiveByUserIdAsync(model.UserId);
            model.FinancialItems = financialItems.Select(fi => new SelectListItem(fi.Name, fi.Id.ToString()));

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var financialOperation = await _financialOperationsService.GetByIdAsync(id);

            if (financialOperation == null)
            {
                return NotFound(financialOperation);
            }

            var financialItem = await _financialItemsService.GetByIdAsync(financialOperation.FinancialItemId);

            if (financialItem == null)
            {
                return NotFound(financialItem);
            }

            var user = await _userService.GetByIdAsync(financialItem.UserId);

            if (user == null)
            {
                return NotFound(user);
            }

            var model = new EditFinancialOperationViewModel()
            {
                Id = financialOperation.Id,
                UserId = user.Id,
                UserName = user.UserName,
                Amount = financialOperation.Amount,
                Timestamp = financialOperation.Timestamp,
                FinancialItemId = financialOperation.FinancialItemId,
                Description = financialOperation.Description
            };

            var financialItems = await _financialItemsService.GetAllActiveByUserIdAsync(model.UserId);
            model.FinancialItems = financialItems.Select(fi => new SelectListItem(fi.Name, fi.Id.ToString()));

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditFinancialOperationViewModel model)
        {
            var financialOperation = await _financialOperationsService.GetByIdAsync(model.Id);

            if (financialOperation == null)
            {
                return NotFound(financialOperation);
            }

            var userId = financialOperation.FinancialItem.UserId;

            if (ModelState.IsValid)
            {
                financialOperation.Amount = model.Amount;
                financialOperation.Timestamp = model.Timestamp;
                financialOperation.FinancialItemId = model.FinancialItemId;
                financialOperation.Description = model.Description;

                await _financialOperationsService.UpdateAsync(financialOperation);

                return RedirectToAction(nameof(FinancialOperationsController.Index), new { id = model.UserId });
            }

            var financialItems = await _financialItemsService.GetAllActiveByUserIdAsync(model.UserId);
            model.FinancialItems = financialItems.Select(fi => new SelectListItem(fi.Name, fi.Id.ToString()));
            model.UserId = userId;
            financialOperation.FinancialItemId = model.FinancialItemId;

            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var operation = await _financialOperationsService.GetByIdAsync(id);

            if (operation == null)
            {
                return NotFound(operation);
            }

            var userId = operation.FinancialItem.UserId;

            await _financialOperationsService.DeleteAsync(id);


            return RedirectToAction(nameof(Index), new { id = userId });
        }
    }
}