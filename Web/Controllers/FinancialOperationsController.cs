using System.Threading.Tasks;
using Core.Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;
using Web.Models.FinancialOperations;

namespace Web.Controllers
{
    public class FinancialOperationsController : Controller
    {
        private readonly IFinancialOperationsService _financialOperationsService;

        public FinancialOperationsController(IFinancialOperationsService financialOperationsService)
        {
            _financialOperationsService = financialOperationsService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new CreateFinancialOperationViewModel();

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

                await _financialOperationsService.CreateAsync(financialOperation);

                return RedirectToAction(nameof(FinancialOperationsController.Index));
            }

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
                Amount = financialOperation.Amount,
                Timestamp = financialOperation.Timestamp,
                FinancialItemId = financialOperation.FinancialItemId,
                Description = financialOperation.Description
            };

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
                financialOperation.Description = model.Description;

                await _financialOperationsService.UpdateAsync(financialOperation);

                return RedirectToAction(nameof(FinancialOperationsController.Index));
            }

            return View(model);
        }
    }
}