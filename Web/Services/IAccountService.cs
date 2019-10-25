using Steffes.Web.Models.Account;
using System.Threading.Tasks;

namespace Web.Services
{
    public interface IAccountService
    {
        Task<CreateAccountResult> CreateAccountAsync(string firstName, string lastName, string email, string password, string role);
    }
}
