using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;
using Models.Entities.Identity;

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

        public async Task<IEnumerable<User>> GetAllAsync()
            => await Set.ToListAsync();

        public async Task<IEnumerable<User>> SearchUsersAsync(string searchText)
             => await Set.Where(u => u.FirstName.Contains(searchText) || u.LastName.Contains(searchText) || u.Email.Contains(searchText)).ToListAsync();
    }
}
