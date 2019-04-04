using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Constants;
using Core.Contracts.Repositories;
using Core.Contracts.Services;
using Microsoft.AspNetCore.Identity;
using Models.Entities.Identity;
using Steffes.Models.ServiceResults.Users;

namespace Business.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserStore<User> _userStore;
        private readonly IUserEmailStore<User> _emailStore;
        private readonly IUserRepository _userRepository;

        public UserService(UserManager<User> userManager,
            IUserStore<User> userStore,
            IUserRepository userRepository)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = _userManager.SupportsUserEmail ? (IUserEmailStore<User>)_userStore : throw new NotSupportedException("A user store with email support is required.");
            _userRepository = userRepository;
        }

        public async Task<CreateUserResult> CreateAsync(string firstName, string lastName, string email, string password, string role)
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

            return new CreateUserResult
            {
                User = createUserResult.Succeeded ? user : null,
                Succeeded = createUserResult.Succeeded,
                Errors = createUserResult.Errors
            };
        }

        public Task<IEnumerable<User>> GetAllAsync()
            => _userRepository.GetAllAsync();

        public Task<User> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(User item)
        {
            throw new NotImplementedException();
        }
    }
}
