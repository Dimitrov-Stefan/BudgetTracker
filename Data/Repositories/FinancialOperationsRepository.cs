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
            => await Set.Include(fo => fo.FinancialItem).ToListAsync();

        public async Task<IEnumerable<FinancialOperation>> GetByFinancialItemIdAsync(int financialItemId)
            => await Set.Where(fo => fo.FinancialItemId == financialItemId).ToListAsync();

        public async Task<IEnumerable<FinancialOperation>> GetAllByUserIdAsync(int userId)
            => await Set.Include(fo => fo.FinancialItem).Where(fo => fo.FinancialItem.UserId == userId).ToListAsync();
    }
}
