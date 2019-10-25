using Core.Constants;
using Microsoft.AspNetCore.Identity;
using Models.Entities.Identity;
using Steffes.Web.Models.Account;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Web.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserStore<User> _userStore;
        private readonly IUserEmailStore<User> _emailStore;

        public AccountService(UserManager<User> userManager,
            IUserStore<User> userStore)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = _userManager.SupportsUserEmail ? (IUserEmailStore<User>)_userStore : throw new NotSupportedException("A user store with email support is required.");
        }

        public async Task<CreateAccountResult> CreateAccountAsync(string firstName, string lastName, string email, string password, string role)
        {
            var user = new User()
            {
                FirstName = firstName,
                LastName = lastName
            };

            await _userStore.SetUserNameAsync(user, email, CancellationToken.None);
            await _emailStore.SetEmailAsync(user, email, CancellationToken.None);

            var createUserResult = await _userManager.CreateAsync(user, password);

            if (createUserResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Roles.User);
            }

            return new CreateAccountResult
            {
                User = createUserResult.Succeeded ? user : null,
                Succeeded = createUserResult.Succeeded,
                Errors = createUserResult.Errors
            };
        }
    }
}
