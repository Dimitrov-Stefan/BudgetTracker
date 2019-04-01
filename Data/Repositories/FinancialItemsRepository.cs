using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Data.Repositories
{
    public class FinancialItemsRepository : RepositoryBase<FinancialItem>, IFinancialItemsRepository
    {
        public FinancialItemsRepository(BudgetTrackerDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<FinancialItem>> GetAllActiveAsync()
            => await Set.Where(fi => fi.IsActive).ToListAsync();
       
    }
}
