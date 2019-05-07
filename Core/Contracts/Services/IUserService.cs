using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Entities.Identity;
using Models.ServiceResults.Users;

namespace Core.Contracts.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllAsync();

        Task<CreateUserResult> CreateAsync(string firstName, string lastName, string email, string password, int roleId);

        Task<User> GetByIdAsync(int id);

        Task UpdateAsync(User item);
    }
}
