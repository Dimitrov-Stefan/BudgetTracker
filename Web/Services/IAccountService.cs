using System.Threading.Tasks;
using Steffes.Web.Models.Account;

namespace Web.Services
{
    public interface IAccountService
    {
        Task<CreateAccountResult> CreateAccountAsync(string firstName, string lastName, string email, string password, string role);
    }
}
