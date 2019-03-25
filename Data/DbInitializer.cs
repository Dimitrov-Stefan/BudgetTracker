using System.Linq;
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

        public DbInitializer(IConfiguration configuration, UserManager<User> userManager)
        {
            _options = configuration.GetSection(nameof(DbInitializerOptions)).Get<DbInitializerOptions>();
            _userManager = userManager;
        }

        public void AddAdmin()
        {
            var initialUser = _options.Users.FirstOrDefault();
            if(initialUser != null)
            {
                var user = new User()
                {
                    Email = initialUser.Email,
                    FirstName = initialUser.FirstName,
                    LastName = initialUser.LastName,
                    IsActive = true,
                    EmailConfirmed = true
                };

                _userManager.CreateAsync(user, initialUser.Password);
            }
        }
    }
}
