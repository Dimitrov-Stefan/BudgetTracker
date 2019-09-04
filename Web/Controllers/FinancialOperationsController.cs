using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Constants;
using Core.Contracts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using Models.Entities;
using Web.Extensions;
using Web.Models.FinancialOperations;

namespace Web.Controllers
{
    [Authorize(Roles = Roles.User)]
    public class FinancialOperationsController : Controller
    {
        private readonly IFinancialOperationsService _financialOperationsService;
        private readonly IFinancialItemsService _financialItemsService;

        public FinancialOperationsController(IFinancialOperationsService financialOperationsService, IFinancialItemsService financialItemsService)
        {
            _financialOperationsService = financialOperationsService;
            _financialItemsService = financialItemsService;
        }

        public async Task<IActionResult> Index(PagedListRequest request)
        {
            var userId = User.GetCurrentUserId();
            var financialOperations = await _financialOperationsService.GetAllAsync(userId, request);

            var model = new FinancialOperationListViewModel()
            {
                FinancialOperations = financialOperations
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new CreateFinancialOperationViewModel()
            {
                Timestamp = DateTimeOffset.UtcNow
            };

            var userId = User.GetCurrentUserId();

            var financialItems = await _financialItemsService.GetAllActiveByUserIdAsync(userId);
            model.FinancialItems = financialItems.Select(fi => new SelectListItem(fi.Name, fi.Id.ToString())).ToList();

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
                    Timestamp = model.Timestamp.ToUniversalTime(),
                    FinancialItemId = model.FinancialItemId,
                    Description = model.Description
                };

                if (financialOperation.FinancialItemId != 0)
                {
                    await _financialOperationsService.CreateAsync(financialOperation);

                    return RedirectToAction(nameof(FinancialOperationsController.Index));
                }
                else
                {
                    // Hack: Find a way to remove this hack
                    ModelState.AddModelError(String.Empty, "Please select a financial item.");
                }
            }

            var userId = User.GetCurrentUserId();
            var financialItems = await _financialItemsService.GetAllActiveByUserIdAsync(userId);
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

            var model = new EditFinancialOperationViewModel()
            {
                Id = financialOperation.Id,
                Amount = financialOperation.Amount,
                Timestamp = financialOperation.Timestamp.ToUniversalTime(),
                FinancialItemId = financialOperation.FinancialItemId,
                Description = financialOperation.Description
            };

            var userId = User.GetCurrentUserId();
            var financialItems = await _financialItemsService.GetAllActiveByUserIdAsync(userId);
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

            if (ModelState.IsValid)
            {
                financialOperation.Amount = model.Amount;
                financialOperation.Timestamp = model.Timestamp.ToUniversalTime();
                financialOperation.FinancialItemId = model.FinancialItemId;
                financialOperation.Description = model.Description;

                await _financialOperationsService.UpdateAsync(financialOperation);

                return RedirectToAction(nameof(FinancialOperationsController.Index));
            }

            var userId = User.GetCurrentUserId();
            var financialItems = await _financialItemsService.GetAllActiveByUserIdAsync(userId);
            model.FinancialItems = financialItems.Select(fi => new SelectListItem(fi.Name, fi.Id.ToString()));
            financialOperation.FinancialItemId = model.FinancialItemId;

            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _financialOperationsService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}