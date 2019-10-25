using Core.Constants;
using Core.Contracts.Services;
using Core.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.Reports;

namespace Web.Controllers
{
    [Authorize(Roles = Roles.User)]
    public class ReportsController : Controller
    {
        private readonly IReportsService _reportsService;
        private readonly IFinancialItemsService _financialItemsService;

        public ReportsController(IReportsService reportsService, IFinancialItemsService financialItemsService)
        {
            _reportsService = reportsService;
            _financialItemsService = financialItemsService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Balance(DateTimeOffset from, DateTimeOffset to)
        {
            var financialItems = await _financialItemsService.GetAllByUserIdAsync(User.GetCurrentUserId());
            var report = await _reportsService.GetBalanceAsync(financialItems, from, to);

            var model = new BalanceReportViewModel()
            {
                BalanceReport = report
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Expenses(IList<FinancialItemSelectViewModel> selectedItems, DateTimeOffset from, DateTimeOffset to)
        {
            var financialItems = await _financialItemsService.GetByUserIdAndTypeAsync(User.GetCurrentUserId(), FinancialItemType.Expense);

            var itemsToFilter = financialItems;

            if (selectedItems.Count > 0)
            {
                itemsToFilter = financialItems.Where(fi => selectedItems.Any(si => si.FinancialItem.Id == fi.Id && si.IsSelected == true));
            }

            var report = await _reportsService.GetExpensesAsync(itemsToFilter, from, to);

            var financialItemsSelectList = financialItems
                .Select(fi => new FinancialItemSelectViewModel()
                {
                    FinancialItem = fi,
                    IsSelected = selectedItems.SingleOrDefault(si => si.FinancialItem.Id == fi.Id && si.IsSelected == true) != null ? true : false
                }).ToList();

            var model = new ExpensesReportViewModel()
            {
                SelectedItems = financialItemsSelectList,
                ExpensesReport = report
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Income(IList<FinancialItemSelectViewModel> selectedItems, DateTimeOffset from, DateTimeOffset to)
        {
            var financialItems = await _financialItemsService.GetByUserIdAndTypeAsync(User.GetCurrentUserId(), FinancialItemType.Income);

            var itemsToFilter = financialItems;

            if (selectedItems.Count > 0)
            {
                itemsToFilter = financialItems.Where(fi => selectedItems.Any(si => si.FinancialItem.Id == fi.Id && si.IsSelected == true));
            }

            var report = await _reportsService.GetIncomeAsync(itemsToFilter, from, to);

            var financialItemsSelectList = financialItems
                .Select(fi => new FinancialItemSelectViewModel()
                {
                    FinancialItem = fi,
                    IsSelected = selectedItems.SingleOrDefault(si => si.FinancialItem.Id == fi.Id && si.IsSelected == true) != null ? true : false
                }).ToList();

            var model = new IncomeReportViewModel()
            {
                SelectedItems = financialItemsSelectList,
                IncomeReport = report
            };

            return View(model);
        }
    }
}