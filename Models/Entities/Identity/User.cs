using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Models.Entities.Identity
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public IList<UserRole> UserRoles { get; set; }

        public IList<FinancialItem> FinancialItems { get; set; }

        public bool IsActive { get; set; }
    }
}
