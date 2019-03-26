﻿using System.Linq;
using System.Threading.Tasks;
using Data.Initialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Models.Entities.Identity;

namespace Data
{
    public class DbInitializer
    {
        private readonly UserManager<User> _userManager;
        private readonly DbInitializerOptions _options;
        private readonly BudgetTrackerDbContext _dbContext;

        public DbInitializer(IConfiguration configuration, UserManager<User> userManager, BudgetTrackerDbContext dbContext)
        {
            _options = configuration.GetSection(nameof(DbInitializerOptions)).Get<DbInitializerOptions>();
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public async Task AddAdmin()
        {
            if (!_dbContext.Users.Any())
            {
                var initialUser = _options.Users.FirstOrDefault();
                if (initialUser != null)
                {
                    var user = new User()
                    {
                        UserName = initialUser.Email,
                        Email = initialUser.Email,
                        FirstName = initialUser.FirstName,
                        LastName = initialUser.LastName,
                        IsActive = true,
                        EmailConfirmed = true
                    };

                    var result = await _userManager.CreateAsync(user, initialUser.Password);
                    var r2 = result;
                }
            }
        }
    }
}
