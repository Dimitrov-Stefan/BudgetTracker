using Models;
using Models.DatatableModels;
using Models.Entities;
using Models.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Contracts.Services
{
    public interface IFinancialItemsService
    {
        Task<IEnumerable<FinancialItem>> GetAllByUserIdAsync(int userId);

        Task<PagedList<FinancialItem>> GetPagedByUserIdAsync(int userId, PagedListRequest request);

        Task<IEnumerable<FinancialItem>> GetFilteredItemsByUserIdAsync(int userId, DTParameters dtParameters);

        Task<IEnumerable<FinancialItem>> GetExpensesByUserIdAsync(int userId);

        Task<IEnumerable<FinancialItem>> GetIncomeByUserIdAsync(int userId);

        Task<IEnumerable<FinancialItem>> GetByUserIdAndTypeAsync(int userId, FinancialItemType type);

        Task<IEnumerable<FinancialItem>> GetAllActiveByUserIdAsync(int userId);

        Task CreateAsync(FinancialItem item);

        Task<FinancialItem> GetByIdAsync(int id);

        Task<FinancialItem> GetByIdAndUserIdAsync(int id, int userId);

        Task UpdateAsync(FinancialItem item);

        Task DeleteAsync(int id);

        Task DeleteAsync(int id, int userId);

        Task<int> GetCountByUserIdAsync(int userId);
    }
}
