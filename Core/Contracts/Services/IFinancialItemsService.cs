using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Entities;

namespace Core.Contracts.Services
{
    public interface IFinancialItemsService
    {
        Task<IEnumerable<FinancialItem>> GetAllByUserIdAsync(int userId);

        Task<IEnumerable<FinancialItem>> GetAllActiveByUserIdAsync(int userId);

        Task CreateAsync(FinancialItem item);

        Task<FinancialItem> GetByIdAsync(int id);

        Task UpdateAsync(FinancialItem item);

        Task DeleteAsync(int id);
    }
}
