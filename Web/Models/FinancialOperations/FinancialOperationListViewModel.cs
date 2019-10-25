using Models;
using Models.Entities;

namespace Web.Models.FinancialOperations
{
    public class FinancialOperationListViewModel
    {
        public PagedList<FinancialOperation> FinancialOperations { get; set; }
    }
}
