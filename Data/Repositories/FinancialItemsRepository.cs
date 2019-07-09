using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Models.Enums;

namespace Data.Repositories
{
    public class FinancialItemsRepository : RepositoryBase<FinancialItem>, IFinancialItemsRepository
    {
        public FinancialItemsRepository(BudgetTrackerDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<FinancialItem>> GetAllByUserIdAsync(int userId)
            => await Set.Where(fi => fi.UserId == userId).ToListAsync();

        public async Task<IEnumerable<FinancialItem>> GetExpensesByUserIdAsync(int userId)
            => await Set.Where(fi => fi.UserId == userId && fi.Type == FinancialItemType.Expense).ToListAsync();

        public async Task<IEnumerable<FinancialItem>> GetIncomeByUserIdAsync(int userId)
            => await Set.Where(fi => fi.UserId == userId && fi.Type == FinancialItemType.Income).ToListAsync();

        public async Task<IEnumerable<FinancialItem>> GetByUserIdAndTypeAsync(int userId, FinancialItemType type)
            => await Set.Where(fi => fi.UserId == userId && fi.Type == type).ToListAsync();

        public async Task<IEnumerable<FinancialItem>> GetAllActiveByUserIdAsync(int userId)
            => await Set.Where(fi => fi.UserId == userId && fi.IsActive).ToListAsync();
       
    }
}
