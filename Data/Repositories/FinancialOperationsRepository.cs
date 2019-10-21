using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Contracts.Repositories;
using Core.Extensions;
using Microsoft.EntityFrameworkCore;
using Models.DatatableModels;
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

        public async Task<IEnumerable<FinancialOperation>> GetAllAsync(int userId, int skip, int take)
        {
            return await Set.Include(fo => fo.FinancialItem)
                .Where(fo => fo.FinancialItem.UserId == userId)
                .OrderBy(fo => fo.Timestamp)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<IEnumerable<FinancialOperation>> GetByFinancialItemIdAsync(int financialItemId)
            => await Set.Where(fo => fo.FinancialItemId == financialItemId).ToListAsync();

        public async Task<IEnumerable<FinancialOperation>> GetAllByUserIdAsync(int userId)
            => await Set.Include(fo => fo.FinancialItem).Where(fo => fo.FinancialItem.UserId == userId).ToListAsync();

        public IQueryable<FinancialOperation> GetAllByUserId(int userId)
            => Set.Include(fo => fo.FinancialItem).Where(fo => fo.FinancialItem.UserId == userId);

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
                    financialOperations = await Set.Where(fo => financialItemIds.Contains(fo.FinancialItem.Id) && fo.Timestamp.Date >= from).ToListAsync();
                }
                else if (to.HasValue)
                {
                    financialOperations = await Set.Where(fo => financialItemIds.Contains(fo.FinancialItem.Id) && fo.Timestamp.Date <= to).ToListAsync();
                }
                else
                {
                    financialOperations = await Set.Where(fo => financialItemIds.Contains(fo.FinancialItem.Id)).ToListAsync();
                }

            }

            return financialOperations;
        }

        public Task<int> GetCountByUserIdAsync(int userId)
            => Set.Include(fo => fo.FinancialItem)
            .Where(fo => fo.FinancialItem.UserId == userId)
            .CountAsync();

        public async Task<IEnumerable<FinancialOperation>> GetFilteredOperationsByUserIdAsync(int userId, DTParameters dtParameters)
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
                result = result.Where(r => r.Description != null && r.Description.ToUpper().Contains(searchBy.ToUpper()) ||
                r.FinancialItem.Name != null && r.FinancialItem.Name.ToUpper().Contains(searchBy.ToUpper()));
            }

            var finalResult = orderAscendingDirection ? await result.OrderByDynamic(orderCriteria, LinqExtensions.Order.Asc).ToListAsync() : await result.OrderByDynamic(orderCriteria, LinqExtensions.Order.Desc).ToListAsync();

            return finalResult;
        }
    }
}
