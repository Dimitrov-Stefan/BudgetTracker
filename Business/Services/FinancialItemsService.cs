using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Contracts.Repositories;
using Core.Contracts.Services;
using Models.Entities;
using Models.Enums;

namespace Business.Services
{
    public class FinancialItemsService : IFinancialItemsService
    {
        private readonly IFinancialItemsRepository _financialItemsRepository;

        public FinancialItemsService(IFinancialItemsRepository financialItemsRepository)
        {
            _financialItemsRepository = financialItemsRepository;
        }

        public async Task CreateAsync(FinancialItem item)
            => await _financialItemsRepository.AddAsync(item);

        public async Task<IEnumerable<FinancialItem>> GetAllByUserIdAsync(int userId)
            => await _financialItemsRepository.GetAllByUserIdAsync(userId);

        public async Task<IEnumerable<FinancialItem>> GetByUserIdAndTypeAsync(int userId, FinancialItemType type)
            => await _financialItemsRepository.GetByUserIdAndTypeAsync(userId, type);

        public async Task<IEnumerable<FinancialItem>> GetAllActiveByUserIdAsync(int userId)
            => await _financialItemsRepository.GetAllActiveByUserIdAsync(userId);

        public async Task<FinancialItem> GetByIdAsync(int id)
            => await _financialItemsRepository.FindAsync(id);

        public async Task UpdateAsync(FinancialItem item)
            => await _financialItemsRepository.UpdateAsync(item);

        public async Task DeleteAsync(int id)
        {
            var financialItem = await GetByIdAsync(id);

            if (financialItem != null)
            {
                await _financialItemsRepository.DeleteAsync(financialItem);
            }
        }
    }
}
