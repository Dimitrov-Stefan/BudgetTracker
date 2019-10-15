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
using Models;
using Models.Entities;
using Models.Enums;
using Web.Extensions;
using Web.Models.Common;
using Web.Models.DatatableModels;
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

        public async Task<IActionResult> Index(PagedListRequest request)
        {
            var userId = User.GetCurrentUserId();
            var financialItems = await _financialItemsService.GetPagedByUserIdAsync(userId, request);

            var model = new FinancialItemListViewModel()
            {
                FinancialItems = financialItems
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> LoadTable([FromBody]DTParameters dtParameters)
        {
            var userId = User.GetCurrentUserId();
            var searchBy = dtParameters.Search?.Value;

            var orderCriteria = string.Empty;
            var orderAscendingDirection = true;

            if (dtParameters.Order != null)
            {
                // in this example we just default sort on the 1st column
                orderCriteria = dtParameters.Columns[dtParameters.Order[0].Column].Data;
                orderAscendingDirection = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";
            }
            else
            {
                // if we have an empty search then just order the results by Id ascending
                orderCriteria = "Id";
                orderAscendingDirection = true;
            }

            var result = await _financialItemsService.GetPagedByUserIdAsync(userId, new PagedListRequest() { Page = 0, PageSize = int.MaxValue }); // TODO: Have to make this IQueryable

            var finalResult = result.Items;

            if (!string.IsNullOrEmpty(searchBy))
            {
                finalResult = finalResult.Where(r => r.Name != null && r.Name.ToUpper().Contains(searchBy.ToUpper()))
                    .ToList();
            }

            finalResult = orderAscendingDirection ? finalResult.AsQueryable().OrderByDynamic(orderCriteria, LinqExtensions.Order.Asc).ToList() : finalResult.AsQueryable().OrderByDynamic(orderCriteria, LinqExtensions.Order.Desc).ToList();

            // now just get the count of items (without the skip and take) - eg how many could be returned with filtering
            var filteredResultsCount = finalResult.Count();
            var totalResultsCount = await _financialItemsService.GetCountByUserIdAsync(userId);

            return Json(new
            {
                draw = dtParameters.Draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = filteredResultsCount,
                data = finalResult
                    .Skip(dtParameters.Start)
                    .Take(dtParameters.Length)
                    .ToList()
            });
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
            var userId = User.GetCurrentUserId();
            var financialItem = await _financialItemsService.GetByIdAndUserIdAsync(id, userId);

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
            var userId = User.GetCurrentUserId();
            var financialItem = await _financialItemsService.GetByIdAndUserIdAsync(model.Id, userId);

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
            var userId = User.GetCurrentUserId();
            await _financialItemsService.DeleteAsync(id, userId);

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