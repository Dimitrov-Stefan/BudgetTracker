using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Entities;

namespace Core.Contracts.Repositories
{
    public interface IFinancialItemsRepository : IRepository<FinancialItem>
    {
        Task<IEnumerable<FinancialItem>> GetAllByUserIdAsync(int userId);

        Task<IEnumerable<FinancialItem>> GetAllActiveByUserIdAsync(int userId);
    }
}
