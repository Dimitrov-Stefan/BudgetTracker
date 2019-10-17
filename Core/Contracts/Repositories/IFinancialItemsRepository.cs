using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.DatatableModels;
using Models.Entities;
using Models.Enums;

namespace Core.Contracts.Repositories
{
    public interface IFinancialItemsRepository : IRepository<FinancialItem>
    {
        Task<IEnumerable<FinancialItem>> GetAllByUserIdAsync(int userId);

        Task<IEnumerable<FinancialItem>> GetPagedByUserIdAsync(int userId, int skip, int take);

        Task<IEnumerable<FinancialItem>> GetExpensesByUserIdAsync(int userId);

        Task<IEnumerable<FinancialItem>> GetIncomeByUserIdAsync(int userId);

        Task<IEnumerable<FinancialItem>> GetByUserIdAndTypeAsync(int userId, FinancialItemType type);

        Task<IEnumerable<FinancialItem>> GetAllActiveByUserIdAsync(int userId);

        IQueryable<FinancialItem> GetAllByUserId(int userId);

        Task<FinancialItem> GetByIdAndUserIdAsync(int id, int userId);

        Task<int> GetAllCountByUserIdAsync(int userId);

        Task<IEnumerable<FinancialItem>> GetFilteredItemsByUserIdAsync(int userId, DTParameters dtParameters);
    }
}
