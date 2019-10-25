using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Models.Entities.Identity
{
    public class Role : IdentityRole<int>
    {
        public IList<UserRole> UserRoles { get; set; }
    }
}
