using Models.DatatableModels;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Contracts.Repositories
{
    public interface IFinancialOperationsRepository : IRepository<FinancialOperation>
    {
        Task<FinancialOperation> GetByIdAsync(int id);

        Task<IEnumerable<FinancialOperation>> GetAllAsync(int userId, int skip, int take);

        Task<IEnumerable<FinancialOperation>> GetByFinancialItemIdAsync(int financialItemId);

        Task<IEnumerable<FinancialOperation>> GetAllByUserIdAsync(int userId);

        Task<IEnumerable<FinancialOperation>> GetByMultuipleFinancialItemIdsAndDateRangeAsync(List<int> financialItemIds, DateTimeOffset? from, DateTimeOffset? to);

        Task<int> GetCountByUserIdAsync(int userId);

        Task<IEnumerable<FinancialOperation>> GetFilteredOperationsByUserIdAsync(int userId, DTParameters dtParameters);
    }
}
