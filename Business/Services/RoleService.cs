using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Models.Entities.Identity;
using System.Linq;

namespace Business.Services
{
    public class RoleService
    {
        private readonly RoleManager<Role> _roleManager;

        public RoleService(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        public IEnumerable<Role> GetAll()
            => _roleManager.Roles.ToList();
        
    }
}
