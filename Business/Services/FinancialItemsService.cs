using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Contracts.Repositories;
using Core.Contracts.Services;
using Models.Entities;

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


        public async Task<IEnumerable<FinancialItem>> GetAllActiveAsync() =>
            await _financialItemsRepository.GetAllActiveAsync();


        public async Task<FinancialItem> GetById(int id)
            => await _financialItemsRepository.FindAsync(id);
    }
}
