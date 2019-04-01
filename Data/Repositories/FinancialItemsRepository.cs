using Core.Contracts.Repositories;
using Models.Entities;

namespace Data.Repositories
{
    public class FinancialItemsRepository : RepositoryBase<FinancialItem>, IFinancialItemsRepository
    {
        public FinancialItemsRepository(BudgetTrackerDbContext dbContext) : base(dbContext)
        {

        }
    }
}
