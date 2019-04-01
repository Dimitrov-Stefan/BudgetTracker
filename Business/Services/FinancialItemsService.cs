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
        {
            await _financialItemsRepository.AddAsync(item);
        }
    }
}
