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
            => await _financialOperationsRepository.AddAsync(item);

        public async Task<PagedList<FinancialOperation>> GetAllAsync(int userId, PagedListRequest request)
        {
            var financialOperations = await _financialOperationsRepository.GetAllAsync(userId, request.Skip, request.PageSize);
            var financialOperationsCount = await _financialOperationsRepository.GetAllCountAsync(userId);

            return new PagedList<FinancialOperation>(financialOperations, request.Page, request.PageSize, financialOperationsCount);
        }

        public async Task<FinancialOperation> GetByIdAsync(int id)
            => await _financialOperationsRepository.GetByIdAsync(id);

        public async Task UpdateAsync(FinancialOperation item)
            => await _financialOperationsRepository.UpdateAsync(item);

        public async Task<IEnumerable<FinancialOperation>> GetByFinancialItemIdAsync(int financialItemId)
        => await _financialOperationsRepository.GetByFinancialItemIdAsync(financialItemId);

        public async Task<IEnumerable<FinancialOperation>> GetAllByUserIdAsync(int userId)
            => await _financialOperationsRepository.GetAllByUserIdAsync(userId);

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
