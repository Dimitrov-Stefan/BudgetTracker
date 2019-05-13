using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Entities;

namespace Core.Contracts.Services
{
    public interface IFinancialOperationsService
    {
        Task<IEnumerable<FinancialOperation>> GetAllAsync();

        Task CreateAsync(FinancialOperation operation);

        Task<FinancialOperation> GetByIdAsync(int id);

        Task UpdateAsync(FinancialOperation operation);

        Task<IEnumerable<FinancialOperation>> GetByFinancialItemIdAsync(int financialItemId);

        Task DeleteAsync(int financialOperation);
    }
}
