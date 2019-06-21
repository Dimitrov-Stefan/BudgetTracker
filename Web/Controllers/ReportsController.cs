using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Business.Services;
using Core.Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;
using Models.Enums;
using Web.Extensions;
using Web.Models.Reports;

namespace Web.Controllers
{
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
        public async Task<IActionResult> Balance(string from, string to)
        {
            var financialItems = await _financialItemsService.GetAllByUserIdAsync(User.GetCurrentUserId());

            var format = "MM/dd/yyyy";
        
            if (!DateTimeOffset.TryParseExact(from, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTimeOffset fromDate))
            {
                fromDate = DateTimeOffset.MinValue;
            }

            if (!DateTimeOffset.TryParseExact(to, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTimeOffset toDate))
            {
                toDate = DateTimeOffset.MaxValue;
            }

            var report = await _reportsService.GetBalanceAsync(financialItems, fromDate, toDate);

            var model = new BalanceReportViewModel()
            {
                From = null,
                To = null,
                BalanceReport = report
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Expenses(IEnumerable<int> financialItemsIds, string from, string to)
        {
            var financialItems = await _financialItemsService.GetByUserIdAndTypeAsync(User.GetCurrentUserId(), FinancialItemType.Expense);

            var format = "MM/dd/yyyy";

            if (!DateTimeOffset.TryParseExact(from, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTimeOffset fromDate))
            {
                fromDate = DateTimeOffset.MinValue;
            }

            if (!DateTimeOffset.TryParseExact(to, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTimeOffset toDate))
            {
                toDate = DateTimeOffset.MaxValue;
            }

            var report = await _reportsService.GetExpensesAsync(financialItems, fromDate, toDate);

            var model = new ExpensesReportViewModel()
            {
                From = null,
                To = null,
                ExpensesReport = report
            };

            return View(model);
        }
    }
}