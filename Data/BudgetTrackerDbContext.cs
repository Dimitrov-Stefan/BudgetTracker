using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Models.Entities.Identity;

namespace Data
{
    public class BudgetTrackerDbContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DbSet<FinancialItem> FinancialItems { get; set; }

        public DbSet<FinancialOperation> FinancialOperations { get; set; }
    }
}
