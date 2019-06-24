using Core.Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Extensions;
using System.Security.Claims;

namespace Web.ViewComponents
{
    public class FinancialItemSearchViewComponent : ViewComponent
    {
        private readonly IFinancialItemsService _financialItemsService;

        public FinancialItemSearchViewComponent(IFinancialItemsService financialItemsService)
        {
            _financialItemsService = financialItemsService;
        }

        public async IViewComponentResult Invoke()
        {
            var id = HttpContext.User.GetCurrentUserId();

            var items = await _financialItemsService.GetAllByUserIdAsync(id);
            //var itemsDict = items.Select(fi => new { Id = fi.Id, Name = fi.Name }).ToDictionary(x => { key});

            return View();
        }
    }
}
