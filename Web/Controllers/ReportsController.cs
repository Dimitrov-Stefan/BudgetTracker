using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Services;
using Core.Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;
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
        public async Task<IActionResult> Balance()
        {
            var financialItems = await _financialItemsService.GetAllByUserIdAsync(User.GetCurrentUserId());

            var report = await _reportsService.GetBalanceAsync(financialItems);

            var model = new BalanceReportViewModel()
            {
                From = null,
                To = null,
                BalanceReport = report
            };

            return View(model);
        }
    }
}