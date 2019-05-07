using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Constants;
using Core.Contracts.Repositories;
using Core.Contracts.Services;
using Microsoft.AspNetCore.Identity;
using Models.Entities.Identity;
using Models.ServiceResults.Users;

namespace Business.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IUserStore<User> _userStore;
        private readonly IUserEmailStore<User> _emailStore;
        private readonly IUserRepository _userRepository;

        public UserService(UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IUserStore<User> userStore,
            IUserRepository userRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userStore = userStore;
            _emailStore = _userManager.SupportsUserEmail ? (IUserEmailStore<User>)_userStore : throw new NotSupportedException("A user store with email support is required.");
            _userRepository = userRepository;
        }

        public async Task<CreateUserResult> CreateAsync(string firstName, string lastName, string email, string password, int roleId)
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
                var roleInDb = await _roleManager.FindByIdAsync(roleId.ToString());

                if (roleInDb != null)
                {
                    await _userManager.AddToRoleAsync(user, roleInDb.Name);
                }
                else
                {
                    IdentityResult.Failed(new IdentityError() { Description = $"Failed to add user {user.UserName} to role." });
                }
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

        public async Task<User> GetByIdAsync(int id)
            => await _userRepository.GetByIdAsync(id);

        public async Task<EditUserResult> EditAsync(User user)
        {
            var userInDb = await _userManager.FindByIdAsync(user.Id.ToString());
            var userInDbRoles = await _userManager.GetRolesAsync(userInDb);

            var updateResult = await _userManager.UpdateAsync(user);

            if (updateResult.Succeeded)
            {
                await _userManager.RemoveFromRoleAsync(user, user.UserRoles.FirstOrDefault().Role.Name);
                await _userManager.AddToRoleAsync(user, userInDbRoles.FirstOrDefault());
            }

            return new EditUserResult
            {
                User = updateResult.Succeeded ? user : null,
                Succeeded = updateResult.Succeeded,
                Errors = updateResult.Errors
            };
        }
    }
}
