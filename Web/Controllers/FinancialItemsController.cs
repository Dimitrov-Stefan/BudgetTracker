using System;
using System.Threading.Tasks;
using Core.Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.Entities;
using Models.Enums;
using Web.Models.FinancialItems;

namespace Web.Controllers
{
    public class FinancialItemsController : Controller
    {
        private readonly IFinancialItemsService _financialItemsService;

        public FinancialItemsController(IFinancialItemsService financialItemsService)
        {
            _financialItemsService = financialItemsService;
        }

        public async Task<IActionResult> Index()
        {
            var financialItems = await _financialItemsService.GetAllActiveAsync();

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
            if (ModelState.IsValid)
            {
                var financialItem = new FinancialItem()
                {
                    Name = model.Name,
                    Type = model.Type,
                    IsActive = true
                };

                await _financialItemsService.CreateAsync(financialItem);
            }

            model.Types = new SelectList(Enum.GetNames(typeof(FinancialItemType)));

            return View(model);
        }
    }
}