﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.Entities;
using Web.Models.FinancialOperations;

namespace Web.Controllers
{
    public class FinancialOperationsController : Controller
    {
        private readonly IFinancialOperationsService _financialOperationsService;
        private readonly IFinancialItemsService _financialItemsService;

        public FinancialOperationsController(IFinancialOperationsService financialOperationsService, IFinancialItemsService financialItemsService)
        {
            _financialOperationsService = financialOperationsService;
            _financialItemsService = financialItemsService;
        }

        public async Task<IActionResult> Index()
        {
            var financialOperations = await _financialOperationsService.GetAllAsync();

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

            var financialItems = await _financialItemsService.GetAllAsync();
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
                    Timestamp = model.Timestamp,
                    FinancialItemId = model.FinancialItemId,
                    Description = model.Description
                };

                await _financialOperationsService.CreateAsync(financialOperation);

                return RedirectToAction(nameof(FinancialOperationsController.Index));
            }

            var financialItems = await _financialItemsService.GetAllAsync();
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
                Amount = financialOperation.Amount,
                Timestamp = financialOperation.Timestamp,
                FinancialItemId = financialOperation.FinancialItemId,
                Description = financialOperation.Description
            };

            var financialItems = await _financialItemsService.GetAllAsync();
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
                financialOperation.Timestamp = model.Timestamp;
                financialOperation.FinancialItemId = model.FinancialItemId;
                financialOperation.Description = model.Description;

                await _financialOperationsService.UpdateAsync(financialOperation);

                return RedirectToAction(nameof(FinancialOperationsController.Index));
            }

            var financialItems = await _financialItemsService.GetAllAsync();
            model.FinancialItems = financialItems.Select(fi => new SelectListItem(fi.Name, fi.Id.ToString()));

            return View(model);
        }
    }
}