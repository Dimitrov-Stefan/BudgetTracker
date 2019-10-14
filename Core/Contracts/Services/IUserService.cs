﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using Models.Entities.Identity;
using Models.ServiceResults.Users;

namespace Core.Contracts.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllAsync();

        Task<PagedList<User>> GetPagedAsync(PagedListRequest request);

        Task<CreateUserResult> CreateAsync(string firstName, string lastName, string email, string password, int roleId);

        Task<User> GetByIdAsync(int id);

        Task<EditUserResult> EditAsync(User user);

        Task<DeleteUserResult> DeleteAsync(int userId, int currentUserId);

        Task<PagedList<User>> SearchUsersAsync(PagedListRequest request, string searchText);
    }
}
