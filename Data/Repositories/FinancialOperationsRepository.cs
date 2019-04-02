using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Data.Repositories
{
    public class FinancialOperationsRepository : RepositoryBase<FinancialOperation>, IFinancialOperationsRepository
    {
        public FinancialOperationsRepository(BudgetTrackerDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<FinancialOperation>> GetAllAsync()
            => await Set.ToListAsync();  
    }
}
