using Core.Contracts.Repositories;
using Core.Extensions;
using Microsoft.EntityFrameworkCore;
using Models.DatatableModels;
using Models.Entities;
using Models.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class FinancialItemsRepository : RepositoryBase<FinancialItem>, IFinancialItemsRepository
    {
        public FinancialItemsRepository(BudgetTrackerDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<FinancialItem>> GetAllByUserIdAsync(int userId)
            => await Set.Where(fi => fi.UserId == userId).ToListAsync();

        public async Task<IEnumerable<FinancialItem>> GetPagedByUserIdAsync(int userId, int skip, int take)
            => await Set.Where(fi => fi.UserId == userId)
            .OrderBy(fi => fi.Name)
            .Skip(skip)
            .Take(take)
            .ToListAsync();

        public async Task<IEnumerable<FinancialItem>> GetExpensesByUserIdAsync(int userId)
            => await Set.Where(fi => fi.UserId == userId && fi.Type == FinancialItemType.Expense).ToListAsync();

        public async Task<IEnumerable<FinancialItem>> GetIncomeByUserIdAsync(int userId)
            => await Set.Where(fi => fi.UserId == userId && fi.Type == FinancialItemType.Income).ToListAsync();

        public async Task<IEnumerable<FinancialItem>> GetByUserIdAndTypeAsync(int userId, FinancialItemType type)
            => await Set.Where(fi => fi.UserId == userId && fi.Type == type).ToListAsync();

        public async Task<IEnumerable<FinancialItem>> GetAllActiveByUserIdAsync(int userId)
            => await Set.Where(fi => fi.UserId == userId && fi.IsActive).ToListAsync();

        public async Task<FinancialItem> GetByIdAndUserIdAsync(int id, int userId)
        => await Set.SingleOrDefaultAsync(fi => fi.Id == id && fi.UserId == userId);

        public Task<int> GetAllCountByUserIdAsync(int userId)
            => Set.Where(fi => fi.UserId == userId)
            .CountAsync();

        public async Task<IEnumerable<FinancialItem>> GetFilteredItemsByUserIdAsync(int userId, DTParameters dtParameters)
        {
            var searchBy = dtParameters.Search?.Value;

            var orderCriteria = string.Empty;
            var orderAscendingDirection = true;

            if (dtParameters.Order != null)
            {
                // in this example we just default sort on the 1st column
                orderCriteria = dtParameters.Columns[dtParameters.Order[0].Column].Data;
                orderAscendingDirection = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";
            }
            else
            {
                // if we have an empty search then just order the results by Id ascending
                orderCriteria = "Id";
                orderAscendingDirection = true;
            }

            var result = GetAllByUserId(userId);

            if (!string.IsNullOrEmpty(searchBy))
            {
                result = result.Where(r => r.Name != null && r.Name.ToUpper().Contains(searchBy.ToUpper()));
            }

            var finalResult = orderAscendingDirection ? await result.OrderByDynamic(orderCriteria, LinqExtensions.Order.Asc).ToListAsync() : await result.OrderByDynamic(orderCriteria, LinqExtensions.Order.Desc).ToListAsync();

            return finalResult;
        }

        private IQueryable<FinancialItem> GetAllByUserId(int userId)
            => Set.Where(fi => fi.UserId == userId);
    }
}
