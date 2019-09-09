using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Contracts.Repositories;
using Core.Contracts.Services;
using Models;
using Models.Entities;

namespace Business.Services
{
    public class FinancialOperationService : IFinancialOperationsService
    {
        private readonly IFinancialOperationsRepository _financialOperationsRepository;

        public FinancialOperationService(IFinancialOperationsRepository financialOperationsRepository)
        {
            _financialOperationsRepository = financialOperationsRepository;
        }

        public async Task CreateAsync(FinancialOperation item)
        {
            item.Timestamp = item.Timestamp.ToUniversalTime();

            await _financialOperationsRepository.AddAsync(item);
        }

        public async Task<PagedList<FinancialOperation>> GetAllAsync(int userId, PagedListRequest request)
        {
            var financialOperations = await _financialOperationsRepository.GetAllAsync(userId, request.Skip, request.PageSize);
            var financialOperationsCount = await _financialOperationsRepository.GetAllCountAsync(userId);

            foreach(var operation in financialOperations)
            {
                operation.Timestamp = operation.Timestamp.ToLocalTime();
            }

            return new PagedList<FinancialOperation>(financialOperations, request.Page, request.PageSize, financialOperationsCount);
        }

        public async Task<FinancialOperation> GetByIdAsync(int id)
        {
            var financialOperation = await _financialOperationsRepository.GetByIdAsync(id);

            financialOperation.Timestamp = financialOperation.Timestamp.ToLocalTime();

            return financialOperation;
        }

        public async Task UpdateAsync(FinancialOperation item)
        {
            item.Timestamp = item.Timestamp.ToUniversalTime();

            await _financialOperationsRepository.UpdateAsync(item);
        }

        public async Task<IEnumerable<FinancialOperation>> GetByFinancialItemIdAsync(int financialItemId)
        {
            var financialOperations = await _financialOperationsRepository.GetByFinancialItemIdAsync(financialItemId);

            foreach (var operation in financialOperations)
            {
                operation.Timestamp = operation.Timestamp.ToLocalTime();
            }

            return financialOperations;
        }

        public async Task<IEnumerable<FinancialOperation>> GetAllByUserIdAsync(int userId)
        {
            var financialOperations = await _financialOperationsRepository.GetAllByUserIdAsync(userId);

            foreach (var operation in financialOperations)
            {
                operation.Timestamp = operation.Timestamp.ToLocalTime();
            }

            return financialOperations;
        }

        public async Task DeleteAsync(int id)
        {
            var financialOperation = await GetByIdAsync(id);

            if (financialOperation != null)
            {
                await _financialOperationsRepository.DeleteAsync(financialOperation);
            }
        }
    }
}
