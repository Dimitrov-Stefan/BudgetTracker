using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Entities.Identity;

namespace Core.Contracts.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetAllAsync();
    }
}
