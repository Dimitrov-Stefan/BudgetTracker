using Models.Entities.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Contracts.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByIdAsync(int id);

        Task<IEnumerable<User>> GetAllAsync();

        Task<IEnumerable<User>> GetPagedAsync(int skip, int take);

        Task<IEnumerable<User>> SearchUsersAsync(string searchText);

        Task<int> GetAllCountAsync();
    }
}
