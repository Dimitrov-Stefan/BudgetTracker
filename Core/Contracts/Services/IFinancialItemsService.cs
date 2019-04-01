using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Entities;

namespace Core.Contracts.Services
{
    public interface IFinancialItemsService
    {
        Task<IEnumerable<FinancialItem>> GetAllActiveAsync();

        Task CreateAsync(FinancialItem item);
    }
}
