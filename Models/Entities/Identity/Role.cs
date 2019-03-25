using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Models.Entities.Identity
{
    public class Role : IdentityRole<int>
    {
        public IList<UserRole> UserRoles { get; set; }
    }
}
