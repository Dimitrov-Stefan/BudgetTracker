using Core.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class FinancialItemsController : Controller
    {
        private readonly IFinancialItemsService _financialItemsService;

        public FinancialItemsController(IFinancialItemsService financialItemsService)
        {
            _financialItemsService = financialItemsService;
        }

        public IActionResult Index()
        {
            return View();
        }


    }
}