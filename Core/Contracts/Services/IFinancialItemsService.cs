using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using Models.Entities;
using Models.Enums;

namespace Core.Contracts.Services
{
    public interface IFinancialItemsService
    {
        Task<IEnumerable<FinancialItem>> GetAllByUserIdAsync(int userId);

        Task<PagedList<FinancialItem>> GetPagedByUserIdAsync(int userId, PagedListRequest request);

        Task<IEnumerable<FinancialItem>> GetExpensesByUserIdAsync(int userId);

        Task<IEnumerable<FinancialItem>> GetIncomeByUserIdAsync(int userId);

        Task<IEnumerable<FinancialItem>> GetByUserIdAndTypeAsync(int userId, FinancialItemType type);

        Task<IEnumerable<FinancialItem>> GetAllActiveByUserIdAsync(int userId);

        Task CreateAsync(FinancialItem item);

        Task<FinancialItem> GetByIdAsync(int id);

        Task UpdateAsync(FinancialItem item);

        Task DeleteAsync(int id);
    }
}
