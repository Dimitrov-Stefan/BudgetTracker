using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Contracts.Repositories;
using Core.Contracts.Services;
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

        public async Task<IEnumerable<FinancialOperation>> GetAllAsync()
            => await _financialOperationsRepository.GetAllAsync();

        public async Task<FinancialOperation> GetByIdAsync(int id)
            => await _financialOperationsRepository.FindAsync(id);

        public async Task UpdateAsync(FinancialOperation item)
            => await _financialOperationsRepository.UpdateAsync(item);

        public async Task<IEnumerable<FinancialOperation>> GetByFinancialItemIdAsync(int financialItemId)
        => await _financialOperationsRepository.GetByFinancialItemIdAsync(financialItemId);
        
    }
}
