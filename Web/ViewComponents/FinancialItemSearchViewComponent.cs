using Core.Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Extensions;
using System.Security.Claims;
using Web.Models.Common;

namespace Web.ViewComponents
{
    public class FinancialItemSearchViewComponent : ViewComponent
    {
        private readonly IFinancialItemsService _financialItemsService;

        public FinancialItemSearchViewComponent(IFinancialItemsService financialItemsService)
        {
            _financialItemsService = financialItemsService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var id = HttpContext.User.GetCurrentUserId();

            var items = await _financialItemsService.GetAllByUserIdAsync(id);
            var itemsSimplified = items.Select(fi => new FinancialItemSimplifiedModel() { Id = fi.Id, Name = fi.Name });

            var model = new FinancialItemSearchViewModel()
            {
                FinancialItems = itemsSimplified
            };

            return View(model);
        }
    }
}
