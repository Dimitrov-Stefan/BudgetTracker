using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Entities;

namespace Core.Contracts.Repositories
{
    public interface IFinancialOperationsRepository : IRepository<FinancialOperation>
    {
        Task<FinancialOperation> GetByIdAsync(int id);

        Task<IEnumerable<FinancialOperation>> GetAllAsync();

        Task<IEnumerable<FinancialOperation>> GetByFinancialItemIdAsync(int financialItemId);

        Task<IEnumerable<FinancialOperation>> GetAllByUserIdAsync(int userId);

        Task<IEnumerable<FinancialOperation>> GetByMultuipleFinancialItemIdsAndDateRangeAsync(List<int> financialItemIds, DateTimeOffset? from, DateTimeOffset? to);
    }
}
