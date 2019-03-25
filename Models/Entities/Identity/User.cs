using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Models.Entities.Identity
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public IList<UserRole> UserRoles { get; set; }

        public bool IsActive { get; set; }
    }
}
