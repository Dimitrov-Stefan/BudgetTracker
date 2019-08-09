using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using Models.Entities;

namespace Core.Contracts.Services
{
    public interface IFinancialOperationsService
    {
        Task<PagedList<FinancialOperation>> GetAllAsync(int userId, PagedListRequest request);

        Task CreateAsync(FinancialOperation operation);

        Task<FinancialOperation> GetByIdAsync(int id);

        Task UpdateAsync(FinancialOperation operation);

        Task<IEnumerable<FinancialOperation>> GetByFinancialItemIdAsync(int financialItemId);

        Task<IEnumerable<FinancialOperation>> GetAllByUserIdAsync(int userId);

        Task DeleteAsync(int financialOperation);
    }
}
