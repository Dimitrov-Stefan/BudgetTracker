using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Entities;

namespace Core.Contracts.Repositories
{
    public interface IFinancialItemsRepository : IRepository<FinancialItem>
    {
        Task<IEnumerable<FinancialItem>> GetAllAsync();

        Task<IEnumerable<FinancialItem>> GetAllActiveAsync();
    }
}
