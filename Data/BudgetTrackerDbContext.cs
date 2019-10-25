using Data.Configurations;
using Data.Configurations.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Models.Entities.Identity;

namespace Data
{
    public class BudgetTrackerDbContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public BudgetTrackerDbContext(DbContextOptions<BudgetTrackerDbContext> options) : base(options)
        {

        }

        public DbSet<FinancialItem> FinancialItems { get; set; }

        public DbSet<FinancialOperation> FinancialOperations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Identity configurations
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());

            //Application configurations
            builder.ApplyConfiguration(new FinancialItemConfiguration());
            builder.ApplyConfiguration(new FinancialOperationConfiguration());
        }
    }
}
