using System.Collections.Generic;
using Models.Entities;

namespace Web.Areas.Admin.Models.FinancialOperations
{
    public class FinancialOperationListViewModel
    {
        public IEnumerable<FinancialOperation> FinancialOperations { get; set; }
    }
}
