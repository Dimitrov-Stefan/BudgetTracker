using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Constants;
using Core.Contracts.Repositories;
using Core.Contracts.Services;
using Core.Extensions;
using Microsoft.AspNetCore.Identity;
using Models;
using Models.Entities.Identity;
using Models.ServiceResults.Users;
using Principal = System.Security.Claims.ClaimsPrincipal;

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

        public async Task<PagedList<User>> GetPagedAsync(PagedListRequest request)
        {
            var users = await _userRepository.GetPagedAsync(request.Skip, request.PageSize);
            var usersCount = await _userRepository.GetAllCountAsync();

            return new PagedList<User>(users, request.Page, request.PageSize, usersCount);
        }

        public async Task<User> GetByIdAsync(int id)
            => await _userRepository.GetByIdAsync(id);

        public async Task<EditUserResult> EditAsync(User user)
        {
            var updateResult = await _userManager.UpdateAsync(user);

            return new EditUserResult
            {
                User = updateResult.Succeeded ? user : null,
                Succeeded = updateResult.Succeeded,
                Errors = updateResult.Errors
            };
        }

        public async Task<DeleteUserResult> DeleteAsync(int userId, int currentUserId)
        {
            DeleteUserResult deleteResult = new DeleteUserResult();

            if (currentUserId == userId)
            {
                deleteResult.Errors = new List<IdentityError>()
                {
                    new IdentityError()
                    {
                        Code = "DeleteSelf",
                        Description = "Cannot delete yourself!"
                    }
                };

                return deleteResult;
            }

            var user = await _userManager.FindByIdAsync(userId.ToString());
            var deleteServiceResult = await _userManager.DeleteAsync(user);

            deleteResult.User = deleteServiceResult.Succeeded ? user : null;
            deleteResult.Succeeded = deleteServiceResult.Succeeded;
            deleteResult.Errors = deleteServiceResult.Errors;

            return deleteResult;
        }

        public async Task<PagedList<User>> SearchUsersAsync(PagedListRequest request, string searchText)
        {
            var users = await _userRepository.SearchUsersAsync(/*request.Page, request.PageSize,*/ searchText);
            var pagedUsers = users
                .Skip(request.Skip)
                .Take(request.PageSize)
                .OrderBy(u => u.FirstName)
                .ThenBy(u => u.LastName)
                .ToList();

            var usersCount = users.Count();

            return new PagedList<User>(pagedUsers, request.Page, request.PageSize, usersCount);
        }
    }
}
