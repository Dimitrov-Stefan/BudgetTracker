using Core.Contracts.Repositories;
using Core.Extensions;
using Microsoft.EntityFrameworkCore;
using Models.DatatableModels;
using Models.Entities.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(BudgetTrackerDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<User> GetByIdAsync(int id)
            => await Set.Where(u => u.Id == id)
            .Include(u => u.UserRoles)
            .SingleOrDefaultAsync();

        public async Task<IEnumerable<User>> GetPagedAsync(int skip, int take)
            => await Set.Skip(skip)
            .Take(take)
            .OrderBy(u => u.FirstName)
            .ThenBy(u => u.LastName)
            .ToListAsync();

        public async Task<IEnumerable<User>> SearchUsersAsync(string searchText)
             => await Set.Where(u => u.FirstName.Contains(searchText) || u.LastName.Contains(searchText) || u.Email.Contains(searchText))
            .ToListAsync();

        public async Task<int> GetCountAsync()
            => await Set.CountAsync();

        public async Task<IEnumerable<User>> GetFilteredUsersAsync(DTParameters dtParameters)
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

            var result = GetAll();

            if (!string.IsNullOrEmpty(searchBy))
            {
                result = result.Where(r => r.Email != null && r.Email.ToUpper().Contains(searchBy.ToUpper()) ||
                r.FirstName != null && r.FirstName.ToUpper().Contains(searchBy.ToUpper()) ||
                r.LastName != null && r.LastName.ToUpper().Contains(searchBy.ToUpper()));
            }

            var finalResult = orderAscendingDirection ? await result.OrderByDynamic(orderCriteria, LinqExtensions.Order.Asc).ToListAsync() : await result.OrderByDynamic(orderCriteria, LinqExtensions.Order.Desc).ToListAsync();

            return finalResult;
        }

        private IQueryable<User> GetAll()
        => Set.AsQueryable();
    }
}
