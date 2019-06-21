using System;
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

        public async Task<FinancialOperation> GetByIdAsync(int id)
            => await Set.Include(fo => fo.FinancialItem).SingleOrDefaultAsync(fo => fo.Id == id);

        public async Task<IEnumerable<FinancialOperation>> GetAllAsync()
            => await Set.Include(fo => fo.FinancialItem).ToListAsync();

        public async Task<IEnumerable<FinancialOperation>> GetByFinancialItemIdAsync(int financialItemId)
            => await Set.Where(fo => fo.FinancialItemId == financialItemId).ToListAsync();

        public async Task<IEnumerable<FinancialOperation>> GetAllByUserIdAsync(int userId)
            => await Set.Include(fo => fo.FinancialItem).Where(fo => fo.FinancialItem.UserId == userId).ToListAsync();

        public async Task<IEnumerable<FinancialOperation>> GetByMultuipleFinancialItemIdsAndDateRangeAsync(List<int> financialItemIds, DateTimeOffset? from, DateTimeOffset? to)
        {
            IEnumerable<FinancialOperation> financialOperations = new List<FinancialOperation>();

            if (financialItemIds != null && financialItemIds.Count != 0)
            {
                if (from.HasValue && to.HasValue)
                {
                    // TODO: Probably need to move the truncations of dates in the service
                    financialOperations = await Set.Where(fo => financialItemIds.Contains(fo.FinancialItem.Id) && fo.Timestamp.Date >= from.Value.Date && fo.Timestamp.Date <= to.Value.Date).ToListAsync();
                }
                else if (from.HasValue)
                {
                    financialOperations = await Set.Where(fo => financialItemIds.Contains(fo.FinancialItem.Id) && fo.Timestamp >= from).ToListAsync();
                }
                else if (to.HasValue)
                {
                    financialOperations = await Set.Where(fo => financialItemIds.Contains(fo.FinancialItem.Id) && fo.Timestamp <= to).ToListAsync();
                }
                else
                {
                    financialOperations = await Set.Where(fo => financialItemIds.Contains(fo.FinancialItem.Id)).ToListAsync();
                }
                
            }

            return financialOperations;
        }
    }
}
