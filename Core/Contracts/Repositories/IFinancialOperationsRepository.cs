using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Entities;

namespace Core.Contracts.Repositories
{
    public interface IFinancialOperationsRepository : IRepository<FinancialOperation>
    {
        Task<IEnumerable<FinancialOperation>> GetAllAsync();
    }
}
