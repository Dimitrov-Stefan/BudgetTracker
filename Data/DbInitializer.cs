using System.Linq;
using System.Threading.Tasks;
using Core.Constants;
using Data.Initialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models.Entities.Identity;

namespace Data
{
    public class DbInitializer
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly DbInitializerOptions _options;
        private readonly BudgetTrackerDbContext _dbContext;

        public DbInitializer(IConfiguration configuration,
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            BudgetTrackerDbContext dbContext)
        {
            _options = configuration.GetSection(nameof(DbInitializerOptions)).Get<DbInitializerOptions>();
            _userManager = userManager;
            _roleManager = roleManager;
            _dbContext = dbContext;
        }

        public async Task InitializeDatabase()
        {
            await ApplyMigrationsAsync();
            await AddRoles();
            await AddAdmin();
        }

        private async Task AddRoles()
        {
            if (!await _roleManager.RoleExistsAsync(Roles.Admin))
            {
                await _roleManager.CreateAsync(new Role()
                {
                    Name = Roles.Admin
                });
            }

            if (!await _roleManager.RoleExistsAsync(Roles.User))
            {
                await _roleManager.CreateAsync(new Role()
                {
                    Name = Roles.User
                });
            }
        }

        private async Task AddAdmin()
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

                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, Roles.Admin);
                    }
                }
            }
        }

        private Task ApplyMigrationsAsync()
        {
            if (_dbContext.Database.GetPendingMigrations().Any())
            {
                return _dbContext.Database.MigrateAsync();
            }

            return Task.CompletedTask;
        }
    }
}
